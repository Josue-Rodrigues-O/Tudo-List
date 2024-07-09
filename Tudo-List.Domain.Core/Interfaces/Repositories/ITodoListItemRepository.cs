using Tudo_List.Domain.Entities;

namespace Tudo_List.Domain.Core.Interfaces.Repositories
{
    public interface ITodoListItemRepository
    {
        IEnumerable<TodoListItem> GetAll();
        Task<IEnumerable<TodoListItem>> GetAllAsync();

        TodoListItem GetById(Guid id);
        Task<TodoListItem> GetByIdAsync(Guid id);

        void Add(TodoListItem item);
        Task AddAsync(TodoListItem item);


        void Update(TodoListItem item);
        Task UpdateAsync(TodoListItem item);

        void Remove(Guid id);
        Task RemoveAsync(Guid id);
    }
}
