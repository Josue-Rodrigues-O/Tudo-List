using Tudo_List.Domain.Entities;
using Tudo_List.Domain.Models;

namespace Tudo_List.Domain.Core.Interfaces.Services
{
    public interface ITodoListItemService
    {
        IEnumerable<TodoListItem> GetAll(TodoListItemQueryFilter filter);
        Task<IEnumerable<TodoListItem>> GetAllAsync(TodoListItemQueryFilter filter);

        TodoListItem? GetById(Guid id);
        Task<TodoListItem?> GetByIdAsync(Guid id);

        void Add(TodoListItem item);
        Task AddAsync(TodoListItem item);

        void Update(TodoListItem model);
        Task UpdateAsync(TodoListItem model);
   
        void Delete(Guid id);
        Task DeleteAsync(Guid id);
    }
}
