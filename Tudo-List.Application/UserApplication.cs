using AutoMapper;
using Tudo_List.Application.Interfaces.Applications;
using Tudo_List.Application.Interfaces.Services;
using Tudo_List.Application.Models.Dtos.User;
using Tudo_List.Domain.Core.Interfaces.Factories;
using Tudo_List.Domain.Core.Interfaces.Services;
using Tudo_List.Domain.Entities;
using Tudo_List.Domain.Enums;
using Tudo_List.Domain.Helpers;
using Tudo_List.Domain.Services.Helpers;

namespace Tudo_List.Application
{
    public class UserApplication(
        IUserService userService, 
        IMapper mapper, 
        IPasswordStrategyFactory passwordStrategyFactory, 
        ICurrentUserService currentUserService) : IUserApplication
    {
        private readonly IUserService _userService = userService;
        private readonly IMapper _mapper = mapper;
        private readonly IPasswordStrategyFactory _passwordStrategyFactory = passwordStrategyFactory;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public IEnumerable<UserDto> GetAll()
        {
            return _mapper.Map<IEnumerable<UserDto>>(_userService.GetAll());
        }
        
        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<UserDto>>(await _userService.GetAllAsync());
        }

        public UserDto GetById(int id)
        {
            return _mapper.Map<UserDto>(_userService.GetById(id));
        }
        
        public async Task<UserDto> GetByIdAsync(int id)
        {
            return _mapper.Map<UserDto>(await _userService.GetByIdAsync(id));
        }

        public UserDto GetByEmail(string email)
        {
            return _mapper.Map<UserDto>(_userService.GetByEmail(email));
        }
        
        public async Task<UserDto> GetByEmailAsync(string email)
        {
            return _mapper.Map<UserDto>(await _userService.GetByEmailAsync(email));
        }

        public void Register(RegisterUserDto model)
        {
            var user = DefineUserInfo(model);
            _userService.Register(user);
        }
        
        public async Task RegisterAsync(RegisterUserDto model)
        {
            var user = DefineUserInfo(model);
            await _userService.RegisterAsync(user);
        }

        public void Update(UpdateUserDto model)
        {
            var user = DefineUserInfo(model);
            _userService.Update(user);
        }
        
        public async Task UpdateAsync(UpdateUserDto model)
        {
            var user = DefineUserInfo(model);
            await _userService.UpdateAsync(user);
        }

        public void Delete(int id)
        {
            _userService.Delete(id);
        }
        
        public async Task DeleteAsync(int id)
        {
            await _userService.DeleteAsync(id);
        }

        private User DefineUserInfo(RegisterUserDto model)
        {
            var user = _mapper.Map<User>(model);

            var passwordStrategy = EnumHelper.GetRandomValue<PasswordStrategy>();
            var strategy = _passwordStrategyFactory.CreatePasswordStrategy(passwordStrategy);
            string? salt = null;

            if (strategy.UsesSalting)
            {
                salt = PasswordHelper.GenerateBase64String();
                user.Salt = salt;
            }

            user.PasswordStrategy = passwordStrategy;
            user.PasswordHash = strategy.HashPassword(model.Password, salt);
            return user;
        }

        private User DefineUserInfo(UpdateUserDto model)
        {
            var user = _mapper.Map<User>(model);
            var strUserId = _currentUserService.Id;

            if (!int.TryParse(strUserId, out int userId))
                throw new Exception();

            user.Id = userId;
            return user;
        }
    }
}
