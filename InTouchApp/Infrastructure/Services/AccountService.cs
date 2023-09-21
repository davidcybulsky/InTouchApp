using AutoMapper;
using InTouchApi.Application.Exceptions;
using InTouchApi.Application.Interfaces;
using InTouchApi.Application.Models;
using InTouchApi.Domain.Constants;
using InTouchApi.Domain.Entities;
using Serilog;

namespace InTouchApi.Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _repository;
        private readonly IUserHttpContextService _userHttpContextService;
        private readonly IMapper _mapper;

        public AccountService(IAccountRepository repository,
                              IUserHttpContextService userHttpContextService,
                              IMapper mapper)
        {
            _repository = repository;
            _userHttpContextService = userHttpContextService;
            _mapper = mapper;
        }

        public async Task DeleteAccountAsync()
        {
            var id = _userHttpContextService.Id ??
                throw new UnauthorizedException("Unauthorized",
                "Unauthorized user tried to delete account");
            var account = await _repository.GetAccountAsync(id);

            account.Id = id;

            await _repository.DeleteAccountAsync(account);

            Log.Information($"User with id: {id}, deleted its account");
        }

        public async Task<AccountDto> GetAccountAsync()
        {
            var id = _userHttpContextService.Id
                ?? throw new UnauthorizedException("Unauthorized",
                "Unauthorized user tried to get account");
            var account = await _repository.GetAccountAsync(id);

            var accountDto = _mapper.Map<AccountDto>(account);

            foreach (var post in account.Posts)
            {
                var postDto = accountDto.Posts.FirstOrDefault(x => x.Id == post.Id);
                postDto.Author.UserPhoto = SetMainPhoto(post.Author.UserPhotos);
                CountPostReactions(postDto, post);
                CheckedMyPostReaction(postDto, post);

                foreach (var comment in post.Comments)
                {
                    var commentDto = postDto.Comments.FirstOrDefault(x => x.Id == comment.Id);
                    commentDto.Author.UserPhoto = SetMainPhoto(comment.Author.UserPhotos);
                }
            }

            Log.Information($"User with id: {id}, got its account");

            return accountDto;
        }

        private void CheckedMyPostReaction(PostDto postDto, Post post)
        {
            var reaction = post.Reactions.FirstOrDefault(r => r.UserId == _userHttpContextService.Id);

            if (reaction != null)
            {
                postDto.ReactionsData.DidIReacted = true;
                postDto.ReactionsData.ReactionType = reaction.ReactionType;
            }

        }

        private void CountPostReactions(PostDto postDto, Post post)
        {
            postDto.ReactionsData.AmountOfDislikes = post.Reactions.Count(r => r.ReactionType == REACTIONS.DISLIKE);
            postDto.ReactionsData.AmountOfLikes = post.Reactions.Count(r => r.ReactionType == REACTIONS.LIKE);

            foreach (var comment in post.Comments)
            {
                var commentDto = postDto.Comments.FirstOrDefault(x => x.Id == comment.Id);
                CountCommentReactions(commentDto, comment);
                CheckMyCommentReaction(commentDto, comment);
            }
        }

        private void CheckMyCommentReaction(IncludeCommentDto commentDto, PostComment comment)
        {
            var reaction = comment.CommentReactions.FirstOrDefault(r => r.UserId == _userHttpContextService.Id);

            if (reaction != null)
            {
                commentDto.ReactionsData.DidIReacted = true;
                commentDto.ReactionsData.ReactionType = reaction.ReactionType;
            }
        }

        private void CountCommentReactions(IncludeCommentDto commentDto, Comment comment)
        {
            commentDto.ReactionsData.AmountOfDislikes = comment.CommentReactions.Count(r => r.ReactionType == REACTIONS.DISLIKE);
            commentDto.ReactionsData.AmountOfLikes = comment.CommentReactions.Count(r => r.ReactionType == REACTIONS.LIKE);
        }

        private IncludePhotoDto? SetMainPhoto(IEnumerable<UserPhoto> userPhotos)
        {
            IncludePhotoDto dto = null;
            if (userPhotos.Any(p => p.IsMain == true))
            {
                var photo = userPhotos.FirstOrDefault(p => p.IsMain == true);
                dto = _mapper.Map<IncludePhotoDto>(photo);
            }
            return dto;
        }

        public async Task UpdateAccountAsync(UpdateAccountDto updateAccountDto)
        {
            var id = _userHttpContextService.Id
                ?? throw new UnauthorizedException("Unauthorized",
                "Unauthorized user tried to update account");

            var account = _mapper.Map<User>(updateAccountDto);

            account.Id = id;

            await _repository.UpdateAccountAsync(account);

            Log.Information($"User with id: {id}, updated its account");
        }
    }
}
