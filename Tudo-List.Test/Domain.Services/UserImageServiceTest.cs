using Microsoft.Extensions.DependencyInjection;
using Tudo_List.Domain.Core.Interfaces.Services;

namespace Tudo_List.Test.Domain.Services
{
    public class UserImageServiceTest : UnitTest
    {
        private readonly IUserImageService _userImageService;

        public UserImageServiceTest()
        {
            _userImageService = _serviceProvider.GetRequiredService<IUserImageService>();
        }
    }
}
