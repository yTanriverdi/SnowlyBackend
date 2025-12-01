using Snowly.Domain.Entities;

namespace Snowly.Application.Commands.RefreshTokenCommands.CheckRefreshToken
{
    public sealed class CheckRefreshTokenResponse
    {
        public RefreshToken RefreshToken { get; set; } = null!;
    }
}
