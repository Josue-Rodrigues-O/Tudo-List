using Tudo_List.Domain.Services.Constants;

namespace Tudo_List.Domain.Services.Helpers
{
    public static class PasswordHelper
    {
        private static byte[] GenerateSalt(int size)
        {
            var salt = new byte[size];
            new Random().NextBytes(salt);
            return salt;
        }

        public static string GenerateBase64String(int size = PasswordConstants.SALT_SIZE)
        {
            var salt = GenerateSalt(size);
            return Convert.ToBase64String(salt);
        }
    }
}
