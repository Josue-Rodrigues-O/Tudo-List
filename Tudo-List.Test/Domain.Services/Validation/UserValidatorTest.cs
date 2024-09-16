using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Tudo_List.Domain.Core.Interfaces.Repositories;
using Tudo_List.Domain.Services.Validation;
using Tudo_List.Test.Mock;

namespace Tudo_List.Test.Domain.Services.Validation
{
    public class UserValidatorTest : UnitTest
    {
        private readonly IUserRepository _userRepository;

        public UserValidatorTest()
        {
            _userRepository = _serviceProvider.GetRequiredService<IUserRepository>();
        }

        [Fact]
        public void Nothing_Should_Happen_If_You_Call_Validate_With_No_Property_Added()
        {
            new UserValidator()
                .Validate();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("A")]
        [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void Validation_Should_Fail_When_Invalid_Name_Is_Passed(string? invalidName)
        {
            Assert.Throws<ValidationException>(() => new UserValidator().WithName(invalidName).Validate());
        }

        [Fact]
        public void Validation_Should_Return_ArgumentNullException_When_You_Try_To_Validate_Email_Without_Passing_User_Repository_Via_Constructor()
        {
            const string validEmail = "test@test.com";
            Assert.Throws<ArgumentNullException>(() => new UserValidator().WithEmail(validEmail).Validate());
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("a@a")]
        [InlineData("aa.com")]
        public void Validation_Should_Fail_When_Invalid_Email_Is_Passed(string? invalidEmail)
        {
            Assert.Throws<ValidationException>(() => new UserValidator(_userRepository).WithEmail(invalidEmail).Validate());
        }
        
        [Fact]
        public void Validation_Should_Fail_When_Email_Is_Already_Registered_In_The_System()
        {
            var currentUserMock = MockData.GetCurrentUser();
            
            _context.Add(currentUserMock);
            _context.SaveChanges();

            Assert.Throws<ValidationException>(() => new UserValidator(_userRepository).WithEmail(currentUserMock.Email).Validate());
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("1234567")]
        [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void Validation_Should_Fail_When_Invalid_Password_Is_Passed(string? invalidPassword)
        {
            Assert.Throws<ValidationException>(() => new UserValidator().WithPassword(invalidPassword).Validate());
        }

        [Theory]
        [InlineData("", "test@test.com", "12345678" )]
        [InlineData("Lucas", "test@testcom", "12345678" )]
        [InlineData("Lucas", "test@test.com", "1234567" )]
        public void Validation_Should_Fail_When_At_Least_One_Property_Passed_Is_Invalid(string name, string email, string password)
        {
            Assert.Throws<ValidationException>(() 
                => new UserValidator(_userRepository)
                    .WithName(name)
                    .WithEmail(email)
                    .WithPassword(password)
                    .Validate());
        }

        [Theory]
        [InlineData("AA")]
        [InlineData("Valid Name")]
        [InlineData("A      A")]
        [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void Validation_Should_Pass_When_Valid_Name_Is_Passed(string validName)
        {
            new UserValidator().WithName(validName).Validate();
        }

        [Fact]
        public void Validation_Should_Pass_When_Valid_Email_Is_Passed()
        {
            const string validEmail = "test@test.com";
           new UserValidator(_userRepository).WithEmail(validEmail).Validate();
        }

        [Theory]
        [InlineData("12345678")]
        [InlineData("PasswordTest@1234")]
        [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void Validation_Should_Pass_When_Valid_Password_Is_Passed(string validPassword)
        {
            new UserValidator().WithPassword(validPassword).Validate();
        }

        [Fact]
        public void Validation_Should_Pass_When_All_Properties_Passed_Are_Valid()
        {
            const string validName = "Lucas";
            const string validEmail = "test@test.com";
            const string validPassword = "12345678";

            new UserValidator(_userRepository)
                .WithName(validName)
                .WithEmail(validEmail)
                .WithPassword(validPassword)
                .Validate();
        }
    }
}
