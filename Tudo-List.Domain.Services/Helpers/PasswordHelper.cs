namespace Tudo_List.Domain.Services.Helpers
{
    public static class PasswordHelper
    {
        public static byte[] GenerateSalt(int length = 128)
        {
            var salt = new byte[length];
            new Random().NextBytes(salt);
            return salt;
        }
    }
}
