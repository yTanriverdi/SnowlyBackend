using Snowly.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snowly.Application.Commands.UserCommands.CreateUser
{
    public sealed class CreateUserResponse
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public UserStatus Status { get; set; }
        public bool EmailConfirmed { get; set; }
    }
}
