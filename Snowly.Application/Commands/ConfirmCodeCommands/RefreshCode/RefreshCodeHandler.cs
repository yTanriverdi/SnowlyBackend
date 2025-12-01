using MediatR;
using Snowly.Application.Commands.ConfirmCodeCommands.ConfirmCodeInterfaces;
using Snowly.Application.Commands.UserCommands.UserInterfaces;
using Snowly.Application.Response;
using Snowly.Application.ResponseMessages;
using Snowly.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snowly.Application.Commands.ConfirmCodeCommands.RefreshCode
{
    public class RefreshCodeHandler : IRequestHandler<RefreshCodeCommand, ApplicationHandlerResponse<RefreshCodeResponse>>
    {
        private readonly IUserConfirmCodeRepository _userConfirmCodeRepository;
        private readonly IUserRepository _userRepository;
        public RefreshCodeHandler(IUserConfirmCodeRepository userConfirmCodeRepository, IUserRepository userRepository)
        {
            _userConfirmCodeRepository = userConfirmCodeRepository;
            _userRepository = userRepository;
        }
        public async Task<ApplicationHandlerResponse<RefreshCodeResponse>> Handle(RefreshCodeCommand request, CancellationToken cancellationToken)
        {
            bool foundUser = await _userRepository.AnyUserByEmailAsync(request.Email, cancellationToken).ConfigureAwait(false);
            if (!foundUser) return ApplicationHandlerResponse<RefreshCodeResponse>.Fail(UserMessages.UserNotFound);
            UserConfirmCode userConfirmCode = await _userConfirmCodeRepository.RefreshUserConfirmCodeAsync(request.Email, cancellationToken).ConfigureAwait(false);
            RefreshCodeResponse refreshCodeResponse = new RefreshCodeResponse()
            {
                Code = userConfirmCode.Code,
            };
            return ApplicationHandlerResponse<RefreshCodeResponse>.Ok(refreshCodeResponse, "Onay kodu yenilendi");
        }
    }
}
