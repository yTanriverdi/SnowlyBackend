using MediatR;
using Snowly.Application.Response;

namespace Snowly.Application.Queries.FriendShipQueries.GetAllPendingFriendShipsForAddressee
{
    public record GetAllPendingFriendShipsForAddresseeQuery(Guid AddresseeId) : IRequest<ApplicationHandlerResponse<List<GetAllPendingFriendShipsForAddresseeResponse>>>;
}
