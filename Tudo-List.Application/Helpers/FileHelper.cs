using Microsoft.AspNetCore.Http;
using Tudo_List.Domain.Entities;

namespace Tudo_List.Application.Helpers
{
    public static class FileHelper
    {
        public static UserImage ToUserImage(this IFormFile file, int userId)
        {
            using var memoryStream = new MemoryStream();
            file.CopyTo(memoryStream);

            return new UserImage
            {
                Name = file.FileName,
                Data = memoryStream.ToArray(),
                ContentType = file.ContentType,
                UserId = userId
            };
        }
    }
}
