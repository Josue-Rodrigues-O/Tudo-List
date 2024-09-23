using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tudo_List.Domain.Core.Interfaces.Factories;
using Tudo_List.Domain.Core.Interfaces.Services;
using Tudo_List.Domain.Entities;
using Tudo_List.Domain.Enums;
using Tudo_List.Domain.Exceptions;
using Tudo_List.Test.Mock;

namespace Tudo_List.Test.Domain.Services
{
    public class UserServiceTest : UnitTest
    {
        private readonly IUserService _userService;
        private readonly IPasswordStrategyFactory _passwordStrategyFactory;

        public UserServiceTest()
        {
            _userService = _serviceProvider.GetRequiredService<IUserService>();
            _passwordStrategyFactory = _serviceProvider.GetRequiredService<IPasswordStrategyFactory>(); 

            InitializeInMemoryDatabase(MockData.GetUsers());
        }

        [Fact]
        public void Can_Get_All_Users_Synchronously()
        {
            var users = MockData.GetUsers();

            var usersInDatabase = _userService.GetAll();

            Assert.Equivalent(users.Count, usersInDatabase.Count());
        }

        [Fact]
        public void Can_Get_an_User_By_Id_Synchronously()
        {
            var usersIds = MockData.GetUsers().Select(x => x.Id);

            foreach (var id in usersIds)
            {
                var user = MockData.GetUsers().First(x => x.Id == id);
                var userInDatabase = _userService.GetById(id);

                Assert.NotNull(userInDatabase);
                Assert.Equivalent(user, userInDatabase, true);
            }
        }

        [Fact]
        public void Should_Return_Null_When_Trying_To_Get_an_Inexistent_User_By_Id_Synchronously()
        {
            var user = _userService.GetById(120);

            Assert.Null(user);
        }

        [Fact]
        public void Can_Get_an_User_By_Email_Synchronously()
        {
            var usersEmails = MockData.GetUsers().Select(x => x.Email);

            foreach (var email in usersEmails)
            {
                var user = MockData.GetUsers().First(x => x.Email == email);
                var userInDatabase = _userService.GetByEmail(email);

                Assert.NotNull(userInDatabase);
                Assert.Equivalent(user, userInDatabase, true);
            }
        }

        [Fact]
        public void Should_Return_Null_When_Trying_To_Get_an_Inexistent_User_By_Email_Synchronously()
        {
            var user = _userService.GetByEmail("invalid@invalid.com");

            Assert.Null(user);
        }

        [Fact]
        public void Can_Register_an_User_With_Valid_Properties_Synchronously()
        {
            const PasswordStrategy strategy = PasswordStrategy.BCrypt;

            var user = new User
            {
                Name = "Jesus",
                Email = "Jesus@gmail.com",
            };

            _userService.Register(user, "12345678");

            var userInDatabase = _context.Users.FirstOrDefault(x => x.Id == user.Id);

            Assert.NotNull(userInDatabase);
            Assert.NotNull(user.PasswordHash);
            Assert.Equal(user.Name, userInDatabase.Name);
            Assert.Equal(user.Email, userInDatabase.Email);
        }

        [Theory]
        [InlineData("teste", "teste@gmail.com", null)]
        [InlineData("teste", null, "12345678")]
        [InlineData(null, "teste@gmail.com", "12345678")]
        public void Cant_Register_an_User_With_Invalid_Properties_Synchronously(string? name, string? email, string? password)
        {
            var user = new User
            {
                Name = name,
                Email = email,
            };

            Assert.Throws<ValidationException>(() => _userService.Register(user, password));
        }

        [Fact]
        public void Can_Update_an_User_Name_Synchronously()
        {
            const string newName = "UpdateTest";

            var user = new User
            {
                Name = "Update",
                Email = "UpdateTest@gmail.com",
                PasswordHash = "lngGsw5S234",
                PasswordStrategy = PasswordStrategy.BCrypt
            };

            _context.Add(user);
            _context.SaveChanges();

            _userService.Update(user.Id, newName);

            var userInDatabase = _context.Users.Find(user.Id);

            Assert.Equal(newName, userInDatabase.Name);
        }
        
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("A")]
        public void Cant_Update_an_User_Name_With_Invalid_Value_Synchronously(string? invalidName)
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

