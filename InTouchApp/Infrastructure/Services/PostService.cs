using AutoMapper;
using InTouchApi.Application.Exceptions;
using InTouchApi.Application.Interfaces;
using InTouchApi.Application.Models;
using InTouchApi.Domain.Entities;

namespace InTouchApi.Infrastructure.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUserHttpContextService _userHttpContextService;

        public PostService(IPostRepository repository, IMapper mapper, IUserHttpContextService userHttpContextService)
        {
            _repository = repository;
            _mapper = mapper;
            _userHttpContextService = userHttpContextService;
        }

        public Task<int> CreatePostAsync(CreatePostDto createPostDto)
        {
            var userId = _userHttpContextService.Id ?? throw new UnauthorizedException("Unauthorized operation");
            var post = _mapper.Map<Post>(createPostDto);

            post.CreatedById = post.AuthorId;
            post.CreationDate = DateTime.UtcNow;

            var id = _repository.CreatePostAsync(post);
            return id;
        }

        public async Task DeletePostAsync(int id)
        {
            var userId = _userHttpContextService.Id ?? throw new UnauthorizedException("Unauthorized operation");
            var post = await _repository.GetPostAsTrackingAsync(id);

            post.IsDeleted = true;

            post.LastModificationDate = DateTime.UtcNow;
            post.LastModifiedById = userId;

            await _repository.UpdatePostAsync();
        }

        public async Task<IEnumerable<PostDto>> GetAllPostsAsync()
        {
            var posts = await _repository.GetAllPostsAsync();
            var postDtos = _mapper.Map<IEnumerable<PostDto>>(posts);
            return postDtos;
        }

        public async Task<PostDto> GetPostByIdAsync(int id)
        {
            var post = await _repository.GetPostByIdAsync(id);
            var postDto = _mapper.Map<PostDto>(post);
            return postDto;
        }

        public async Task<IEnumerable<PostDto>> GetUserPostsAsync(int id)
        {
            var posts = await _repository.GetAllPostsAsync();
            var userPosts = posts.Where(p => p.AuthorId == id).ToList();
            var postDtos = _mapper.Map<IEnumerable<PostDto>>(userPosts);
            return postDtos;
        }

        public async Task UpdatePostAsync(int id, UpdatePostDto updatePostDto)
        {
            var userId = _userHttpContextService.Id ?? throw new UnauthorizedException("Unauthorized operation");
            var post = await _repository.GetPostAsTrackingAsync(id);

            post.Title = updatePostDto.Title;
            post.Content = updatePostDto.Content;

            post.LastModificationDate = DateTime.UtcNow;
            post.LastModifiedById = userId;

            await _repository.UpdatePostAsync();
        }
    }
}