using Application.Handlers.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controler]")]
    public class UsersController : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register(RegisterUserCommand command)
        {
            return NoContent(); //TODO
        }
    }
}
