using Microsoft.EntityFrameworkCore;
using Tudo_List.Domain.Entities;

namespace Tudo_list.Infrastructure.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<TodoListItem> TodoListItems { get; set; }
    }
}
