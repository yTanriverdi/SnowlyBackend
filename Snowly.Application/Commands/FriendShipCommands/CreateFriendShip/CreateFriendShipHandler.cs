using MediatR;
using Snowly.Application.Commands.UserCommands.UserInterfaces;
using Snowly.Application.Interfaces;
using Snowly.Application.Response;
using Snowly.Application.ResponseMessages;
using Snowly.Domain.Entities;
using Snowly.Domain.Enums;

namespace Snowly.Application.Commands.FriendShipCommands.CreateFriendShip
{
    public class CreateFriendShipHandler : IRequestHandler<CreateFriendShipCommand, ApplicationHandlerResponse<CreateFriendShipResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IFriendShipRepository _friendShipRepository;

        public CreateFriendShipHandler(IUserRepository userRepository, IFriendShipRepository friendShipRepository)
        {
            _userRepository = userRepository;
            _friendShipRepository = friendShipRepository;
        }

        public async Task<ApplicationHandlerResponse<CreateFriendShipResponse>> Handle(CreateFriendShipCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userRepository.GetUserByIdAsync(request.AddresseeId, cancellationToken).ConfigureAwait(false);
            if (user == null) return ApplicationHandlerResponse<CreateFriendShipResponse>.Fail(UserMessages.UserNotFound);
            bool anyFrindShip = await _friendShipRepository.AnyFriendShipAsync(request.RequesterId, request.AddresseeId, cancellationToken).ConfigureAwait(false);
            if (anyFrindShip) return ApplicationHandlerResponse<CreateFriendShipResponse>.Fail(FriendShipMessages.FriendShipBeforeCreated);
            FriendShip friendShip = new FriendShip()
            {
                Status = FriendShipStatus.Pending,
                RequesterId = request.RequesterId,
                AddresseeId = request.AddresseeId,
            };
            FriendShip addedFriendShip = await _friendShipRepository.AddFriendShipAsync(friendShip, cancellationToken).ConfigureAwait(false);
            CreateFriendShipResponse response = new CreateFriendShipResponse()
            {
                Success = true,
                RequesterId = friendShip.RequesterId,
                AddresseeId = friendShip.AddresseeId,

            };
            return ApplicationHandlerResponse<CreateFriendShipResponse>.Ok(response, FriendShipMessages.FriendShipSuccess);
        }
    }
}
