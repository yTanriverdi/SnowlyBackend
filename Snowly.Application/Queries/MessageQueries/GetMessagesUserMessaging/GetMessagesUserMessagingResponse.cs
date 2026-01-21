using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snowly.Application.Queries.MessageQueries.GetMessagesUserMessaging
{
    public sealed class GetMessagesUserMessagingResponse
    {
        public Guid UserId { get; set; }
        public bool IsLastMessageFromMe { get; set; }
        public string LastMessageContent { get; set; } = string.Empty;
        public DateTime LastMessageTime { get; set; }
        public int UnreadMessageCount { get; set; }
        public string FullName { get; set; } = default!;
    }
}
