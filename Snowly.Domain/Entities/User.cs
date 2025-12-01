using Snowly.Domain.BaseEntities;
using Snowly.Domain.Enums;

namespace Snowly.Domain.Entities
{
    public class User : BaseEntity
    {
        public User()
        {
            Messages = new List<Message>();
            RefreshTokens = new List<RefreshToken>();
            FriendshipsSent = new List<FriendShip>();
            FriendshipsReceived = new List<FriendShip>();
            UserGroups = new List<UserGroup>();
            ConfirmCodes = new List<UserConfirmCode>();
            Status = UserStatus.Active;
            EmailConfirmed = false;
            IsOnline = false;
            Role = "User";
            JCMToken = "new";
        }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public UserStatus Status { get; set; } 
        public bool EmailConfirmed { get; set; }
        public bool IsOnline { get; set; }
        public string Role { get; set; }
        public string JCMToken { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; }
        public ICollection<FriendShip> FriendshipsSent { get; set; }
        public ICollection<FriendShip> FriendshipsReceived { get; set; }
        public ICollection<UserGroup> UserGroups { get; set; }
        public ICollection<UserConfirmCode> ConfirmCodes { get; set; }
    }
}
