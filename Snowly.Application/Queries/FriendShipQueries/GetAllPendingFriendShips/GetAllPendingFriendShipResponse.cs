using Snowly.Domain.Entities;

namespace Snowly.Application.Queries.FriendShipQueries.GetAllPendingFriendShips
{
    public sealed class GetAllPendingFriendShipResponse
    {
        public Guid Id { get; set; }
        public Guid AddresseeId { get; set; }
        public Guid RequesterId { get; set; }
        public User AddresseeUser { get; set; } = null!;
        public User RequesterUser { get; set; } = null!;
    }
}
