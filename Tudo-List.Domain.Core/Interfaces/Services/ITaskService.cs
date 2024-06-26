using Task = Tudo_List.Domain.Entities.Task;

namespace Tudo_List.Domain.Core.Interfaces.Services
{
    public interface ITaskService
    {
        IEnumerable<Task> GetAll();
        Task GetById(int id);
        void Register(Task task);
        void Update(Task task);
        void Delete(int id);
    }
}
