using Tudo_List.Application.DtoValidation;

namespace Tudo_List.Application.Models.Dtos.User
{
    public class UpdateUserDto
    {
        [RequiredProperty]
        public int UserId { get; set; }

        public string? NewName { get; set; }
    }
}
