using MediatR;
using Snowly.Application.Response;

namespace Snowly.Application.Commands.MessageCommands.DeleteMessage
{
    public record DeleteMessageCommand(Guid MessageId) : IRequest<ApplicationHandlerResponse<DeleteMessageResponse>>;
}
