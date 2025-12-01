using MediatR;
using Snowly.Application.Interfaces;
using Snowly.Application.Response;
using Snowly.Application.ResponseMessages;

namespace Snowly.Application.Commands.MessageCommands.MarkReadMessage
{
    public class MarkReadMessageHandler : IRequestHandler<MarkReadMessageCommand, ApplicationHandlerResponse<MarkReadMessageResponse>>
    {
        private readonly IMessageRepository _messageRepository;

        public MarkReadMessageHandler(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task<ApplicationHandlerResponse<MarkReadMessageResponse>> Handle(MarkReadMessageCommand request, CancellationToken cancellationToken)
        {
            int markedAsReadMessages = await _messageRepository.MarkAsReadMessageAsync(request.ReceiverId, request.SenderId, cancellationToken).ConfigureAwait(false);
            MarkReadMessageResponse response = new MarkReadMessageResponse()
            {
                MessageCount = markedAsReadMessages
            };
            return ApplicationHandlerResponse<MarkReadMessageResponse>.Ok(response, MessageMessages.MessagesMarkedRead);
        }
    }
}
