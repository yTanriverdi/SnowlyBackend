using MediatR;
using Snowly.Application.Response;

namespace Snowly.Application.Commands.UserCommands.ChangePassword
{
    public record ChangePasswordCommand(Guid UserId, string OldPassword, string NewPassword) : IRequest<ApplicationHandlerResponse<ChangePasswordResponse>>;
}
