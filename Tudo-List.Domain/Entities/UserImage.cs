using Tudo_List.Domain.Models;

namespace Tudo_List.Domain.Entities
{
    public class UserImage : Image
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
