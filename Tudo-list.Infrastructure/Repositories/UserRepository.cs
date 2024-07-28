using Microsoft.EntityFrameworkCore;
using Tudo_list.Infrastructure.Context;
using Tudo_List.Domain.Core.Interfaces.Repositories;
using Tudo_List.Domain.Entities;

namespace Tudo_list.Infrastructure.Repositories
{
    public class UserRepository(ApplicationDbContext context) : IUserRepository
    {
        private readonly ApplicationDbContext _context = context;
        private readonly DbSet<User> _users = context.Users;

        public IEnumerable<User> GetAll()
        {
            return _users;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _users.ToListAsync();
        }

        public User? GetById(int id)
        {
            return _users.Find(id);
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _users.FindAsync(id);
        }

        public User? GetByEmail(string email)
        {
            return _users.AsNoTracking().FirstOrDefault(x => x.Email.Equals(email));
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _users.AsNoTracking().FirstOrDefaultAsync(x => x.Email.Equals(email));
        }

        public void Add(User user)
        {
            _users.Add(user);
            _context.SaveChanges();
        }

        public async Task AddAsync(User user)
        {
            await _users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public void Update(User user)
        {
            _users.Update(user);
            _context.SaveChanges();
        }

        public async Task UpdateAsync(User user)
        {
            _users.Update(user);
            await _context.SaveChangesAsync();
        }

        public void Remove(User user)
        {
            _users.Remove(user);
            _context.SaveChanges();
        }

        public async Task RemoveAsync(User user)
        {
            _users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
