using MediatR;
using Snowly.Application.Response;
using Snowly.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snowly.Application.Commands.MessageCommands.CreateMessage
{
    public record CreateMessageCommand(
         string Content,
         Guid SenderId,
         Guid ReceiverId
        ) : IRequest<ApplicationHandlerResponse<CreateMessageResponse>>;
}
