using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snowly.Domain.Entities
{
    public class UserGroup
    {
        public UserGroup()
        {
            JoinedAt = DateTime.UtcNow;
        }
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        public Guid GroupId { get; set; }
        public Group Group { get; set; } = null!;
        public DateTime JoinedAt { get; set; }
    }
}
