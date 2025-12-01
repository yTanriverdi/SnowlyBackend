using MediatR;
using Snowly.Application.Interfaces;
using Snowly.Application.Response;
using Snowly.Application.ResponseMessages;
using Snowly.Domain.Entities;

namespace Snowly.Application.Commands.MessageCommands.DeleteMessage
{
    public class DeleteMessageHandler : IRequestHandler<DeleteMessageCommand, ApplicationHandlerResponse<DeleteMessageResponse>>
    {
        private readonly IMessageRepository _messageRepository;

        public DeleteMessageHandler(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task<ApplicationHandlerResponse<DeleteMessageResponse>> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
        {
            Message? message = await _messageRepository.GetMessageById(request.MessageId, cancellationToken).ConfigureAwait(false);
            if (message == null) return ApplicationHandlerResponse<DeleteMessageResponse>.Fail(MessageMessages.MessageNotFound);
            bool messageDeleteResult = await _messageRepository.DeleteMessageAsync(request.MessageId, cancellationToken).ConfigureAwait(false);
            if (!messageDeleteResult) return ApplicationHandlerResponse<DeleteMessageResponse>.Fail(MessageMessages.MessageDeleteFail);
            DeleteMessageResponse response = new DeleteMessageResponse()
            {
                Message = message,
                Success = true
            };
            return ApplicationHandlerResponse<DeleteMessageResponse>.Ok(response, MessageMessages.MessageDeleteSuccess);
        }
    }
}
