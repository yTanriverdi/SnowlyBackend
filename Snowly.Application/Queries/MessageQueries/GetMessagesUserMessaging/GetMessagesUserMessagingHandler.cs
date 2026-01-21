using MediatR;
using Snowly.Application.Interfaces;
using Snowly.Application.MessageSecure;
using Snowly.Application.Response;
using Snowly.Application.ResponseMessages;
namespace Snowly.Application.Queries.MessageQueries.GetMessagesUserMessaging
{
    public class GetMessagesUserMessagingHandler : IRequestHandler<GetMessagesUserMessagingQuery, ApplicationHandlerResponse<List<GetMessagesUserMessagingResponse>>>
    {
        private readonly IMessageRepository _messageRepository;
        private readonly MessageCrypto _messageCrypto;
        public GetMessagesUserMessagingHandler(IMessageRepository messageRepository, MessageCrypto messageCrypto)
        {
            _messageRepository = messageRepository;
            _messageCrypto = messageCrypto;
        }
        public async Task<ApplicationHandlerResponse<List<GetMessagesUserMessagingResponse>>> Handle(
            GetMessagesUserMessagingQuery request,
            CancellationToken cancellationToken)
        {
            var messages = await _messageRepository
        .GetAllMessagesByUserId(request.UserId, cancellationToken);

            if (!messages.Any())
            {
                return ApplicationHandlerResponse<List<GetMessagesUserMessagingResponse>>
                    .Fail(ChatMessages.NotFoundChats);
            }

            var chats = messages
                .GroupBy(m => m.SenderId == request.UserId
                    ? m.ReceiverUser
                    : m.SenderUser)
                .Select(g =>
                {
                    var lastMessage = g
                        .OrderByDescending(x => x.CreateDate)
                        .First();

                    return new GetMessagesUserMessagingResponse
                    {
                        UserId = g.Key.Id,
                        IsOnline = g.Key.IsOnline,
                        FullName = g.Key.FirstName + " " + g.Key.LastName,
                        LastMessageContent = _messageCrypto.Decrypt(lastMessage.Content),
                        LastMessageTime = lastMessage.CreateDate,
                        IsLastMessageFromMe = lastMessage.SenderId == request.UserId,
                        UnreadMessageCount = g.Count(x =>
                            x.ReceiverId == request.UserId && !x.IsRead)
                    };
                })
                .OrderByDescending(x => x.LastMessageTime)
                .ToList();

            return ApplicationHandlerResponse<List<GetMessagesUserMessagingResponse>>.Ok(chats, ChatMessages.FoundChats);
        }
    }
}
