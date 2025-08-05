using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tudo_List.Application.Dtos.User;
using Tudo_List.Application.Interfaces.Applications;
using Tudo_List.Domain.Core.Interfaces.Factories;
using Tudo_List.Domain.Entities;
using Tudo_List.Domain.Enums;
using Tudo_List.Domain.Exceptions;
using Tudo_List.Test.Mock;

namespace Tudo_List.Test.Application
{
    public class UserApplicationTest : UnitTest
    {
        private readonly IUserApplication _userApplication;
        private readonly IPasswordStrategyFactory _passwordStrategyFactory;

        private static User CurrentUser => MockData.GetCurrentUser();

        public UserApplicationTest()
        {
            _userApplication = _serviceProvider.GetRequiredService<IUserApplication>();
            _passwordStrategyFactory = _serviceProvider.GetRequiredService<IPasswordStrategyFactory>();

            SaveInMemoryDatabase(MockData.GetUsers());
        }

        [Fact]
        public void Can_Get_All_Users_Synchronously()
        {
            var users = MockData.GetUsers();

            var usersInDatabase = _userApplication.GetAll();

            Assert.Equal(users.Count, usersInDatabase.Count());

            for (int i = 0; i < users.Count; i++)
            {
                var user = users[i];
                var userInDatabase = usersInDatabase.ElementAt(i);

                Assert.Equal(user.Id, userInDatabase.Id);
                Assert.Equal(user.Name, userInDatabase.Name);
                Assert.Equal(user.Email, userInDatabase.Email);
            }
        }

        [Fact]
        public void Can_Get_an_User_By_Id_Synchronously()
        {
            var usersIds = MockData.GetUsers().Select(x => x.Id);

            foreach (var id in usersIds)
            {
                var user = MockData.GetUsers().First(x => x.Id == id);
                var userInDatabase = _userApplication.GetById(id);

                Assert.NotNull(userInDatabase);
                Assert.Equal(user.Id, userInDatabase.Id);
                Assert.Equal(user.Name, userInDatabase.Name);
                Assert.Equal(user.Email, userInDatabase.Email);
            }
        }

        [Fact]
        public void Should_Throw_EntityNotFoundException_When_Trying_To_Find_Inexistent_User_By_Id_Synchronously()
        {
            Assert.Throws<EntityNotFoundException>(() => _userApplication.GetById(120));
        }

        [Fact]
        public void Can_Get_an_User_By_Email_Synchronously()
        {
            var usersEmails = MockData.GetUsers().Select(x => x.Email);

            foreach (var email in usersEmails)
            {
                var user = MockData.GetUsers().First(x => x.Email == email);
                var userInDatabase = _userApplication.GetByEmail(email);

                Assert.NotNull(userInDatabase);
                Assert.Equal(user.Id, userInDatabase.Id);
                Assert.Equal(user.Name, userInDatabase.Name);
                Assert.Equal(user.Email, userInDatabase.Email);
            }
        }

        [Fact]
        public async Task Should_Throw_EntityNotFoundException_When_Trying_To_Find_Inexistent_User_By_Email_Synchronously()
        {
            await Assert.ThrowsAsync<EntityNotFoundException>(() => _userApplication.GetByEmailAsync("invalid@invalid.com"));
        }

        [Fact]
        public void Can_Register_an_User_With_Valid_Properties_Synchronously()
        {
            var registerUserDto = new RegisterUserDto
            (
                Name: "Jesus",
                Email: "Jesus@gmail.com",
                Password: "12345678",
                ConfirmPassword: "12345678"
            );

            _userApplication.Register(registerUserDto);

            var userInDatabase = _context.Users.FirstOrDefault(x => x.Email == registerUserDto.Email);

            Assert.NotNull(userInDatabase);
            Assert.NotNull(userInDatabase.PasswordHash);

            Assert.NotEqual(registerUserDto.Password, userInDatabase.PasswordHash);
            Assert.Equal(registerUserDto.Name, userInDatabase.Name);
            Assert.Equal(registerUserDto.Email, userInDatabase.Email);
        }

