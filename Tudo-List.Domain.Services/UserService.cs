using Tudo_List.Domain.Core.Interfaces.Repositories;
using Tudo_List.Domain.Core.Interfaces.Services;
using Tudo_List.Domain.Entities;

namespace Tudo_List.Domain.Services
{
    public class UserService(IUserRepository userRepository) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;

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

        public void Register(User user)
        {
            _userRepository.Add(user);
        }
        
        public async Task RegisterAsync(User user)
        {
            await _userRepository.AddAsync(user);
        }

        public void Update(User user)
        {
            _userRepository.Update(user);
        }
        
        public async Task UpdateAsync(User user)
        {
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
    }
}
