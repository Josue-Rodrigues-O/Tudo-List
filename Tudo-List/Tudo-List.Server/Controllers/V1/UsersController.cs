using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tudo_List.Application.Interfaces;
using Tudo_List.Application.Models.Dtos;
using Tudo_List.Domain.Core.Interfaces.Services;
using Tudo_List.Server.Controllers.Common;

namespace Tudo_List.Server.Controllers.V1
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    public class UsersController(IUserApplication userApplication, ITokenService tokenService) : ApiController
    {
        private readonly IUserApplication _userApplication = userApplication;
        private readonly ITokenService _tokenService = tokenService;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAll()
        {
            return Ok(_userApplication.GetAll());
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById([FromRoute] int id)
        {
            var user = _userApplication.GetById(id);

            if (user is null)
                return NotFound($"{nameof(User)} with the id {id} was not found!");

            return Ok(user);
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Register([FromBody] RegisterUserDto model)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            _userApplication.Register(model);

            return Ok();
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Update([FromBody] UpdateUserDto model)
        {
            _userApplication.Update(model);
            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete([FromRoute] int id)
        {
            _userApplication.Delete(id);
            return NoContent();
        }
    }
}