        [Theory]
        [InlineData("teste", "teste@gmail.com", "12345678", null)]
        [InlineData("teste", "teste@gmail.com", null, "12345678")]
        [InlineData("teste", null, "12345678", "12345678")]
        [InlineData(null, "teste@gmail.com", "12345678", "12345678")]
        public void Cant_Register_an_User_With_Invalid_Properties_Synchronously(string? name, string? email, string? password, string? confirmPassword)
        {
            var registerUserDto = new RegisterUserDto
            (
                Name: name!,
                Email: email!,
                Password: password!,
                ConfirmPassword: confirmPassword!
            );

            Assert.Throws<ValidationException>(() => _userApplication.Register(registerUserDto));
        }

        [Fact]
        public void Can_Update_an_User_Name_Synchronously()
        {
            const string newName = "UpdateTest";

            SaveInMemoryDatabase(CurrentUser);

            var updateUserDto = new UpdateUserDto
            (
                UserId: CurrentUser.Id,
                NewName: newName
            );

            _userApplication.Update(updateUserDto);

            var userInDatabase = _context.Users.Find(CurrentUser.Id);

            Assert.Equal(newName, userInDatabase!.Name);
        }
        
        [Fact]
        public void Cant_Update_Another_User_Name_Synchronously()
        {
            const string newName = "UpdateTest";

            SaveInMemoryDatabase(CurrentUser);

            var updateUserDto = new UpdateUserDto
            (
                UserId: 10,
                NewName: newName
            );

            Assert.Throws<UnauthorizedAccessException>(() => _userApplication.Update(updateUserDto));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("A")]
        public void Cant_Update_an_User_Name_With_Invalid_Value_Synchronously(string invalidName)
        {
            SaveInMemoryDatabase(CurrentUser);

            var updateUserDto = new UpdateUserDto
            (
                UserId: CurrentUser.Id,
                NewName: invalidName
            );

            Assert.Throws<ValidationException>(() => _userApplication.Update(updateUserDto));
        }

        [Fact]
        public void Cant_Update_an_User_Name_With_His_Own_Name_Synchronously()
        {
            SaveInMemoryDatabase(CurrentUser);

            var updateUserDto = new UpdateUserDto
            (
                UserId: CurrentUser.Id,
                NewName: CurrentUser.Name
            );

            Assert.Throws<InvalidOperationException>(() => _userApplication.Update(updateUserDto));
        }

        [Fact]
        public void Can_Update_an_User_Email_Synchronously()
        {
            const string newEmail = "UpdateEmailTest@gmail.com";

            SaveInMemoryDatabase(CurrentUser);

            var updateEmailDto = new UpdateEmailDto
            (
                UserId: CurrentUser.Id,
                NewEmail: newEmail,
                CurrentPassword: MockData.GetCurrentUserPassword()
            );

            _userApplication.UpdateEmail(updateEmailDto);

            var userInDatabase = _context.Users.Find(CurrentUser.Id);

            Assert.Equal(newEmail, userInDatabase!.Email);
        }

        [Fact]
        public void Cant_Update_Another_User_Email_Synchronously()
        {
            const string newEmail = "UpdateEmailTest@gmail.com";

            SaveInMemoryDatabase(CurrentUser);

            var updateEmailDto = new UpdateEmailDto
            (
                UserId: 10,
                NewEmail: newEmail,
                CurrentPassword: MockData.GetCurrentUserPassword()
            );

            Assert.Throws<UnauthorizedAccessException>(() => _userApplication.UpdateEmail(updateEmailDto));
        }

        [Fact]
        public void Cant_Update_an_User_Email_With_Wrong_Password_Synchronously()
        {
            const string newEmail = "UpdateEmailTest@gmail.com";

            SaveInMemoryDatabase(CurrentUser);

            var updateEmailDto = new UpdateEmailDto
            (
                UserId: CurrentUser.Id,
                NewEmail: newEmail,
                CurrentPassword: "WrongPassword123"
            );

            Assert.Throws<ValidationException>(() => _userApplication.UpdateEmail(updateEmailDto));
        }

