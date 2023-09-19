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

        public Task DeleteMessageAsync(int messageId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DoesRecipientExist(int recipientId)
        {
            return await _repository.DoesRecipientExist(recipientId);
        }

        public async Task<MessageDto> EditMessageContentAsync(UpdateMessageDto updateMessageDto)
        {
            var getMessage = await _repository.GetMessageAsync(updateMessageDto.Id);

            if (getMessage.SenderId != updateMessageDto.SenderId || getMessage.Id != updateMessageDto.Id)
            {
                return null;
            }

            var updateMessage = _mapper.Map<Message>(updateMessageDto);

            var message = await _repository.EditMessageContentAsync(updateMessage);
            var messageDto = _mapper.Map<MessageDto>(message);

            return messageDto;
        }

        public Task<string> GetMessageGroupName(int user, int otherUser)
        {
            return Task.FromResult(user - otherUser > 1 ? $"{user}-{otherUser}" : $"{otherUser}-{user}");
        }

        public async Task<IEnumerable<MessageDto>> GetMessageThreadAsync(int firstUserId, int secondUserId)
        {
            var messages = await _repository.GetMessageThreadAsync(firstUserId, secondUserId);
            var messageDtos = _mapper.Map<IEnumerable<MessageDto>>(messages);
            return messageDtos;
        }

        public Task ReadMessage(int messageId)
        {
            throw new NotImplementedException();
        }

        public async Task<MessageDto> SendMessageAsync(int identity, SendMessageDto sendMessageDto)
        {
            var message = _mapper.Map<Message>(sendMessageDto);

            message.SenderId = identity;
            message.CreatedById = identity;

            var message1 = await _repository.SendMessageAsync(message);

            var messageDto = _mapper.Map<MessageDto>(message1);

            return messageDto;
        }
    }
}
