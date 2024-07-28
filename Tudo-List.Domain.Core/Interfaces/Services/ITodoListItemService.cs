using Tudo_List.Domain.Entities;
using Tudo_List.Domain.Enums;

namespace Tudo_List.Domain.Core.Interfaces.Services
{
    public interface ITodoListItemService
    {
        IEnumerable<TodoListItem> GetAll();
        Task<IEnumerable<TodoListItem>> GetAllAsync();

        TodoListItem GetById(Guid id);
        Task<TodoListItem> GetByIdAsync(Guid id);

        void Add(TodoListItem item, int userId);
        Task AddAsync(TodoListItem item, int userId);

        void Update(TodoListItem model);
        Task UpdateAsync(TodoListItem model);
   
        void Delete(Guid id);
        Task DeleteAsync(Guid id);
    }
}