        [Fact]
        public void Cant_Update_an_User_Email_With_Same_Email_Synchronously()
        {
            SaveInMemoryDatabase(CurrentUser);

            var updateEmailDto = new UpdateEmailDto
            (
                UserId: CurrentUser.Id,
                NewEmail: CurrentUser.Email,
                CurrentPassword: MockData.GetCurrentUserPassword()
            );

            Assert.Throws<ValidationException>(() => _userApplication.UpdateEmail(updateEmailDto));
        }

        [Fact]
        public void Cant_Update_an_User_Email_With_Invalid_Value_Synchronously()
        {
            const string newEmail = "UpdateEmailTest@gmailcom";

            SaveInMemoryDatabase(CurrentUser);

            var updateEmailDto = new UpdateEmailDto
            (
                UserId: CurrentUser.Id,
                NewEmail: newEmail,
                CurrentPassword: MockData.GetCurrentUserPassword()
            );

            Assert.Throws<ValidationException>(() => _userApplication.UpdateEmail(updateEmailDto));
        }

        [Fact]
        public void Can_Update_an_User_Password_Synchronously()
        {
            const string newPassword = "UpdatePassword123";

            SaveInMemoryDatabase(CurrentUser);

            var updateUserDto = new UpdatePasswordDto
            (
                UserId: CurrentUser.Id,
                NewPassword: newPassword,
                ConfirmNewPassword: newPassword,
                CurrentPassword: MockData.GetCurrentUserPassword()
            );

            _userApplication.UpdatePassword(updateUserDto);

            var userInDatabase = _context.Users.Find(CurrentUser.Id);

            Assert.NotEqual(CurrentUser.PasswordHash, userInDatabase!.PasswordHash);
        }

        [Fact]
        public void Cant_Update_an_User_Password_With_Invalid_Value_Synchronously()
        {
            const string invalidPassword = "1234567";

            var strategy = _passwordStrategyFactory.CreatePasswordStrategy(PasswordStrategy.BCrypt);

            SaveInMemoryDatabase(CurrentUser);

            var updateUserDto = new UpdatePasswordDto
            (
                UserId: CurrentUser.Id,
                NewPassword: invalidPassword,
                CurrentPassword: MockData.GetCurrentUserPassword(),
                ConfirmNewPassword: MockData.GetCurrentUserPassword()
            );

            Assert.Throws<ValidationException>(() => _userApplication.UpdatePassword(updateUserDto));
        }

        [Fact]
        public void Cant_Update_an_User_Password_With_The_Same_Value_Synchronously()
        {
            SaveInMemoryDatabase(CurrentUser);

            var updateUserDto = new UpdatePasswordDto
            (
                UserId: CurrentUser.Id,
                NewPassword: MockData.GetCurrentUserPassword(),
                CurrentPassword: MockData.GetCurrentUserPassword(),
                ConfirmNewPassword: MockData.GetCurrentUserPassword()
            );

            Assert.Throws<ValidationException>(() => _userApplication.UpdatePassword(updateUserDto));
        }
        
        [Fact]
        public void Cant_Update_Another_User_Password_Synchronously()
        {
            SaveInMemoryDatabase(CurrentUser);

            var updateUserDto = new UpdatePasswordDto
            (
                UserId: 10,
                NewPassword: "NewPassword123",
                ConfirmNewPassword: "NewPassword123",
                CurrentPassword: MockData.GetCurrentUserPassword()
            );

            Assert.Throws<UnauthorizedAccessException>(() => _userApplication.UpdatePassword(updateUserDto));
        }

        [Fact]
        public void Can_Delete_An_User_Synchronously()
        {
            SaveInMemoryDatabase(CurrentUser);

            _userApplication.Delete(CurrentUser.Id);

            var userInDatabase = _context.Users.Find(CurrentUser.Id);

            Assert.Null(userInDatabase);
        }

