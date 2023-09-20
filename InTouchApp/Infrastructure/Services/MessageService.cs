using AutoMapper;
using InTouchApi.Application.Interfaces;
using InTouchApi.Application.Models;
using InTouchApi.Domain.Entities;

namespace InTouchApi.Infrastructure.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _repository;
        private readonly IMapper _mapper;

        public MessageService(IMessageRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> DoesRecipientExist(int recipientId)
        {
            return await _repository.DoesRecipientExist(recipientId);
        }

        public Task<string> GetMessageGroupName(int user, int otherUser)
        {
            var groupName = (user > otherUser) ? $"{user}-{otherUser}" : $"{otherUser}-{user}";
            return Task.FromResult(groupName);
        }

        public async Task<IEnumerable<MessageDto>> GetMessageThreadAsync(int firstUserId, int secondUserId)
        {
            var messages = await _repository.GetMessageThreadAsync(firstUserId, secondUserId);
            var messageDtos = _mapper.Map<IEnumerable<MessageDto>>(messages);
            return messageDtos;
        }

        public async Task<MessageDto> SendMessageAsync(int identity, SendMessageDto sendMessageDto)
        {
            var message = _mapper.Map<Message>(sendMessageDto);

            message.SenderId = identity;
            message.CreatedById = identity;
            message.CreationDate = DateTime.UtcNow;

            var message1 = await _repository.SendMessageAsync(message);

            var messageDto = _mapper.Map<MessageDto>(message1);

            return messageDto;
        }
    }
}
