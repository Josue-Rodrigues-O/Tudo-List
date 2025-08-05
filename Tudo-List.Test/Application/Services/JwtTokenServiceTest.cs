using Microsoft.Extensions.DependencyInjection;
using Tudo_List.Application.Dtos.User;
using Tudo_List.Application.Interfaces.Services;

namespace Tudo_List.Test.Application.Services
{
    public class JwtTokenServiceTest : UnitTest
    {
        private readonly ITokenService _jwtTokenService;

        public JwtTokenServiceTest()
        {
            _jwtTokenService = _serviceProvider.GetRequiredService<ITokenService>();
        }

        [Fact]
        public void Should_Return_True_When_Validating_Valid_Token()
        {
            var validToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIzIiwidW5pcXVlX25hbWUiOiJfdGVzdCIsImVtYWlsIjoidGVzdEB0ZXN0LmNvbSIsIm5iZiI6MTcyNjg0Njc3OCwiZXhwIjo0ODgyNTIwMzc4LCJpYXQiOjE3MjY4NDY3Nzh9.Ht1owVDYj_M84A-6ZQCe3hc5vP8t75r1stanuvoAWX8";
            Assert.True(_jwtTokenService.ValidateToken(validToken));
        }

        [Fact]
        public void Should_Return_False_When_Validating_Invalid_Token()
        {
            var validToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIzIiwidW5pcXVlX25hbWUiOiJfdGVzdCIsImVtYWlsIjoidGVzdEB0ZXN0LmNvbSIsIm5iZiI6MTcyNjg0Njc3OCwiZXhwIjo0ODgyNTIwMzc4LCJpYXQiOjE3MjY4NDY3Nzh9.Ht1owVDYj_M84A-6ZQCe3hc5vP8t75r1stanuvoRTX8";
            Assert.False(_jwtTokenService.ValidateToken(validToken));
        }

        // I know it is bad using a service method to validate another, but at this moment, i don't have any ideas of how to do it other way
        [Fact]
        public void Should_Create_Valid_Tokens()
        {
            var token = _jwtTokenService.GenerateToken(new UserDto
            (
                Id: 5,
                Email: "test@test.com",
                Name: "Test"
            ));

            Assert.True(_jwtTokenService.ValidateToken(token));
        }
    }
}