        [Fact]
        public void Cant_Delete_Another_User_Synchronously()
        {
            var user = new User
            {
                Name = "RemoveTest",
                Email = "RemoveTest@gmail.com",
                PasswordHash = "lngGsw5S234",
                PasswordStrategy = PasswordStrategy.BCrypt
            };

            SaveInMemoryDatabase(user);

            Assert.Throws<UnauthorizedAccessException>(() => _userApplication.Delete(user.Id));
        }

        [Fact]
        public async Task Can_Get_All_Users_Asynchronously()
        {
            var users = MockData.GetUsers();

            var usersInDatabase = await _userApplication.GetAllAsync();

            Assert.Equivalent(users.Count, usersInDatabase.Count());

            for (int i = 0; i < users.Count; i++)
            {
                var user = users[i];
                var userInDatabase = usersInDatabase.ElementAt(i);

                Assert.Equal(user.Id, userInDatabase.Id);
                Assert.Equal(user.Name, userInDatabase.Name);
                Assert.Equal(user.Email, userInDatabase.Email);
            }
        }

        [Fact]
        public async Task Can_Get_an_User_By_Id_Asynchronously()
        {
            var usersIds = MockData.GetUsers().Select(x => x.Id);

            foreach (var id in usersIds)
            {
                var user = MockData.GetUsers().First(x => x.Id == id);
                var userInDatabase = await _userApplication.GetByIdAsync(id);

                Assert.NotNull(userInDatabase);
                Assert.Equal(user.Id, userInDatabase.Id);
                Assert.Equal(user.Name, userInDatabase.Name);
                Assert.Equal(user.Email, userInDatabase.Email);
            }
        }

        [Fact]
        public async Task Should_Throw_EntityNotFoundException_When_Trying_To_Find_Inexistent_User_By_Id_Asynchronously()
        {
            await Assert.ThrowsAsync<EntityNotFoundException>(() => _userApplication.GetByIdAsync(120));
        }

        [Fact]
        public async Task Can_Get_an_User_By_Email_Asynchronously()
        {
            var usersEmails = MockData.GetUsers().Select(x => x.Email);

            foreach (var email in usersEmails)
            {
                var user = MockData.GetUsers().First(x => x.Email == email);
                var userInDatabase = await _userApplication.GetByEmailAsync(email);

                Assert.NotNull(userInDatabase);
                Assert.Equal(user.Id, userInDatabase.Id);
                Assert.Equal(user.Name, userInDatabase.Name);
                Assert.Equal(user.Email, userInDatabase.Email);
            }
        }

        [Fact]
        public async Task Should_Throw_EntityNotFoundException_When_Trying_To_Find_Inexistent_User_By_Email_Asynchronously()
        {
            await Assert.ThrowsAsync<EntityNotFoundException>(() => _userApplication.GetByEmailAsync("invalid@invalid.com"));
        }

        [Fact]
        public async Task Can_Register_an_User_With_Valid_Properties_Asynchronously()
        {
            var registerUserDto = new RegisterUserDto
            (
                Name: "Jesus",
                Email: "Jesus@gmail.com",
                Password: "12345678",
                ConfirmPassword: "12345678"
            );

            await _userApplication.RegisterAsync(registerUserDto);

            var userInDatabase = await _context.Users.FirstOrDefaultAsync(x => x.Email == registerUserDto.Email);

            Assert.NotNull(userInDatabase);
            Assert.NotNull(userInDatabase.PasswordHash);

            Assert.NotEqual(registerUserDto.Password, userInDatabase.PasswordHash);
            Assert.Equal(registerUserDto.Name, userInDatabase.Name);
            Assert.Equal(registerUserDto.Email, userInDatabase.Email);
        }

        [Theory]
        [InlineData("teste", "teste@gmail.com", "12345678", null)]
        [InlineData("teste", "teste@gmail.com", null, "12345678")]
        [InlineData("teste", null, "12345678", "12345678")]
        [InlineData(null, "teste@gmail.com", "12345678", "12345678")]
        public async Task Cant_Register_an_User_With_Invalid_Properties_Asynchronously(string? name, string? email, string? password, string? confirmPassword)
        {
            var registerUserDto = new RegisterUserDto
            (
                Name: name!,
                Email: email!,
                Password: password!,
                ConfirmPassword: confirmPassword!
            );

            await Assert.ThrowsAsync<ValidationException>(() => _userApplication.RegisterAsync(registerUserDto));
        }

