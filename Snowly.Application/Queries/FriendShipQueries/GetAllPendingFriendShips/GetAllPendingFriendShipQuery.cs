using MediatR;
using Snowly.Application.Response;

namespace Snowly.Application.Queries.FriendShipQueries.GetAllPendingFriendShips
{
    public record GetAllPendingFriendShipQuery(Guid RequesterId) : IRequest<ApplicationHandlerResponse<List<GetAllPendingFriendShipResponse>>>;
}
