using Microsoft.Extensions.DependencyInjection;
using Tudo_List.Application.Interfaces.Services;
using Tudo_List.Domain.Core.Interfaces.Factories;
using Tudo_List.Domain.Entities;
using Tudo_List.Domain.Enums;
using Tudo_List.Domain.Exceptions;

namespace Tudo_List.Test.Application.Services
{
    public class AuthServiceTest : UnitTest
    {
        private readonly IAuthService _authService;
        private readonly IPasswordStrategyFactory _passwordStrategyFactory;

        private const string ValidPassword = "12345678";

        public AuthServiceTest()
        {
            _authService = _serviceProvider.GetRequiredService<IAuthService>();
            _passwordStrategyFactory = _serviceProvider.GetRequiredService<IPasswordStrategyFactory>();
        }

        [Fact]
        public void Should_Return_True_When_Checking_Correct_User_Password_From_User()
        {
            var userId = SaveValidUserInDatabase();

            Assert.True(_authService.CheckPassword(userId, ValidPassword));
        }
        
        [Fact]
        public void Should_Return_False_When_Checking_Incorrect_User_Password_From_User()
        {
            var userId = SaveValidUserInDatabase();

            Assert.False(_authService.CheckPassword(userId, "87654321"));
        }
        
        [Fact]
        public void Should_Throw_EntityNotFoundException_False_When_User_Is_Not_In_Database()
        {
            var userId = SaveValidUserInDatabase();

            Assert.Throws<EntityNotFoundException>(() => _authService.CheckPassword(500, ValidPassword));
        }

        private int SaveValidUserInDatabase()
        {
            var strategy = _passwordStrategyFactory.CreatePasswordStrategy(PasswordStrategy.BCrypt);

            var user = new User
            {
                Email = "test@test.com",
                Name = "Test",
                PasswordHash = strategy.HashPassword(ValidPassword),
                PasswordStrategy = PasswordStrategy.BCrypt,
                Salt = null
            };

            SaveInMemoryDatabase(user);

            return user.Id;
        }
    }
}
