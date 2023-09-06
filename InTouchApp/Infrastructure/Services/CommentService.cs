using AutoMapper;
using InTouchApi.Application.Exceptions;
using InTouchApi.Application.Interfaces;
using InTouchApi.Application.Models;
using InTouchApi.Domain.Entities;

namespace InTouchApi.Infrastructure.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUserHttpContextService _userHttpContextService;

        public CommentService(ICommentRepository repository,
                              IMapper mapper,
                              IUserHttpContextService userHttpContextService)
        {
            _repository = repository;
            _mapper = mapper;
            _userHttpContextService = userHttpContextService;
        }

        public async Task DeletePostCommentAsync(int id)
        {
            var userId = _userHttpContextService.Id ?? throw new UnauthorizedException("");
            var postComment = await _repository.GetPostCommentByIdAsync(id);

            postComment.LastModifiedById = userId;

            await _repository.DeletePostCommentAsync(postComment);
        }

        public async Task UpdatePostCommentAsync(int id, UpdateCommentDto updateCommentDto)
        {
            var userId = _userHttpContextService.Id ?? throw new UnauthorizedException("");
            var postComment = _mapper.Map<PostComment>(updateCommentDto);

            postComment.Id = id;
            postComment.LastModifiedById = userId;

            await _repository.UpdatePostCommentAsync(postComment);
        }

        public async Task<int> CreatePostCommentAsync(int postId, CreateCommentDto createCommentDto)
        {
            var userId = _userHttpContextService.Id ?? throw new UnauthorizedException("");

            var postComment = _mapper.Map<PostComment>(createCommentDto);

            postComment.PostId = postId;
            postComment.UserId = userId;

            postComment.CreationDate = DateTime.UtcNow;
            postComment.CreatedById = userId;

            var postCommentId = await _repository.CreatePostCommentAsync(postComment);

            return postCommentId;
        }
    }
}
