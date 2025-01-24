using FluentValidation;
using FluentValidation.Results;
using Tudo_List.Domain.Core.Interfaces.Factories;
using Tudo_List.Domain.Core.Interfaces.Repositories;
using Tudo_List.Domain.Core.Interfaces.Services;
using Tudo_List.Domain.Entities;
using Tudo_List.Domain.Enums;
using Tudo_List.Domain.Exceptions;
using Tudo_List.Domain.Helpers;
using Tudo_List.Domain.Services.Helpers;
using Tudo_List.Domain.Services.Validation;

namespace Tudo_List.Domain.Services
{
    public class UserService(IUserRepository userRepository, IPasswordStrategyFactory passwordStrategyFactory) : IUserService
    {
        private const string PasswordProperty = "Password";

        public IEnumerable<User> GetAll()
        {
            return userRepository.GetAll();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await userRepository.GetAllAsync();
        }

        public User? GetById(int id)
        {
            return userRepository.GetById(id);
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await userRepository.GetByIdAsync(id);
        }

        public User? GetByEmail(string email)
        {
            return userRepository.GetByEmail(email);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await userRepository.GetByEmailAsync(email);
        }

        public void Register(User user, string password)
        {
            new UserValidator(userRepository)
                .WithName(user.Name)
                .WithEmail(user.Email)
                .WithPassword(password)
                .Validate();

            DefineUserPasswordHash(user, password);
            userRepository.Add(user);
        }

        public async Task RegisterAsync(User user, string password)
        {
            new UserValidator(userRepository)
                .WithName(user.Name)
                .WithEmail(user.Email)
                .WithPassword(password)
                .Validate();

            DefineUserPasswordHash(user, password);
            await userRepository.AddAsync(user);
        }

        public void Update(int id, string? newName)
        {
            var user = GetUser(id);

            if (newName is null || user.Name.Equals(newName))
                throw new InvalidOperationException("No property is being updated!");

            var userValidator = new UserValidator(userRepository);

            if (newName is not null)
            {
                user.Name = newName;
                userValidator.WithName(newName);
            }

            userValidator.Validate();

            userRepository.Update(user);
        }

        public async Task UpdateAsync(int id, string? newName)
        {
            var user = await GetUserAsync(id);

            if (newName is null || user.Name.Equals(newName))
                throw new InvalidOperationException("No property is being updated!");

            var userValidator = new UserValidator(userRepository);

            if (newName is not null)
            {
                user.Name = newName;
                userValidator.WithName(newName);
            }

            userValidator.Validate();
            await userRepository.UpdateAsync(user);
        }

        public void UpdateEmail(int id, string newEmail, string currentPassword)
        {
            var user = GetUser(id);

            if (user.Email.Equals(newEmail))
            {
                throw new ValidationException(
                [
                    new(nameof(User.Email), ValidationMessageHelper.GetCantBeTheSameAsCurrentPropertyMessage(nameof(User.Email)))
                ]);
            }
            new UserValidator(userRepository)
                .WithEmail(newEmail)
                .Validate();

            ValidateUserPassword(user, currentPassword);
            user.Email = newEmail;
            
            userRepository.Update(user);
        }

        public async Task UpdateEmailAsync(int id, string newEmail, string currentPassword)
        {
            var user = await GetUserAsync(id);

            if (user.Email.Equals(newEmail))
            {
                throw new ValidationException(
                [
                    new(nameof(User.Email), ValidationMessageHelper.GetCantBeTheSameAsCurrentPropertyMessage(nameof(User.Email)))
                ]);
            }

            new UserValidator(userRepository)
                .WithEmail(newEmail)
                .Validate();

            ValidateUserPassword(user, currentPassword);
            user.Email = newEmail;
            
            await userRepository.UpdateAsync(user);
        }

        public void UpdatePassword(int id, string currentPassword, string newPassword)
        {
            var user = GetUser(id);

            new UserValidator()
                .WithPassword(newPassword)
                .Validate();

            if (newPassword.Equals(currentPassword))
            {
                throw new ValidationException(
                [
                    new(PasswordProperty, ValidationMessageHelper.GetCantBeTheSameAsCurrentPropertyMessage(PasswordProperty))
                ]);
            }

            ValidateUserPassword(user, currentPassword);
            DefineUserPasswordHash(user, newPassword);
            
            userRepository.Update(user);
        }

        public async Task UpdatePasswordAsync(int id, string currentPassword, string newPassword)
        {
            var user = await GetUserAsync(id);

            new UserValidator()
                .WithPassword(newPassword)
                .Validate();

            if (newPassword.Equals(currentPassword))
            {
                var error = new ValidationFailure[]
                {
                    new(PasswordProperty, ValidationMessageHelper.GetCantBeTheSameAsCurrentPropertyMessage(PasswordProperty))
                };

                throw new ValidationException(error);
            }

            ValidateUserPassword(user, currentPassword);
            DefineUserPasswordHash(user, newPassword);

            await userRepository.UpdateAsync(user);
        }

        public void Delete(int id)
        {
            var user = GetUser(id);
            userRepository.Remove(user);
        }

        public async Task DeleteAsync(int id)
        {
            var user = await GetUserAsync(id);
            await userRepository.RemoveAsync(user);
        }
        
        private User GetUser(int id)
        {
            return userRepository.GetById(id)
                 ?? throw new EntityNotFoundException(nameof(User), nameof(User.Id), id);
        }

        private async Task<User> GetUserAsync(int id)
        {
            return await userRepository.GetByIdAsync(id)
                ?? throw new EntityNotFoundException(nameof(User), nameof(User.Id), id);
        }

        private void ValidateUserPassword(User user, string password)
        {
            var isPasswordValid = passwordStrategyFactory
                .CreatePasswordStrategy(user.PasswordStrategy)
                .VerifyPassword(password, user.PasswordHash, user.Salt);

            if (!isPasswordValid)
            {
                throw new ValidationException(
                [
                    new(PasswordProperty, $"The {PasswordProperty} is incorrect!")
                ]);
            }
        }

        private void DefineUserPasswordHash(User user, string password)
        {
            var strategyToUse = EnumHelper.GetRandomValue<PasswordStrategy>();
            var strategy = passwordStrategyFactory.CreatePasswordStrategy(strategyToUse);
            string? saltString = null;

            if (strategy.UsesSaltingParameter)
            {
                var saltInBytes = PasswordHelper.GenerateSalt();

                saltString = Convert.ToBase64String(saltInBytes);
                user.Salt = saltString;
            }

            user.PasswordStrategy = strategyToUse;
            user.PasswordHash = strategy.HashPassword(password, saltString);
        }
    }
}
