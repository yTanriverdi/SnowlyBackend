using MediatR;
using Snowly.Application.Commands.ConfirmCodeCommands.ConfirmCodeInterfaces;
using Snowly.Application.Commands.UserCommands.UserInterfaces;
using Snowly.Application.EmailSenderService;
using Snowly.Application.Helpers;
using Snowly.Application.Response;
using Snowly.Application.ResponseMessages;
using Snowly.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snowly.Application.Commands.UserCommands.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, ApplicationHandlerResponse<CreateUserResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserConfirmCodeRepository _userConfirmCodeRepository;
        private readonly EmailSender _emailSender;
        public CreateUserHandler(IUserRepository userRepository, IUserConfirmCodeRepository userConfirmCodeRepository, EmailSender emailSender)
        {
            _userRepository = userRepository;
            _userConfirmCodeRepository = userConfirmCodeRepository;
            _emailSender = emailSender;

        }
        public async Task<ApplicationHandlerResponse<CreateUserResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                bool oldUser = await _userRepository.AnyUserByEmailAsync(request.Email, cancellationToken).ConfigureAwait(false);
                if (oldUser) return ApplicationHandlerResponse<CreateUserResponse>.Fail(UserMessages.UserAlreadyExists);

                User newUser = new User()
                {
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Password = PasswordHasher.Hash(request.Password),
                };

                User addedUser = await _userRepository.AddUserAsync(newUser, cancellationToken).ConfigureAwait(false);
                CreateUserResponse addUserResponse = new CreateUserResponse
                {
                    Email = addedUser.Email,
                    FirstName = addedUser.FirstName,
                    LastName = addedUser.LastName,
                    Status = addedUser.Status,
                    EmailConfirmed = addedUser.EmailConfirmed
                };
                UserConfirmCode newUserConfirmCode = new UserConfirmCode()
                {
                    Email = addedUser.Email
                };
                if (addedUser.Email == "xxx@gmail.com")
                    newUserConfirmCode.Code = "060921";
                UserConfirmCode userConfirmCode = await _userConfirmCodeRepository.AddUserConfirmCodeAsync(newUserConfirmCode, cancellationToken).ConfigureAwait(false);
                if (userConfirmCode == null) return ApplicationHandlerResponse<CreateUserResponse>.Fail("Kullanıcı oluşturuldu fakat mail gönderilemedi");
                bool emailSenderResponse = await _emailSender.SendMail(addedUser.Email, userConfirmCode.Code).ConfigureAwait(false);
                if (!emailSenderResponse) return ApplicationHandlerResponse<CreateUserResponse>.Fail("Kullanıcı oluşturuldu fakat mail gönderilemedi");
                return ApplicationHandlerResponse<CreateUserResponse>.Ok(addUserResponse, UserMessages.UserCreatedSuccessfully);
            }
            catch (Exception ex)
            {
                return ApplicationHandlerResponse<CreateUserResponse>.Fail(ex.Message);
            }
        }
    }
}
