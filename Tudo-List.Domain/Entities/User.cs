using Tudo_List.Domain.Enums;

namespace Tudo_List.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string? Salt { get; set; }
        public PasswordStrategy PasswordStrategy { get; set; }
    }
}
