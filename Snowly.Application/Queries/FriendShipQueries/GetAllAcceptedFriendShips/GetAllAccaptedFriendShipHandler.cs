using MediatR;
using Snowly.Application.Interfaces;
using Snowly.Application.Response;
using Snowly.Application.ResponseMessages;

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
            List<GetAllAcceptedFriendShipResponse> getAllAcceptedFriendShipResponses = acceptedFriendShips.Select(x => new GetAllAcceptedFriendShipResponse()
            {
                AddresseeId = x.AddresseeId,
                RequesterId = x.RequesterId,
                RequesterUser = x.Requester!,
                AddresseeUser = x.Addressee!
            }).ToList();
            return ApplicationHandlerResponse<List<GetAllAcceptedFriendShipResponse>>.Ok(getAllAcceptedFriendShipResponses, $"Toplam {acceptedFriendShips.Count} arkadaşınız var");
        }
    }
}
