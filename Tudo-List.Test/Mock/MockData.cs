using System.Collections.Immutable;
using Tudo_List.Domain.Entities;
using Tudo_List.Domain.Enums;

namespace Tudo_List.Test.Mock
{
    internal class MockData
    {
        internal static User GetCurrentUser()
        {
            return new User()
            {
                Id = 404,
                Name = "UserMock",
                Email = "UserMock@mock.com",
                PasswordHash = "$2a$10$.ZRZ1mQzd8XZwuVqtYd/4OKAaw03lU42dPTIu7u7WRhYHLq1pazhW",
                PasswordStrategy = PasswordStrategy.BCrypt,
                Salt = null
            };
        }

        internal static string GetCurrentUserPassword()
        {
            return "12345678";
        }

        internal static IImmutableList<TodoListItem> GetItems()
        {
            var currentUserId = GetCurrentUser().Id;

            return [
                new() { Id = Guid.NewGuid(), Title = "Do the dishes", CreationDate = DateTime.Now, Status = Status.Completed, Priority = Priority.Low, UserId = currentUserId },
                new() { Id = Guid.NewGuid(), Title = "Clean the house", CreationDate = DateTime.Now, Status = Status.Completed, Priority = Priority.Low, UserId = currentUserId },
                new() { Id = Guid.NewGuid(), Title = "Wash the car", CreationDate = DateTime.Now, Status = Status.Completed, Priority = Priority.Low, UserId = currentUserId },
                new() { Id = Guid.NewGuid(), Title = "Watch Death Note", CreationDate = DateTime.Now, Status = Status.Completed, Priority = Priority.Low, UserId = 3 },
            ];
        }

        internal static IImmutableList<User> GetUsers()
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
