using Microsoft.AspNetCore.Mvc;
using Tudo_List.Application.Models.Auth;
using Tudo_List.Domain.Core.Interfaces.Services;
using Tudo_List.Server.Controllers.Common;

namespace Tudo_List.Server.Controllers.V1
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    public class LoginController(IUserService userService) : ApiController
    {
        private readonly IUserService _userService = userService;

        [HttpPost]
        public Task<IActionResult> Login([FromBody] LoginRequest model)
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
