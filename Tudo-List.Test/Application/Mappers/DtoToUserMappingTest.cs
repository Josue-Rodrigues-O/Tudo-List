using AutoMapper;
using Tudo_List.Application.Dtos.User;
using Tudo_List.Application.Mappers;
using Tudo_List.Domain.Entities;
using Tudo_List.Domain.Enums;

namespace Tudo_List.Test.Application.Mappers
{
    public class DtoToUserMappingTest : UnitTest
    {
        private readonly IMapper _mapper;

        public DtoToUserMappingTest()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.AddProfile(new DtoToUserMapping());
            });

            _mapper = mappingConfig.CreateMapper();
        }

        [Fact]
        public void Should_Map_UserDto_To_User_Ignoring_PasswordHash_Salt_And_PasswordStrategy_Properties()
        {
            var userDto = new UserDto
            {
                Id = 1,
                Email = "test@test.com",
                Name = "Test"
            };

            var user = _mapper.Map<User>(userDto);

            Assert.Equal(userDto.Id, user.Id);
            Assert.Equal(userDto.Email, user.Email);
            Assert.Equal(userDto.Name, user.Name);
            
            Assert.Equal(default, user.PasswordHash);
            Assert.Equal(default, user.Salt);
            Assert.Equal(default, user.PasswordStrategy);
        }
        
        [Fact]
        public void Should_Map_User_To_UserDto_Ignoring_PasswordHash_Salt_And_PasswordStrategy_Properties()
        {
            var user = new User
            {
                Id = 1,
                Email = "test@test.com",
                Name = "Test",
                PasswordHash = "12345678",
                PasswordStrategy = PasswordStrategy.BCrypt,
                Salt = null
            };

            var userDto = _mapper.Map<UserDto>(user);

            Assert.Equal(user.Id, userDto.Id);
            Assert.Equal(user.Email, userDto.Email);
            Assert.Equal(user.Name, userDto.Name);
        }

        [Fact]
        public void Should_Map_RegisterUserDto_To_User_Ignoring_Id_PasswordHash_Salt_And_PasswordStrategy_Properties()
        {
            var registerUserDto = new RegisterUserDto
            {
                Email = "test@test.com",
                Name = "Test",
                Password = "12345678",
                ConfirmPassword = "12345678"
            };

            var user = _mapper.Map<User>(registerUserDto);

            Assert.Equal(registerUserDto.Email, user.Email);
            Assert.Equal(registerUserDto.Name, user.Name);

            Assert.Equal(default, user.Id);
            Assert.Equal(default, user.PasswordStrategy);
            Assert.Equal(default, user.PasswordHash);
            Assert.Equal(default, user.Salt);
        }
    }
}
