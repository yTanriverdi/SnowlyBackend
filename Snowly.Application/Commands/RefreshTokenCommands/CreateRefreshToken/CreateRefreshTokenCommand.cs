using MediatR;
using Snowly.Application.Response;

namespace Snowly.Application.Commands.RefreshTokenCommands.CreateRefreshToken
{
    public record CreateRefreshTokenCommand(Guid UserId) : IRequest<ApplicationHandlerResponse<CreateRefreshTokenResponse>>;
}
