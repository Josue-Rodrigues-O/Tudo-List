using Microsoft.EntityFrameworkCore;
using Tudo_List.Domain.Entities;

namespace Tudo_list.Infrastructure.Context
{
    public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<TodoListItem> TodoListItems { get; set; }
    }
}
