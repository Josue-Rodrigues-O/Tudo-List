using Microsoft.AspNetCore.Mvc;
using Tudo_List.Domain.Core.Interfaces.Services;

namespace Tudo_List.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpGet]
        public IActionResult Teste()
        {
            return Ok();
        }
    }
}
