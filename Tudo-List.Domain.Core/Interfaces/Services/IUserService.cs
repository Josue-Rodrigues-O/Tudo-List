using Tudo_List.Domain.Entities;

namespace Tudo_List.Domain.Core.Interfaces.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();
        User Get(int id);
        void Register(User user);
        void Update(User user);
        void Delete(int id);
    }
}
