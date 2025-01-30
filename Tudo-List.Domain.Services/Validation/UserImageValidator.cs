using FluentValidation;
using SixLabors.ImageSharp;
using Tudo_List.Domain.Core.Interfaces.Services;
using Tudo_List.Domain.Entities;

namespace Tudo_List.Domain.Services.Validation
{
    public class UserImageValidator : AbstractValidator<UserImage>
    {
        public UserImageValidator(ICurrentUserService currentUserService)
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            ValidateUserId(currentUserService);
            ValidateData();
        }

        private void ValidateUserId(ICurrentUserService currentUserService)
        {
            RuleFor(item => item.UserId)
                .Equal(int.Parse(currentUserService.Id))
                .WithMessage("The Current User can't add an image to another User!");
        }

        private void ValidateData()
        {
            RuleFor(item => item.Data)
                .Must(ValidateImageFormat)
                    .WithMessage("The Image Format should be one of the followings:\n- TGA\n- PBM\n- QOI\n- TIFF\n- JPEG\n- Webp\n- BMP\n- PNG\n- GIF")
                .Must(ValidateProportion)
                    .WithMessage("The proportion should be one of the followings:\n- 64x64\n- 128x128\n- 256x256\n- 512x512");
        }

        private bool ValidateImageFormat(byte[] imageBytes)
        {
            try
            {
                Image.Load(imageBytes);
                return true;
            }
            catch(UnknownImageFormatException)
            {
                return false;
            }
        }

        private bool ValidateProportion(byte[] imageBytes)
        {
            using var image = Image.Load(imageBytes);
            int width = image.Width;
            int height = image.Height;

            return (width == 64 && height == 64) || (width == 128 && height == 128) || (width == 256 && height == 256) || (width == 512 && height == 512);
        }
    }
}
