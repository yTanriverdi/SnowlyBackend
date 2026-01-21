using MediatR;
using Snowly.Application.Commands.UserCommands.UserInterfaces;
using Snowly.Application.Response;
using Snowly.Application.ResponseMessages;
using Snowly.Domain.Entities;

namespace Snowly.Application.Queries.UserQueries.GetUserId
{
    public class GetUserIdHandler : IRequestHandler<GetUserIdQuery, ApplicationHandlerResponse<GetUserIdResponse>>
    {
        private readonly IUserRepository _userRepository;

        public GetUserIdHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ApplicationHandlerResponse<GetUserIdResponse>> Handle(GetUserIdQuery request, CancellationToken cancellationToken)
        {
            User? user = await _userRepository.GetUserByIdAsync(request.UserId, cancellationToken).ConfigureAwait(false);
            if (user == null) return ApplicationHandlerResponse<GetUserIdResponse>.Fail(UserMessages.UserNotFound);
            GetUserIdResponse response = new GetUserIdResponse()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                IsOnline = user.IsOnline,
            };
            return ApplicationHandlerResponse<GetUserIdResponse>.Ok(response, UserMessages.UserFound);
        }
    }
}
