using Tudo_List.Domain.Entities;
using Tudo_List.Domain.Models;

namespace Tudo_List.Domain.Core.Interfaces.Repositories
{
    public interface IUserImageRepository
    {
        UserImage? GetByUserId(int userId);
        Task<UserImage?> GetByUserIdAsync(int userId);

        void Add(UserImage image);
        Task AddAsync(UserImage image);

        void Remove(UserImage image);

        void Commit();
        Task CommitAsync();
    }
}
