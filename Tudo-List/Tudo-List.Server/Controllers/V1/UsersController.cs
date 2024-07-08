using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tudo_List.Application.Interfaces.Applications;
using Tudo_List.Application.Interfaces.Services;
using Tudo_List.Application.Models.Dtos.User;
using Tudo_List.Server.Controllers.Common;

namespace Tudo_List.Server.Controllers.V1
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class UsersController(IUserApplication userApplication) : ApiController
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
            var user = _userApplication.GetById(id);

            if (user is null)
                return NotFound($"{nameof(User)} with the id {id} was not found!");

            return Ok(user);
        }
        
        [HttpGet("get-by-id-async/{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var user = await _userApplication.GetByIdAsync(id);

            if (user is null)
                return NotFound($"{nameof(User)} with the id {id} was not found!");

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterUserDto model)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            _userApplication.Register(model);

            return Ok();
        }
        
        [AllowAnonymous]
        [HttpPost("register-async")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterUserDto model)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            await _userApplication.RegisterAsync(model);
            
            return Ok();
        }

        [HttpPut("update")]
        public IActionResult Update([FromBody] UpdateUserDto model)
        {
            _userApplication.Update(model);
            return NoContent();
        }
        
        [HttpPut("update-async")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateUserDto model)
        {
            await _userApplication.UpdateAsync(model);
            return NoContent();
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
