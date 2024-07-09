using System.ComponentModel.DataAnnotations;

namespace Tudo_List.Domain.Models.User
{
    public class UpdateUserRequest
    {
        [Key, Required]
        public int Id { get; set; }

        public string? Name { get; set; }
    }
}
