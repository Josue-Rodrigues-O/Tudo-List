using Tudo_List.Domain.Core.Interfaces.Repositories;
using Tudo_List.Domain.Core.Interfaces.Validation;

namespace Tudo_List.Domain.Services.Validation
{
    public class UserValidator(IUserRepository userRepository) : IUserValidator
    {
        private readonly IUserRepository _userRepository = userRepository;
    }
}
