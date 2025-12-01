using MediatR;
using Snowly.Application.Response;
using Snowly.Domain.Entities;

namespace Snowly.Application.Commands.RefreshTokenCommands.CheckRefreshToken
{
    public record CheckRefreshTokenCommand(Guid UserId, string RefreshToken) : IRequest<ApplicationHandlerResponse<CheckRefreshTokenResponse>>;
}
