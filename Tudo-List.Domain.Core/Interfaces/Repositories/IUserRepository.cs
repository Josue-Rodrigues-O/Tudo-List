using Tudo_List.Domain.Core.Interfaces.Repositories.Common;
using Tudo_List.Domain.Entities;

namespace Tudo_List.Domain.Core.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByEmail(string email);
        Task<User> GetByEmailAsync(string email);
    }
}
