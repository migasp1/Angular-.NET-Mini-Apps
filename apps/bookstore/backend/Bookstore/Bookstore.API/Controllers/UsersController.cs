using Application.Commands.Users.RefresToken;
using Application.CQRS.Interfaces;
using Application.Handlers.Users.AuthenticateUser;
using Application.Handlers.Users.RegisterUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController(IBookStoreCommandDispatcher commandDispatcher) : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
        {
            var _ = await commandDispatcher.DispatchCommand(command);
            return NoContent();
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateUserCommand command)
        {
            var actionResult = await commandDispatcher.DispatchCommand(command);
            return Ok(actionResult.JWTToken);
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand command)
        {
            var actionResult = await commandDispatcher.DispatchCommand(command);
            return Ok(actionResult.JWTToken);
        }
    }
}
