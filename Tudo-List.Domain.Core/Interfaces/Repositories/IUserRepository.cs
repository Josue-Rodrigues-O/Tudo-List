using Tudo_List.Domain.Entities;

namespace Tudo_List.Domain.Core.Interfaces.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        Task<IEnumerable<User>> GetAllAsync();

        User? GetById(int id);
        Task<User?> GetByIdAsync(int id);

        User? GetByEmail(string email);
        Task<User?> GetByEmailAsync(string email);

        void Add(User user);
        Task AddAsync(User user);

        void Update(User user);
        Task UpdateAsync(User user);

        void Remove(User user);
        Task RemoveAsync(User user);
    }
}
