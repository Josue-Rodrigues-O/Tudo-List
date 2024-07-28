using AutoMapper;
using FluentValidation;
using Tudo_List.Application.Interfaces.Applications;
using Tudo_List.Application.Interfaces.Services;
using Tudo_List.Application.Models.Dtos.User;
using Tudo_List.Domain.Core.Interfaces.Services;
using Tudo_List.Domain.Entities;
using Tudo_List.Domain.Exceptions;
using Tudo_List.Domain.Services.Helpers;

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

        private int CurrentUserId => int.Parse(_currentUserService.Id);

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
            var user = _userService.GetById(id)
                ?? throw new EntityNotFoundException(nameof(User), nameof(User.Id), id);

            return _mapper.Map<UserDto>(user);
        }
        
        public async Task<UserDto> GetByIdAsync(int id)
        {
            var user = await _userService.GetByIdAsync(id)
                ?? throw new EntityNotFoundException(nameof(User), nameof(User.Id), id);

            return _mapper.Map<UserDto>(user);
        }

        public UserDto GetByEmail(string email)
        {
            var user = _userService.GetByEmail(email)
                ?? throw new EntityNotFoundException(nameof(User), nameof(User.Email), email);

            return _mapper.Map<UserDto>(user);
        }
        
        public async Task<UserDto> GetByEmailAsync(string email)
        {
            var user = await _userService.GetByEmailAsync(email)
                ?? throw new EntityNotFoundException(nameof(User), nameof(User.Email), email);

            return _mapper.Map<UserDto>(user);
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
            if (model.UserId != CurrentUserId)
                throw new UnauthorizedAccessException(ValidationHelper.GetUnauthorizedOperationMessage(nameof(User), nameof(Update)));

            _userService.Update(model.UserId, model.NewName);
        }
        
        public async Task UpdateAsync(UpdateUserDto model)
        {
            if (model.UserId != CurrentUserId)
                throw new UnauthorizedAccessException(ValidationHelper.GetUnauthorizedOperationMessage(nameof(User), nameof(UpdateAsync)));

            await _userService.UpdateAsync(model.UserId, model.NewName);
        }

        public void UpdateEmail(UpdateEmailDto model)
        {
            if (model.UserId != CurrentUserId)
                throw new UnauthorizedAccessException(ValidationHelper.GetUnauthorizedOperationMessage(nameof(User), nameof(UpdateEmail)));

            _userService.UpdateEmail(model.UserId, model.NewEmail, model.CurrentPassword);
        }

        public async Task UpdateEmailAsync(UpdateEmailDto model)
        {
            if (model.UserId != CurrentUserId)
                throw new UnauthorizedAccessException(ValidationHelper.GetUnauthorizedOperationMessage(nameof(User), nameof(UpdateEmailAsync)));

            await _userService.UpdateEmailAsync(model.UserId, model.NewEmail, model.CurrentPassword);
        }

        public void UpdatePassword(UpdatePasswordDto model)
        {
            if (model.UserId != CurrentUserId)
                throw new UnauthorizedAccessException(ValidationHelper.GetUnauthorizedOperationMessage(nameof(User), nameof(UpdatePassword)));

            if (model.NewPassword != model.ConfirmNewPassword)
                throw new ValidationException(ValidationHelper.GetMustBeEqualMessage(nameof(model.NewPassword), nameof(model.ConfirmNewPassword)));

            _userService.UpdatePassword(model.UserId, model.CurrentPassword, model.NewPassword);
        }

        public async Task UpdatePasswordAsync(UpdatePasswordDto model)
        {
            if (model.UserId != CurrentUserId)
                throw new UnauthorizedAccessException(ValidationHelper.GetUnauthorizedOperationMessage(nameof(User), nameof(UpdatePasswordAsync)));

            if (model.NewPassword != model.ConfirmNewPassword)
                throw new ValidationException(ValidationHelper.GetMustBeEqualMessage(nameof(model.NewPassword), nameof(model.ConfirmNewPassword)));

            await _userService.UpdatePasswordAsync(model.UserId, model.CurrentPassword, model.NewPassword);
        }

        public void Delete(int id)
        {
            if (id != CurrentUserId)
                throw new UnauthorizedAccessException(ValidationHelper.GetUnauthorizedOperationMessage(nameof(User), nameof(Delete)));

            _userService.Delete(id);
        }
        
        public async Task DeleteAsync(int id)
        {
            if (id != CurrentUserId)
                throw new UnauthorizedAccessException(ValidationHelper.GetUnauthorizedOperationMessage(nameof(User), nameof(Delete)));

            await _userService.DeleteAsync(id);
        }
    }
}
