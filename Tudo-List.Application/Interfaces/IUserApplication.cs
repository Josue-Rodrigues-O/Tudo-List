using Tudo_List.Application.Models.Users;
using Tudo_List.Domain.Entities;

namespace Tudo_List.Application.Interfaces
{
    public interface IUserApplication
    {
        IEnumerable<User> GetAll();
        User GetById(int id);
        void Register(RegisterUserRequest model);
        void Update(UpdateUserRequest model);
        void Delete(int id);
    }
}
