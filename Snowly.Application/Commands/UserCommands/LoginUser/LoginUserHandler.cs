using MediatR;
using Snowly.Application.Commands.UserCommands.UserInterfaces;
using Snowly.Application.Helpers;
using Snowly.Application.Interfaces;
using Snowly.Application.Response;
using Snowly.Application.ResponseMessages;
using Snowly.Domain.Entities;
using System.Security.Cryptography;

namespace Snowly.Application.Commands.UserCommands.LoginUser
{
    public class LoginUserHandler : IRequestHandler<LoginUserCommand, ApplicationHandlerResponse<LoginUserResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public LoginUserHandler(IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository)
        {
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<ApplicationHandlerResponse<LoginUserResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userRepository.GetUserByEmailAsync(request.Email, cancellationToken).ConfigureAwait(false);
            if (user == null) return ApplicationHandlerResponse<LoginUserResponse>.Fail(UserMessages.WrongEmailOrPassword);
            if (user.EmailConfirmed == false) return ApplicationHandlerResponse<LoginUserResponse>.Fail(UserMessages.StillWrongConfirmCode);
            bool userPassword = PasswordHasher.Verify(user.Password, request.Password);
            if (!userPassword) return ApplicationHandlerResponse<LoginUserResponse>.Fail(UserMessages.WrongEmailOrPassword);
            RefreshToken? refreshToken;
               refreshToken = await _refreshTokenRepository.GetRefreshTokenAsync(user.Id, cancellationToken).ConfigureAwait(false);
            if (refreshToken == null || (refreshToken.Expires < DateTime.UtcNow && refreshToken.IsRevoked == true) || refreshToken.IsRevoked == true) {
                RefreshToken addRefreshToken = CreateRefreshToken(user.Id);
                refreshToken = await _refreshTokenRepository.AddRefreshToken(addRefreshToken, cancellationToken).ConfigureAwait(false);
            }
            else
            {
                RefreshToken addRefreshToken = CreateRefreshToken(user.Id);
                refreshToken = await _refreshTokenRepository.RotateRefreshTokenAsync(refreshToken, addRefreshToken, cancellationToken).ConfigureAwait(false);
            }
            LoginUserResponse response = new LoginUserResponse()
            {
                UserId = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role,
                RefreshToken = refreshToken.Token
            };
            return ApplicationHandlerResponse<LoginUserResponse>.Ok(response, RefreshTokenMessages.ReturnRefreshToken);
        }

        private RefreshToken CreateRefreshToken(Guid userId) => new RefreshToken
             {
                 UserId = userId,
                 Expires = DateTime.UtcNow.AddDays(10),
                 Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                 IsRevoked = false
             };
    }
}
