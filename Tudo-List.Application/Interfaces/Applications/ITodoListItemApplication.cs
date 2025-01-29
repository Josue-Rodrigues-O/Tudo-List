using Tudo_List.Application.Dtos.TodoListItem;
using Tudo_List.Domain.Models;

namespace Tudo_List.Application.Interfaces.Applications
{
    public interface ITodoListItemApplication
    {
        IEnumerable<TodoListItemDto> GetAll(TodoListItemQueryFilter filter);
        Task<IEnumerable<TodoListItemDto>> GetAllAsync(TodoListItemQueryFilter filter);

        TodoListItemDto GetById(Guid id);
        Task<TodoListItemDto> GetByIdAsync(Guid id);

        void Add(AddItemDto item);
        Task AddAsync(AddItemDto item);

        void Update(UpdateItemDto item);
        Task UpdateAsync(UpdateItemDto item);

        void Delete(Guid id);
        Task DeleteAsync(Guid id);
    }
}
