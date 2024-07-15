using AutoMapper;
using Tudo_List.Application.Interfaces.Applications;
using Tudo_List.Application.Interfaces.Services;
using Tudo_List.Application.Models.Dtos.User;
using Tudo_List.Domain.Core.Interfaces.Services;
using Tudo_List.Domain.Entities;

namespace Tudo_List.Application
{
    public class UserApplication(
        IUserService userService, 
        IMapper mapper, 
        ICurrentUserService currentUserService) : IUserApplication
    {
        private readonly IUserService _userService = userService;
        private readonly IMapper _mapper = mapper;
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
            _userService.Register(_mapper.Map<User>(model), model.Password);
        }
        
        public async Task RegisterAsync(RegisterUserDto model)
        {
            await _userService.RegisterAsync(_mapper.Map<User>(model), model.Password);
        }

        public void Update(UpdateUserDto model)
        {
            if (model.UserId != GetCurrentUserId())
                throw new Exception();

            if (model.NewName is null)
                throw new Exception();

            _userService.Update(model.UserId, model.NewName);
        }
        
        public async Task UpdateAsync(UpdateUserDto model)
        {
            if (model.UserId != GetCurrentUserId())
                throw new Exception();

            if (model.NewName is null) 
                throw new Exception();

            await _userService.UpdateAsync(model.UserId, model.NewName);
        }

        public void UpdateEmail(UpdateEmailDto model)
        {
            if (model.UserId != GetCurrentUserId())
                throw new Exception();

            _userService.UpdateEmail(model.UserId, model.NewEmail, model.CurrentPassword);
        }

        public async Task UpdateEmailAsync(UpdateEmailDto model)
        {
            if (model.UserId != GetCurrentUserId())
                throw new Exception();

            await _userService.UpdateEmailAsync(model.UserId, model.NewEmail, model.CurrentPassword);
        }

        public void UpdatePassword(UpdatePasswordDto model)
        {
            if (model.UserId != GetCurrentUserId())
                throw new Exception();

            if (model.NewPassword != model.ConfirmNewPassword)
                throw new Exception();

            _userService.UpdatePassword(model.UserId, model.CurrentPassword, model.NewPassword);
        }

        public async Task UpdatePasswordAsync(UpdatePasswordDto model)
        {
            if (model.UserId != GetCurrentUserId())
                throw new Exception();

            if (model.NewPassword != model.ConfirmNewPassword)
                throw new Exception();

            await _userService.UpdatePasswordAsync(model.UserId, model.CurrentPassword, model.NewPassword);
        }

        public void Delete(int id)
        {
            if (id != GetCurrentUserId())
                throw new Exception();

            _userService.Delete(id);
        }
        
        public async Task DeleteAsync(int id)
        {
            if (id != GetCurrentUserId())
                throw new Exception();

            await _userService.DeleteAsync(id);
        }

        private int GetCurrentUserId()
        {
            var strUserId = _currentUserService.Id;

            if (!int.TryParse(strUserId, out int userId))
                throw new Exception();

            return userId;
        }
    }
}
