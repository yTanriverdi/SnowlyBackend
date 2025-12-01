using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snowly.Domain.Entities
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; } = default!;
        public Guid UserId { get; set; }
        public DateTime Expires { get; set; }
        public bool IsRevoked { get; set; } = false;

        public bool IsActive => !IsRevoked && DateTime.UtcNow < Expires;
    }
}
