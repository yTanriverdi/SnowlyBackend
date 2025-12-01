using MediatR;
using Snowly.Application.Response;

namespace Snowly.Application.Commands.FriendShipCommands.AcceptFriendShip
{
    public record AcceptFriendShipCommand(Guid FriendShipId) : IRequest<ApplicationHandlerResponse<AcceptFriendShipResponse>>;
}
