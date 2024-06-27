using Tudo_List.Domain.Entities;

namespace Tudo_List.Domain.Core.Interfaces.Services
{
    public interface ITaskService
    {
        IEnumerable<TodoListItem> GetAll();
        TodoListItem GetById(int id);
        void Register(TodoListItem task);
        void Update(TodoListItem task);
        void Delete(int id);
    }
}
