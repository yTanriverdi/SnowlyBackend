using MediatR;
using Snowly.Application.Commands.UserCommands.UserInterfaces;
using Snowly.Application.Interfaces;
using Snowly.Application.Response;
using Snowly.Application.ResponseMessages;
using Snowly.Domain.Entities;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace Snowly.Application.Commands.RefreshTokenCommands.CheckRefreshToken
{
    public class CheckRefreshTokenHandler : IRequestHandler<CheckRefreshTokenCommand, ApplicationHandlerResponse<CheckRefreshTokenResponse>>
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUserRepository _userRepository;

        public CheckRefreshTokenHandler(IRefreshTokenRepository refreshTokenRepository, IUserRepository userRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _userRepository = userRepository;
        }

        public async Task<ApplicationHandlerResponse<CheckRefreshTokenResponse>> Handle(CheckRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userRepository.GetUserByIdAsync(request.UserId, cancellationToken).ConfigureAwait(false);
            if (user == null)
                return ApplicationHandlerResponse<CheckRefreshTokenResponse>.Fail(UserMessages.UserNotFound);

            RefreshToken? checkRefreshToken = await _refreshTokenRepository.GetActiveTokenAsync(
                request.UserId, request.RefreshToken, cancellationToken).ConfigureAwait(false);

            RefreshToken? forResponseRefreshToken;

            if (checkRefreshToken == null || checkRefreshToken.IsRevoked || checkRefreshToken.Expires <= DateTime.UtcNow)
            {
                RefreshToken newRefreshToken = new RefreshToken()
                {
                    UserId = request.UserId,
                    Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                    IsRevoked = false,
                    Expires = DateTime.UtcNow.AddDays(10),
                };

                if (checkRefreshToken != null)
                    forResponseRefreshToken = await _refreshTokenRepository.RotateRefreshTokenAsync(checkRefreshToken, newRefreshToken, cancellationToken).ConfigureAwait(false);
                else
                    forResponseRefreshToken = await _refreshTokenRepository.AddRefreshToken(newRefreshToken, cancellationToken).ConfigureAwait(false);
                CheckRefreshTokenResponse newResponse = new CheckRefreshTokenResponse()
                {
                    RefreshToken = forResponseRefreshToken
                };
                return ApplicationHandlerResponse<CheckRefreshTokenResponse>.Ok(newResponse, RefreshTokenMessages.RecreateRefreshToken);
            }
            else
                forResponseRefreshToken = checkRefreshToken;
                CheckRefreshTokenResponse response = new CheckRefreshTokenResponse()
                {
                    RefreshToken = forResponseRefreshToken
                };
            return ApplicationHandlerResponse<CheckRefreshTokenResponse>.Ok(response, RefreshTokenMessages.RecreateRefreshToken);
        }
    }
}
