using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tudo_list.Infrastructure.Context;
using Tudo_List.Domain.Core.Interfaces.Repositories;
using Tudo_List.Domain.Entities;

namespace Tudo_list.Infrastructure.Repositories
{
    public class TodoListItemRepository(ApplicationDbContext context) : ITodoListItemRepository
    {
        private readonly ApplicationDbContext _context = context;
        private readonly DbSet<TodoListItem> _items = context.TodoListItems;

        public IEnumerable<TodoListItem> GetAll(int? userId = null)
        {
            var items = userId is null
                ? _items
                : _items.Where(item => item.UserId == userId);

            return [.. items];
        }

        public async Task<IEnumerable<TodoListItem>> GetAllAsync(int? userId = null)
        {
            var items = userId is null 
                ? _items 
                : _items.Where(item => item.UserId == userId);

            return await items.ToListAsync();
        }

        public TodoListItem? GetById(Guid id, int? userId = null)
        {
            return userId is null
                ? _items.Find(id)
                : _items.AsNoTracking().FirstOrDefault(item => item.Id == id && item.UserId == userId);
        }

        public async Task<TodoListItem?> GetByIdAsync(Guid id, int? userId = null)
        {
            return userId is null
                ? await _items.FindAsync(id)
                : await _items.AsNoTracking().FirstOrDefaultAsync(item => item.Id == id && item.UserId == userId);
        }

        public void Add(TodoListItem item)
        {
            _items.Add(item);
            _context.SaveChanges();
        }

        public async Task AddAsync(TodoListItem item)
        {
            await _items.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public void Update(TodoListItem item)
        {
            _items.Update(item);
            _context.SaveChanges();
        }

        public async Task UpdateAsync(TodoListItem item)
        {
            _items.Update(item);
            await _context.SaveChangesAsync();
        }

        public void Remove(TodoListItem item)
        {
            _items.Remove(item);
            _context.SaveChanges();
        }

        public async Task RemoveAsync(TodoListItem item)
        {
            _items.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}
