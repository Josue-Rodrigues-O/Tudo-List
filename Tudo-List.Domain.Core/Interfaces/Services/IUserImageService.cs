using Tudo_List.Domain.Entities;
using Tudo_List.Domain.Models;

namespace Tudo_List.Domain.Core.Interfaces.Services
{
    public interface IUserImageService
    {
        Image? GetByUserId(int userId);
        Task<Image?> GetByUserIdAsync(int userId);

        Image Add(UserImage image);
        Task<Image> AddAsync(UserImage image);
    }
}