        [Fact]
        public async Task Can_Update_an_User_Name_Asynchronously()
        {
            const string newName = "UpdateTest";

            SaveInMemoryDatabase(CurrentUser);

            var updateUserDto = new UpdateUserDto
            (
                UserId: CurrentUser.Id,
                NewName: newName
            );

            await _userApplication.UpdateAsync(updateUserDto);

            var userInDatabase = await _context.Users.FindAsync(CurrentUser.Id);

            Assert.Equal(newName, userInDatabase!.Name);
        }

        [Fact]
        public async Task Cant_Update_Another_User_Name_Asynchronously()
        {
            const string newName = "UpdateTest";

            SaveInMemoryDatabase(CurrentUser);

            var updateUserDto = new UpdateUserDto
            (
                UserId: 10,
                NewName: newName
            );

            await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _userApplication.UpdateAsync(updateUserDto));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("A")]
        public async Task Cant_Update_an_User_Name_With_Invalid_Value_Asynchronously(string? invalidName)
        {
            SaveInMemoryDatabase(CurrentUser);

            var updateUserDto = new UpdateUserDto
            (
                UserId: CurrentUser.Id,
                NewName: invalidName
            );

            await Assert.ThrowsAsync<ValidationException>(() => _userApplication.UpdateAsync(updateUserDto));
        }

        [Fact]
        public async Task Cant_Update_an_User_Name_With_His_Own_Name_Asynchronously()
        {
            SaveInMemoryDatabase(CurrentUser);

            var updateUserDto = new UpdateUserDto
            (
                UserId: CurrentUser.Id,
                NewName: CurrentUser.Name
            );

            await Assert.ThrowsAsync<InvalidOperationException>(() => _userApplication.UpdateAsync(updateUserDto));
        }

        [Fact]
        public async Task Can_Update_an_User_Email_Asynchronously()
        {
            const string newEmail = "UpdateEmailTest@gmail.com";

            SaveInMemoryDatabase(CurrentUser);

            var updateEmailDto = new UpdateEmailDto
            (
                UserId: CurrentUser.Id,
                NewEmail: newEmail,
                CurrentPassword: MockData.GetCurrentUserPassword()
            );

            await _userApplication.UpdateEmailAsync(updateEmailDto);

            var userInDatabase = _context.Users.Find(CurrentUser.Id);

            Assert.Equal(newEmail, userInDatabase!.Email);
        }

