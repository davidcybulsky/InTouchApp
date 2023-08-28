using AutoMapper;
using InTouchApi.Application.Exceptions;
using InTouchApi.Application.Interfaces;
using InTouchApi.Application.Models;
using InTouchApi.Domain.Entities;

namespace InTouchApi.Infrastructure.Services
{
    public class FriendService : IFriendService
    {
        private readonly IFriendRepository _repository;
        private readonly IUserHttpContextService _userHttpContextService;
        private readonly IMapper _mapper;

        public FriendService(IFriendRepository repository,
                             IUserHttpContextService userHttpContextService,
                             IMapper mapper)
        {
            _repository = repository;
            _userHttpContextService = userHttpContextService;
            _mapper = mapper;
        }

        public async Task AcceptFriendRequestAsync(int friendId)
        {
            var userId = _userHttpContextService.Id ?? throw new UnauthorizedException("");
            var friendship = await _repository.GetFriendshipAsTrackingAsync(userId, friendId);
            var friendship2 = await _repository.GetFriendshipAsTrackingAsync(friendId, userId);

            friendship.IsAccepted = true;
            friendship.LastModificationDate = DateTime.UtcNow;
            friendship.LastModifiedById = userId;

            friendship2.IsAccepted = true;
            friendship2.LastModificationDate = friendship.LastModificationDate;
            friendship2.LastModifiedById = userId;

            await _repository.UpdateFriendshipAsync();
        }

        public async Task<IEnumerable<FriendDto>> GetFriendRequestsAsync()
        {
            var userId = _userHttpContextService.Id ?? throw new UnauthorizedException("");
            var friendRequests = await _repository.GetUserFriendRequestsAsync(userId);
            var friendRequestDtos = _mapper.Map<IEnumerable<FriendDto>>(friendRequests);
            return friendRequestDtos;
        }

        public async Task<IEnumerable<FriendDto>> GetFriendsAsync()
        {
            var userId = _userHttpContextService.Id ?? throw new UnauthorizedException("Anauthorized access");
            var friends = await _repository.GetUserFriendsAsync(userId);
            var friendDtos = _mapper.Map<IEnumerable<FriendDto>>(friends);
            return friendDtos;
        }

        public async Task<IEnumerable<FriendDto>> GetUserFriendRequestsAsync(int id)
        {
            var friendRequests = await _repository.GetUserFriendRequestsAsync(id);
            var friendRequestDtos = _mapper.Map<IEnumerable<FriendDto>>(friendRequests);
            return friendRequestDtos;
        }

        public async Task<IEnumerable<FriendDto>> GetUserFriendsAsync(int id)
        {
            var friends = await _repository.GetUserFriendsAsync(id);
            var friendDtos = _mapper.Map<IEnumerable<FriendDto>>(friends);
            return friendDtos;
        }

        public async Task SendFriendRequestAsync(int friendId)
        {
            var userId = _userHttpContextService.Id ?? throw new UnauthorizedException("Anauthorized access");

            var friendship = new Friendship
            {
                UserId = userId,
                FriendId = friendId,
                SendById = userId,
                CreatedById = userId,
                CreationDate = DateTime.UtcNow,
                IsAccepted = false,
            };

            var recursiceFriendship = new Friendship
            {
                UserId = friendship.FriendId,
                FriendId = friendship.UserId,
                SendById = userId,
                CreatedById = userId,
                CreationDate = DateTime.UtcNow,
                IsAccepted = false,
            };

            await _repository.CreateFriendshipAsync(friendship);
            await _repository.CreateFriendshipAsync(recursiceFriendship);
        }
    }
}
