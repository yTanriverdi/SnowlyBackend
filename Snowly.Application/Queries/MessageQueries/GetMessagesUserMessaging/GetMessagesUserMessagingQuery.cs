using MediatR;
using Snowly.Application.Response;

namespace Snowly.Application.Queries.MessageQueries.GetMessagesUserMessaging
{
    public record GetMessagesUserMessagingQuery(Guid UserId) : IRequest<ApplicationHandlerResponse<List<GetMessagesUserMessagingResponse>>>;
}
