using AutoMapper;
using InTouchApi.Application.Interfaces;
using InTouchApi.Application.Models;
using InTouchApi.Domain.Entities;

namespace InTouchApi.Infrastructure.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _repository;
        private readonly IMapper _mapper;

        public PostService(IPostRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<int> CreatePostAsync(CreatePostDto createPostDto)
        {
            var post = _mapper.Map<Post>(createPostDto);
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
            post.Id = id;
            await _repository.UpdatePostAsync(post);
        }
    }
}
