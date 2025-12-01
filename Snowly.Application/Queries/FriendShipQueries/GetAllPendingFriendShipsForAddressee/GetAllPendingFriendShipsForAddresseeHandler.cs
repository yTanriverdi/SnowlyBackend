using MediatR;
using Snowly.Application.Interfaces;
using Snowly.Application.Queries.FriendShipQueries.GetAllPendingFriendShips;
using Snowly.Application.Response;
using Snowly.Application.ResponseMessages;

namespace Snowly.Application.Queries.FriendShipQueries.GetAllPendingFriendShipsForAddressee
{
    public class GetAllPendingFriendShipsForAddresseeHandler : IRequestHandler<GetAllPendingFriendShipsForAddresseeQuery, ApplicationHandlerResponse<List<GetAllPendingFriendShipsForAddresseeResponse>>>
    {
        private readonly IFriendShipRepository _friendShipRepository;

        public GetAllPendingFriendShipsForAddresseeHandler(IFriendShipRepository friendShipRepository)
        {
            _friendShipRepository = friendShipRepository;
        }

        public async Task<ApplicationHandlerResponse<List<GetAllPendingFriendShipsForAddresseeResponse>>> Handle(GetAllPendingFriendShipsForAddresseeQuery request, CancellationToken cancellationToken)
        {
            var pendingFriendShips = await _friendShipRepository.AllPendingFriendShipForAddresseeAsync(request.UserId, cancellationToken).ConfigureAwait(false);
            if (!pendingFriendShips.Any())
                return ApplicationHandlerResponse<List<GetAllPendingFriendShipsForAddresseeResponse>>.Ok(new List<GetAllPendingFriendShipsForAddresseeResponse>(), FriendShipMessages.FriendShipsPendingNo);
            List<GetAllPendingFriendShipsForAddresseeResponse> getAllPendingFriendShipResponses = pendingFriendShips.Select(x => new GetAllPendingFriendShipsForAddresseeResponse()
            {
                AddresseeId = x.AddresseeId,
                RequesterId = x.RequesterId,
                RequesterUser = x.Requester!,
                AddresseeUser = x.Addressee!
            }).ToList();
            return ApplicationHandlerResponse<List<GetAllPendingFriendShipsForAddresseeResponse>>.Ok(getAllPendingFriendShipResponses, $"Bekleyen {pendingFriendShips.Count} arkadaşlık isteği mevcut");
        }
    }
}
