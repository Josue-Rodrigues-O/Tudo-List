using Tudo_List.Application.Models.Dtos;
using Tudo_List.Domain.Commands.Dtos.TodoListItem;

namespace Tudo_List.Application.Interfaces.Applications
{
    public interface ITodoListItemApplication
    {
        IEnumerable<TodoListItemDto> GetAll();
        Task<IEnumerable<TodoListItemDto>> GetAllAsync();

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
