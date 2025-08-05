using System.ComponentModel;
using Tudo_List.Domain.Helpers;

namespace Tudo_List.Test.Domain.Helpers
{
    public class EnumHelperTest : UnitTest
    {
        private const string TestDescription = "Test";

        private enum TestEnum
        {
            [Description(TestDescription)]
            Foo,
            NoDescriptionTest
        }

        [Fact]
        public void Can_Get_Enum_Description_If_It_Is_Defined()
        {
            Assert.Equal(TestDescription, TestEnum.Foo.GetDescription());
        }
        
        [Fact]
        public void Can_Get_Enum_in_String_When_Description_If_It_Is_Not_Defined()
        {
            Assert.Equal(TestEnum.NoDescriptionTest.ToString(), TestEnum.NoDescriptionTest.GetDescription());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void Can_Parse_Valid_Enum_Int_Value(int value)
        {
            Assert.Equal(typeof(TestEnum), value.AsEnum<TestEnum>().GetType());
        }

        [Fact]
        public void Cant_Parse_Invalid_Status_Int_Value()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => 5.AsEnum<TestEnum>());
        }
    }
}
