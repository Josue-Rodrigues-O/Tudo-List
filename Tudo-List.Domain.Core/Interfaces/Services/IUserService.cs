using Tudo_List.Domain.Entities;

namespace Tudo_List.Domain.Core.Interfaces.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();
        Task<IEnumerable<User>> GetAllAsync();

        User? GetById(int id);
        Task<User?> GetByIdAsync(int id);

        User? GetByEmail(string email);
        Task<User?> GetByEmailAsync(string email);

        void Register(User user, string password);
        Task RegisterAsync(User user, string password);

        void Update(int id, string? newName);
        Task UpdateAsync(int id, string? newName);

        void UpdateEmail(int id, string newEmail, string currentPassword);
        Task UpdateEmailAsync(int id, string newEmail, string currentPassword);
        
        void UpdatePassword(int id, string currentPassword, string newPassword);
        Task UpdatePasswordAsync(int id, string currentPassword, string newPassword);

        void Delete(int id);
        Task DeleteAsync(int id);
    }
}
