using Microsoft.EntityFrameworkCore;
using Tudo_List.Domain.Entities;

namespace Tudo_list.Infrastructure.Context
{
    public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<TodoListItem> TodoListItems { get; set; }
        public DbSet<UserImage> UserImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TodoListItem>()
                .HasOne(t => t.User)
                .WithMany()
                .HasForeignKey(t => t.UserId)
                .IsRequired();

            modelBuilder.Entity<UserImage>()
                .HasOne(i => i.User)
                .WithOne(u => u.Image)
                .HasForeignKey<UserImage>(i => i.UserId)
                .IsRequired();  
        }
    }
}
