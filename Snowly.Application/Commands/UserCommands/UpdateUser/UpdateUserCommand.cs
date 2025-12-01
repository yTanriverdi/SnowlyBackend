using MediatR;
using Snowly.Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snowly.Application.Commands.UserCommands.UpdateUser
{
    public record UpdateUserCommand(string Email, string FirstName, string LastName) : IRequest<ApplicationHandlerResponse<UpdateUserResponse>>;
}
