using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tudo_List.Application.Models.Users;
using Tudo_List.Domain.Core.Interfaces.Services;
using Tudo_List.Server.Controllers.Common;

namespace Tudo_List.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController(IUserService userService) : ApiController
    {
        private readonly IUserService _userService = userService;

        [AllowAnonymous]
        [HttpPost("login")]
        public Task<IActionResult> Login([FromBody] AuthenticateRequest model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(x => x.Errors)
                    .Select(x => x.ErrorMessage)
                    .ToArray();

                return Task
                        .FromResult<IActionResult>(BadRequest(new
                        {
                            Errors = errors
                        }));
            }

            return Task.FromResult<IActionResult>(Ok());
        }
    }
}
