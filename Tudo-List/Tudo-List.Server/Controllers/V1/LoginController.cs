using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Tudo_List.Application.Dtos.Login;
using Tudo_List.Application.Interfaces.Applications;
using Tudo_List.Application.Interfaces.Services;

namespace Tudo_List.Server.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class LoginController(IUserApplication userApplication, IAuthService authService, ITokenService tokenService) : ControllerBase
    {
        [HttpPost]
        public IActionResult Login([FromBody] LoginRequestDto model)
        {
            var user = userApplication.GetByEmail(model.Email);

            if (!authService.CheckPassword(user.Id, model.Password))
            {
                throw new ValidationException(
                [
                    new(nameof(model.Password), $"The {nameof(model.Password)} is incorrect!")
                ]);
            }

            return Ok(new
            {
                Token = tokenService.GenerateToken(user)
            });
        }

        [HttpPost("login-async")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequestDto model)
        {
            var user = await userApplication.GetByEmailAsync(model.Email);

            if (!authService.CheckPassword(user.Id, model.Password))
            {
                throw new ValidationException(
                [
                    new(nameof(model.Password), $"The {nameof(model.Password)} is incorrect!")
                ]);
            }

            return Ok(new 
            { 
                Token = tokenService.GenerateToken(user) 
            });
        }
    }
}