        [Fact]
        public async Task Cant_Update_Another_User_Email_Asynchronously()
        {
            const string newEmail = "UpdateEmailTest@gmail.com";

            SaveInMemoryDatabase(CurrentUser);

            var updateEmailDto = new UpdateEmailDto
            (
                UserId: 10,
                NewEmail: newEmail,
                CurrentPassword: MockData.GetCurrentUserPassword()
            );

            await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _userApplication.UpdateEmailAsync(updateEmailDto));
        }

        [Fact]
        public async Task Cant_Update_an_User_Email_With_Wrong_Password_Asynchronously()
        {
            const string newEmail = "UpdateEmailTest@gmail.com";

            SaveInMemoryDatabase(CurrentUser);

            var updateEmailDto = new UpdateEmailDto
            (
                UserId: CurrentUser.Id,
                NewEmail: newEmail,
                CurrentPassword: "WrongPassword123"
            );

            await Assert.ThrowsAsync<ValidationException>(() => _userApplication.UpdateEmailAsync(updateEmailDto));
        }

        [Fact]
        public async Task Cant_Update_an_User_Email_With_Same_Email_Asynchronously()
        {
            SaveInMemoryDatabase(CurrentUser);

            var updateEmailDto = new UpdateEmailDto
            (
                UserId: CurrentUser.Id,
                NewEmail: CurrentUser.Email,
                CurrentPassword: MockData.GetCurrentUserPassword()
            );

            await Assert.ThrowsAsync<ValidationException>(() => _userApplication.UpdateEmailAsync(updateEmailDto));
        }

        [Fact]
        public async Task Cant_Update_an_User_Email_With_Invalid_Value_Asynchronously()
        {
            const string newEmail = "UpdateEmailTest@gmailcom";

            SaveInMemoryDatabase(CurrentUser);

            var updateEmailDto = new UpdateEmailDto
            (
                UserId: CurrentUser.Id,
                NewEmail: newEmail,
                CurrentPassword: MockData.GetCurrentUserPassword()
            );

            await Assert.ThrowsAsync<ValidationException>(() => _userApplication.UpdateEmailAsync(updateEmailDto));
        }

        [Fact]
        public async Task Can_Update_an_User_Password_Asynchronously()
        {
            const string newPassword = "UpdatePassword123";

            SaveInMemoryDatabase(CurrentUser);

            var updateUserDto = new UpdatePasswordDto
            (
                UserId: CurrentUser.Id,
                NewPassword: newPassword,
                ConfirmNewPassword: newPassword,
                CurrentPassword: MockData.GetCurrentUserPassword()
            );

            await _userApplication.UpdatePasswordAsync(updateUserDto);

            var userInDatabase = _context.Users.Find(CurrentUser.Id);

            Assert.NotEqual(CurrentUser.PasswordHash, userInDatabase!.PasswordHash);
        }

        [Fact]
        public async Task Cant_Update_an_User_Password_With_Invalid_Value_Asynchronously()
        {
            const string invalidPassword = "1234567";

            var strategy = _passwordStrategyFactory.CreatePasswordStrategy(PasswordStrategy.BCrypt);

            SaveInMemoryDatabase(CurrentUser);

            var updateUserDto = new UpdatePasswordDto
            (
                UserId: CurrentUser.Id,
                NewPassword: invalidPassword,
                CurrentPassword: MockData.GetCurrentUserPassword(),
                ConfirmNewPassword: MockData.GetCurrentUserPassword()
            );

            await Assert.ThrowsAsync<ValidationException>(() => _userApplication.UpdatePasswordAsync(updateUserDto));
        }

        [Fact]
        public async Task Cant_Update_an_User_Password_With_The_Same_Value_Asynchronously()
        {
            SaveInMemoryDatabase(CurrentUser);

            var updateUserDto = new UpdatePasswordDto
            (
                UserId: CurrentUser.Id,
                NewPassword: MockData.GetCurrentUserPassword(),
                CurrentPassword: MockData.GetCurrentUserPassword(),
                ConfirmNewPassword: MockData.GetCurrentUserPassword()
            );

            await Assert.ThrowsAsync<ValidationException>(() => _userApplication.UpdatePasswordAsync(updateUserDto));
        }

        [Fact]
        public async Task Cant_Update_Another_User_Password_Asynchronously()
        {
            SaveInMemoryDatabase(CurrentUser);

            var updateUserDto = new UpdatePasswordDto
            (
                UserId: 10,
                NewPassword: "NewPassword123",
                ConfirmNewPassword: "NewPassword123",
                CurrentPassword: MockData.GetCurrentUserPassword()
            );

            await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _userApplication.UpdatePasswordAsync(updateUserDto));
        }

        [Fact]
        public async Task Can_Delete_An_User_Asynchronously()
        {
            SaveInMemoryDatabase(CurrentUser);

            await _userApplication.DeleteAsync(CurrentUser.Id);

            var userInDatabase = await _context.Users.FindAsync(CurrentUser.Id);

            Assert.Null(userInDatabase);
        }

        [Fact]
        public async Task Cant_Delete_Another_User_Asynchronously()
        {
            var user = new User
            {
                Name = "RemoveTest",
                Email = "RemoveTest@gmail.com",
                PasswordHash = "lngGsw5S234",
                PasswordStrategy = PasswordStrategy.BCrypt
            };

            SaveInMemoryDatabase(user);

            await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _userApplication.DeleteAsync(user.Id));
        }
    }
}
