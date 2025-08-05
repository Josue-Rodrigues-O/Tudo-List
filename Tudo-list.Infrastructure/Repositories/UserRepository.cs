using Microsoft.EntityFrameworkCore;
using Tudo_list.Infrastructure.Context;
using Tudo_List.Domain.Core.Interfaces.Repositories;
using Tudo_List.Domain.Entities;

namespace Tudo_list.Infrastructure.Repositories
{
    public class UserRepository(ApplicationDbContext context) : IUserRepository
    {
        public IEnumerable<User> GetAll()
        {
            return context.Users.AsNoTracking();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await context.Users.AsNoTracking().ToListAsync();
        }

        public User? GetById(int id)
        {
            return context.Users.Find(id);
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await context.Users.FindAsync(id);
        }

        public User? GetByEmail(string email)
        {
            return context.Users.FirstOrDefault(user => user.Email.Equals(email));
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await context.Users.FirstOrDefaultAsync(user => user.Email.Equals(email));
        }

        public void Add(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
        }

        public async Task AddAsync(User user)
        {
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
        }

        public void Update(User user)
        {
            context.Users.Update(user);
            context.SaveChanges();
        }

        public async Task UpdateAsync(User user)
        {
            context.Users.Update(user);
            await context.SaveChangesAsync();
        }

        public void Remove(User user)
        {
            context.Users.Remove(user);
            context.SaveChanges();
        }

        public async Task RemoveAsync(User user)
        {
            context.Users.Remove(user);
            await context.SaveChangesAsync();
        }
    }
}
