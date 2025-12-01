using Snowly.Domain.BaseEntities;
using Snowly.Domain.Enums;

namespace Snowly.Domain.Entities
{
    public class FriendShip : BaseEntity
    {
        public FriendShip()
        {
            Status = FriendShipStatus.Pending;
        }
        public required Guid RequesterId { get; set; }
        public User? Requester { get; set; }
        public required Guid AddresseeId { get; set; }
        public User? Addressee { get; set; }
        public required FriendShipStatus Status { get; set; } 
    }
}
