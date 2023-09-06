using AutoMapper;
using InTouchApi.Application.Authorization;
using InTouchApi.Application.Exceptions;
using InTouchApi.Application.Interfaces;
using InTouchApi.Application.Models;
using InTouchApi.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Serilog;

namespace InTouchApi.Infrastructure.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUserHttpContextService _userHttpContextService;
        private readonly IAuthorizationService _authorizationService;

        public PostService(IPostRepository repository,
                            IMapper mapper,
                            IUserHttpContextService userHttpContextService,
                            IAuthorizationService authorizationService)
        {
            _repository = repository;
            _mapper = mapper;
            _userHttpContextService = userHttpContextService;
            _authorizationService = authorizationService;
        }

        public async Task<int> CreatePostAsync(CreatePostDto createPostDto)
        {
            var userId = _userHttpContextService.Id
                ?? throw new UnauthorizedException("Unauthorized",
                $"Unauthorized user tried to create a post");

            var post = _mapper.Map<Post>(createPostDto);

            post.AuthorId = userId;
            post.CreatedById = userId;
            post.CreationDate = DateTime.UtcNow;

            var id = await _repository.CreatePostAsync(post);

            Log.Logger.Information($"User with id: {userId} created post with id: {id}");

            return id;
        }

        public async Task DeletePostAsync(int id)
        {
            var postToDelete = await _repository.GetPostByIdAsync(id);

            var userId = _userHttpContextService.Id
                ?? throw new UnauthorizedException("Unauthorized",
                $"Unauthorized user tried to delete post with id: {id}");

            var authorizationResult = _authorizationService
                .AuthorizeAsync(_userHttpContextService.User, postToDelete, new DeleteResourceRequirement()).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbiddenException("Forbidden operation",
                    $"User with id: {userId} tried to delete post with id: {id}, which is a forbidden operation");
            }

            var post = await _repository.GetPostByIdAsync(id);

            post.LastModifiedById = userId;

            await _repository.DeletePostAsync(post);

            Log.Logger.Information($"User with id: {userId} deleted post with id: {id}");
        }

        public async Task<IEnumerable<PostDto>> GetAllPostsAsync()
        {
            var posts = await _repository.GetAllPostsAsync();
            var postDtos = _mapper.Map<IEnumerable<PostDto>>(posts);

            Log.Logger.Information($"List of posts was returned. Count: {postDtos.Count()}");

            return postDtos;
        }

        public async Task<PostDto> GetPostByIdAsync(int id)
        {
            var post = await _repository.GetPostByIdAsync(id);
            var postDto = _mapper.Map<PostDto>(post);

            Log.Logger.Information($"Post with id: {id} was returned");

            return postDto;
        }

        public async Task<IEnumerable<PostDto>> GetUserPostsAsync(int id)
        {
            var posts = await _repository.GetAllPostsAsync();
            var userPosts = posts.Where(p => p.AuthorId == id).ToList();
            var postDtos = _mapper.Map<IEnumerable<PostDto>>(userPosts);

            Log.Logger.Information($"List of user with id: {id} posts was returned");

            return postDtos;
        }

        public async Task UpdatePostAsync(int id, UpdatePostDto updatePostDto)
        {
            var postToEdit = await _repository.GetPostByIdAsync(id)
                ?? throw new NotFoundException("The post was not found",
                $"Post with id: {id} was not found");

            var userId = _userHttpContextService.Id
                ?? throw new UnauthorizedException("Unauthorized",
                $"Unauthorized user tried to update post with id: {id}");

            var authorizationResult = _authorizationService
                .AuthorizeAsync(_userHttpContextService.User, postToEdit, new EditResourceRequirement()).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbiddenException("Forbidden operation",
                    $"User with id: {userId} tried to update post with id: {id}, which is a forbidden operation");
            }

            var post = _mapper.Map<Post>(updatePostDto);

            post.Id = id;

            post.LastModifiedById = userId;

            await _repository.UpdatePostAsync(post);

            Log.Logger.Information($"Post with id: {id} was updated by user with id: {userId}");
        }
    }
}