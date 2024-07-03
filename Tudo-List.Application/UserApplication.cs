using AutoMapper;
using Tudo_List.Application.Interfaces;
using Tudo_List.Application.Models.Dtos;
using Tudo_List.Domain.Core.Interfaces.Factories;
using Tudo_List.Domain.Core.Interfaces.Services;
using Tudo_List.Domain.Entities;
using Tudo_List.Domain.Enums;
using Tudo_List.Domain.Helpers;
using Tudo_List.Domain.Services.Factories;
using Tudo_List.Domain.Services.Helpers;

namespace Tudo_List.Application
{
    public class UserApplication(IUserService userService, IMapper mapper, IPasswordStrategyFactory passwordStrategyFactory) : IUserApplication
    {
        private readonly IUserService _userService = userService;
        private readonly IMapper _mapper = mapper;
        private readonly IPasswordStrategyFactory _passwordStrategyFactory = passwordStrategyFactory;

        public IEnumerable<UserDto> GetAll()
        {
            return _mapper.Map<IEnumerable<UserDto>>(_userService.GetAll());
        }

        public UserDto GetById(int id)
        {
            return _mapper.Map<UserDto>(_userService.GetById(id));
        }

        public void Register(RegisterUserDto model)
        {
            var user = _mapper.Map<User>(model);

            var passwordStrategy = EnumHelper.GetRandomValue<PasswordStrategyEnum>();
            var strategy = _passwordStrategyFactory.CreatePasswordStrategy(passwordStrategy);
            string? salt = null;

            if (strategy.UsesSalting())
            {
                salt = PasswordHelper.GenerateBase64String();
                user.Salt = salt;   
            }

            user.PasswordStrategy = passwordStrategy;
            user.PasswordHash = strategy.HashPassword(model.Password, salt);

            _userService.Register(user);
        }

        public void Update(UpdateUserDto model)
        {
            _userService.Update(_mapper.Map<User>(model));
        }

        public void Delete(int id)
        {
            _userService.Delete(id);
        }
    }
}
