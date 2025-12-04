using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace Snowly.WebAPI.UserIdentifier
{
    public class UserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            // JWT'den user ID -> ClaimTypes.NameIdentifier = "nameid"
            return connection.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