            Assert.Throws<ValidationException>(() => _userService.Update(user.Id, invalidName));
        }
        
        [Fact]
        public void Cant_Update_an_User_Name_With_His_Own_Name_Synchronously()
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

            Assert.Throws<InvalidOperationException>(() => _userService.Update(user.Id, user.Name));
        }

        [Fact]
        public void Can_Update_an_User_Email_Synchronously()
        {
            const string newEmail = "UpdateEmailTest@gmail.com";
            const string password = "lngGsw5S234";
            
            var strategy = _passwordStrategyFactory.CreatePasswordStrategy(PasswordStrategy.BCrypt);

            var user = new User
            {
                Name = "Update",
                Email = "UpdateTest@gmail.com",
                PasswordHash = strategy.HashPassword(password),
                PasswordStrategy = PasswordStrategy.BCrypt
            };

            _context.Add(user);
            _context.SaveChanges();

            _userService.UpdateEmail(user.Id, newEmail, password);

            var userInDatabase = _context.Users.Find(user.Id);

            Assert.Equal(newEmail, userInDatabase.Email);
        }
        
        [Fact]
        public void Cant_Update_an_User_Email_With_Wrong_Password_Synchronously()
        {
            const string newEmail = "UpdateEmailTest@gmail.com";
            const string password = "lngGsw5S234";
            
            var strategy = _passwordStrategyFactory.CreatePasswordStrategy(PasswordStrategy.BCrypt);

            var user = new User
            {
                Name = "Update",
                Email = "UpdateTest@gmail.com",
                PasswordHash = strategy.HashPassword(password),
                PasswordStrategy = PasswordStrategy.BCrypt
            };

            _context.Add(user);
            _context.SaveChanges();

            Assert.Throws<ValidationException>(() => _userService.UpdateEmail(user.Id, newEmail, password + "123"));
        }
        
        [Fact]
        public void Cant_Update_an_User_Email_With_Same_Email_Synchronously()
        {
            const string userEmail = "UpdateTest@gmail.com";
            const string password = "lngGsw5S234";
            
            var strategy = _passwordStrategyFactory.CreatePasswordStrategy(PasswordStrategy.BCrypt);

            var user = new User
            {
                Name = "Update",
                Email = userEmail,
                PasswordHash = strategy.HashPassword(password),
                PasswordStrategy = PasswordStrategy.BCrypt
            };

            _context.Add(user);
            _context.SaveChanges();

            Assert.Throws<ValidationException>(() => _userService.UpdateEmail(user.Id, userEmail, password + "123"));
        }

        [Fact]
        public void Cant_Update_an_User_Email_With_Invalid_Value_Synchronously()
        {
            const string newEmail = "UpdateEmailTest@gmailcom";
            const string password = "lngGsw5S234";

            var strategy = _passwordStrategyFactory.CreatePasswordStrategy(PasswordStrategy.BCrypt);

            var user = new User
            {
                Name = "Update",
                Email = "UpdateTest@gmail.com",
                PasswordHash = strategy.HashPassword(password),
                PasswordStrategy = PasswordStrategy.BCrypt
            };

            _context.Add(user);
            _context.SaveChanges();

            Assert.Throws<ValidationException>(() => _userService.UpdateEmail(user.Id, newEmail, password + "123"));
        }
        
        [Fact]
        public void Can_Update_an_User_Password_Synchronously()
        {
            const string currentPassword = "lngGsw5S234";
            const string newPassword = "12345678";

            var strategy = _passwordStrategyFactory.CreatePasswordStrategy(PasswordStrategy.BCrypt);

            var user = new User
            {
                Name = "Update",
                Email = "UpdateTest@gmail.com",
                PasswordHash = strategy.HashPassword(currentPassword),
                PasswordStrategy = PasswordStrategy.BCrypt
            };

            var oldPasswordHash = user.PasswordHash;

            _context.Add(user);
            _context.SaveChanges();

            _userService.UpdatePassword(user.Id, currentPassword, newPassword);

            var userInDatabase = _context.Users.Find(user.Id);

           Assert.NotEqual(oldPasswordHash, userInDatabase.PasswordHash);
        }

        [Fact]
        public void Cant_Update_an_User_Password_With_Invalid_Value_Synchronously()
        {
            const string currentPassword = "lngGsw5S234";
            const string invalidPassword = "1234567";

            var strategy = _passwordStrategyFactory.CreatePasswordStrategy(PasswordStrategy.BCrypt);

            var user = new User
            {
                Name = "Update",
                Email = "UpdateTest@gmail.com",
                PasswordHash = strategy.HashPassword(currentPassword),
                PasswordStrategy = PasswordStrategy.BCrypt
            };

            _context.Add(user);
            _context.SaveChanges();

            Assert.Throws<ValidationException>(() => _userService.UpdatePassword(user.Id, currentPassword, invalidPassword));
        }

        [Fact]
        public void Cant_Update_an_User_Password_With_The_Same_Value_Synchronously()
        {
            const string currentPassword = "lngGsw5S234";

            var strategy = _passwordStrategyFactory.CreatePasswordStrategy(PasswordStrategy.BCrypt);

            var user = new User
            {
                Name = "Update",
                Email = "UpdateTest@gmail.com",
                PasswordHash = strategy.HashPassword(currentPassword),
                PasswordStrategy = PasswordStrategy.BCrypt
            };

            _context.Add(user);
            _context.SaveChanges();

            Assert.Throws<ValidationException>(() => _userService.UpdatePassword(user.Id, currentPassword, currentPassword));
        }

        [Fact]
        public void Can_Delete_An_User_Synchronously()
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

            _userService.Delete(user.Id);

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

            Assert.Throws<EntityNotFoundException>(() => _userService.Delete(user.Id));
        }

        [Fact]
        public async Task Can_Get_All_Users_Asynchronously()
        {
            var users = MockData.GetUsers();

            var usersInDatabase = await _userService.GetAllAsync();

            Assert.Equivalent(users.Count, usersInDatabase.Count());
        }

        [Fact]
        public async Task Can_Get_an_User_By_Id_Asynchronously()
        {
            var usersIds = MockData.GetUsers().Select(x => x.Id);

            foreach (var id in usersIds)
            {
                var user = MockData.GetUsers().First(x => x.Id == id);
                var userInDatabase = await _userService.GetByIdAsync(id);

                Assert.NotNull(userInDatabase);
                Assert.Equivalent(user, userInDatabase, true);
            }
        }

        [Fact]
        public async Task Should_Return_Null_When_Trying_To_Get_an_Inexistent_User_By_Id_Asynchronously()
        {
            var user = await _userService.GetByIdAsync(120);

            Assert.Null(user);
        }

        [Fact]
        public async Task Can_Get_an_User_By_Email_Asynchronously()
        {
            var usersEmails = MockData.GetUsers().Select(x => x.Email);

            foreach (var email in usersEmails)
            {
                var user = MockData.GetUsers().First(x => x.Email == email);
                var userInDatabase = await _userService.GetByEmailAsync(email);

                Assert.NotNull(userInDatabase);
                Assert.Equivalent(user, userInDatabase, true);
            }
        }

        [Fact]
        public async Task Should_Return_Null_When_Trying_To_Get_an_Inexistent_User_By_Email_Asynchronously()
        {
            var user = await _userService.GetByEmailAsync("invalid@invalid.com");

            Assert.Null(user);
        }

        [Fact]
        public async Task Can_Register_an_User_With_Valid_Properties_Asynchronously()
        {
            const PasswordStrategy strategy = PasswordStrategy.BCrypt;

            var user = new User
            {
                Name = "Jesus",
                Email = "Jesus@gmail.com",
            };

            await _userService.RegisterAsync(user, "12345678");

            var userInDatabase = await _context.Users.FirstOrDefaultAsync(x => x.Id == user.Id);

            Assert.NotNull(userInDatabase);
            Assert.NotNull(user.PasswordHash);
            Assert.Equal(user.Name, userInDatabase.Name);
            Assert.Equal(user.Email, userInDatabase.Email);
        }

        [Theory]
        [InlineData("teste", "teste@gmail.com", null)]
        [InlineData("teste", null, "12345678")]
        [InlineData(null, "teste@gmail.com", "12345678")]
        public async Task Cant_Register_an_User_With_Invalid_Properties_Asynchronously(string? name, string? email, string? password)
        {
            var user = new User
            {
                Name = name,
                Email = email,
            };

            await Assert.ThrowsAsync<ValidationException>(() => _userService.RegisterAsync(user, password));
        }

        [Fact]
        public async Task Can_Update_an_User_Name_Asynchronously()
        {
            const string newName = "UpdateTest";

            var user = new User
            {
                Name = "Update",
                Email = "UpdateTest@gmail.com",
                PasswordHash = "lngGsw5S234",
                PasswordStrategy = PasswordStrategy.BCrypt
            };

            _context.Add(user);
            _context.SaveChanges();

            await _userService.UpdateAsync(user.Id, newName);

            var userInDatabase = await _context.Users.FindAsync(user.Id);

            Assert.Equal(newName, userInDatabase.Name);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("A")]
        public async Task Cant_Update_an_User_Name_With_Invalid_Value_Asynchronously(string? invalidName)
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

            await Assert.ThrowsAsync<ValidationException>(() => _userService.UpdateAsync(user.Id, invalidName));
        }

        [Fact]
        public async Task Cant_Update_an_User_Name_With_His_Own_Name_Asynchronously()
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

            await Assert.ThrowsAsync<InvalidOperationException>(() => _userService.UpdateAsync(user.Id, user.Name));
        }

        [Fact]
        public async Task Can_Update_an_User_Email_Asynchronously()
        {
            const string newEmail = "UpdateEmailTest@gmail.com";
            const string password = "lngGsw5S234";

            var strategy = _passwordStrategyFactory.CreatePasswordStrategy(PasswordStrategy.BCrypt);

            var user = new User
            {
                Name = "Update",
                Email = "UpdateTest@gmail.com",
                PasswordHash = strategy.HashPassword(password),
                PasswordStrategy = PasswordStrategy.BCrypt
            };

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            await _userService.UpdateEmailAsync(user.Id, newEmail, password);

            var userInDatabase = await _context.Users.FindAsync(user.Id);

            Assert.Equal(newEmail, userInDatabase.Email);
        }

        [Fact]
        public async Task Cant_Update_an_User_Email_With_Wrong_Password_Asynchronously()
        {
            const string newEmail = "UpdateEmailTest@gmail.com";
            const string password = "lngGsw5S234";

            var strategy = _passwordStrategyFactory.CreatePasswordStrategy(PasswordStrategy.BCrypt);

            var user = new User
            {
                Name = "Update",
                Email = "UpdateTest@gmail.com",
                PasswordHash = strategy.HashPassword(password),
                PasswordStrategy = PasswordStrategy.BCrypt
            };

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            await Assert.ThrowsAsync<ValidationException>(() => _userService.UpdateEmailAsync(user.Id, newEmail, password + "123"));
        }

        [Fact]
        public async Task Cant_Update_an_User_Email_With_Same_Email_Asynchronously()
        {
            const string userEmail = "UpdateTest@gmail.com";
            const string password = "lngGsw5S234";

            var strategy = _passwordStrategyFactory.CreatePasswordStrategy(PasswordStrategy.BCrypt);

            var user = new User
            {
                Name = "Update",
                Email = userEmail,
                PasswordHash = strategy.HashPassword(password),
                PasswordStrategy = PasswordStrategy.BCrypt
            };

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            await Assert.ThrowsAsync<ValidationException>(async() => await _userService.UpdateEmailAsync(user.Id, userEmail, password + "123"));
        }

        [Fact]
        public async Task Cant_Update_an_User_Email_With_Invalid_Value_Asynchronously()
        {
            const string newEmail = "UpdateEmailTest@gmailcom";
            const string password = "lngGsw5S234";

            var strategy = _passwordStrategyFactory.CreatePasswordStrategy(PasswordStrategy.BCrypt);

            var user = new User
            {
                Name = "Update",
                Email = "UpdateTest@gmail.com",
                PasswordHash = strategy.HashPassword(password),
                PasswordStrategy = PasswordStrategy.BCrypt
            };

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            await Assert.ThrowsAsync<ValidationException>(() => _userService.UpdateEmailAsync(user.Id, newEmail, password + "123"));
        }

        [Fact]
        public async Task Can_Update_an_User_Password_Asynchronously()
        {
            const string currentPassword = "lngGsw5S234";
            const string newPassword = "12345678";

            var strategy = _passwordStrategyFactory.CreatePasswordStrategy(PasswordStrategy.BCrypt);

            var user = new User
            {
                Name = "Update",
                Email = "UpdateTest@gmail.com",
                PasswordHash = strategy.HashPassword(currentPassword),
                PasswordStrategy = PasswordStrategy.BCrypt
            };

            var oldPasswordHash = user.PasswordHash;

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            await _userService.UpdatePasswordAsync(user.Id, currentPassword, newPassword);

            var userInDatabase = await _context.Users.FindAsync(user.Id);

            Assert.NotEqual(oldPasswordHash, userInDatabase.PasswordHash);
        }

        [Fact]
        public async Task Cant_Update_an_User_Password_With_Invalid_Value_Asynchronously()
        {
            const string currentPassword = "lngGsw5S234";
            const string invalidPassword = "1234567";

            var strategy = _passwordStrategyFactory.CreatePasswordStrategy(PasswordStrategy.BCrypt);

            var user = new User
            {
                Name = "Update",
                Email = "UpdateTest@gmail.com",
                PasswordHash = strategy.HashPassword(currentPassword),
                PasswordStrategy = PasswordStrategy.BCrypt
            };

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            await Assert.ThrowsAsync<ValidationException>(() => _userService.UpdatePasswordAsync(user.Id, currentPassword, invalidPassword));
        }

        [Fact]
        public async Task Cant_Update_an_User_Password_With_The_Same_Value_Asynchronously()
        {
            const string currentPassword = "lngGsw5S234";

            var strategy = _passwordStrategyFactory.CreatePasswordStrategy(PasswordStrategy.BCrypt);

            var user = new User
            {
                Name = "Update",
                Email = "UpdateTest@gmail.com",
                PasswordHash = strategy.HashPassword(currentPassword),
                PasswordStrategy = PasswordStrategy.BCrypt
            };

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            await Assert.ThrowsAsync<ValidationException>(() => _userService.UpdatePasswordAsync(user.Id, currentPassword, currentPassword));
        }

        [Fact]
        public async Task Can_Delete_An_User_Asynchronously()
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

            await _userService.DeleteAsync(user.Id);

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

            await Assert.ThrowsAsync<EntityNotFoundException>(() => _userService.DeleteAsync(user.Id));
        }
    }
}
