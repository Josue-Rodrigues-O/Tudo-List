using Tudo_List.Domain.Helpers;

namespace Tudo_List.Test.Domain.Helpers
{
    public class StringHelperTest : UnitTest
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Should_Return_False_When_I_Verify_If_Contains_String_With_Invalid_Values(string? value)
        {
            Assert.False(value.ContainsValue());
        }

        [Fact]
        public void Should_Return_True_When_I_Verify_If_Contains_String_With_Valid_Value()
        {
            const string value = "Test";
            Assert.True(value.ContainsValue());
        }

        [Theory]
        [InlineData("Manual", 7, 10)]
        [InlineData("123", 5, 6)]
        [InlineData("Test", 10, 15)]
        [InlineData(null, 10, 15)]
        public void Should_Return_False_When_Verifying_Length_Between_With_Invalid_Values(string? value, int minLength, int maxLength)
        {
            Assert.False(value.IsLengthBetween(minLength, maxLength));
        }
        
        [Theory]
        [InlineData("Manual", 1, 6)]
        [InlineData("123", 3, 4)]
        [InlineData("Test", 2, 10)]
        public void Should_Return_True_When_Verifying_Length_Between_With_Valid_Values(string? value, int minLength, int maxLength)
        {
            Assert.True(value.IsLengthBetween(minLength, maxLength));
        }

        [Theory]
        [InlineData("    The    String       Is Trimmed ", "The String Is Trimmed")]
        [InlineData("Test  2 ", "Test 2")]
        [InlineData("Test 3", "Test 3")]
        [InlineData(" Test 4 ", "Test 4")]
        [InlineData(null, "")]
        public void Should_Return_Trimmed_String_With_No_More_Than_One_Space_In_The_Middle(string? rawValue, string formattedValue)
        {
            Assert.Equal(formattedValue, rawValue.TrimAndCondenseSpaces());
        }
    }
}
