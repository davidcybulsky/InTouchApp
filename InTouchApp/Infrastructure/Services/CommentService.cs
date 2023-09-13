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
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUserHttpContextService _userHttpContextService;
        private readonly IAuthorizationService _authorizationService;

        public CommentService(ICommentRepository repository,
                              IMapper mapper,
                              IUserHttpContextService userHttpContextService,
                              IAuthorizationService authorizationService)
        {
            _repository = repository;
            _mapper = mapper;
            _userHttpContextService = userHttpContextService;
            _authorizationService = authorizationService;
        }

        public async Task DeletePostCommentAsync(int id)
        {
            var userId = _userHttpContextService.Id ??
                throw new UnauthorizedException("Unauthorized",
                $"Unauthorized user tried to delete post comment with id: {id}");

            var postComment = await _repository.GetPostCommentByIdAsync(id);

            var authorizationResult = _authorizationService
                .AuthorizeAsync(_userHttpContextService.User, postComment, new DeleteResourceRequirement()).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbiddenException("You can not delete the comment",
                    $"User with id: {userId} tried to delete post comment with id: {postComment.PostId}, which is a forbidden operation");
            }

            postComment.LastModifiedById = userId;

            await _repository.DeletePostCommentAsync(postComment);

            Log.Logger.Information($"Post comment with id: {id} was deleted by user with id: {userId}");
        }

        public async Task UpdatePostCommentAsync(int id, UpdateCommentDto updateCommentDto)
        {
            var postCommentToEdit = await _repository.GetPostCommentByIdAsync(id);

            var userId = _userHttpContextService.Id
                ?? throw new UnauthorizedException("Unauthorized",
                $"Unauthorized user tried to update post comment with id: {id}");

            var authorizationResult = _authorizationService
                .AuthorizeAsync(_userHttpContextService.User, postCommentToEdit, new EditResourceRequirement()).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbiddenException("You can not update the comment",
                    $"User with id: {userId} tried to update post comment with id: {postCommentToEdit.Id}, which is a forbidden operation");
            }

            var postComment = _mapper.Map<PostComment>(updateCommentDto);

            postComment.Id = id;
            postComment.LastModifiedById = userId;

            await _repository.UpdatePostCommentAsync(postComment);

            Log.Logger.Information($"Post comment with id: {id} was updated by user with id: {userId}");
        }

        public async Task<IncludeCommentDto> CreatePostCommentAsync(int postId, CreateCommentDto createCommentDto)
        {
            var userId = _userHttpContextService.Id
                ?? throw new UnauthorizedException("Unauthorized",
                $"Unauthorized user tried to create post comment for post with id: {postId}");

            var postComment = _mapper.Map<PostComment>(createCommentDto);

            postComment.PostId = postId;
            postComment.UserId = userId;

            postComment.CreationDate = DateTime.UtcNow;
            postComment.CreatedById = userId;

            var savedPostComment = await _repository.CreatePostCommentAsync(postComment);

            var postCommentDto = _mapper.Map<IncludeCommentDto>(savedPostComment);

            Log.Logger.Information($"User with id: {userId} created post comment with id: {savedPostComment.Id}");

            return postCommentDto;
        }
    }
}
