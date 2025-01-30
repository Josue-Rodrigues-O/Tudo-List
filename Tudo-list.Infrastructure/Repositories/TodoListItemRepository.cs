using Microsoft.EntityFrameworkCore;
using Tudo_list.Infrastructure.Context;
using Tudo_List.Domain.Core.Interfaces.Repositories;
using Tudo_List.Domain.Entities;
using Tudo_List.Domain.Models;

namespace Tudo_list.Infrastructure.Repositories
{
    public class TodoListItemRepository(ApplicationDbContext context) : ITodoListItemRepository
    {
        public IEnumerable<TodoListItem> GetAll(int userId, TodoListItemQueryFilter filter)
        {
            var itemsQuery = context.TodoListItems.AsNoTracking().Where(item => item.UserId == userId);

            if (filter.Title is not null)
            {
                itemsQuery = itemsQuery.Where(item => item.Title.Contains(filter.Title, StringComparison.OrdinalIgnoreCase));
            }

            if (filter.Status is not null)
            {
                itemsQuery = itemsQuery.Where(item => item.Status == filter.Status);
            }

            if (filter.Priority is not null)
            {
                itemsQuery = itemsQuery.Where(item => item.Priority == filter.Priority);
            }

            if (filter.CreationDate is not null)
            {
                itemsQuery = itemsQuery.Where(item => item.CreationDate.Date == filter.CreationDate.Value.Date);
            }

            if (filter.InitialDate is not null)
            {
                itemsQuery = itemsQuery.Where(item => item.CreationDate.Date >= filter.InitialDate.Value.Date);
            }

            if (filter.FinalDate is not null)
            {
                itemsQuery = itemsQuery.Where(item => item.CreationDate.Date <= filter.FinalDate.Value.Date);
            }

            return [.. itemsQuery];
        }

        public async Task<IEnumerable<TodoListItem>> GetAllAsync(int userId, TodoListItemQueryFilter filter)
        {
            var itemsQuery = context.TodoListItems.AsNoTracking().Where(item => item.UserId == userId);

            if (filter.Title is not null)
            {
                itemsQuery = itemsQuery.Where(item => item.Title.Contains(filter.Title, StringComparison.OrdinalIgnoreCase));
            }

            if (filter.Status is not null)
            {
                itemsQuery = itemsQuery.Where(item => item.Status == filter.Status);
            }

            if (filter.Priority is not null)
            {
                itemsQuery = itemsQuery.Where(item => item.Priority == filter.Priority);
            }

            if (filter.CreationDate is not null)
            {
                itemsQuery = itemsQuery.Where(item => item.CreationDate.Date == filter.CreationDate.Value.Date);
            }

            if (filter.InitialDate is not null)
            {
                itemsQuery = itemsQuery.Where(item => item.CreationDate.Date >= filter.InitialDate.Value.Date);
            }

            if (filter.FinalDate is not null)
            {
                itemsQuery = itemsQuery.Where(item => item.CreationDate.Date <= filter.FinalDate.Value.Date);
            }

            return await itemsQuery.ToListAsync();
        }

        public TodoListItem? GetById(Guid id, int userId)
        {
            return context.TodoListItems.FirstOrDefault(item => item.Id == id && item.UserId == userId);
        }

        public async Task<TodoListItem?> GetByIdAsync(Guid id, int userId)
        {
            return await context.TodoListItems.FirstOrDefaultAsync(item => item.Id == id && item.UserId == userId);
        }

        public void Add(TodoListItem item)
        {
            context.TodoListItems.Add(item);
            context.SaveChanges();
        }

        public async Task AddAsync(TodoListItem item)
        {
            await context.TodoListItems.AddAsync(item);
            await context.SaveChangesAsync();
        }

        public void Update(TodoListItem item)
        {
            context.TodoListItems.Update(item);
            context.SaveChanges();
        }

        public async Task UpdateAsync(TodoListItem item)
        {
            context.TodoListItems.Update(item);
            await context.SaveChangesAsync();
        }

        public void Remove(TodoListItem item)
        {
            context.TodoListItems.Remove(item);
            context.SaveChanges();
        }

        public async Task RemoveAsync(TodoListItem item)
        {
            context.TodoListItems.Remove(item);
            await context.SaveChangesAsync();
        }
    }
}
