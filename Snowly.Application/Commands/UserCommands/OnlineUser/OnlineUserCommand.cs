using MediatR;
using Snowly.Application.Response;

namespace Snowly.Application.Commands.UserCommands.OnlineUser
{
    public record OnlineUserCommand(Guid UserId, bool isOnline) : IRequest<ApplicationHandlerResponse<OnlineUserResponse>>;
}
