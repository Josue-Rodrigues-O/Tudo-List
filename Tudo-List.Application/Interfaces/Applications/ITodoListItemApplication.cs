using Tudo_List.Application.Models.Dtos;
using Tudo_List.Domain.Models.TodoListItem;

namespace Tudo_List.Application.Interfaces.Applications
{
    public interface ITodoListItemApplication
    {
        IEnumerable<TodoListItemDto> GetAll();
        Task<IEnumerable<TodoListItemDto>> GetAllAsync();

        TodoListItemDto GetById(Guid id);
        Task<TodoListItemDto> GetByIdAsync(Guid id);

        void Add(AddItemRequest item);
        Task AddAsync(AddItemRequest item);

        void Update(UpdateItemRequest item);
        Task UpdateAsync(UpdateItemRequest item);

        void Delete(Guid id);
        Task DeleteAsync(Guid id);
    }
}
