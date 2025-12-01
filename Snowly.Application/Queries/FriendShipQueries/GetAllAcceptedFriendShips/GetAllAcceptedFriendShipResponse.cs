using Snowly.Domain.Entities;

namespace Snowly.Application.Queries.FriendShipQueries.GetAllAcceptedFriendShips
{
    public sealed class GetAllAcceptedFriendShipResponse
    {
        public Guid Id { get; set; }
        public Guid AddresseeId { get; set; }
        public Guid RequesterId { get; set; }
        public User AddresseeUser { get; set; } = null!;
        public User RequesterUser { get; set; } = null!;
    }
}
