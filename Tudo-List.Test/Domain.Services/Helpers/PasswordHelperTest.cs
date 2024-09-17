using Tudo_List.Domain.Services.Helpers;

namespace Tudo_List.Test.Domain.Services.Helpers
{
    public class PasswordHelperTest : UnitTest
    {
        [Theory]
        [InlineData(16)]
        [InlineData(32)]
        [InlineData(64)]
        [InlineData(128)]
        [InlineData(256)]
        [InlineData(512)]
        public void Should_Generate_Valid_Salt_With_Informed_Size(int length)
        {
            var salt = PasswordHelper.GenerateSalt(length);

            Assert.NotEmpty(salt);
            Assert.Equal(length, salt.Length);
        }
    }
}
