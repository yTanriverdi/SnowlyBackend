using MediatR;
using Snowly.Application.Interfaces;
using Snowly.Application.MessageSecure;
using Snowly.Application.Response;
using Snowly.Application.ResponseMessages;
using Snowly.Domain.Entities;

namespace Snowly.Application.Queries.MessageQueries.GetMessagesBetweenUser
{
    public class GetMessagesBetweenUserHandler : IRequestHandler<GetMessagesBetweenUserQuery, ApplicationHandlerResponse<List<GetMessagesBetweenUserResponse>>>
    {
        private readonly IMessageRepository _messageRepository;
        private readonly MessageCrypto _messageCrypto;

        public GetMessagesBetweenUserHandler(IMessageRepository messageRepository, MessageCrypto messageCrypto)
        {
            _messageRepository = messageRepository;
            _messageCrypto = messageCrypto;
        }

        public async Task<ApplicationHandlerResponse<List<GetMessagesBetweenUserResponse>>> Handle(GetMessagesBetweenUserQuery request, CancellationToken cancellationToken)
        {
            List<Message>? messages = await _messageRepository.GetMessagesBetweenUserAsync(request.SenderId, request.ReceiverId, request.MessageSize, request.MessageStack, cancellationToken).ConfigureAwait(false);

            if (!messages.Any()) return ApplicationHandlerResponse<List<GetMessagesBetweenUserResponse>>.Fail(MessageMessages.MessagesNotFound);

            int totalMessageCount = await _messageRepository.MarkAsReadMessageAsync(request.ReceiverId, request.SenderId,cancellationToken).ConfigureAwait(false);

            var result = messages.Select(m => new GetMessagesBetweenUserResponse
            {
                Id = m.Id,
                Content = _messageCrypto.Decrypt(m.Content),
                SenderId = m.SenderId,
                ReceiverId = m.ReceiverId,
                IsRead = m.IsRead,
                ReadAt = m.ReadAt,
                CreatedAt = m.CreateDate
            }).ToList();

            return ApplicationHandlerResponse<List<GetMessagesBetweenUserResponse>>.Ok(result, $"Toplam {totalMessageCount} mesaj okundu olarak işaretlendi");
        }
    }
}
