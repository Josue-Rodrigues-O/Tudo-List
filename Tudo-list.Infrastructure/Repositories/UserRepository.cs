using Tudo_list.Infrastructure.Context;
using Tudo_List.Domain.Core.Interfaces.Repositories;
using Tudo_List.Domain.Entities;

namespace Tudo_list.Infrastructure.Repositories
{
    public class UserRepository(ApplicationDbContext context) : IUserRepository
    {
        private readonly ApplicationDbContext _context = context;

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        public User Get(int id)
        {
            return GetUser(id);
        }

        public void Save(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Update(User entity)
        {
            _context.Users.Update(entity);
        }

        public void Delete(int id)
        {
            var user = GetUser(id);
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        private User GetUser(int id)
        {
            var user = _context.Users.Find(id) ?? throw new KeyNotFoundException(nameof(User));
            return user;
        }
    }
}
