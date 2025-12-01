using MediatR;
using Snowly.Application.Response;

namespace Snowly.Application.Queries.UserQueries.GetUserEmail
{
    public record GetUserEmailQuery(string Email) : IRequest<ApplicationHandlerResponse<GetUserEmailResponse>>;
}
