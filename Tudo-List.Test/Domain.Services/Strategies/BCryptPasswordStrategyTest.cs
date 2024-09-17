using Microsoft.Extensions.DependencyInjection;
using Tudo_List.Domain.Core.Interfaces.Factories;
using Tudo_List.Domain.Enums;
using Tudo_List.Domain.Services.Strategies;

namespace Tudo_List.Test.Domain.Services.Strategies
{
    public class BCryptPasswordStrategyTest : UnitTest
    {
        private readonly BCryptPasswordStrategy _bcryptPasswordStrategy;

        public BCryptPasswordStrategyTest()
        {
            _bcryptPasswordStrategy = new BCryptPasswordStrategy();
        }

        [Fact]
        public void Should_Return_A_Valid_Password_Hash()
        {
            const string password = "password123";
            var passwordHash = _bcryptPasswordStrategy.HashPassword(password);

            Assert.NotNull(passwordHash);
            Assert.NotEqual(password, passwordHash);
        }

        [Fact]
        public void Should_Return_True_When_Validating_Valid_Password_Hash()
        {
            const string password = "verifyTesting123";
            var passwordHash = _bcryptPasswordStrategy.HashPassword(password);

            Assert.True(_bcryptPasswordStrategy.VerifyPassword(password, passwordHash));
        }

        [Fact]
        public void Should_Return_False_When_Validating_Invalid_Password()
        {
            const string password = "verifyTesting123";
            var passwordHash = _bcryptPasswordStrategy.HashPassword(password);

            Assert.False(_bcryptPasswordStrategy.VerifyPassword("invalidPasswordHash", passwordHash));
        }

        [Fact]
        public void Same_Password_Cannot_Generate_Same_Password_Hash_Due_To_Inner_Salting()
        {
            const string password = "testingBCryptSalting";

            var passwordHash1 = _bcryptPasswordStrategy.HashPassword(password);
            var passwordHash2 = _bcryptPasswordStrategy.HashPassword(password);

            Assert.NotEqual(passwordHash1, passwordHash2);
        }
    }
}
