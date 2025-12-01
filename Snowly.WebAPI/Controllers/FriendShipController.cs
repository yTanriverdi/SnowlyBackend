using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Snowly.Application.Commands.FriendShipCommands.AcceptFriendShip;
using Snowly.Application.Commands.FriendShipCommands.CreateFriendShip;
using Snowly.Application.Commands.FriendShipCommands.DeleteFriendShip;
using Snowly.Application.Queries.FriendShipQueries.GetAllAcceptedFriendShips;
using Snowly.Application.Queries.FriendShipQueries.GetAllPendingFriendShips;
using Snowly.Application.Queries.FriendShipQueries.GetAllPendingFriendShipsForAddressee;
using Snowly.Application.Response;
using Snowly.WebAPI.APIResponse;
using Snowly.WebAPI.SignalRControl;

namespace Snowly.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendShipController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHubContext<SnowlyChatHub> _snowlyChatHubContext;

        public FriendShipController(IMediator mediator, IHubContext<SnowlyChatHub> snowlyChatHubContext)
        {
            _mediator = mediator;
            _snowlyChatHubContext = snowlyChatHubContext;
        }

        [Authorize("UserOrAdmin")]
        [HttpPost("AcceptFriendShip")]
        public async Task<IActionResult> AcceptFriendShip(AcceptFriendShipCommand acceptFriendShipCommand, CancellationToken cancellationToken)
        {
            ApplicationHandlerResponse<AcceptFriendShipResponse> acceptFriendShipResponse = await _mediator.Send(acceptFriendShipCommand, cancellationToken).ConfigureAwait(false);
            if (!acceptFriendShipResponse.Success) return BadRequest(ApiResponse.FailResponse(acceptFriendShipResponse.Message, 400));
            await _snowlyChatHubContext.Clients.Group(
            acceptFriendShipResponse.Data!.RequesterId.ToString()).SendAsync("FriendRequestAccepted", new
             {
                 Success = true,
                 RequesterId = acceptFriendShipResponse.Data.RequesterId.ToString(),
                 AddresseeId = acceptFriendShipResponse.Data.AddresseeId.ToString(),
             }, cancellationToken);

            return Ok(ApiResponse<AcceptFriendShipResponse>.SuccessResponse(acceptFriendShipResponse.Data!, acceptFriendShipResponse.Message, 200));
        }

        [Authorize("UserOrAdmin")]
        [HttpPost("AddFriendShip")]
        public async Task<IActionResult> AddFriendShip(CreateFriendShipCommand createFriendShipCommand, CancellationToken cancellationToken)
        {
            ApplicationHandlerResponse<CreateFriendShipResponse> createFriendShipResponse = await _mediator.Send(createFriendShipCommand, cancellationToken).ConfigureAwait(false);
            if (!createFriendShipResponse.Success) return BadRequest(ApiResponse.FailResponse(createFriendShipResponse.Message, 400));
            await _snowlyChatHubContext.Clients.Group(
            createFriendShipResponse.Data!.RequesterId.ToString()).SendAsync("FriendRequestCreated", new
            {
                Success = true,
                RequesterId = createFriendShipResponse.Data.RequesterId.ToString(),
                AddresseeId = createFriendShipResponse.Data.AddresseeId.ToString(),
            }, cancellationToken);

            return Ok(ApiResponse<CreateFriendShipResponse>.SuccessResponse(createFriendShipResponse.Data!, createFriendShipResponse.Message, 200));
        }


        [Authorize("UserOrAdmin")]
        [HttpPost("DeleteFriendShip")]
        public async Task<IActionResult> DeleteFriendShip(DeleteFriendShipCommand deleteFriendShipCommand, CancellationToken cancellationToken)
        {
            ApplicationHandlerResponse<DeleteFriendShipResponse> deleteFriendShipResponse = await _mediator.Send(deleteFriendShipCommand, cancellationToken).ConfigureAwait(false);
            if (!deleteFriendShipResponse.Success) return BadRequest(ApiResponse.FailResponse(deleteFriendShipResponse.Message, 400));
            return Ok(ApiResponse<DeleteFriendShipResponse>.SuccessResponse(deleteFriendShipResponse.Data!, deleteFriendShipResponse.Message, 200));
        }


        [Authorize("UserOrAdmin")]
        [HttpGet("GetAllAcceptedFriendShip")]
        public async Task<IActionResult> GetAllAcceptedFriendShip(GetAllAcceptedFriendShipQuery getAllAcceptedFriendShipQuery, CancellationToken cancellationToken)
        {
            ApplicationHandlerResponse<List<GetAllAcceptedFriendShipResponse>> acceptedFriendShipsResponse = await _mediator.Send(getAllAcceptedFriendShipQuery, cancellationToken).ConfigureAwait(false);
            return Ok(ApiResponse<List<GetAllAcceptedFriendShipResponse>>.SuccessResponse(acceptedFriendShipsResponse.Data, acceptedFriendShipsResponse.Message, 200));
        }
        
        
        [Authorize("UserOrAdmin")]
        [HttpGet("GetAllPendingFriendShipForRequester")]
        public async Task<IActionResult> GetAllPendingFriendShipForRequester(GetAllPendingFriendShipQuery getAllPendingFriendShipQuery, CancellationToken cancellationToken)
        {
            ApplicationHandlerResponse<List<GetAllPendingFriendShipResponse>> pendingFriendShipsResponse = await _mediator.Send(getAllPendingFriendShipQuery, cancellationToken).ConfigureAwait(false);
            return Ok(ApiResponse<List<GetAllPendingFriendShipResponse>>.SuccessResponse(pendingFriendShipsResponse.Data, pendingFriendShipsResponse.Message, 200));
        }

        [Authorize("UserOrAdmin")]
        [HttpGet("GetAllPendingFriendShipForAddressee")]
        public async Task<IActionResult> GetAllPendingFriendShipForAddressee(GetAllPendingFriendShipsForAddresseeQuery getAllPendingFriendShipsForAddresseeQuery, CancellationToken cancellationToken)
        {
            ApplicationHandlerResponse<List<GetAllPendingFriendShipsForAddresseeResponse>> pendingFriendShipsResponse = await _mediator.Send(getAllPendingFriendShipsForAddresseeQuery, cancellationToken).ConfigureAwait(false);
            return Ok(ApiResponse<List<GetAllPendingFriendShipsForAddresseeResponse>>.SuccessResponse(pendingFriendShipsResponse.Data, pendingFriendShipsResponse.Message, 200));
        }
    }
}
