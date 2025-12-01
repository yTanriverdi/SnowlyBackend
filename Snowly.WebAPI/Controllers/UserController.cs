using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Snowly.Application.Commands.ConfirmCodeCommands.ConfirmCode;
using Snowly.Application.Commands.RefreshTokenCommands.CheckRefreshToken;
using Snowly.Application.Commands.UserCommands.ChangePassword;
using Snowly.Application.Commands.UserCommands.CreateUser;
using Snowly.Application.Commands.UserCommands.DeleteUser;
using Snowly.Application.Commands.UserCommands.JCM;
using Snowly.Application.Commands.UserCommands.LoginUser;
using Snowly.Application.Commands.UserCommands.UpdateUser;
using Snowly.Application.Queries.UserQueries.GetUserEmail;
using Snowly.Application.Queries.UserQueries.GetUserId;
using Snowly.Application.Response;
using Snowly.WebAPI.APIResponse;
using Snowly.WebAPI.JwtToken;
using Snowly.WebAPI.SignalRControl;

namespace Snowly.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;
        private readonly IHubContext<SnowlyChatHub> _snowlyChatHubContext;
        public UserController(IMediator mediator, IConfiguration configuration, IHubContext<SnowlyChatHub> snowlyChatHubContext)
        {
            _mediator = mediator;
            _configuration = configuration;
            _snowlyChatHubContext = snowlyChatHubContext;
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] CreateUserCommand createUserCommand, CancellationToken cancellationToken)
        {
            ApplicationHandlerResponse<CreateUserResponse> addUserResult = await _mediator.Send(createUserCommand, cancellationToken).ConfigureAwait(false);
            if (!addUserResult.Success) return BadRequest(ApiResponse.FailResponse(addUserResult.Message, 400));
            return Ok(ApiResponse<CreateUserResponse>.SuccessResponse(addUserResult.Data!, addUserResult.Message, 200));
        }

        [HttpPost("AcceptUserCode")]
        public async Task<IActionResult> AcceptUserCode([FromBody] ConfirmCodeCommand confirmCodeCommand, CancellationToken cancellationToken)
        {
            ApplicationHandlerResponse<ConfirmCodeResponse> confirmCodeResult = await _mediator.Send(confirmCodeCommand, cancellationToken).ConfigureAwait(false);
            if (!confirmCodeResult.Success) return BadRequest(ApiResponse.FailResponse(confirmCodeResult.Message, 400));
            return Ok(ApiResponse<ConfirmCodeResponse>.SuccessResponse(confirmCodeResult.Data!, confirmCodeResult.Message, 200));
        }

        [HttpPost("LoginUser")]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserCommand loginUserCommand, CancellationToken cancellationToken)
        {
            ApplicationHandlerResponse<LoginUserResponse> loginUserResult = await _mediator.Send(loginUserCommand, cancellationToken).ConfigureAwait(false);
            if (!loginUserResult.Success) return BadRequest(ApiResponse.FailResponse(loginUserResult.Message, 400));
            string jwtToken = JwtTokenService.GenerateToken(loginUserResult.Data!.UserId.ToString(), loginUserResult.Data.FirstName + " " + loginUserResult.Data.LastName, loginUserResult.Data.Email, loginUserResult.Data.Role, _configuration);
            loginUserResult.Data.JwtToken = jwtToken;
            return Ok(ApiResponse<LoginUserResponse>.SuccessResponse(loginUserResult.Data!, loginUserResult.Message, 200));
        }

        [Authorize("UserOrAdmin")]
        [HttpPost("AddJcmToken")]
        public async Task<IActionResult> AddJcmToken([FromBody] JCMCreateCommand jcmCreateCommand, CancellationToken cancellationToken)
        {
            ApplicationHandlerResponse<JCMCreateResponse> jcmCreateResponse = await _mediator.Send(jcmCreateCommand, cancellationToken).ConfigureAwait(false);
            if (!jcmCreateResponse.Success) return BadRequest(ApiResponse.FailResponse(jcmCreateResponse.Message, 400));
            return Ok(ApiResponse<JCMCreateResponse>.SuccessResponse(jcmCreateResponse.Data!, jcmCreateResponse.Message, 200));
        }

        [Authorize("UserOrAdmin")]
        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand updateUserCommand, CancellationToken cancellationToken)
        {
            ApplicationHandlerResponse<UpdateUserResponse> updateUserResponse = await _mediator.Send(updateUserCommand, cancellationToken).ConfigureAwait(false);
            if (!updateUserResponse.Success) return BadRequest(ApiResponse.FailResponse(updateUserResponse.Message, 400));
            return Ok(ApiResponse<UpdateUserResponse>.SuccessResponse(updateUserResponse.Data!, updateUserResponse.Message, 200));
        }

        [Authorize("UserOrAdmin")]
        [HttpPut("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand changePasswordCommand, CancellationToken cancellationToken)
        {
            ApplicationHandlerResponse<ChangePasswordResponse> changePasswordResponse = await _mediator.Send(changePasswordCommand, cancellationToken).ConfigureAwait(false);
            if (!changePasswordResponse.Success) return BadRequest(ApiResponse.FailResponse(changePasswordResponse.Message, 400));
            return Ok(ApiResponse<ChangePasswordResponse>.SuccessResponse(changePasswordResponse.Data!, changePasswordResponse.Message, 200));
        }

        [Authorize("UserOrAdmin")]
        [HttpPut("DeleteUser")]
        public async Task<IActionResult> DeleteUser([FromBody] DeleteUserCommand deleteUserCommand, CancellationToken cancellationToken)
        {
            ApplicationHandlerResponse<DeleteUserResponse> deleteUserResponse = await _mediator.Send(deleteUserCommand, cancellationToken).ConfigureAwait(false);
            if (!deleteUserResponse.Success) return BadRequest(ApiResponse.FailResponse(deleteUserResponse.Message, 400));
            return Ok(ApiResponse<DeleteUserResponse>.SuccessResponse(deleteUserResponse.Data!, deleteUserResponse.Message, 200));
        }

        [Authorize("UserOrAdmin")]
        [HttpGet("GetUserByEmail")]
        public async Task<IActionResult> GetUserByEmail([FromQuery] GetUserEmailQuery getUserEmailQuery, CancellationToken cancellationToken)
        {
            ApplicationHandlerResponse<GetUserEmailResponse> getUserEmailResponse = await _mediator.Send(getUserEmailQuery, cancellationToken).ConfigureAwait(false);
            if (!getUserEmailResponse.Success) return BadRequest(ApiResponse.FailResponse(getUserEmailResponse.Message, 400));
            return Ok(ApiResponse<GetUserEmailResponse>.SuccessResponse(getUserEmailResponse.Data!, getUserEmailResponse.Message, 200));
        }
        
        [Authorize("UserOrAdmin")]
        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUserById([FromQuery] GetUserIdQuery getUserIdQuery, CancellationToken cancellationToken)
        {
            ApplicationHandlerResponse<GetUserIdResponse> getUserIdResponse = await _mediator.Send(getUserIdQuery, cancellationToken).ConfigureAwait(false);
            if (!getUserIdResponse.Success) return BadRequest(ApiResponse.FailResponse(getUserIdResponse.Message, 400));
            return Ok(ApiResponse<GetUserIdResponse>.SuccessResponse(getUserIdResponse.Data!, getUserIdResponse.Message, 200));
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] CheckRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            ApplicationHandlerResponse<CheckRefreshTokenResponse> result = await _mediator.Send(request, cancellationToken).ConfigureAwait(false);
            if (!result.Success)
                return BadRequest(ApiResponse.FailResponse(result.Message, 400));
            var response = new CheckRefreshTokenResponse
            {
                RefreshToken = result.Data!.RefreshToken,
            };
            return Ok(ApiResponse<CheckRefreshTokenResponse>.SuccessResponse(response, result.Message, 200));
        }


    }
}
