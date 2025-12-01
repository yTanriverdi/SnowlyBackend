using MediatR;
using Snowly.Application.Interfaces;
using Snowly.Application.Response;
using Snowly.Application.ResponseMessages;
using Snowly.Domain.Entities;

namespace Snowly.Application.Commands.FriendShipCommands.DeleteFriendShip
{
    public class DeleteFriendShipHandler : IRequestHandler<DeleteFriendShipCommand, ApplicationHandlerResponse<DeleteFriendShipResponse>>
    {
        private readonly IFriendShipRepository _friendShipRepository;

        public DeleteFriendShipHandler(IFriendShipRepository friendShipRepository)
        {
            _friendShipRepository = friendShipRepository;
        }

        public async Task<ApplicationHandlerResponse<DeleteFriendShipResponse>> Handle(DeleteFriendShipCommand request, CancellationToken cancellationToken)
        {
            bool anyFriendShip = await _friendShipRepository.AnyFriendShipAsync(request.RequesterId, request.AddresseeId, cancellationToken).ConfigureAwait(false);
            if (!anyFriendShip) return ApplicationHandlerResponse<DeleteFriendShipResponse>.Fail(FriendShipMessages.FriendShipsAny);
            FriendShip? friendShip = await _friendShipRepository.GetFriendShipAsync(request.RequesterId, request.AddresseeId, cancellationToken).ConfigureAwait(false);
            bool friendShipDeleteResult = await _friendShipRepository.DeleteFriendShipAsync(friendShip!.Id, cancellationToken).ConfigureAwait(false);
            if (!friendShipDeleteResult) return ApplicationHandlerResponse<DeleteFriendShipResponse>.Fail(FriendShipMessages.FriendShipDeleteFail);
            DeleteFriendShipResponse deleteRes = new DeleteFriendShipResponse()
            {
                Success = true,
            };
            return ApplicationHandlerResponse<DeleteFriendShipResponse>.Ok(deleteRes, FriendShipMessages.FriendShipDeleteSuccess);
        }
    }
}
