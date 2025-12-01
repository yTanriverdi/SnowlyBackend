using MediatR;
using Snowly.Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snowly.Application.Commands.ConfirmCodeCommands.RefreshCode
{
    public record RefreshCodeCommand(string Email) : IRequest<ApplicationHandlerResponse<RefreshCodeResponse>>;
}
