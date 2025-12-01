using MediatR;
using Snowly.Application.Response;

namespace Snowly.Application.Commands.MessageCommands.MarkReadMessage
{
    public record MarkReadMessageCommand(Guid ReceiverId, Guid SenderId) : IRequest<ApplicationHandlerResponse<MarkReadMessageResponse>>;
}
