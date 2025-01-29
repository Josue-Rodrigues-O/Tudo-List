using Tudo_List.Domain.Enums;

namespace Tudo_List.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string? Salt { get; set; }
        public PasswordStrategy PasswordStrategy { get; set; }
    }
}
