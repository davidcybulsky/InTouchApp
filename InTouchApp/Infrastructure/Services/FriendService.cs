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
    public class FriendService : IFriendService
    {
        private readonly IFriendRepository _repository;
        private readonly IUserHttpContextService _userHttpContextService;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;

        public FriendService(IFriendRepository repository,
                             IUserHttpContextService userHttpContextService,
                             IMapper mapper,
                             IAuthorizationService authorizationService)
        {
            _repository = repository;
            _userHttpContextService = userHttpContextService;
            _mapper = mapper;
            _authorizationService = authorizationService;
        }

        public async Task AcceptFriendRequestAsync(int friendId)
        {
            var userId = _userHttpContextService.Id ??
                throw new UnauthorizedException("Unauthorized",
                $"Unauthorized user tried to accept friend request with friendId: {friendId}");

            var friendship = await _repository.GetFriendshipAsync(userId, friendId);
            var friendship2 = await _repository.GetFriendshipAsync(friendId, userId);

            var authorizationResult = _authorizationService
                .AuthorizeAsync(_userHttpContextService.User,
                                friendship,
                                new AcceptFriendRequestRequirement()).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbiddenException("You can not accept the request",
                    $"User with id: {userId}, tried accept friend request with friendId {friendId}, which is a forbidden operation");
            }

            var authorizationResult2 = _authorizationService
                    .AuthorizeAsync(_userHttpContextService.User,
                    friendship2,
                    new AcceptFriendRequestRequirement()).Result;

            if (!authorizationResult2.Succeeded)
            {
                throw new ForbiddenException("You can not accept the request",
                    $"User with id: {userId}, tried accept friend request with friendId {friendId}, which is a forbidden operation");
            }

            friendship.IsAccepted = true;
            friendship.LastModifiedById = userId;

            friendship2.IsAccepted = true;
            friendship2.LastModifiedById = userId;

            await _repository.UpdateFriendshipAsync(friendship);
            await _repository.UpdateFriendshipAsync(friendship2);

            Log.Logger.Information($"User with id: {userId} accepted friend request from user with id: {friendId}");
        }

        public async Task DeleteFriendAsync(int friendId)
        {
            var userId = _userHttpContextService.Id
                ?? throw new UnauthorizedException("Unauthorized",
                $"Unauthorized user tried to delete friend with friendId: {friendId}");

            var friendship = await _repository.GetFriendshipAsync(userId, friendId);
            var friendship2 = await _repository.GetFriendshipAsync(friendId, userId);

            friendship.LastModifiedById = userId;
            friendship2.LastModifiedById = userId;

            await _repository.DeleteFriendshipAsync(friendship);
            await _repository.DeleteFriendshipAsync(friendship2);

            Log.Logger.Information($"User with id: {userId} deleted friend with id: {friendId}");
        }

        public async Task<IEnumerable<FriendDto>> GetFriendRequestsAsync()
        {
            var userId = _userHttpContextService.Id
                ?? throw new UnauthorizedException("Unauthorized",
                $"Unauthorized user tried to get its friend requests");

            var friendRequests = await _repository.GetUserFriendRequestsAsync(userId);
            var friendRequestDtos = _mapper.Map<IEnumerable<FriendDto>>(friendRequests);

            Log.Logger.Information($"User with id: {userId} got friend requests. Count: {friendRequestDtos.Count()}");

            return friendRequestDtos;
        }

        public async Task<IEnumerable<FriendDto>> GetFriendsAsync()
        {
            var userId = _userHttpContextService.Id
                ?? throw new UnauthorizedException("Unauthorized",
                $"Unauthorized user tried to get its friends");

            var friends = await _repository.GetUserFriendsAsync(userId);
            var friendDtos = _mapper.Map<IEnumerable<FriendDto>>(friends);

            Log.Logger.Information($"User with id: {userId} got friends. Count: {friendDtos.Count()}");

            return friendDtos;
        }

        public async Task<IEnumerable<FriendDto>> GetUserFriendRequestsAsync(int id)
        {
            var friendRequests = await _repository.GetUserFriendRequestsAsync(id);
            var friendRequestDtos = _mapper.Map<IEnumerable<FriendDto>>(friendRequests);

            Log.Logger.Information($"List of user with id: {id} friends was returned");

            return friendRequestDtos;
        }

        public async Task<IEnumerable<FriendDto>> GetUserFriendsAsync(int id)
        {
            var friends = await _repository.GetUserFriendsAsync(id);
            var friendDtos = _mapper.Map<IEnumerable<FriendDto>>(friends);

            Log.Logger.Information($"List of user with id: {id} friend requests was returned");

            return friendDtos;
        }

        public async Task SendFriendRequestAsync(int friendId)
        {
            var userId = _userHttpContextService.Id
                ?? throw new UnauthorizedException("Unauthorized",
                $"Unauthorized user tried to send friend request. FriendId: {friendId}");

            if (friendId == userId)
            {
                throw new BadRequestException("You can not send a friend request to yourself",
                    $"User with id: {userId} send a frend request to itself");
            }

            var friendshipInDb = await _repository.DoesFriendshipExist(userId, friendId);
            var friendshipInDb2 = await _repository.DoesFriendshipExist(friendId, userId);

            if (friendshipInDb == true)
            {
                var friendship = new Friendship
                {
                    UserId = userId,
                    FriendId = friendId,
                    SendById = userId,
                    LastModifiedById = userId,
                    CreatedById = userId,
                    CreationDate = DateTime.UtcNow,
                    IsAccepted = false,
                };
                await _repository.RecreateFriendshipAsync(friendship);
            }
            else
            {
                var friendship = new Friendship
                {
                    UserId = userId,
                    FriendId = friendId,
                    SendById = userId,
                    CreatedById = userId,
                    CreationDate = DateTime.UtcNow,
                    IsAccepted = false,
                };

                await _repository.CreateFriendshipAsync(friendship);
            }

            if (friendshipInDb2 == false)
            {
                var recursiceFriendship = new Friendship
                {
                    UserId = friendId,
                    FriendId = userId,
                    SendById = userId,
                    CreatedById = userId,
                    CreationDate = DateTime.UtcNow,
                    IsAccepted = false,
                };

                await _repository.CreateFriendshipAsync(recursiceFriendship);
            }
            else
            {
                var recursiceFriendship = new Friendship
                {
                    UserId = friendId,
                    FriendId = userId,
                    SendById = userId,
                    CreatedById = userId,
                    CreationDate = DateTime.UtcNow,
                    LastModifiedById = userId,
                    IsAccepted = false,
                };

                await _repository.RecreateFriendshipAsync(recursiceFriendship);
            }

            Log.Logger.Information($"User with id: {userId} sent friend request to user with id: {friendId}");
        }
    }
}
