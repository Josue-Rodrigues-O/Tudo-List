using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tudo_List.Application.Interfaces.Applications;
using Tudo_List.Application.Models.Dtos.TodoListItem;
using Tudo_List.Application.Models.Dtos.User;
using Tudo_List.Domain.Entities;
using Tudo_List.Server.Controllers.Common;

namespace Tudo_List.Server.Controllers.V1
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class TodoListItemsController(ITodoListItemApplication todoListItemApplication) : ApiController
    {
        private readonly ITodoListItemApplication _todoListItemApplication = todoListItemApplication;

        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            return Ok(_todoListItemApplication.GetAll());
        }

        [HttpGet("get-all-async")]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _todoListItemApplication.GetAllAsync());
        }

        [HttpGet("get-by-id/{id:guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var item = _todoListItemApplication.GetById(id);

            if (item is null)
                return NotFound($"{nameof(TodoListItem)} with the id {id} was not found!");

            return Ok(item);
        }

        [HttpGet("get-by-id-async/{id:guid}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            var item = await _todoListItemApplication.GetByIdAsync(id);

            if (item is null)
                return NotFound($"{nameof(TodoListItem)} with the id {id} was not found!");

            return Ok(item);
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody] AddItemDto model)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            _todoListItemApplication.Add(model);

            return Ok();
        }

        [HttpPost("add-async")]
        public async Task<IActionResult> AddAsync([FromBody] AddItemDto model)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            await _todoListItemApplication.AddAsync(model);

            return Ok();
        }

        [HttpPut("update")]
        public IActionResult Update([FromBody] UpdateItemDto model)
        {
            _todoListItemApplication.Update(model);
            return NoContent();
        }

        [HttpPut("update-async")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateItemDto model)
        {
            await _todoListItemApplication.UpdateAsync(model);
            return NoContent();
        }

        [HttpDelete("delete/{id:guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            _todoListItemApplication.Delete(id);
            return NoContent();
        }

        [HttpDelete("delete-async/{id:guid}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            await _todoListItemApplication.DeleteAsync(id);
            return NoContent();
        }
    }
}
