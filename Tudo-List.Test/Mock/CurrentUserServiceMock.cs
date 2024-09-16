using Tudo_List.Domain.Core.Interfaces.Services;
using Tudo_List.Domain.Entities;

namespace Tudo_List.Test.Mock
{
    internal class CurrentUserServiceMock : ICurrentUserService
    {
        private static User CurrentUserMock 
            => MockData.GetCurrentUser();

        public string Id => CurrentUserMock.Id.ToString();

        public string Name => CurrentUserMock.Name;

        public string Email => CurrentUserMock.Email;
    }
}
