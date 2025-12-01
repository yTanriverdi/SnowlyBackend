using MediatR;
using Snowly.Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snowly.Application.Commands.UserCommands.CreateUser
{
    public record CreateUserCommand(
        string FirstName,
        string LastName,
        string Email,
        string Password
        ) : IRequest<ApplicationHandlerResponse<CreateUserResponse>>;
}
