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
            var pendingFriendShips = await _friendShipRepository.AllPendingFriendShipForAddresseeAsync(request.AddresseeId, cancellationToken).ConfigureAwait(false);
            if (!pendingFriendShips.Any())
                return ApplicationHandlerResponse<List<GetAllPendingFriendShipsForAddresseeResponse>>.Ok(new List<GetAllPendingFriendShipsForAddresseeResponse>(), FriendShipMessages.FriendShipsPendingNo);
            List<GetAllPendingFriendShipsForAddresseeResponse> getAllPendingFriendShipResponses = pendingFriendShips.Select(f =>
            {
                var requester = f.Requester!;

                return new GetAllPendingFriendShipsForAddresseeResponse
                {
                    FriendShipId = f.Id,
                    FriendId = requester.Id,
                    FullName = $"{requester.FirstName} {requester.LastName}",
                    Email = requester.Email,
                    IsOnline = requester.IsOnline
                };
            }).ToList();
            return ApplicationHandlerResponse<List<GetAllPendingFriendShipsForAddresseeResponse>>.Ok(getAllPendingFriendShipResponses, $"Bekleyen {pendingFriendShips.Count} arkadaşlık isteği mevcut");
        }
    }
}
