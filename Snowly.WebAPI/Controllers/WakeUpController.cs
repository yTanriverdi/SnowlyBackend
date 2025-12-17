using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Snowly.Application.Commands.FriendShipCommands.AcceptFriendShip;
using Snowly.WebAPI.APIResponse;

namespace Snowly.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WakeUpController : ControllerBase
    {
        [HttpGet("Wake")]
        public IActionResult Wake()
        {
            return Ok(ApiResponse<bool>.SuccessResponse(true, "Kullanıma hazır", 200));
        }
    }
}
