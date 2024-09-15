using Tudo_List.Domain.Entities;
using Tudo_List.Domain.Enums;

namespace Tudo_List.Test.Mock
{
    internal class MockData
    {
        internal static User GetCurrentUserMock()
        {
            return new User()
            {
                Id = 404,
                Name = "UserMock",
                Email = "UserMock@mock.com",
                PasswordHash = "Mock123",
                PasswordStrategy = PasswordStrategy.BCrypt,
                Salt = null
            };
        }
    }
}
