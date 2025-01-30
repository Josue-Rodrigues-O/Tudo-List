using Microsoft.AspNetCore.Http;
using Tudo_List.Application.Helpers;
using Tudo_List.Application.Interfaces.Applications;
using Tudo_List.Domain.Core.Interfaces.Services;
using Tudo_List.Domain.Entities;
using Tudo_List.Domain.Models;

namespace Tudo_List.Application
{
    public class UserImageApplication(IUserImageService userImageService) : IUserImageApplication
    {
        public Image? GetByUserId(int userId)
        {
            return userImageService.GetByUserId(userId);
        }

        public async Task<Image?> GetByUserIdAsync(int userId)
        {
            return await userImageService.GetByUserIdAsync(userId);
        }

        public Image Upload(int userId, IFormFile file)
        {
            ValidateFile(file);
            return userImageService.Add(file.ToUserImage(userId));
        }

        public async Task<Image> UploadAsync(int userId, IFormFile file)
        {
            ValidateFile(file);
            return await userImageService.AddAsync(file.ToUserImage(userId));
        }

        private static void ValidateFile(IFormFile file)
        {
            if (file is null or { Length: default(long) })
                throw new InvalidOperationException("No file uploaded.");
        }
    }
}
