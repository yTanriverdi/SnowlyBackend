using MediatR;
using Snowly.Application.Commands.UserCommands.UserInterfaces;
using Snowly.Application.Response;
using Snowly.Application.ResponseMessages;
using Snowly.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snowly.Application.Commands.UserCommands.UpdateUser
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, ApplicationHandlerResponse<UpdateUserResponse>>
    {
        private readonly IUserRepository _userRepository;
        public UpdateUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<ApplicationHandlerResponse<UpdateUserResponse>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userRepository.UpdateUserAsync(request.Email, request.FirstName, request.LastName, cancellationToken).ConfigureAwait(false);
            if (user == null) return ApplicationHandlerResponse<UpdateUserResponse>.Fail(UserMessages.UserNotFound);
            UpdateUserResponse updateUserResponse = new UpdateUserResponse()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
            };
            return ApplicationHandlerResponse<UpdateUserResponse>.Ok(updateUserResponse, UserMessages.UserUpdateSuccess);
        }
    }
}
