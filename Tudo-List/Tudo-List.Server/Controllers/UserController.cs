using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tudo_List.Application.Interfaces;
using Tudo_List.Application.Models.Users;
using Tudo_List.Server.Controllers.Common;

namespace Tudo_List.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserApplication userApplication) : ApiController
    {
        private readonly IUserApplication _userApplication = userApplication;

        [AllowAnonymous]
        [HttpGet]
        [Route("users")]
        public IActionResult GetAll()
        {
            var users = _userApplication.GetAll();
            return Ok(users);
        }
        
        [HttpGet]
        [Route("users/{id}")]
        public IActionResult GetAll([FromRoute] int id)
        {
            var user = _userApplication.GetById(id);

            if (user is null)
                return NotFound($"{nameof(User)} was not found with the id {id}!");

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("users")]
        public IActionResult Register([FromBody] RegisterUserRequest model)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            _userApplication.Register(model);

            return Ok();
        }

        [HttpPut]
        [Route("users/{id}")]
        public IActionResult Update([FromBody] UpdateUserRequest model)
        {
            _userApplication.Update(model);

            return NoContent();
        }
    }
}
