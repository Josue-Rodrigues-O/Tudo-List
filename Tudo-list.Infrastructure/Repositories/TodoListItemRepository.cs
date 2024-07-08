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

        public IEnumerable<TodoListItem> GetAll()
        {
            return _items;
        }

        public async Task<IEnumerable<TodoListItem>> GetAllAsync()
        {
            return await _items.ToListAsync();
        }

        public TodoListItem GetById(Guid id)
        {
            return GetItemById(id);
        }

        public async Task<TodoListItem> GetByIdAsync(Guid id)
        {
            return await GetItemByIdAsync(id);
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

        public void Remove(Guid id)
        {
            var item = GetItemById(id);
            _items.Remove(item);
            _context.SaveChanges();
        }

        public async Task RemoveAsync(Guid id)
        {
            var item = await GetItemByIdAsync(id);
            _items.Remove(item);
            await _context.SaveChangesAsync();
        }

        private TodoListItem GetItemById(Guid id)
        {
            return _items.Find(id)
                ?? throw new KeyNotFoundException(nameof(TodoListItem));
        }

        private async Task<TodoListItem> GetItemByIdAsync(Guid id)
        {
            return await _items.FindAsync(id)
                ?? throw new KeyNotFoundException(nameof(TodoListItem));
        }
    }
}
