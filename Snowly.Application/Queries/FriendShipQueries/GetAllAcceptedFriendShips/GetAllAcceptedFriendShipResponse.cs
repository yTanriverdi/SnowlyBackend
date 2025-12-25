using Snowly.Domain.Entities;

namespace Snowly.Application.Queries.FriendShipQueries.GetAllAcceptedFriendShips
{
    public sealed class GetAllAcceptedFriendShipResponse
    {
        public Guid FriendShipId { get; set; }
        public Guid FriendId { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool IsOnline { get; set; }
    }
}
