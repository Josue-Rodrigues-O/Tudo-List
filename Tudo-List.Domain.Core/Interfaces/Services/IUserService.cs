using Tudo_List.Domain.Commands.Dtos.User;
using Tudo_List.Domain.Entities;

namespace Tudo_List.Domain.Core.Interfaces.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();
        Task<IEnumerable<User>> GetAllAsync();

        User GetById(int id);
        Task<User> GetByIdAsync(int id);

        User GetByEmail(string email);
        Task<User> GetByEmailAsync(string email);

        void Register(User user, string password);
        Task RegisterAsync(User user, string password);

        void Update(UpdateUserDto model);
        Task UpdateAsync(UpdateUserDto model);
   
        void Delete(int id);
        Task DeleteAsync(int id);
    }
}
