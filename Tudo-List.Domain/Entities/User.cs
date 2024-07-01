using System.Text.Json.Serialization;
using Tudo_List.Domain.Enums;

namespace Tudo_List.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        [JsonIgnore]
        public string PasswordHash { get; set; }

        [JsonIgnore]
        public string? Salt { get; set; }
        
        [JsonIgnore]
        public PasswordStrategyEnum PasswordStrategy { get; set; }

    }
}
