using MediatR;
using Snowly.Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snowly.Application.Queries.UserQueries.GetUserId
{
    public record GetUserIdQuery(Guid UserId) : IRequest<ApplicationHandlerResponse<GetUserIdResponse>>;
}
