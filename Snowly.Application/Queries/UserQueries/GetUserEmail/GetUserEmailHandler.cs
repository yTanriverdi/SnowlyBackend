using MediatR;
using Snowly.Application.Commands.UserCommands.UserInterfaces;
using Snowly.Application.Response;
using Snowly.Application.ResponseMessages;
using Snowly.Domain.Entities;

namespace Snowly.Application.Queries.UserQueries.GetUserEmail
{
    public class GetUserEmailHandler : IRequestHandler<GetUserEmailQuery, ApplicationHandlerResponse<GetUserEmailResponse>>
    {
        private readonly IUserRepository _userRepository;

        public GetUserEmailHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ApplicationHandlerResponse<GetUserEmailResponse>> Handle(GetUserEmailQuery request, CancellationToken cancellationToken)
        {
            User? user = await _userRepository.GetUserByEmailAsync(request.Email, cancellationToken).ConfigureAwait(false);
            if (user == null) return ApplicationHandlerResponse<GetUserEmailResponse>.Fail(UserMessages.UserNotFound);
            GetUserEmailResponse response = new GetUserEmailResponse()
            {
                UserId = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
            return ApplicationHandlerResponse<GetUserEmailResponse>.Ok(response, UserMessages.UserFound);
        }
    }
}
