using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using Tudo_List.Domain.Core.Interfaces.Factories;
using Tudo_List.Domain.Enums;
using Tudo_List.Domain.Services.Strategies;

namespace Tudo_List.Test.Domain.Services.Factories
{
    public class PasswordStrategyFactoryTest : UnitTest
    {
        private readonly IPasswordStrategyFactory _passwordStrategyFactory;

        public PasswordStrategyFactoryTest()
        {
            _passwordStrategyFactory = _serviceProvider.GetRequiredService<IPasswordStrategyFactory>();
        }

        [Fact]
        public void Should_Return_BCryptPasswordStrategy_When_BCryptStrategy_Is_Used()
        {
            Assert.Equal(typeof(BCryptPasswordStrategy), _passwordStrategyFactory.CreatePasswordStrategy(PasswordStrategy.BCrypt).GetType());
        }

        [Fact]
        public void Should_Return_ArgumentException_When_Using_Invalid_Strategy_Value()
        {
            Assert.Throws<ArgumentException>(() => _passwordStrategyFactory.CreatePasswordStrategy((PasswordStrategy)100));
        }
    }
}
