using Tudo_List.Domain.Core.Interfaces.Strategies;
using Tudo_List.Domain.Enums;

namespace Tudo_List.Domain.Core.Interfaces.Factories
{
    public interface IPasswordStrategyFactory
    {
        IPasswordStrategy CreatePasswordStrategy(PasswordStrategyEnum strategy);
    }
}
