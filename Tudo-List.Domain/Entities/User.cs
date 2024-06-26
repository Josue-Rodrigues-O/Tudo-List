using System.Text.Json.Serialization;

namespace Tudo_List.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public string PasswordHash { get; set; }

        [JsonIgnore]
        public string? Salt { get; set; }
    }
}
