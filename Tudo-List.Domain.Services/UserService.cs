using Tudo_List.Domain.Core.Interfaces.Factories;
using Tudo_List.Domain.Core.Interfaces.Repositories;
using Tudo_List.Domain.Core.Interfaces.Services;
using Tudo_List.Domain.Entities;
using Tudo_List.Domain.Enums;
using Tudo_List.Domain.Helpers;
using Tudo_List.Domain.Models.User;
using Tudo_List.Domain.Services.Helpers;

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

        public User GetById(int id)
        {
            return _userRepository.GetById(id);
        }
        
        public async Task<User> GetByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public User GetByEmail(string email)
        {
            return _userRepository.GetByEmail(email);
        }
        
        public async Task<User> GetByEmailAsync(string email)
        {
            return await _userRepository.GetByEmailAsync(email);
        }

        public void Register(User user, string password)
        {
            if (password != null)
                DefineUserPasswordHash(user, password);

            _userRepository.Add(user);
        }
        
        public async Task RegisterAsync(User user, string password)
        {
            if (password != null)
                DefineUserPasswordHash(user, password);

            await _userRepository.AddAsync(user);
        }

        public void Update(UpdateUserRequest model)
        {
            var user = _userRepository.GetById(model.UserId);

            if (model.Name != null) 
                user.Name = model.Name;

            _userRepository.Update(user);
        }
        
        public async Task UpdateAsync(UpdateUserRequest model)
        {
            var user = _userRepository.GetById(model.UserId);

            if (model.Name != null)
                user.Name = model.Name;

            await _userRepository.UpdateAsync(user);
        }

        public void UpdateEmail(UpdateEmailRequest model)
        {
            var user = _userRepository.GetById(model.UserId);
            var passwordStrategy = _passwordStrategyFactory.CreatePasswordStrategy(user.PasswordStrategy);

            if (!passwordStrategy.VerifyPassword(model.CurrentPassword, user.PasswordHash, user.Salt))
                throw new ArgumentException();

            user.Email = model.NewEmail;
            _userRepository.Update(user);
        }
        
        public async Task UpdateEmailAsync(UpdateEmailRequest model)
        {
            var user = await _userRepository.GetByIdAsync(model.UserId);
            var passwordStrategy = _passwordStrategyFactory.CreatePasswordStrategy(user.PasswordStrategy);

            if (!passwordStrategy.VerifyPassword(model.CurrentPassword, user.PasswordHash, user.Salt))
                throw new ArgumentException();

            user.Email = model.NewEmail;
            await _userRepository.UpdateAsync(user);
        }

        public void UpdatePassword(UpdatePasswordRequest model)
        {
            var user = _userRepository.GetById(model.UserId);
            var passwordStrategy = _passwordStrategyFactory.CreatePasswordStrategy(user.PasswordStrategy);

            if (!passwordStrategy.VerifyPassword(model.CurrentPassword, user.PasswordHash, user.Salt))
                throw new ArgumentException();

            if (model.NewPassword != model.ConfirmNewPassword)
                throw new ArgumentException();

            DefineUserPasswordHash(user, model.NewPassword);
            _userRepository.Update(user);
        }

        public async Task UpdatePasswordAsync(UpdatePasswordRequest model)
        {
            var user = await _userRepository.GetByIdAsync(model.UserId);
            var passwordStrategy = _passwordStrategyFactory.CreatePasswordStrategy(user.PasswordStrategy);

            if (!passwordStrategy.VerifyPassword(model.CurrentPassword, user.PasswordHash, user.Salt))
                throw new ArgumentException();

            if (model.NewPassword != model.ConfirmNewPassword)
                throw new ArgumentException();

            DefineUserPasswordHash(user, model.NewPassword);
            await _userRepository.UpdateAsync(user);
        }

        public void Delete(int id)
        {
            _userRepository.Remove(id);
        }
        
        public async Task DeleteAsync(int id)
        {
            await _userRepository.RemoveAsync(id);
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
