using Microsoft.AspNetCore.Http;
using Tudo_List.Domain.Models;

namespace Tudo_List.Application.Interfaces.Applications
{
    public interface IUserImageApplication
    {
        Image Upload(int userId, IFormFile file);
        Task<Image> UploadAsync(int userId, IFormFile file);
    }
}
