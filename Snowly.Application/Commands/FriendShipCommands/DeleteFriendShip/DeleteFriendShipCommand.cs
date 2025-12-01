using MediatR;
using Snowly.Application.Response;

namespace Snowly.Application.Commands.FriendShipCommands.DeleteFriendShip
{
    public record DeleteFriendShipCommand(Guid RequesterId, Guid AddresseeId) : IRequest<ApplicationHandlerResponse<DeleteFriendShipResponse>>;
}
