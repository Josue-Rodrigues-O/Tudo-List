using System.Drawing;
using Tudo_List.Domain.Entities;
using Tudo_List.Domain.Enums;
using Tudo_List.Domain.Services;
using Tudo_List.Domain.Services.Strategies;

namespace Tudo_List.Test
{
    public class UnitTest
    {
        [Fact]
        public void Test()
        {
            ehVerdade(null);
        }

        private bool ehVerdade(string teste)
            => teste.Any(x => x == '1');
    }
}