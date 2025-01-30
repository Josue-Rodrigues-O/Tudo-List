using Tudo_List.Domain.Entities;
using Tudo_List.Domain.Models;

namespace Tudo_List.Domain.Helpers
{
    public static class ConversionHelper
    {
        public static Image ToImage(this UserImage image)
        {
            return new()
            {
                Name = image.Name,
                Data = image.Data,
                ContentType = image.ContentType,
            };
        }
    }
}
