using Tudo_List.Domain.Core.Interfaces.Repositories;
using Tudo_List.Domain.Core.Interfaces.Services;
using Tudo_List.Domain.Entities;

namespace Tudo_List.Domain.Services
{
    public class UserService(IUserRepository repository) : IUserService
    {
        private readonly IUserRepository _repository = repository;

        public IEnumerable<User> GetAll()
        {
            return _repository.GetAll();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public User GetById(int id)
        {
            return _repository.GetById(id);
        }
        
        public async Task<User> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public User GetByEmail(string email)
        {
            return _repository.GetByEmail(email);
        }
        
        public async Task<User> GetByEmailAsync(string email)
        {
            return await _repository.GetByEmailAsync(email);
        }

        public void Register(User user)
        {
            _repository.Add(user);
        }
        
        public async Task RegisterAsync(User user)
        {
            await _repository.AddAsync(user);
        }

        public void Update(User user)
        {
            _repository.Update(user);
        }
        
        public async Task UpdateAsync(User user)
        {
            await _repository.UpdateAsync(user);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }
        
        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
