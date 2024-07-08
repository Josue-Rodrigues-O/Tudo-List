using Tudo_List.Domain.Commands.Dtos.TodoListItem;
using Tudo_List.Domain.Entities;

namespace Tudo_List.Domain.Core.Interfaces.Services
{
    public interface ITodoListItemService
    {
        IEnumerable<TodoListItem> GetAll();
        Task<IEnumerable<TodoListItem>> GetAllAsync();

        TodoListItem GetById(Guid id);
        Task<TodoListItem> GetByIdAsync(Guid id);

        void Add(TodoListItem item);
        Task AddAsync(TodoListItem item);

        void Update(UpdateItemDto model);
        Task UpdateAsync(UpdateItemDto model);
   
        void Delete(Guid id);
        Task DeleteAsync(Guid id);
    }
}
