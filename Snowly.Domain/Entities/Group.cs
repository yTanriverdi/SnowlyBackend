using Snowly.Domain.BaseEntities;

namespace Snowly.Domain.Entities
{
    public class Group : BaseEntity
    {
        public Group()
        {
            Members = new List<UserGroup>();
            Messages = new List<Message>();
        }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public ICollection<UserGroup> Members { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}
