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
        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            return Ok(userApplication.GetAll());
        }
        
        [HttpGet("get-all-async")]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await userApplication.GetAllAsync());
        }

        [HttpGet("get-by-id/{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            return Ok(userApplication.GetById(id));
        }
        
        [HttpGet("get-by-id-async/{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            return Ok(await userApplication.GetByIdAsync(id));
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterUserDto model)
        {
            userApplication.Register(model);
            return Ok();
        }
        
        [AllowAnonymous]
        [HttpPost("register-async")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterUserDto model)
        {
            await userApplication.RegisterAsync(model);
            return Ok();
        }

        [HttpPatch("update")]
        public IActionResult Update([FromBody] UpdateUserDto model)
        {
            userApplication.Update(model);
            return NoContent();
        }
        
        [HttpPatch("update-async")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateUserDto model)
        {
            await userApplication.UpdateAsync(model);
            return NoContent();
        }

        [HttpPatch("update-email")]
        public IActionResult UpdateEmail([FromBody] UpdateEmailDto model)
        {
            userApplication.UpdateEmail(model);
            return Ok();
        }
        
        [HttpPatch("update-email-async")]
        public async Task<IActionResult> UpdateEmailAsync([FromBody] UpdateEmailDto model)
        {
            await userApplication.UpdateEmailAsync(model);
            return Ok();
        }

        [HttpPatch("update-password")]
        public IActionResult UpdatePassword([FromBody] UpdatePasswordDto model)
        {
            userApplication.UpdatePassword(model);
            return Ok();
        }

        [HttpPatch("update-password-async")]
        public async Task<IActionResult> UpdatePasswordAsync([FromBody] UpdatePasswordDto model)
        {
            await userApplication.UpdatePasswordAsync(model);
            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            userApplication.Delete(id);
            return NoContent();
        }
        
        [HttpDelete("delete-async/{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            await userApplication.DeleteAsync(id);
            return NoContent();
        }
    }
}
