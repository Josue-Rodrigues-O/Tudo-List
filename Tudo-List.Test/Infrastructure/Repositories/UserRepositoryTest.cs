using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tudo_List.Domain.Core.Interfaces.Repositories;
using Tudo_List.Domain.Entities;
using Tudo_List.Domain.Enums;
using Tudo_List.Test.Mock;

namespace Tudo_List.Test.Infrastructure.Repositories
{
    public class UserRepositoryTest : UnitTest
    {
        private readonly IUserRepository _userRepository;

        public UserRepositoryTest()
        {
            _userRepository = _serviceProvider.GetRequiredService<IUserRepository>();
            InitializeInMemoryDatabase(MockData.GetUsers());
        }

        [Fact]
        public void Can_Get_All_Users_Synchronously()
        {
            var users = MockData.GetUsers();

            var usersInDatabase = _userRepository.GetAll();

            Assert.Equivalent(users.Count, usersInDatabase.Count());
        }

        [Fact]
        public void Can_Get_an_User_By_Id_Synchronously()
        {
            var usersIds = MockData.GetUsers().Select(x => x.Id);

            foreach (var id in usersIds)
            {
                var user = MockData.GetUsers().First(x => x.Id == id);
                var userInDatabase = _userRepository.GetById(id);

                Assert.NotNull(userInDatabase);
                Assert.Equivalent(user, userInDatabase, true);
            }
        }

        [Fact]
        public void Can_Get_an_User_By_Email_Synchronously()
        {
            var usersEmails = MockData.GetUsers().Select(x => x.Email);

            foreach (var email in usersEmails)
            {
                var user = MockData.GetUsers().First(x => x.Email == email);
                var userInDatabase = _userRepository.GetByEmail(email);

                Assert.NotNull(userInDatabase);
                Assert.Equivalent(user, userInDatabase, true);
            }
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
        public void Can_Update_an_User_Synchronously()
        {
            var user = new User
            {
                Name = "Update",
                Email = "UpdateTest@gmail.com",
                PasswordHash = "lngGsw5S234",
                PasswordStrategy = PasswordStrategy.BCrypt
            };

            _context.Add(user);
            _context.SaveChanges();

            user.Name = "UpdateTest";

            _userRepository.Update(user);

            var userInDatabase = _context.Users.Find(user.Id);

            Assert.Equal(user.Name, userInDatabase?.Name);
        }

        [Fact]
        public void Can_Remove_An_User_Synchronously()
        {
            var user = new User
            {
                Name = "RemoveTest",
                Email = "RemoveTest@gmail.com",
                PasswordHash = "lngGsw5S234",
                PasswordStrategy = PasswordStrategy.BCrypt
            };

            _context.Add(user);
            _context.SaveChanges();

            _userRepository.Remove(user);

            var userInDatabase = _context.Users.Find(user.Id);

            Assert.Null(userInDatabase);
        }

        [Fact]
        public void Cant_Remove_An_Non_Existent_User_Synchronously()
        {
            var user = new User
            {
                Name = "RemoveTest",
                Email = "RemoveTest@gmail.com",
                PasswordHash = "lngGsw5S234",
                PasswordStrategy = PasswordStrategy.BCrypt
            };

            Assert.Throws<DbUpdateConcurrencyException>(() => _userRepository.Remove(user));
        }

        [Fact]
        public async Task Can_Get_All_Users_Asynchronously()
        {
            var users = MockData.GetUsers();

            var usersInDatabase = await _userRepository.GetAllAsync();

            Assert.Equivalent(users.Count, usersInDatabase.Count());
        }

        [Fact]
        public async Task Can_Get_an_User_By_Id_Asynchronously()
        {
            var usersIds = MockData.GetUsers().Select(x => x.Id);

            foreach (var id in usersIds)
            {
                var user = MockData.GetUsers().First(x => x.Id == id);
                var userInDatabase = await _userRepository.GetByIdAsync(id);

                Assert.NotNull(userInDatabase);
                Assert.Equivalent(user, userInDatabase, true);
            }
        }

        [Fact]
        public async Task Can_Get_an_User_By_Email_Asynchronously()
        {
            var usersEmails = MockData.GetUsers().Select(x => x.Email);

            foreach (var email in usersEmails)
            {
                var user = MockData.GetUsers().First(x => x.Email == email);
                var userInDatabase = await _userRepository.GetByEmailAsync(email);

                Assert.NotNull(userInDatabase);
                Assert.Equivalent(user, userInDatabase, true);
            }
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

        [Fact]
        public async Task Can_Update_an_User_Asynchronously()
        {
            var user = new User
            {
                Name = "Update",
                Email = "UpdateTest@gmail.com",
                PasswordHash = "lngGsw5S234",
                PasswordStrategy = PasswordStrategy.BCrypt
            };

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            user.Name = "UpdateTest";

            await _userRepository.UpdateAsync(user);

            var userInDatabase = await _context.Users.FindAsync(user.Id);

            Assert.Equal(user.Name, userInDatabase?.Name);
        }

        [Fact]
        public async Task Can_Remove_An_User_Asynchronously()
        {
            var user = new User
            {
                Name = "RemoveTest",
                Email = "RemoveTest@gmail.com",
                PasswordHash = "lngGsw5S234",
                PasswordStrategy = PasswordStrategy.BCrypt
            };

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            await _userRepository.RemoveAsync(user);

            var userInDatabase = await _context.Users.FindAsync(user.Id);

            Assert.Null(userInDatabase);
        }

        [Fact]
        public async Task Cant_Remove_An_Non_Existent_User_Asynchronously()
        {
            var user = new User
            {
                Name = "RemoveTest",
                Email = "RemoveTest@gmail.com",
                PasswordHash = "lngGsw5S234",
                PasswordStrategy = PasswordStrategy.BCrypt
            };

            await Assert.ThrowsAsync<DbUpdateConcurrencyException>(async () => await _userRepository.RemoveAsync(user));
        }
    }
}
