using Microsoft.Extensions.DependencyInjection;
using Tudo_List.Domain.Core.Interfaces.Repositories;

namespace Tudo_List.Test.Infrastructure.Repositories
{
    public class UserImageRepository : UnitTest
    {
        private readonly IUserImageRepository _userImageRepository;

        public UserImageRepository()
        {
            _userImageRepository = _serviceProvider.GetRequiredService<IUserImageRepository>();
        }
    }
}
