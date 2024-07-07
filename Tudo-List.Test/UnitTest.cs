using System.Drawing;
using Tudo_List.Domain.Entities;
using Tudo_List.Domain.Enums;
using Tudo_List.Domain.Services;
using Tudo_List.Domain.Services.Strategies;

namespace Tudo_List.Test
{
    public class UnitTest
    {
        [Fact]
        public void Test()
        {
        }

        private static byte[] GenerateSalt(int size = 128)
        {
            var salt = new byte[size];
            new Random().NextBytes(salt);

            return salt;
        }

        public static string GenerateBase64String(int size = 128)
        {
            var salt = GenerateSalt(size);
            return Convert.ToBase64String(salt);
        }
    }
}