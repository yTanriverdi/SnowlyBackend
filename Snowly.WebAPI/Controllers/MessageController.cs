using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Snowly.Application.Commands.MessageCommands.CreateMessage;
using Snowly.Application.Commands.MessageCommands.DeleteMessage;
using Snowly.Application.Commands.MessageCommands.MarkReadMessage;
using Snowly.Application.Queries.MessageQueries.GetMessagesBetweenUser;
using Snowly.Application.Queries.MessageQueries.GetMessagesUserMessaging;
using Snowly.Application.Response;
using Snowly.WebAPI.APIResponse;
using Snowly.WebAPI.SignalRControl;

namespace Snowly.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHubContext<SnowlyChatHub> _snowlyChatHubContext;

        public MessageController(IMediator mediator, IHubContext<SnowlyChatHub> snowlyChatHubContext)
        {
            _mediator = mediator;
            _snowlyChatHubContext = snowlyChatHubContext;
        }

        [Authorize("UserOrAdmin")]
        [HttpGet("GetBetweenUserMessages")]
        public async Task<IActionResult> GetBetweenUserMessages([FromQuery] GetMessagesBetweenUserQuery getMessagesBetweenUserQuery, CancellationToken cancellationToken)
        {
            ApplicationHandlerResponse<List<GetMessagesBetweenUserResponse>> messages = await _mediator.Send(getMessagesBetweenUserQuery, cancellationToken).ConfigureAwait(false);
            if (!messages.Success) return NotFound(ApiResponse.FailResponse(messages.Message, 404));
            return Ok(ApiResponse<List<GetMessagesBetweenUserResponse>>.SuccessResponse(messages.Data!, messages.Message, 200));
        }


        [Authorize("UserOrAdmin")]
        [HttpPost("AddMessage")]
        public async Task<IActionResult> AddMessage([FromBody] CreateMessageCommand createMessageCommand, CancellationToken cancellationToken)
        {
            ApplicationHandlerResponse<CreateMessageResponse> addedMessage = await _mediator.Send(createMessageCommand, cancellationToken).ConfigureAwait(false);
            if (!addedMessage.Success) return BadRequest(ApiResponse.FailResponse(addedMessage.Message, 400));
            await _snowlyChatHubContext.Clients.User(createMessageCommand.SenderId.ToString()).SendAsync("ReceiveMessage", new { MessageId = addedMessage.Data!.Id, From = createMessageCommand.SenderId, Text = createMessageCommand.Content });
            return Ok(ApiResponse<CreateMessageResponse>.SuccessResponse(addedMessage.Data!, addedMessage.Message, 200));
        }

        [Authorize("UserOrAdmin")]
        [HttpPost("DeleteMessage")]
        public async Task<IActionResult> DeleteMessage([FromBody] DeleteMessageCommand deleteMessageCommand, CancellationToken cancellationToken)
        {
            ApplicationHandlerResponse<DeleteMessageResponse> deletedMessage = await _mediator.Send(deleteMessageCommand, cancellationToken).ConfigureAwait(false);
            if (!deletedMessage.Success)
                return BadRequest(ApiResponse.FailResponse(deletedMessage.Message, 400));
            await _snowlyChatHubContext.Clients.User(deletedMessage.Data!.Message.SenderId.ToString())
                .SendAsync("MessageDeleted", new
                {
                    MessageId = deletedMessage.Data.Message.Id,
                    From = deletedMessage.Data!.Message.SenderId.ToString()}
                    ,cancellationToken);
            return Ok(ApiResponse<DeleteMessageResponse>.SuccessResponse(deletedMessage.Data!, deletedMessage.Message, 200));
        }


        [Authorize("UserOrAdmin")]
        [HttpPost("MarkAsRead")]
        public async Task<IActionResult> MarkAsRead([FromBody] MarkReadMessageCommand markReadMessageCommand, CancellationToken cancellationToken)
        {
            ApplicationHandlerResponse<MarkReadMessageResponse> markReadResponse = await _mediator.Send(markReadMessageCommand, cancellationToken).ConfigureAwait(false);
            if (!markReadResponse.Success) return BadRequest(ApiResponse.FailResponse(markReadResponse.Message, 400));
            return Ok(ApiResponse<MarkReadMessageResponse>.SuccessResponse(markReadResponse.Data!, markReadResponse.Message, 200));
        }


        [Authorize("UserOrAdmin")]
        [HttpGet("GetAllChats")]
        public async Task<IActionResult> GetAllChats([FromQuery] GetMessagesUserMessagingQuery getMessagesUserMessagingQuery, CancellationToken cancellationToken)
        {
            ApplicationHandlerResponse<List<GetMessagesUserMessagingResponse>> chats = await _mediator.Send(getMessagesUserMessagingQuery, cancellationToken).ConfigureAwait(false);
            if (!chats.Success) return BadRequest(ApiResponse.FailResponse(chats.Message, 400));
            return Ok(ApiResponse<List<GetMessagesUserMessagingResponse>>.SuccessResponse(chats.Data!, chats.Message, 200));
        }

    }
}
