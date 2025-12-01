using MediatR;
using Snowly.Application.Interfaces;
using Snowly.Application.Response;
using Snowly.Application.ResponseMessages;
using Snowly.Domain.Entities;

namespace Snowly.Application.Commands.FriendShipCommands.AcceptFriendShip
{
    public class AcceptFriendShipHandler : IRequestHandler<AcceptFriendShipCommand, ApplicationHandlerResponse<AcceptFriendShipResponse>>
    {
        private readonly IFriendShipRepository _friendShipRepository;

        public AcceptFriendShipHandler(IFriendShipRepository friendShipRepository)
        {
            _friendShipRepository = friendShipRepository;
        }

        public async Task<ApplicationHandlerResponse<AcceptFriendShipResponse>> Handle(AcceptFriendShipCommand request, CancellationToken cancellationToken)
        {
            bool acceptFriendShipRes = await _friendShipRepository.AcceptFriendShipAsync(request.FriendShipId, cancellationToken).ConfigureAwait(false);
            if (!acceptFriendShipRes) return ApplicationHandlerResponse<AcceptFriendShipResponse>.Fail(FriendShipMessages.FriendShipAcceptFail);
            FriendShip? friendShip = await _friendShipRepository.GetFriendShipByIdAsync(request.FriendShipId, cancellationToken).ConfigureAwait(false);
            AcceptFriendShipResponse acceptFriendShipResponse = new AcceptFriendShipResponse() 
            {
                AddresseeId = friendShip!.AddresseeId,
                RequesterId = friendShip!.RequesterId,
                Success = true
            };
            return ApplicationHandlerResponse<AcceptFriendShipResponse>.Ok(acceptFriendShipResponse, FriendShipMessages.FriendShipAcceptSuccess);
        }
    }
}
