using MediatR;
using Snowly.Application.Response;

namespace Snowly.Application.Commands.UserCommands.LoginUser
{
    public record LoginUserCommand(string Email, string Password) : IRequest<ApplicationHandlerResponse<LoginUserResponse>>;
}
