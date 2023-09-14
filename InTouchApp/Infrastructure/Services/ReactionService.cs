using AutoMapper;
using InTouchApi.Application.Exceptions;
using InTouchApi.Application.Interfaces;
using InTouchApi.Application.Interfaces.Reaction;
using InTouchApi.Application.Models;
using InTouchApi.Domain.Entities;
using Serilog;

namespace InTouchApi.Infrastructure.Services
{
    public class ReactionService : IReactionService
    {
        private readonly IReactionRepository _repository;
        private readonly IUserHttpContextService _userHttpContextService;
        private readonly IMapper _mapper;

        public ReactionService(IReactionRepository repository,
                                IUserHttpContextService userHttpContextService,
                                IMapper mapper)
        {
            _repository = repository;
            _userHttpContextService = userHttpContextService;
            _mapper = mapper;
        }

        public async Task CreateCommentReactionAsync(int commentId, CreateReactionDto createReactionDto)
        {
            var userId = _userHttpContextService.Id
                ?? throw new UnauthorizedException("Unauthorized",
                $"Unauthorized user tried to react on comment with id: {commentId}");

            var reaction = _mapper.Map<CommentReaction>(createReactionDto);

            reaction.CommentId = commentId;
            reaction.UserId = userId;

            reaction.CreatedById = userId;
            reaction.CreationDate = DateTime.UtcNow;

            await _repository.CreateCommentReactionAsync(reaction);

            Log.Logger.Information($"User with id: {userId} created reaction with id: {reaction.Id} to comment with id: {commentId}");
        }

        public async Task CreatePostReactionAsync(int postId, CreateReactionDto createReactionDto)
        {
            var userId = _userHttpContextService.Id
                ?? throw new UnauthorizedException("Unauthorized",
                $"Unauthorized user tried to react on post with id: {postId}");

            var reaction = _mapper.Map<PostReaction>(createReactionDto);

            reaction.PostId = postId;
            reaction.UserId = userId;

            reaction.CreatedById = userId;
            reaction.CreationDate = DateTime.UtcNow;

            await _repository.CreatePostReactionAsync(reaction);

            Log.Logger.Information($"User with id: {userId} created reaction with id: {reaction.Id} to post with id: {postId}");

        }

        public async Task DeleteCommentReactionAsync(int commentId)
        {

            var userId = _userHttpContextService.Id
                ?? throw new UnauthorizedException("Unauthorized",
                $"Unauthorized user tried to delete comment reaction with commentId: {commentId}");


            var reaction = await _repository.GetCommentReactionAsync(commentId, userId);

            reaction.LastModifiedById = userId;

            await _repository.DeleteCommentReactionAsync(reaction);

            Log.Logger.Information($"User with id: {userId} deleted comment reaction with id: {reaction.Id}");
        }

        public async Task DeletePostReactionAsync(int postId)
        {
            var userId = _userHttpContextService.Id
                ?? throw new UnauthorizedException("Unauthorized",
                $"Unauthorized user tried to delete post reaction with postId: {postId}");

            var reaction = await _repository.GetPostReactionAsync(postId, userId);

            reaction.LastModifiedById = userId;

            await _repository.DeletePostReactionAsync(reaction);

            Log.Logger.Information($"User with id: {userId} deleted post reaction with id: {reaction.Id}");
        }

        public async Task UpdateCommentReactionAsync(int commentId, UpdateReactionDto updateReactionDto)
        {
            var userId = _userHttpContextService.Id
                ?? throw new UnauthorizedException("Unauthorized",
                $"Unauthorized user tried to update comment reaction with commentId: {commentId}");

            var reactionToUpdate = await _repository.GetCommentReactionAsync(commentId, userId);

            var reaction = _mapper.Map<CommentReaction>(updateReactionDto);

            reaction.Id = reactionToUpdate.Id;
            reaction.LastModifiedById = userId;

            await _repository.UpdateCommentReactionAsync(reaction);

            Log.Logger.Information($"User with id: {userId} updated comment reaction with id: {reaction.Id}");
        }

        public async Task UpdatePostReactionAsync(int postId, UpdateReactionDto updateReactionDto)
        {
            var userId = _userHttpContextService.Id
                ?? throw new UnauthorizedException("Unauthorized",
                $"Unauthorized user tried to update post reaction with postId: {postId}");

            var reactionToUpdate = await _repository.GetPostReactionAsync(postId, userId);

            var reaction = _mapper.Map<PostReaction>(updateReactionDto);

            reaction.Id = reactionToUpdate.Id;
            reaction.LastModifiedById = userId;

            await _repository.UpdatePostReactionAsync(reaction);

            Log.Logger.Information($"User with id: {userId} updated post reaction with id: {reaction.Id}");
        }
    }
}
