using Microsoft.AspNetCore.Http;
using Tudo_List.Domain.Models;

namespace Tudo_List.Application.Interfaces.Applications
{
    public interface IUserImageApplication
    {
        Image? GetByUserId(int userId);
        Task<Image?> GetByUserIdAsync(int userId);

        Image Upload(int userId, IFormFile file);
        Task<Image> UploadAsync(int userId, IFormFile file);
    }
}
