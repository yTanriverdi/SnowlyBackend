namespace Snowly.Application.Commands.FriendShipCommands.CreateFriendShip
{
    public sealed class CreateFriendShipResponse
    {
        public bool Success { get; set; }
        public Guid RequesterId { get; set; }
        public Guid AddresseeId { get; set; }
    }
}
