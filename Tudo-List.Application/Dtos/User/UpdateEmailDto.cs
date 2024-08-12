using System.ComponentModel.DataAnnotations;
using Tudo_List.Application.DtoValidation;

namespace Tudo_List.Application.Dtos.User
{
    public record UpdateEmailDto
    {
        [RequiredProperty]
        public int UserId { get; init; }

        [RequiredProperty]
        [EmailAddress]
        public string NewEmail { get; init; }

        [RequiredProperty]
        public string CurrentPassword { get; init; }
    }
}
