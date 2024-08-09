using FluentValidation;
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
        private readonly IUserApplication _userApplication = userApplication;
        private readonly IAuthService _authService = authService;
        private readonly ITokenService _tokenService = tokenService;

        [HttpPost]
        public IActionResult Login([FromBody] LoginRequestDto model)
        {
            var user = _userApplication.GetByEmail(model.Email);

            if (!_authService.CheckPassword(user.Id, model.Password))
                throw new ValidationException("The password is incorrect!");

            var token = _tokenService.GenerateToken(user);
            return Ok(new { Token = token });
        }

        [HttpPost("login-async")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequestDto model)
        {
            var user = await _userApplication.GetByEmailAsync(model.Email);

            if (!_authService.CheckPassword(user.Id, model.Password))
                throw new ValidationException("The password is incorrect!");

            var token = _tokenService.GenerateToken(user);
            return Ok(new { Token = token });
        }
    }
}
