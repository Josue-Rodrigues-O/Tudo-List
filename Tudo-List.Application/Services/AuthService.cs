using Tudo_List.Application.Interfaces.Services;
using Tudo_List.Domain.Core.Interfaces.Factories;
using Tudo_List.Domain.Core.Interfaces.Services;
using Tudo_List.Domain.Entities;
using Tudo_List.Domain.Exceptions;

namespace Tudo_List.Application.Services
{
    public class AuthService(IUserService userService, IPasswordStrategyFactory passwordStrategyFactory) : IAuthService
    {
        public bool CheckPassword(int userId, string password)
        {
            var user = userService.GetById(userId)
                ?? throw new EntityNotFoundException(nameof(User), nameof(User.Id), userId);

            return passwordStrategyFactory
                .CreatePasswordStrategy(user.PasswordStrategy)
                .VerifyPassword(password, user.PasswordHash, user.Salt);
        }
    }
}
