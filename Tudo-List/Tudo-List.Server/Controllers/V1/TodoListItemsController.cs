using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tudo_List.Application.Dtos.TodoListItem;
using Tudo_List.Application.Interfaces.Applications;
using Tudo_List.Domain.Entities;
using Tudo_List.Domain.Models;

namespace Tudo_List.Server.Controllers.V1
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class TodoListItemsController(ITodoListItemApplication todoListItemApplication) : ControllerBase
    {
        [HttpGet("get-all")]
        public IActionResult GetAll([FromQuery] TodoListItemQueryFilter filter)
        {
            return Ok(todoListItemApplication.GetAll(filter));
        }

        [HttpGet("get-all-async")]
        public async Task<IActionResult> GetAllAsync([FromQuery] TodoListItemQueryFilter filter)
        {
            return Ok(await todoListItemApplication.GetAllAsync(filter));
        }

        [HttpGet("get-by-id/{id:guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var item = todoListItemApplication.GetById(id);

            return item is not null
                ? Ok(item)
                : NotFound($"{nameof(TodoListItem)} with id {id} was not found!");
        }

        [HttpGet("get-by-id-async/{id:guid}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            var item = await todoListItemApplication.GetByIdAsync(id);

            return item is not null
                ? Ok(item)
                : NotFound($"{nameof(TodoListItem)} with id {id} was not found!");
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody] AddItemDto model)
        {
            todoListItemApplication.Add(model);
            return Ok();
        }

        [HttpPost("add-async")]
        public async Task<IActionResult> AddAsync([FromBody] AddItemDto model)
        {
            await todoListItemApplication.AddAsync(model);
            return Ok();
        }

        [HttpPatch("update")]
        public IActionResult Update([FromBody] UpdateItemDto model)
        {
            todoListItemApplication.Update(model);
            return NoContent();
        }

        [HttpPatch("update-async")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateItemDto model)
        {
            await todoListItemApplication.UpdateAsync(model);
            return NoContent();
        }

        [HttpDelete("delete/{id:guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            todoListItemApplication.Delete(id);
            return NoContent();
        }

        [HttpDelete("delete-async/{id:guid}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            await todoListItemApplication.DeleteAsync(id);
            return NoContent();
        }
    }
}
