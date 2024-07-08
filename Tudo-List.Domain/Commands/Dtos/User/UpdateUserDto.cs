using System.ComponentModel.DataAnnotations;

namespace Tudo_List.Domain.Commands.Dtos.User
{
    public class UpdateUserDto
    {
        [Key, Required]
        public int Id { get; set; }

        public string? Name { get; set; }
    }
}
