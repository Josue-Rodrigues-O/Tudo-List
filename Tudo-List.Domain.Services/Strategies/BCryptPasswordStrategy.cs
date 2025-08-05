using Tudo_List.Domain.Core.Interfaces.Strategies;

namespace Tudo_List.Domain.Services.Strategies
{
    public class BCryptPasswordStrategy : IPasswordStrategy
    {
        public bool UsesSaltingParameter => false;

        public string HashPassword(string password, string? salt = null)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(password, workFactor: 10);
        }

        public bool VerifyPassword(string password, string passwordHash, string? salt = null)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(password, passwordHash);
        }
    }
}
