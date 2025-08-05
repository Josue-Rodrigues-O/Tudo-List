using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Tudo_List.Application.Dtos.User;
using Tudo_List.Application.Interfaces.Applications;
using Tudo_List.Domain.Core.Interfaces.Services;
using Tudo_List.Domain.Entities;
using Tudo_List.Domain.Exceptions;
using Tudo_List.Domain.Services.Helpers;

namespace Tudo_List.Application
{
    public class UserApplication(IUserService userService, IMapper mapper, ICurrentUserService currentUserService) : IUserApplication
    {
        private int CurrentUserId 
            => int.Parse(currentUserService.Id);

        public IEnumerable<UserDto> GetAll()
        {
            return mapper.Map<IEnumerable<UserDto>>(userService.GetAll());
        }
        
        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            return mapper.Map<IEnumerable<UserDto>>(await userService.GetAllAsync());
        }

        public UserDto GetById(int id)
        {
            var user = userService.GetById(id)
                ?? throw new EntityNotFoundException(nameof(User), nameof(User.Id), id);

            return mapper.Map<UserDto>(user);
        }
        
        public async Task<UserDto> GetByIdAsync(int id)
        {
            var user = await userService.GetByIdAsync(id)
                ?? throw new EntityNotFoundException(nameof(User), nameof(User.Id), id);

            return mapper.Map<UserDto>(user);
        }

        public UserDto GetByEmail(string email)
        {
            var user = userService.GetByEmail(email)
                ?? throw new EntityNotFoundException(nameof(User), nameof(User.Email), email);

            return mapper.Map<UserDto>(user);
        }
        
        public async Task<UserDto> GetByEmailAsync(string email)
        {
            var user = await userService.GetByEmailAsync(email)
                ?? throw new EntityNotFoundException(nameof(User), nameof(User.Email), email);

            return mapper.Map<UserDto>(user);
        }

        public void Register(RegisterUserDto model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                throw new ValidationException(
                [
                    new(nameof(model.Password), ValidationMessageHelper.GetMustBeEqualMessage(nameof(model.Password), nameof(model.ConfirmPassword)))
                ]);
            }

            userService.Register(mapper.Map<User>(model), model.Password);
        }
        
        public async Task RegisterAsync(RegisterUserDto model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                throw new ValidationException(
                [
                    new(nameof(model.Password), ValidationMessageHelper.GetMustBeEqualMessage(nameof(model.Password), nameof(model.ConfirmPassword)))
                ]);
            }

            await userService.RegisterAsync(mapper.Map<User>(model), model.Password);
        }

        public void Update(UpdateUserDto model)
        {
            if (model.UserId != CurrentUserId)
                throw new UnauthorizedAccessException(ValidationMessageHelper.GetUnauthorizedOperationMessage(nameof(User), nameof(Update)));

            userService.Update(model.UserId, model.NewName);
        }
        
        public async Task UpdateAsync(UpdateUserDto model)
        {
            if (model.UserId != CurrentUserId)
                throw new UnauthorizedAccessException(ValidationMessageHelper.GetUnauthorizedOperationMessage(nameof(User), nameof(UpdateAsync)));

            await userService.UpdateAsync(model.UserId, model.NewName);
        }

        public void UpdateEmail(UpdateEmailDto model)
        {
            if (model.UserId != CurrentUserId)
                throw new UnauthorizedAccessException(ValidationMessageHelper.GetUnauthorizedOperationMessage(nameof(User), nameof(UpdateEmail)));

            userService.UpdateEmail(model.UserId, model.NewEmail, model.CurrentPassword);
        }

        public async Task UpdateEmailAsync(UpdateEmailDto model)
        {
            if (model.UserId != CurrentUserId)
                throw new UnauthorizedAccessException(ValidationMessageHelper.GetUnauthorizedOperationMessage(nameof(User), nameof(UpdateEmailAsync)));

            await userService.UpdateEmailAsync(model.UserId, model.NewEmail, model.CurrentPassword);
        }

        public void UpdatePassword(UpdatePasswordDto model)
        {
            if (model.UserId != CurrentUserId)
                throw new UnauthorizedAccessException(ValidationMessageHelper.GetUnauthorizedOperationMessage(nameof(User), nameof(UpdatePassword)));

            if (model.NewPassword != model.ConfirmNewPassword)
            {
                var error = new ValidationFailure[]
                {
                    new(nameof(model.NewPassword), ValidationMessageHelper.GetMustBeEqualMessage(nameof(model.NewPassword), nameof(model.ConfirmNewPassword)))
                };

                throw new ValidationException(error);
            }

            userService.UpdatePassword(model.UserId, model.CurrentPassword, model.NewPassword);
        }

        public async Task UpdatePasswordAsync(UpdatePasswordDto model)
        {
            if (model.UserId != CurrentUserId)
                throw new UnauthorizedAccessException(ValidationMessageHelper.GetUnauthorizedOperationMessage(nameof(User), nameof(UpdatePasswordAsync)));

            if (model.NewPassword != model.ConfirmNewPassword)
            {
                var error = new ValidationFailure[]
                {
                    new(nameof(model.NewPassword), ValidationMessageHelper.GetMustBeEqualMessage(nameof(model.NewPassword), nameof(model.ConfirmNewPassword)))
                };

                throw new ValidationException(error);
            }

            await userService.UpdatePasswordAsync(model.UserId, model.CurrentPassword, model.NewPassword);
        }

        public void Delete(int id)
        {
            if (id != CurrentUserId)
                throw new UnauthorizedAccessException(ValidationMessageHelper.GetUnauthorizedOperationMessage(nameof(User), nameof(Delete)));

            userService.Delete(id);
        }
        
        public async Task DeleteAsync(int id)
        {
            if (id != CurrentUserId)
                throw new UnauthorizedAccessException(ValidationMessageHelper.GetUnauthorizedOperationMessage(nameof(User), nameof(Delete)));

            await userService.DeleteAsync(id);
        }
    }
}
