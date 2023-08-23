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
            var post = _mapper.Map<Post>(createPostDto);
            post.AuthorId = _userHttpContextService.Id
                ?? throw new UnauthorizedException("Unauthorized operation");
            post.CreatedById = post.AuthorId;
            post.CreationDate = DateTime.UtcNow;
            var id = _repository.CreatePostAsync(post);
            return id;
        }

        public async Task DeletePostAsync(int id)
        {
            await _repository.DeletePostAsync(id);
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

        public async Task UpdatePostAsync(int id, UpdatePostDto updatePostDto)
        {
            var post = _mapper.Map<Post>(updatePostDto);
            var userId = _userHttpContextService.Id ?? throw new UnauthorizedException("Unauthorized operation");
            post.LastModificationDate = DateTime.UtcNow;
            post.LastModifiedById = userId;
            post.Id = id;
            await _repository.UpdatePostAsync(post);
        }
    }
}
