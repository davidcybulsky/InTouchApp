using AutoMapper;
using InTouchApi.Application.Exceptions;
using InTouchApi.Application.Interfaces;
using InTouchApi.Application.Models;
using InTouchApi.Domain.Entities;
using Serilog;

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
            var userId = _userHttpContextService.Id ??
                throw new UnauthorizedException("Unauthorized",
                $"Unauthorized user tried to delete post comment with id: {id}");
            var postComment = await _repository.GetPostCommentByIdAsync(id);

            postComment.LastModifiedById = userId;

            await _repository.DeletePostCommentAsync(postComment);

            Log.Logger.Information($"Post comment with id: {id} was deleted by user with id: {userId}");
        }

        public async Task UpdatePostCommentAsync(int id, UpdateCommentDto updateCommentDto)
        {
            var userId = _userHttpContextService.Id
                ?? throw new UnauthorizedException("Unauthorized",
                $"Unauthorized user tried to update post comment with id: {id}");
            var postComment = _mapper.Map<PostComment>(updateCommentDto);

            postComment.Id = id;
            postComment.LastModifiedById = userId;

            await _repository.UpdatePostCommentAsync(postComment);

            Log.Logger.Information($"Post comment with id: {id} was updated by user with id: {userId}");
        }

        public async Task<int> CreatePostCommentAsync(int postId, CreateCommentDto createCommentDto)
        {
            var userId = _userHttpContextService.Id
                ?? throw new UnauthorizedException("Unauthorized",
                $"Unauthorized user tried to create post comment for post with id: {postId}");

            var postComment = _mapper.Map<PostComment>(createCommentDto);

            postComment.PostId = postId;
            postComment.UserId = userId;

            postComment.CreationDate = DateTime.UtcNow;
            postComment.CreatedById = userId;

            var postCommentId = await _repository.CreatePostCommentAsync(postComment);

            Log.Logger.Information($"User with id: {userId} created post comment with id: {postCommentId}");

            return postCommentId;
        }
    }
}
