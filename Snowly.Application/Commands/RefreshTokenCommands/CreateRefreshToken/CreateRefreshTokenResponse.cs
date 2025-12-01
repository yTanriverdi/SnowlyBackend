using Snowly.Domain.Entities;

namespace Snowly.Application.Commands.RefreshTokenCommands.CreateRefreshToken
{
    public sealed class CreateRefreshTokenResponse
    {
        public RefreshToken RefreshToken { get; set; } = null!;
    }
}
