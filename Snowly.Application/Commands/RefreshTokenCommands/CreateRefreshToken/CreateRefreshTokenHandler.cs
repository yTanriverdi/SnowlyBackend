using MediatR;
using Snowly.Application.Commands.UserCommands.UserInterfaces;
using Snowly.Application.Interfaces;
using Snowly.Application.Response;
using Snowly.Application.ResponseMessages;
using Snowly.Domain.Entities;
using System.Security.Cryptography;

namespace Snowly.Application.Commands.RefreshTokenCommands.CreateRefreshToken
{
    public class CreateRefreshTokenHandler : IRequestHandler<CreateRefreshTokenCommand, ApplicationHandlerResponse<CreateRefreshTokenResponse>>
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUserRepository _userRepository;

        public CreateRefreshTokenHandler(IRefreshTokenRepository refreshTokenRepository, IUserRepository userRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _userRepository = userRepository;
        }

        public async Task<ApplicationHandlerResponse<CreateRefreshTokenResponse>> Handle(CreateRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userRepository.GetUserByIdAsync(request.UserId, cancellationToken).ConfigureAwait(false);
            if (user == null) return ApplicationHandlerResponse<CreateRefreshTokenResponse>.Fail(UserMessages.UserNotFound);
            RefreshToken newRefreshToken = new RefreshToken()
            {
                UserId = request.UserId,
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                IsRevoked = false,
                Expires = DateTime.UtcNow.AddDays(10),
            };
            RefreshToken addedRefreshToken = await _refreshTokenRepository.AddRefreshToken(newRefreshToken, cancellationToken).ConfigureAwait(false);
            CreateRefreshTokenResponse createRefreshTokenResponse = new CreateRefreshTokenResponse()
            {
                RefreshToken = addedRefreshToken,
            };
            return ApplicationHandlerResponse<CreateRefreshTokenResponse>.Ok(createRefreshTokenResponse, RefreshTokenMessages.AddedRefreshToken);
        }
    }
}
