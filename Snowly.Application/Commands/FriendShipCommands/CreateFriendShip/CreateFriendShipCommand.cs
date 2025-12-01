using MediatR;
using Snowly.Application.Response;

namespace Snowly.Application.Commands.FriendShipCommands.CreateFriendShip
{
    public record CreateFriendShipCommand(Guid RequesterId, Guid AddresseeId) : IRequest<ApplicationHandlerResponse<CreateFriendShipResponse>>;
}
