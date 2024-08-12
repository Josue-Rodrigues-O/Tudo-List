using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tudo_List.Application.Dtos.User;
using Tudo_List.Application.Interfaces.Applications;

namespace Tudo_List.Server.Controllers.V1
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class UsersController(IUserApplication userApplication) : ControllerBase
    {
        private readonly IUserApplication _userApplication = userApplication;

        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            return Ok(_userApplication.GetAll());
        }
        
        [HttpGet("get-all-async")]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _userApplication.GetAllAsync());
        }

        [HttpGet("get-by-id/{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            return Ok(_userApplication.GetById(id));
        }
        
        [HttpGet("get-by-id-async/{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            return Ok(await _userApplication.GetByIdAsync(id));
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterUserDto model)
        {
            _userApplication.Register(model);
            return Ok();
        }
        
        [AllowAnonymous]
        [HttpPost("register-async")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterUserDto model)
        {
            await _userApplication.RegisterAsync(model);
            return Ok();
        }

        [HttpPatch("update")]
        public IActionResult Update([FromBody] UpdateUserDto model)
        {
            _userApplication.Update(model);
            return NoContent();
        }
        
        [HttpPatch("update-async")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateUserDto model)
        {
            await _userApplication.UpdateAsync(model);
            return NoContent();
        }

        [HttpPatch("update-email")]
        public IActionResult UpdateEmail([FromBody] UpdateEmailDto model)
        {
            _userApplication.UpdateEmail(model);
            return Ok();
        }
        
        [HttpPatch("update-email-async")]
        public async Task<IActionResult> UpdateEmailAsync([FromBody] UpdateEmailDto model)
        {
            await _userApplication.UpdateEmailAsync(model);
            return Ok();
        }

        [HttpPatch("update-password")]
        public IActionResult UpdatePassword([FromBody] UpdatePasswordDto model)
        {
            _userApplication.UpdatePassword(model);
            return Ok();
        }

        [HttpPatch("update-password-async")]
        public async Task<IActionResult> UpdatePasswordAsync([FromBody] UpdatePasswordDto model)
        {
            await _userApplication.UpdatePasswordAsync(model);
            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            _userApplication.Delete(id);
            return NoContent();
        }
        
        [HttpDelete("delete-async/{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            await _userApplication.DeleteAsync(id);
            return NoContent();
        }
    }
}
