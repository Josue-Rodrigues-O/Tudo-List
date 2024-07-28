using Tudo_List.Domain.Core.Interfaces.Factories;
using Tudo_List.Domain.Core.Interfaces.Repositories;
using Tudo_List.Domain.Core.Interfaces.Services;
using Tudo_List.Domain.Entities;
using Tudo_List.Domain.Enums;
using Tudo_List.Domain.Helpers;
using Tudo_List.Domain.Services.Helpers;
using Tudo_List.Domain.Services.Validation;

namespace Tudo_List.Domain.Services
{
    public class UserService(IUserRepository userRepository, IPasswordStrategyFactory passwordStrategyFactory) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IPasswordStrategyFactory _passwordStrategyFactory = passwordStrategyFactory;

        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public User? GetById(int id)
        {
            return _userRepository.GetById(id);
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public User? GetByEmail(string email)
        {
            return _userRepository.GetByEmail(email);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _userRepository.GetByEmailAsync(email);
        }

        public void Register(User user, string password)
        {
            new UserValidator(_userRepository)
                .WithName(user.Name)
                .WithEmail(user.Email)
                .WithPassword(password)
                .Validate();

            DefineUserPasswordHash(user, password);
            _userRepository.Add(user);
        }

        public async Task RegisterAsync(User user, string password)
        {
            new UserValidator(_userRepository)
                .WithName(user.Name)
                .WithEmail(user.Email)
                .WithPassword(password)
                .Validate();

            DefineUserPasswordHash(user, password);
            await _userRepository.AddAsync(user);
        }

        public void Update(int id, string? newName)
        {
            var user = _userRepository.GetById(id) 
                ?? throw new KeyNotFoundException(nameof(id));

            var nothingChanged = newName is null || user.Name.Equals(newName);

            if (nothingChanged)
                throw new Exception("No property is being updated!");

            var userValidator = new UserValidator(_userRepository);

            if (newName is not null)
            {
                user.Name = newName;
                userValidator.WithName(newName);
            }

            userValidator.Validate();

            _userRepository.Update(user);
        }

        public async Task UpdateAsync(int id, string? newName)
        {
            var user = _userRepository.GetById(id)
                ?? throw new KeyNotFoundException(nameof(id));

            var nothingChanged = newName is null || user.Name.Equals(newName);

            if (nothingChanged)
                throw new Exception("No property is being updated!");

            var userValidator = new UserValidator(_userRepository);

            if (newName is not null)
            {
                user.Name = newName;
                userValidator.WithName(newName);
            }

            userValidator.Validate();
            await _userRepository.UpdateAsync(user);
        }

        public void UpdateEmail(int id, string newEmail, string currentPassword)
        {
            var user = _userRepository.GetById(id)
                 ?? throw new KeyNotFoundException(nameof(id));

            if (user.Email.Equals(newEmail))
                throw new Exception("The new email can't be the same as the current one!");

            new UserValidator(_userRepository)
                .WithEmail(newEmail)
                .Validate();

            var passwordStrategy = _passwordStrategyFactory.CreatePasswordStrategy(user.PasswordStrategy);
            if (!passwordStrategy.VerifyPassword(currentPassword, user.PasswordHash, user.Salt))
                throw new Exception();

            user.Email = newEmail;
            _userRepository.Update(user);
        }

        public async Task UpdateEmailAsync(int id, string newEmail, string currentPassword)
        {
            var user = await _userRepository.GetByIdAsync(id)
                 ?? throw new KeyNotFoundException(nameof(id));

            if (user.Email.Equals(newEmail))
                throw new Exception("The new email can't be the same as the current one!");

            new UserValidator(_userRepository)
                .WithEmail(newEmail)
                .Validate();

            var passwordStrategy = _passwordStrategyFactory.CreatePasswordStrategy(user.PasswordStrategy);
            if (!passwordStrategy.VerifyPassword(currentPassword, user.PasswordHash, user.Salt))
                throw new Exception();

            user.Email = newEmail;
            await _userRepository.UpdateAsync(user);
        }

        public void UpdatePassword(int id, string currentPassword, string newPassword)
        {
            var user = _userRepository.GetById(id)
                 ?? throw new KeyNotFoundException(nameof(id));

            new UserValidator()
                .WithPassword(newPassword)
                .Validate();

            if (newPassword.Equals(currentPassword))
                throw new Exception("The new password can't be the same as the current password!");

            var passwordStrategy = _passwordStrategyFactory.CreatePasswordStrategy(user.PasswordStrategy);
            if (!passwordStrategy.VerifyPassword(currentPassword, user.PasswordHash, user.Salt))
                throw new Exception();

            DefineUserPasswordHash(user, newPassword);
            _userRepository.Update(user);
        }

        public async Task UpdatePasswordAsync(int id, string currentPassword, string newPassword)
        {
            var user = await _userRepository.GetByIdAsync(id)
                 ?? throw new KeyNotFoundException(nameof(id));

            new UserValidator()
                .WithPassword(newPassword)
                .Validate();

            if (newPassword.Equals(currentPassword))
                throw new Exception("The new password can't be the same as the current password!");

            var passwordStrategy = _passwordStrategyFactory.CreatePasswordStrategy(user.PasswordStrategy);
            if (!passwordStrategy.VerifyPassword(currentPassword, user.PasswordHash, user.Salt))
                throw new Exception();

            DefineUserPasswordHash(user, newPassword);
            await _userRepository.UpdateAsync(user);
        }

        public void Delete(int id)
        {
            var user = _userRepository.GetById(id) 
                ?? throw new KeyNotFoundException(nameof(id));

            _userRepository.Remove(user);
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException(nameof(id));

            await _userRepository.RemoveAsync(user);
        }

        private void DefineUserPasswordHash(User user, string password)
        {
            var strategyToUse = EnumHelper.GetRandomValue<PasswordStrategy>();
            var strategy = _passwordStrategyFactory.CreatePasswordStrategy(strategyToUse);
            string? salt = null;

            if (strategy.UsesSalting)
            {
                salt = PasswordHelper.GenerateBase64String();
                user.Salt = salt;
            }

            user.PasswordStrategy = strategyToUse;
            user.PasswordHash = strategy.HashPassword(password, salt);
        }
    }
}
