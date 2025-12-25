using MediatR;
using Snowly.Application.Interfaces;
using Snowly.Application.Response;
using Snowly.Application.ResponseMessages;
using Snowly.Domain.Entities;

namespace Snowly.Application.Queries.FriendShipQueries.GetAllAcceptedFriendShips
{
    public class GetAllAccaptedFriendShipHandler : IRequestHandler<GetAllAcceptedFriendShipQuery, ApplicationHandlerResponse<List<GetAllAcceptedFriendShipResponse>>>
    {
        private readonly IFriendShipRepository _friendShipRepository;

        public GetAllAccaptedFriendShipHandler(IFriendShipRepository friendShipRepository)
        {
            _friendShipRepository = friendShipRepository;
        }

        public async Task<ApplicationHandlerResponse<List<GetAllAcceptedFriendShipResponse>>> Handle(GetAllAcceptedFriendShipQuery request, CancellationToken cancellationToken)
        {
            var acceptedFriendShips = await _friendShipRepository.AllAcceptedFriendShipAsync(request.RequesterId, cancellationToken).ConfigureAwait(false);
            if (!acceptedFriendShips.Any())
                return ApplicationHandlerResponse<List<GetAllAcceptedFriendShipResponse>>.Ok(new List<GetAllAcceptedFriendShipResponse>(), FriendShipMessages.FriendShipsAcceptedNo);

            List<GetAllAcceptedFriendShipResponse> allFriendShips = acceptedFriendShips.Select(f =>
            {
                var friend = f.RequesterId == request.RequesterId
                    ? f.Addressee!
                    : f.Requester!;

                return new GetAllAcceptedFriendShipResponse
                {
                    FriendShipId = f.Id,
                    FriendId = friend.Id,
                    FullName = $"{friend.FirstName} {friend.LastName}",
                    Email = friend.Email,
                    IsOnline = friend.IsOnline
                };
            }).ToList();
            return ApplicationHandlerResponse<List<GetAllAcceptedFriendShipResponse>>.Ok(allFriendShips, $"Toplam {allFriendShips.Count} arkadaşınız var");
        }
    }
}
