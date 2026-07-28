using Application.DTOs.Auth;
using Application.Features.Auth.ChangePassword;
using Application.Features.Auth.CompleteFirstLogin;
using Application.Features.Auth.Login;
using Application.Features.Auth.Logout;
using Application.Features.Auth.RefreshTokenCommand;
using Application.Features.Auth.Register;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("change-password")]
        [Authorize]
        [ProducesResponseType(typeof(AuthChangeStateResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<AuthChangeStateResponse> ChangePassword([FromBody] ChangePasswordCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPost("complete-first-login")]
        [Authorize]
        [ProducesResponseType(typeof(CompleteFirstLoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<CompleteFirstLoginResponse> CompleteFirstLogin(
            [FromBody] CompleteFirstLoginCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<LoginResponse> Login([FromBody] LoginCommand command)
        {
            return await _mediator.Send(command); 
        }

        [HttpPost("refresh-token")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<LoginResponse> RefreshToken([FromBody] RefreshTokenCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPost("register")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(RegisterResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<RegisterResponse> Register([FromBody]RegisterCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPost("logout")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Logout()
        {
            await _mediator.Send(new LogoutCommand());
            return NoContent();
        }
    }
}
