using Tudo_List.Application.DtoValidation;

namespace Tudo_List.Application.Models.Dtos.User
{
    public class UpdateEmailDto
    {
        [RequiredProperty]
        public int UserId { get; set; }

        [RequiredProperty]
        public string NewEmail { get; set; }

        [RequiredProperty]
        public string CurrentPassword { get; set; }
    }
}
