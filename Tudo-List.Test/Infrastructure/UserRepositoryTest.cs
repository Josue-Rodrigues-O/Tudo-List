using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tudo_List.Domain.Core.Interfaces.Repositories;
using Tudo_List.Domain.Entities;
using Tudo_List.Domain.Enums;

namespace Tudo_List.Test.Infrastructure
{
    public class UserRepositoryTest : UnitTest
    {
        private readonly IUserRepository _userRepository;

        public UserRepositoryTest()
        {
            _userRepository = _serviceProvider.GetRequiredService<IUserRepository>();
            InitializeInMemoryDatabase(_context, GetUsers());
        }

        [Fact]
        public void Can_Get_All_Users_Synchronously()
        {
            var users = GetUsers();

            var usersInDatabase = _userRepository.GetAll();

            Assert.Equivalent(users.Count(), usersInDatabase.Count());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        public void Can_Get_an_User_By_Id_Synchronously(int id)
        {
            var user = GetUsers().FirstOrDefault(x => x.Id == id);
            var userInDatabase = _userRepository.GetById(id);

            Assert.NotNull(user);
            Assert.NotNull(userInDatabase);
            Assert.Equivalent(user, userInDatabase, true);
        }

        [Theory]
        [InlineData("Lucas@gmail.com")]
        [InlineData("Josue@gmail.com")]
        [InlineData("Mateus@gmail.com")]
        [InlineData("Douglas@gmail.com")]
        [InlineData("Ana@gmail.com")]
        [InlineData("Victor@gmail.com")]
        [InlineData("Eduardo@gmail.com")]
        [InlineData("Julio@gmail.com")]
        public void Can_Get_an_User_By_Email_Synchronously(string email)
        {
            var user = GetUsers().FirstOrDefault(x => x.Email == email);
            var userInDatabase = _userRepository.GetByEmail(email);

            Assert.NotNull(user);
            Assert.NotNull(userInDatabase);
            Assert.Equivalent(user, userInDatabase, true);
        }

        [Fact]
        public void Can_Add_an_User_With_Valid_Properties_Synchronously()
        {
            const PasswordStrategy strategy = PasswordStrategy.BCrypt;

            var user = new User 
            { 
                Name = "Jesus", 
                Email = "Jesus@gmail.com", 
                PasswordHash = "ljYRhBNHu2", 
                PasswordStrategy = strategy 
            };

            _userRepository.Add(user);

            var userInDatabase = _context.Users.FirstOrDefault(x => x.Id == user.Id);

            Assert.Equivalent(user, userInDatabase, true);
        }

        [Theory]
        [InlineData("teste", "teste@gmail.com", null)]
        [InlineData("teste", null, "12345678")]
        [InlineData(null, "teste@gmail.com", "12345678")]
        public void Cant_Add_an_User_With_Invalid_Properties_Synchronously(string? name, string? email, string? password)
        {
            const PasswordStrategy strategy = PasswordStrategy.BCrypt;

            var user = new User
            {
                Name = name,
                Email = email,
                PasswordHash = password,
                PasswordStrategy = strategy
            };

            Assert.Throws<DbUpdateException>(() => _userRepository.Add(user));
        }

        [Fact]
        public async Task Can_Get_All_Users_Asynchronously()
        {
            var users = GetUsers();

            var usersInDatabase = await _userRepository.GetAllAsync();

            Assert.Equivalent(users.Count(), usersInDatabase.Count());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        public async Task Can_Get_an_User_By_Id_Asynchronously(int id)
        {
            var user = GetUsers().FirstOrDefault(x => x.Id == id);
            var userInDatabase = await _userRepository.GetByIdAsync(id);

            Assert.NotNull(user);
            Assert.NotNull(userInDatabase);
            Assert.Equivalent(user, userInDatabase, true);
        }

        [Theory]
        [InlineData("Lucas@gmail.com")]
        [InlineData("Josue@gmail.com")]
        [InlineData("Mateus@gmail.com")]
        [InlineData("Douglas@gmail.com")]
        [InlineData("Ana@gmail.com")]
        [InlineData("Victor@gmail.com")]
        [InlineData("Eduardo@gmail.com")]
        [InlineData("Julio@gmail.com")]
        public async Task Can_Get_an_User_By_Email_Asynchronously(string email)
        {
            var user = GetUsers().FirstOrDefault(x => x.Email == email);
            var userInDatabase = await _userRepository.GetByEmailAsync(email);

            Assert.NotNull(user);
            Assert.NotNull(userInDatabase);
            Assert.Equivalent(user, userInDatabase, true);
        }

        [Fact]
        public async Task Can_Add_an_User_With_Valid_Properties_Asynchronously()
        {
            const PasswordStrategy strategy = PasswordStrategy.BCrypt;

            var user = new User
            {
                Name = "Jesus",
                Email = "Jesus@gmail.com",
                PasswordHash = "ljYRhBNHu2",
                PasswordStrategy = strategy
            };

            await _userRepository.AddAsync(user);

            var userInDatabase = await _context.Users.FirstOrDefaultAsync(x => x.Id == user.Id);

            Assert.Equivalent(user, userInDatabase, true);
        }

        [Theory]
        [InlineData("teste", "teste@gmail.com", null)]
        [InlineData("teste", null, "12345678")]
        [InlineData(null, "teste@gmail.com", "12345678")]
        public async Task Cant_Add_an_User_With_Invalid_Properties_Asynchronously(string? name, string? email, string? password)
        {
            const PasswordStrategy strategy = PasswordStrategy.BCrypt;

            var user = new User
            {
                Name = name,
                Email = email,
                PasswordHash = password,
                PasswordStrategy = strategy
            };

            await Assert.ThrowsAsync<DbUpdateException>(async () => await _userRepository.AddAsync(user));
        }

        private static IEnumerable<User> GetUsers()
        {
            const PasswordStrategy strategy = PasswordStrategy.BCrypt;

            return [
                    new() { Id = 1, Name = "Lucas", Email = "Lucas@gmail.com", PasswordHash = "lngGsw5S", PasswordStrategy = strategy },
                    new() { Id = 2, Name = "Josué", Email = "Josue@gmail.com", PasswordHash = "K7wPzC7o", PasswordStrategy = strategy },
                    new() { Id = 3, Name = "Mateus", Email = "Mateus@gmail.com", PasswordHash = "bEMfTXVz", PasswordStrategy = strategy },
                    new() { Id = 4, Name = "Douglas", Email = "Douglas@gmail.com", PasswordHash = "APlhcwsW9dqu0yN3Hl5u", PasswordStrategy = strategy },
                    new() { Id = 5, Name = "Ana Carolina", Email = "Ana@gmail.com", PasswordHash = "fmzQxo9lSP41hmujCp6c", PasswordStrategy = strategy },
                    new() { Id = 6, Name = "Victor", Email = "Victor@gmail.com", PasswordHash = "tqA=Oj;dc2fSHAZ9", PasswordStrategy = strategy },
                    new() { Id = 7, Name = "Eduardo", Email = "Eduardo@gmail.com", PasswordHash = "=cZnI)CLW+%pam00{$rW=", PasswordStrategy = strategy },
                    new() { Id = 8, Name = "Júlio", Email = "Julio@gmail.com", PasswordHash = "r=V3^Na-Z", PasswordStrategy = strategy },
            ];
        }
    }
}
