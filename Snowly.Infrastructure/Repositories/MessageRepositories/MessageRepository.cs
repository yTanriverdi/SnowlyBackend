using Microsoft.EntityFrameworkCore;
using Snowly.Application.Interfaces;
using Snowly.Domain.Entities;
using Snowly.Infrastructure.SnowlyDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snowly.Infrastructure.Repositories.MessageRepositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly SnowlyDbContext _snowlyDbContext;

        public MessageRepository(SnowlyDbContext snowlyDbContext)
        {
            _snowlyDbContext = snowlyDbContext;
        }

        public async Task<Message> AddMessageAsync(Message newMessage, CancellationToken cancellationToken)
        {
            await _snowlyDbContext.Messages.AddAsync(newMessage, cancellationToken).ConfigureAwait(false);
            await _snowlyDbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return newMessage;
        }

        public async Task<bool> DeleteMessageAsync(Guid messageId, CancellationToken cancellationToken)
        {
            Message? message = await _snowlyDbContext.Messages.FirstOrDefaultAsync(x => x.Id == messageId, cancellationToken).ConfigureAwait(false);
            if (message == null) return false;
            message.IsDeleted = true;
            message.UpdateDate = DateTime.UtcNow;
            await _snowlyDbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return true;
        }

        public async Task<List<Message>> GetAllMessagesByUserId(Guid userId, CancellationToken cancellationToken)
        {
            return await _snowlyDbContext.Messages
            .AsNoTracking()
            .Include(x => x.SenderUser)
            .Include(x => x.ReceiverUser)
            .Where(x => x.SenderId == userId || x.ReceiverId == userId)
            .ToListAsync(cancellationToken);
        }

        public async Task<Message?> GetMessageById(Guid messageId, CancellationToken cancellationToken)
        {
            return await _snowlyDbContext.Messages.FirstOrDefaultAsync(x => x.Id == messageId, cancellationToken).ConfigureAwait(false);
        }

        public async Task<List<Message>> GetMessagesBetweenUserAsync(Guid senderId, Guid receiverId, int messageSize, int messageStack, CancellationToken cancellationToken)
        {
            var query = _snowlyDbContext.Messages.Where(x =>(x.SenderId == senderId && x.ReceiverId == receiverId) || (x.SenderId == receiverId && x.ReceiverId == senderId)).OrderByDescending(x => x.CreateDate);
            List<Message> messages = await query.Skip((messageStack - 1) * messageSize).Take(messageSize).ToListAsync(cancellationToken);
            messages.Reverse();
            return messages;
        }

        public async Task<int> MarkAsReadMessageAsync(Guid receiverId, Guid senderId, CancellationToken cancellationToken)
        {
            var updatedRows = await _snowlyDbContext.Messages.Where(m => !m.IsDeleted &&
                    m.SenderId == senderId &&
                    m.ReceiverId == receiverId &&
                    !m.IsRead).ExecuteUpdateAsync(u => u
            .SetProperty(m => m.IsRead, true)
            .SetProperty(m => m.ReadAt, DateTime.UtcNow),
            cancellationToken);
            return updatedRows;
        }

    }
}
