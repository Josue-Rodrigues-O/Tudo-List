using Tudo_List.Application.Interfaces.Services;
using Tudo_List.Domain.Core.Interfaces.Factories;
using Tudo_List.Domain.Core.Interfaces.Services;
using Tudo_List.Domain.Entities;
using Tudo_List.Domain.Exceptions;

namespace Tudo_List.Application.Services
{
    public class AuthService(IUserService userService, IPasswordStrategyFactory passwordStrategyFactory) : IAuthService
    {
        private readonly IUserService _userService = userService;
        private readonly IPasswordStrategyFactory _passwordStrategyFactory = passwordStrategyFactory;

        public bool CheckPassword(int userId, string password)
        {
            var user = _userService.GetById(userId)
                ?? throw new EntityNotFoundException(nameof(User), nameof(User.Id), userId);

            var passwordStrategy = _passwordStrategyFactory.CreatePasswordStrategy(user.PasswordStrategy);
            
            return passwordStrategy.VerifyPassword(password, user.PasswordHash, user.Salt);
        }
    }
}
