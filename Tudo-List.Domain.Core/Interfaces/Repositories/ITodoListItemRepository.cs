using Tudo_List.Domain.Entities;
using Tudo_List.Domain.Models;

namespace Tudo_List.Domain.Core.Interfaces.Repositories
{
    public interface ITodoListItemRepository
    {
        IEnumerable<TodoListItem> GetAll(int userId, TodoListItemQueryFilter filter);
        Task<IEnumerable<TodoListItem>> GetAllAsync(int userId, TodoListItemQueryFilter filter);

        TodoListItem? GetById(Guid id, int? userId = null);
        Task<TodoListItem?> GetByIdAsync(Guid id, int? userId = null);

        void Add(TodoListItem item);
        Task AddAsync(TodoListItem item);

        void Update(TodoListItem item);
        Task UpdateAsync(TodoListItem item);

        void Remove(TodoListItem item);
        Task RemoveAsync(TodoListItem item);
    }
}
