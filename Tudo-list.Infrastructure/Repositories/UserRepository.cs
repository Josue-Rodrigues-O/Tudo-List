using Tudo_list.Infrastructure.Context;
using Tudo_List.Domain.Core.Interfaces.Repositories;
using Tudo_List.Domain.Entities;

namespace Tudo_list.Infrastructure.Repositories
{
    public class UserRepository(ApplicationDbContext context) : IUserRepository
    {
        private readonly ApplicationDbContext _context = context;

        public void Save(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        public User GetById(int id)
        {
            return GetUser(id);
        }

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(User entity)
        {
            throw new NotImplementedException();
        }

        private User GetUser(int id)
        {
            var user = _context.Users.Find(id) ?? throw new KeyNotFoundException();
            return user;
        }
    }
}
