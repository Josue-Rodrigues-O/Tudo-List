using Tudo_List.Domain.Core.Interfaces.Strategies;
using Tudo_List.Domain.Services.Constants;

namespace Tudo_List.Domain.Services.Strategies
{
    public class BCryptPasswordStrategy : IPasswordStrategy
    {
        public bool UsesSalting => false;

        public string HashPassword(string password, string? salt = null)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(password, PasswordConstants.BCRYPT_WORK_FACTOR);
        }

        public bool VerifyPassword(string password, string passwordHash, string? salt = null)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(password, passwordHash);
        }
    }
}
