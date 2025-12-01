using MediatR;
using Snowly.Application.Commands.UserCommands.UserInterfaces;
using Snowly.Application.Helpers;
using Snowly.Application.Response;
using Snowly.Application.ResponseMessages;
using Snowly.Domain.Entities;

namespace Snowly.Application.Commands.UserCommands.ChangePassword
{
    public class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand, ApplicationHandlerResponse<ChangePasswordResponse>>
    {
        private readonly IUserRepository _userRepository;

        public ChangePasswordHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ApplicationHandlerResponse<ChangePasswordResponse>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userRepository.GetUserByIdAsync(request.UserId, cancellationToken).ConfigureAwait(false);
            if (user == null) return ApplicationHandlerResponse<ChangePasswordResponse>.Fail(UserMessages.UserNotFound);
            string oldPassword = PasswordHasher.Hash(request.OldPassword);
            if (user.Password != oldPassword) return ApplicationHandlerResponse<ChangePasswordResponse>.Fail(UserMessages.WrongPassword);
            string newPassword = PasswordHasher.Hash(request.NewPassword);
            bool passwordChangeResult = await _userRepository.PasswordChangeAsync(user.Id, newPassword, cancellationToken).ConfigureAwait(false);
            if (!passwordChangeResult) return ApplicationHandlerResponse<ChangePasswordResponse>.Fail(UserMessages.FailPasswordChange);
            ChangePasswordResponse result = new ChangePasswordResponse()
            {
                Success = true
            };
            return ApplicationHandlerResponse<ChangePasswordResponse>.Ok(result, UserMessages.SuccessPasswordChange);
        }
    }
}
