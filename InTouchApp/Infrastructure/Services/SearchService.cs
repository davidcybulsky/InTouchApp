using AutoMapper;
using InTouchApi.Application.Interfaces;
using InTouchApi.Application.Models;

namespace InTouchApi.Infrastructure.Services
{
    public class SearchService : ISearchService
    {
        private readonly ISearchRepository _repository;
        private readonly IMapper _mapper;

        public SearchService(ISearchRepository repository,
                             IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<IEnumerable<PostDto>> GetPostsAsync(string pattern)
        {
            var posts = await _repository.GetPostsAsync(pattern);
            var postDtos = _mapper.Map<IEnumerable<PostDto>>(posts);
            return postDtos;
        }

        public async Task<IEnumerable<UserDto>> GetUsersAsync(string pattern)
        {
            var users = await _repository.GetUsersAsync(pattern);
            var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);
            return userDtos;
        }
    }
}
