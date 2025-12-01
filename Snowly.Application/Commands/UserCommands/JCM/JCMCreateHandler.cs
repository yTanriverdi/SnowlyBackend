using MediatR;
using Snowly.Application.Commands.UserCommands.UserInterfaces;
using Snowly.Application.Interfaces;
using Snowly.Application.Response;
using Snowly.Application.ResponseMessages;
using Snowly.Domain.Entities;

namespace Snowly.Application.Commands.UserCommands.JCM
{
    public class JCMCreateHandler : IRequestHandler<JCMCreateCommand, ApplicationHandlerResponse<JCMCreateResponse>>
    {
        private readonly IJCMRepository _jcmRepository;
        private readonly IUserRepository _userRepository;


        public JCMCreateHandler(IJCMRepository jcmRepository, IUserRepository userRepository)
        {
            _jcmRepository = jcmRepository;
            _userRepository = userRepository;
        }

        public async Task<ApplicationHandlerResponse<JCMCreateResponse>> Handle(JCMCreateCommand request, CancellationToken cancellationToken)
        {
            string jcmToken;
            User? user = await _userRepository.GetUserByIdAsync(request.UserId, cancellationToken).ConfigureAwait(false);
            if (user == null) return ApplicationHandlerResponse<JCMCreateResponse>.Fail(UserMessages.UserNotFound);
            if (user.JCMToken == "new")
                jcmToken = await _jcmRepository.AddJCMToken(request.UserId, request.JcmToken, cancellationToken).ConfigureAwait(false);
            else
                jcmToken = await _jcmRepository.UpdateJCMToken(request.UserId, request.JcmToken, cancellationToken).ConfigureAwait(false);
            JCMCreateResponse jCMCreateResponse = new JCMCreateResponse()
            {
                JCMToken = jcmToken,
            };
            return ApplicationHandlerResponse<JCMCreateResponse>.Ok(jCMCreateResponse, JCMTokenMessages.JCMTokenCreateSuccess);
        }
    }
}
