using Tudo_List.Domain.Entities;

namespace Tudo_List.Domain.Core.Interfaces.Repositories
{
    public interface ITodoListItemRepository
    {
        IEnumerable<TodoListItem> GetAll(int? userId = null);
        Task<IEnumerable<TodoListItem>> GetAllAsync(int? userId = null);

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
