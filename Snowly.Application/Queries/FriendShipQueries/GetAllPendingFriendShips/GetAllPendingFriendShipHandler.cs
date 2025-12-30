using MediatR;
using Snowly.Application.Interfaces;
using Snowly.Application.Queries.FriendShipQueries.GetAllAcceptedFriendShips;
using Snowly.Application.Response;
using Snowly.Application.ResponseMessages;

namespace Snowly.Application.Queries.FriendShipQueries.GetAllPendingFriendShips
{
    public class GetAllPendingFriendShipHandler : IRequestHandler<GetAllPendingFriendShipQuery, ApplicationHandlerResponse<List<GetAllPendingFriendShipResponse>>>
    {
        private readonly IFriendShipRepository _friendShipRepository;

        public GetAllPendingFriendShipHandler(IFriendShipRepository friendShipRepository)
        {
            _friendShipRepository = friendShipRepository;
        }

        public async Task<ApplicationHandlerResponse<List<GetAllPendingFriendShipResponse>>> Handle(GetAllPendingFriendShipQuery request, CancellationToken cancellationToken)
        {
            var pendingFriendShips = await _friendShipRepository.AllPendingFriendShipAsync(request.RequesterId, cancellationToken).ConfigureAwait(false);
            if (!pendingFriendShips.Any())
                return ApplicationHandlerResponse<List<GetAllPendingFriendShipResponse>>.Ok(new List<GetAllPendingFriendShipResponse>(), FriendShipMessages.FriendShipsPendingNo);
            List<GetAllPendingFriendShipResponse> getAllPendingFriendShipResponses = pendingFriendShips.Select(f =>
            {
                var addressee = f.Addressee!;

                return new GetAllPendingFriendShipResponse
                {
                    FriendShipId = f.Id,
                    FriendId = addressee.Id,
                    FullName = $"{addressee.FirstName} {addressee.LastName}",
                    Email = addressee.Email,
                    IsOnline = addressee.IsOnline
                };
            }).ToList();
            return ApplicationHandlerResponse<List<GetAllPendingFriendShipResponse>>.Ok(getAllPendingFriendShipResponses, $"Bekleyen {pendingFriendShips.Count} arkadaşlık isteği mevcut");
        }
    }
}
