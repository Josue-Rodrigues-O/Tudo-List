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
            var description = TestEnum.Foo.GetDescription();

            Assert.Equal(TestDescription, description);
        }
        
        [Fact]
        public void Can_Get_Enum_in_String_When_Description_If_It_Is_Not_Defined()
        {
            var expectedDescription = TestEnum.NoDescriptionTest.ToString();
            var description = TestEnum.NoDescriptionTest.GetDescription();

            Assert.Equal(expectedDescription, description);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void Can_Parse_Valid_Enum_Int_Value(int value)
        {
            var status = value.AsEnum<TestEnum>();

            Assert.Equal(typeof(TestEnum), status.GetType());
        }

        [Fact]
        public void Cant_Parse_Invalid_Status_Int_Value()
        {
            const int invalidValue = 5;

            Assert.Throws<ArgumentOutOfRangeException>(() => invalidValue.AsEnum<TestEnum>());
        }
    }
}
