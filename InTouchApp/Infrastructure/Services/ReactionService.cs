﻿using AutoMapper;
using InTouchApi.Application.Authorization;
using InTouchApi.Application.Exceptions;
using InTouchApi.Application.Interfaces;
using InTouchApi.Application.Interfaces.Reaction;
using InTouchApi.Application.Models;
using InTouchApi.Domain.Constants;
using InTouchApi.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Serilog;

namespace InTouchApi.Infrastructure.Services
{
    public class ReactionService : IReactionService
    {
        private readonly IReactionRepository _repository;
        private readonly IUserHttpContextService _userHttpContextService;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;

        public ReactionService(IReactionRepository repository,
                                IUserHttpContextService userHttpContextService,
                                IMapper mapper,
                                IAuthorizationService authorizationService)
        {
            _repository = repository;
            _userHttpContextService = userHttpContextService;
            _mapper = mapper;
            _authorizationService = authorizationService;
        }

        public async Task<int> CreateCommentReactionAsync(int commentId, CreateReactionDto createReactionDto)
        {
            var userId = _userHttpContextService.Id
                ?? throw new UnauthorizedException("Unauthorized",
                $"Unauthorized user tried to react on comment with id: {commentId}");

            var reaction = _mapper.Map<CommentReaction>(createReactionDto);

            if (reaction.ReactionType.ToUpper() == REACTIONS.LIKE)
            {
                reaction.ReactionType = REACTIONS.LIKE;
            }
            else if (reaction.ReactionType.ToUpper() == REACTIONS.DISLIKE)
            {
                reaction.ReactionType = REACTIONS.DISLIKE;
            }
            else
            {
                throw new BadRequestException("Bad request. Invalid reaction type.",
                    $"User with id: {userId} tried to react on comment with id: {commentId}, but reaction type: {reaction.ReactionType} is in correct.");
            }

            reaction.CommentId = commentId;
            reaction.UserId = userId;

            reaction.CreatedById = userId;
            reaction.CreationDate = DateTime.UtcNow;

            var reactionId = await _repository.CreateCommentReactionAsync(reaction);

            Log.Logger.Information($"User with id: {userId} created reaction with id: {reactionId} to comment with id: {commentId}");

            return reactionId;

        }

        public async Task<int> CreatePostReactionAsync(int postId, CreateReactionDto createReactionDto)
        {
            var userId = _userHttpContextService.Id
                ?? throw new UnauthorizedException("Unauthorized",
                $"Unauthorized user tried to react on post with id: {postId}");

            var reaction = _mapper.Map<PostReaction>(createReactionDto);

            if (reaction.ReactionType.ToUpper() == REACTIONS.LIKE)
            {
                reaction.ReactionType = REACTIONS.LIKE;
            }
            else if (reaction.ReactionType.ToUpper() == REACTIONS.DISLIKE)
            {
                reaction.ReactionType = REACTIONS.DISLIKE;
            }
            else
            {
                throw new BadRequestException("Bad request. Invalid reaction type.",
                    $"User with id: {userId} tried to react on post with id: {postId}, but reaction type: {reaction.ReactionType} is in correct.");
            }

            reaction.PostId = postId;
            reaction.UserId = userId;

            reaction.CreatedById = userId;
            reaction.CreationDate = DateTime.UtcNow;

            var reactionId = await _repository.CreatePostReactionAsync(reaction);

            Log.Logger.Information($"User with id: {userId} created reaction with id: {reactionId} to post with id: {postId}");


            return reactionId;
        }

        public async Task DeleteCommentReactionAsync(int reactionId)
        {
            var reaction = await _repository.GetCommentReactionAsync(reactionId);

            var userId = _userHttpContextService.Id
                ?? throw new UnauthorizedException("Unauthorized",
                $"Unauthorized user tried to delete comment reaction with reactionId: {reactionId}");

            var authorizationResult = _authorizationService
                .AuthorizeAsync(_userHttpContextService.User, reaction, new EditOrDeleteResourceRequirement()).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbiddenException("You can not delete the reaction",
                    $"User with id: {userId} tried to delete comment reaction with id: {reactionId}, which is a forbidden operation");
            }

            reaction.LastModifiedById = userId;

            await _repository.DeleteCommentReactionAsync(reaction);

            Log.Logger.Information($"User with id: {userId} deleted comment reaction with id: {reactionId}");
        }

        public async Task DeletePostReactionAsync(int reactionId)
        {
            var userId = _userHttpContextService.Id
                ?? throw new UnauthorizedException("Unauthorized",
                $"Unauthorized user tried to delete post reaction with reactionId: {reactionId}");

            var reaction = await _repository.GetPostReactionAsync(reactionId);

            var authorizationResult = _authorizationService
                .AuthorizeAsync(_userHttpContextService.User, reaction, new EditOrDeleteResourceRequirement()).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbiddenException("You can not delete the reaction",
                    $"User with id: {userId} tried to delete post reaction with id: {reactionId}, which is a forbidden operation");
            }

            reaction.LastModifiedById = userId;

            await _repository.DeletePostReactionAsync(reaction);

            Log.Logger.Information($"User with id: {userId} deleted post reaction with id: {reactionId}");
        }

        public async Task UpdateCommentReactionAsync(int reactionId, UpdateReactionDto updateReactionDto)
        {
            var reactionToUpdate = _repository.GetCommentReactionAsync(reactionId);

            var userId = _userHttpContextService.Id
                ?? throw new UnauthorizedException("Unauthorized",
                $"Unauthorized user tried to update comment reaction with reactionId: {reactionId}");

            var authorizationResult = _authorizationService
                .AuthorizeAsync(_userHttpContextService.User, reactionToUpdate, new EditOrDeleteResourceRequirement()).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbiddenException("You can not update the reaction",
                    $"User with id: {userId} tried to update comment reaction with id: {reactionId}, which is a forbidden operation");
            }

            var reaction = _mapper.Map<CommentReaction>(updateReactionDto);

            reaction.Id = reactionId;
            reaction.LastModifiedById = userId;

            await _repository.UpdateCommentReactionAsync(reaction);

            Log.Logger.Information($"User with id: {userId} updated comment reaction with id: {reactionId}");
        }

        public async Task UpdatePostReactionAsync(int reactionId, UpdateReactionDto updateReactionDto)
        {
            var reactionToUpdate = _repository.GetPostReactionAsync(reactionId);

            var userId = _userHttpContextService.Id
                ?? throw new UnauthorizedException("Unauthorized",
                $"Unauthorized user tried to update post reaction with reactionId: {reactionId}");

            var authorizationResult = _authorizationService
                .AuthorizeAsync(_userHttpContextService.User, reactionToUpdate, new EditOrDeleteResourceRequirement()).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbiddenException("You can not update the reaction",
                    $"User with id: {userId} tried to update post reaction with id: {reactionId}, which is a forbidden operation");
            }

            var reaction = _mapper.Map<PostReaction>(updateReactionDto);

            reaction.Id = reactionId;
            reaction.LastModifiedById = userId;

            await _repository.UpdatePostReactionAsync(reaction);

            Log.Logger.Information($"User with id: {userId} updated post reaction with id: {reactionId}");
        }
    }
}
