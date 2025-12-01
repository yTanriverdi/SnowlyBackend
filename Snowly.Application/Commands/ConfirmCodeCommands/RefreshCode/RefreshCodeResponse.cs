using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snowly.Application.Commands.ConfirmCodeCommands.RefreshCode
{
    public sealed class RefreshCodeResponse
    {
        public string Code { get; set; } = null!;
    }
}
