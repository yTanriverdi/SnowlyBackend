namespace Snowly.Application.Commands.FriendShipCommands.AcceptFriendShip
{
    public sealed class AcceptFriendShipResponse
    {
        public bool Success { get; set; }
        public Guid RequesterId { get; set; }
        public Guid AddresseeId { get; set; }
    }
}
