using Application.CQRS.Interfaces;
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
    }
}
