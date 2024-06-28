using Tudo_List.Domain.Core.Interfaces.Repositories;
using Tudo_List.Domain.Core.Interfaces.Services;
using Tudo_List.Domain.Entities;

namespace Tudo_List.Domain.Services
{
    public class UserService(IUserRepository repository) : IUserService
    {
        private readonly IUserRepository _repository = repository;

        public void Register(User user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            return _repository.GetAll();
        }

        public User GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Update(User user)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
