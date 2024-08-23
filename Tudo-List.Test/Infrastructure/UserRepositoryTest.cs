using Microsoft.Extensions.DependencyInjection;
using Tudo_list.Infrastructure.Context;
using Tudo_list.Infrastructure.CrossCutting.Ioc;
using Tudo_list.Infrastructure.Repositories;
using Tudo_List.Domain.Core.Interfaces.Factories;
using Tudo_List.Domain.Core.Interfaces.Repositories;
using Tudo_List.Domain.Core.Interfaces.Strategies;
using Tudo_List.Domain.Entities;
using Tudo_List.Domain.Enums;

namespace Tudo_List.Test.Infrastructure
{
    public class UserRepositoryTest : UnitTest
    {
        private readonly IPasswordStrategyFactory _passwordStrategyFactory;
        private readonly IUserRepository _userRepository;

        public UserRepositoryTest()
        {
            _userRepository = _serviceProvider.GetRequiredService<IUserRepository>();
            _passwordStrategyFactory = _serviceProvider.GetRequiredService<IPasswordStrategyFactory>();

            var context = _serviceProvider.GetRequiredService<ApplicationDbContext>();
            InitializeInMemoryDatabase(context, GetUsers());
        }

        [Fact]
        public void Can_Get_All_Users()
        {
            var users = GetUsers();

            var usersInDatabase = _userRepository.GetAll();

            Assert.Equal(users.Count(), usersInDatabase.Count());
        }

        private IEnumerable<User> GetUsers()
        {
            const PasswordStrategy strategy = PasswordStrategy.BCrypt;
            var passwordStrategy = _passwordStrategyFactory.CreatePasswordStrategy(strategy);

            return [
                    new() { Id = 1, Name = "Lucas", Email = "Lucas@gmail.com", PasswordHash = passwordStrategy.HashPassword("lngGsw5S"), PasswordStrategy = strategy },
                    new() { Id = 2, Name = "Josué", Email = "Josue@gmail.com", PasswordHash = passwordStrategy.HashPassword("K7wPzC7o"), PasswordStrategy = strategy },
                    new() { Id = 3, Name = "Mateus", Email = "Mateus@gmail.com", PasswordHash = passwordStrategy.HashPassword("bEMfTXVz"), PasswordStrategy = strategy },
                    new() { Id = 4, Name = "Douglas", Email = "Lucas@gmail.com", PasswordHash = passwordStrategy.HashPassword("APlhcwsW9dqu0yN3Hl5u"), PasswordStrategy = strategy },
                    new() { Id = 5, Name = "Ana Carolina", Email = "Ana@gmail.com", PasswordHash = passwordStrategy.HashPassword("fmzQxo9lSP41hmujCp6c"), PasswordStrategy = strategy },
                    new() { Id = 6, Name = "Victor", Email = "Victor@gmail.com", PasswordHash = passwordStrategy.HashPassword("tqA=Oj;dc2fSHAZ9"), PasswordStrategy = strategy },
                    new() { Id = 7, Name = "Eduardo", Email = "Eduardo@gmail.com", PasswordHash = passwordStrategy.HashPassword("=cZnI)CLW+%pam00{$rW="), PasswordStrategy = strategy },
                    new() { Id = 8, Name = "Júlio", Email = "Julio@gmail.com", PasswordHash = passwordStrategy.HashPassword("r=V3^Na-Z"), PasswordStrategy = strategy },
            ];
        }
    }
}
