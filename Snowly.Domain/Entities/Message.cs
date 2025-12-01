using Snowly.Domain.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snowly.Domain.Entities
{
    public class Message : BaseEntity
    {
        public required string Content { get; set; }
        public required Guid SenderId { get; set; }
        public required Guid ReceiverId { get; set; }

        public required User SenderUser { get; set; }
        public required User ReceiverUser { get; set; }
        public bool IsDeleted { get; set; } = false;

        public bool IsRead { get; set; }
        public DateTime? ReadAt { get; set; }
    }
}
