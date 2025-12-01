using MediatR;
using Snowly.Application.Commands.ConfirmCodeCommands.ConfirmCodeInterfaces;
using Snowly.Application.Commands.UserCommands.UserInterfaces;
using Snowly.Application.Response;
using Snowly.Application.ResponseMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Snowly.Application.Commands.ConfirmCodeCommands.ConfirmCode
{
    public class ConfirmCodeHandler : IRequestHandler<ConfirmCodeCommand, ApplicationHandlerResponse<ConfirmCodeResponse>>
    {
        private readonly IUserConfirmCodeRepository _userConfirmCodeRepository;
        private readonly IUserRepository _userRepository;
        public ConfirmCodeHandler(IUserConfirmCodeRepository userConfirmCodeRepository, IUserRepository userRepository)
        {
            _userConfirmCodeRepository = userConfirmCodeRepository;
            _userRepository = userRepository;
        }
        public async Task<ApplicationHandlerResponse<ConfirmCodeResponse>> Handle(ConfirmCodeCommand request, CancellationToken cancellationToken)
        {
            bool userFound = await _userRepository.AnyUserByEmailAsync(request.Email, cancellationToken).ConfigureAwait(false);
            if (!userFound) return ApplicationHandlerResponse<ConfirmCodeResponse>.Fail(UserMessages.UserNotFound);
            bool userCode = await _userConfirmCodeRepository.ConfirmCodeAsync(request.Email, request.Code, cancellationToken).ConfigureAwait(false);
            if (!userCode) return ApplicationHandlerResponse<ConfirmCodeResponse>.Fail(UserConfirmCodeMessages.IncorrectCode);
            bool userConfirmResult = await _userRepository.ConfirmEmailAsync(request.Email, cancellationToken).ConfigureAwait(false);
            if (!userConfirmResult) return ApplicationHandlerResponse<ConfirmCodeResponse>.Fail(UserConfirmCodeMessages.ConfirmCodeFail);
            return ApplicationHandlerResponse<ConfirmCodeResponse>.Ok(new ConfirmCodeResponse() { Success = true }, UserConfirmCodeMessages.ConfirmCodeSuccess);
        }
    }
}
