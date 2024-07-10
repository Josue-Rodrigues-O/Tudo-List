using Tudo_List.Application.DtoValidation;

namespace Tudo_List.Application.Models.Dtos.User
{
    public class UpdatePasswordDto
    {
        [RequiredProperty]
        public int UserId { get; set; }

        [RequiredProperty]
        public string CurrentPassword { get; set; }

        [RequiredProperty]
        public string NewPassword { get; set; }

        [RequiredProperty]
        public string ConfirmNewPassword { get; set; }
    }
}
