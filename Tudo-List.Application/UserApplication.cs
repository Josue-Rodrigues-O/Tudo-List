using AutoMapper;
using Tudo_List.Application.Interfaces.Applications;
using Tudo_List.Application.Interfaces.Services;
using Tudo_List.Application.Models.Dtos;
using Tudo_List.Domain.Commands.Dtos.User;
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
            if (model.Id != GetCurrentUserId())
                throw new Exception();

            _userService.Update(model);
        }
        
        public async Task UpdateAsync(UpdateUserDto model)
        {
            if (model.Id != GetCurrentUserId())
                throw new Exception();

            await _userService.UpdateAsync(model);
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
