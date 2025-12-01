using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snowly.Application.Queries.MessageQueries.GetMessagesBetweenUser
{
    public sealed class GetMessagesBetweenUserResponse
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = default!;
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
        public bool IsRead { get; set; }
        public DateTime? ReadAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
