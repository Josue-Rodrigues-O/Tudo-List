using FluentValidation;
using Tudo_List.Domain.Core.Interfaces.Repositories;
using Tudo_List.Domain.Core.Interfaces.Services;
using Tudo_List.Domain.Entities;
using Tudo_List.Domain.Helpers;
using Tudo_List.Domain.Models;

namespace Tudo_List.Domain.Services
{
    public class UserImageService(IUserImageRepository userImageRepository, IValidator<UserImage> imageValidator) : IUserImageService
    {
        public Image? GetByUserId(int userId)
        {
            var userImage = userImageRepository.GetByUserId(userId);
            if (userImage == null)
            {
                return null;
            }

            return userImage.ToImage();
        }

        public async Task<Image?> GetByUserIdAsync(int userId)
        {
            var userImage = await userImageRepository.GetByUserIdAsync(userId);
            if (userImage == null)
            {
                return null;
            }

            return userImage.ToImage();
        }

        public Image Add(UserImage image)
        {
            imageValidator.ValidateAndThrow(image);

            var previousImage = userImageRepository.GetByUserId(image.UserId);
            if (previousImage != null)
            {
                userImageRepository.Remove(previousImage);
            }

            userImageRepository.Add(image);
            userImageRepository.Commit();

            return image.ToImage();
        }

        public async Task<Image> AddAsync(UserImage image)
        {
            await imageValidator.ValidateAndThrowAsync(image);

            var previousImage = userImageRepository.GetByUserId(image.UserId);
            if (previousImage != null)
            {
                userImageRepository.Remove(previousImage);
            }

            await userImageRepository.AddAsync(image);
            await userImageRepository.CommitAsync();

            return image.ToImage();
        }
    }
}
