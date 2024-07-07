using Tudo_List.Domain.Core.Interfaces.Factories;
using Tudo_List.Domain.Core.Interfaces.Strategies;
using Tudo_List.Domain.Enums;
using Tudo_List.Domain.Services.Strategies;

namespace Tudo_List.Domain.Services.Factories
{
    public class PasswordStrategyFactory : IPasswordStrategyFactory
    {
        public IPasswordStrategy CreatePasswordStrategy(PasswordStrategy strategy)
        {
            return strategy switch
            {
                PasswordStrategy.BCrypt => new BCryptPasswordStrategy(),
                _ => throw new ArgumentException("Invalid Password Strategy!")
            };
        }
    }
}
