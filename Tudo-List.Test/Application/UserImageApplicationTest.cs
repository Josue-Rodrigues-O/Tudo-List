using Microsoft.Extensions.DependencyInjection;
using Tudo_List.Application.Interfaces.Applications;

namespace Tudo_List.Test.Application
{
    public class UserImageApplicationTest : UnitTest
    {
        private readonly IUserImageApplication _userImageApplication;

        public UserImageApplicationTest()
        {
            _userImageApplication = _serviceProvider.GetRequiredService<IUserImageApplication>();
        }
    }
}
