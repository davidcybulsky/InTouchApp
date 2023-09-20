using InTouchApi.Application.Interfaces;
using InTouchApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InTouchApi.Infrastructure.Data.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ApiContext _apiContext;

        public MessageRepository(ApiContext apiContext)
        {
            _apiContext = apiContext;
        }

        public Task DeleteMessageAsync(int messageId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DoesRecipientExist(int recipientId)
        {
            return (await _apiContext.Users
                .AsNoTracking()
                .Where(u => u.IsDeleted == false)
                .FirstOrDefaultAsync(u => u.Id == recipientId)) != null;
        }

        public Task<Message> EditMessageContentAsync(Message message)
        {
            throw new NotImplementedException();
        }

        public async Task<Message> GetMessageAsync(int messageId)
        {
            var message = await _apiContext.Messages
                .AsNoTracking()
                .Where(m => m.IsDeleted == false)
                .FirstOrDefaultAsync(m => m.Id == messageId);

            return message;
        }

        public async Task<IEnumerable<Message>> GetMessageThreadAsync(int firstUserId, int secondUserId)
        {
            var thread = await _apiContext.Messages
                .Include(m => m.Sender)
                .Include(m => m.Recipient)
                .Where(m => (m.IsDeleted == false &&
                ((m.SenderId == firstUserId && m.RecipientId == secondUserId) || (m.SenderId == secondUserId && m.RecipientId == firstUserId))))
                .OrderByDescending(m => m.CreationDate)
                .ToListAsync();

            return thread;
        }

        public async Task<Message> SendMessageAsync(Message message)
        {
            await _apiContext.AddAsync(message);
            await _apiContext.SaveChangesAsync();
            var newMessage = await _apiContext.Messages
                .Include(m => m.Sender)
                .Include(m => m.Recipient)
                .FirstOrDefaultAsync(m => m.Id == message.Id);
            return newMessage;
        }
    }
}