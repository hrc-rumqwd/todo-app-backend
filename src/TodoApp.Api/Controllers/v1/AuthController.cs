using Microsoft.AspNetCore.Mvc;
using TodoApp.Api.Controllers.v1.RefreshUserToken;
using TodoApp.Application.Auth.Commands.Login;
using TodoApp.Application.Auth.Commands.Register;
using TodoApp.Application.Commons;

namespace TodoApp.Api.Controllers.v1
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IBroker _broker;

        public AuthController(IBroker broker)
        {
            _broker = broker;
        }

        [HttpPost("/login")]
        public async Task<IActionResult> Login(LoginCommand command, CancellationToken cancellationToken)
        {
            var result = await _broker.CommandAsync(command, cancellationToken);
            if (result == null)
            {
                return Unauthorized();
            }

            return Ok(result);
        }

        [HttpPost("/register")]
        public async Task<IActionResult> Register(RegisterCommand command, CancellationToken cancellationToken)
        {
            var result = await _broker.CommandAsync(command, cancellationToken);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpPost("/token/refresh")]
        public async Task<IActionResult> RefreshToken(RefreshUserTokenCommand command, CancellationToken cancellationToken)
        {
            var result = await _broker.CommandAsync(command, cancellationToken);
            return result.IsSuccess ? Ok(result) : Unauthorized(result);
        }
    }
}
