using MediatR;
using Snowly.Application.Response;

namespace Snowly.Application.Queries.FriendShipQueries.GetAllPendingFriendShipsForAddressee
{
    public record GetAllPendingFriendShipsForAddresseeQuery(Guid UserId) : IRequest<ApplicationHandlerResponse<List<GetAllPendingFriendShipsForAddresseeResponse>>>;
}
