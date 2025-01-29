using AutoMapper;
using Tudo_List.Application.Dtos.TodoListItem;
using Tudo_List.Application.Interfaces.Applications;
using Tudo_List.Domain.Core.Interfaces.Services;
using Tudo_List.Domain.Entities;
using Tudo_List.Domain.Exceptions;
using Tudo_List.Domain.Models;

namespace Tudo_List.Application
{
    public class TodoListItemApplication(ITodoListItemService todoListItemService, IMapper mapper) : ITodoListItemApplication
    {
        public IEnumerable<TodoListItemDto> GetAll(TodoListItemQueryFilter filter)
        {
            return mapper.Map<IEnumerable<TodoListItemDto>>(todoListItemService.GetAll(filter));
        }

        public async Task<IEnumerable<TodoListItemDto>> GetAllAsync(TodoListItemQueryFilter filter)
        {
            return mapper.Map<IEnumerable<TodoListItemDto>>(await todoListItemService.GetAllAsync(filter));
        }

        public TodoListItemDto GetById(Guid id)
        {
            var item = todoListItemService.GetById(id)
                ?? throw new EntityNotFoundException(nameof(TodoListItem), nameof(TodoListItem.Id), id);

            return mapper.Map<TodoListItemDto>(item);
        }

        public async Task<TodoListItemDto> GetByIdAsync(Guid id)
        {
            var item = await todoListItemService.GetByIdAsync(id)
                ?? throw new EntityNotFoundException(nameof(TodoListItem), nameof(TodoListItem.Id), id);

            return mapper.Map<TodoListItemDto>(item);
        }

        public void Add(AddItemDto model)
        {
            todoListItemService.Add(mapper.Map<TodoListItem>(model));
        }

        public async Task AddAsync(AddItemDto model)
        {
            await todoListItemService.AddAsync(mapper.Map<TodoListItem>(model));
        }

        public void Update(UpdateItemDto model)
        {
            todoListItemService.Update(mapper.Map<TodoListItem>(model));
        }

        public async Task UpdateAsync(UpdateItemDto model)
        {
            await todoListItemService.UpdateAsync(mapper.Map<TodoListItem>(model));
        }

        public void Delete(Guid id)
        {
            todoListItemService.Delete(id);
        }

        public async Task DeleteAsync(Guid id)
        {
            await todoListItemService.DeleteAsync(id);
        }
    }
}
