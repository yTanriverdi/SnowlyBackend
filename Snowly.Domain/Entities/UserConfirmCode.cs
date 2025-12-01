using Snowly.Domain.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snowly.Domain.Entities
{
    public class UserConfirmCode : BaseEntity
    {
        public UserConfirmCode()
        {
            Random rnd = new Random();
            Code = rnd.Next(111111,999999).ToString("D6");
        }

        public UserConfirmCode(string code)
        {
            Code = code;
        }
        public string Email { get; set; } = null!;
        public string Code { get; set; }
    }
}
