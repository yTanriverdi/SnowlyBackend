using MediatR;
using Snowly.Application.Commands.UserCommands.UserInterfaces;
using Snowly.Application.Response;
using Snowly.Application.ResponseMessages;

namespace Snowly.Application.Commands.UserCommands.OnlineUser
{
    public class OnlineUserHandler : IRequestHandler<OnlineUserCommand, ApplicationHandlerResponse<OnlineUserResponse>>
    {
        private readonly IUserRepository _userRepository;

        public OnlineUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ApplicationHandlerResponse<OnlineUserResponse>> Handle(OnlineUserCommand request, CancellationToken cancellationToken)
        {
            bool result = await _userRepository.OnlineChangeAsync(request.UserId, cancellationToken).ConfigureAwait(false);
            if (!result) return ApplicationHandlerResponse<OnlineUserResponse>.Fail(UserMessages.OnlineStatusChangeFail);
            OnlineUserResponse onlineUserResponse = new OnlineUserResponse()
            {
                IsOnline = result
            };
            return ApplicationHandlerResponse<OnlineUserResponse>.Ok(onlineUserResponse, UserMessages.OnlineStatusChangeSuccess);
        }
    }
}
