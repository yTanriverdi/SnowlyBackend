using MediatR;
using Snowly.Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snowly.Application.Queries.MessageQueries.GetMessagesBetweenUser
{
    public record GetMessagesBetweenUserQuery(
        Guid SenderId,
        Guid ReceiverId,
        int MessageSize = 100,
        int MessageStack = 1) : IRequest<ApplicationHandlerResponse<List<GetMessagesBetweenUserResponse>>>;
}
