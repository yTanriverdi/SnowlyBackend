using MediatR;
using Snowly.Application.Response;

namespace Snowly.Application.Queries.FriendShipQueries.GetAllAcceptedFriendShips
{
    public record GetAllAcceptedFriendShipQuery(Guid RequesterId) : IRequest<ApplicationHandlerResponse<List<GetAllAcceptedFriendShipResponse>>>;
}
