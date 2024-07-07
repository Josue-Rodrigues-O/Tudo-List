using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public User GetById(int id)
        {
            return GetUserById(id);
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await GetUserByIdAsync(id);
        }

        public User GetByEmail(string email)
        {
            return _context.Users.AsNoTracking().FirstOrDefault(x => x.Email.Equals(email))
                ?? throw new KeyNotFoundException(nameof(User));
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email.Equals(email))
                ?? throw new KeyNotFoundException(nameof(User));
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        
        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public void Update(User entity)
        {
            _context.Users.Update(entity);
            _context.SaveChanges();
        }
        
        public async Task UpdateAsync(User entity)
        {
            _context.Users.Update(entity);
            await _context.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            var user = GetUserById(id);
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
        
        public async Task DeleteAsync(int id)
        {
            var user = await GetUserByIdAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        private User GetUserById(int id)
        {
            return _context.Users.Find(id)
                ?? throw new KeyNotFoundException(nameof(User));
        }

        private async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id)
                ?? throw new KeyNotFoundException(nameof(User));
        }
    }
}
