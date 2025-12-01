using MediatR;
using Snowly.Application.Commands.UserCommands.UserInterfaces;
using Snowly.Application.Helpers;
using Snowly.Application.Response;
using Snowly.Application.ResponseMessages;
using Snowly.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snowly.Application.Commands.UserCommands.DeleteUser
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, ApplicationHandlerResponse<DeleteUserResponse>>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ApplicationHandlerResponse<DeleteUserResponse>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userRepository.GetUserByEmailAsync(request.Email.ToLower(), cancellationToken).ConfigureAwait(false);
            if(user == null) return ApplicationHandlerResponse<DeleteUserResponse>.Fail(UserMessages.UserNotFound);
            bool userPasswordIsCorrect = PasswordHasher.Verify(user.Password, request.Password);
            if (!userPasswordIsCorrect) return ApplicationHandlerResponse<DeleteUserResponse>.Fail(UserMessages.InvalidPassword);
            bool userDeleteResult = await _userRepository.DeleteUserAsync(request.Email.ToLower(), cancellationToken).ConfigureAwait(false);
            if (!userDeleteResult) return ApplicationHandlerResponse<DeleteUserResponse>.Fail(UserMessages.UserDeleteFail);
            return ApplicationHandlerResponse<DeleteUserResponse>.Ok(new DeleteUserResponse() { Success = true }, UserMessages.UserDeleteSuccess);
        }
    }
}
