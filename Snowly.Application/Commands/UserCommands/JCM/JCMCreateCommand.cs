using MediatR;
using Snowly.Application.Response;

namespace Snowly.Application.Commands.UserCommands.JCM
{
    public record JCMCreateCommand(Guid UserId, string JcmToken) : IRequest<ApplicationHandlerResponse<JCMCreateResponse>>;
}
