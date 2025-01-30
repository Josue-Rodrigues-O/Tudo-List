using Microsoft.EntityFrameworkCore;
using Tudo_list.Infrastructure.Context;
using Tudo_List.Domain.Core.Interfaces.Repositories;
using Tudo_List.Domain.Entities;

namespace Tudo_list.Infrastructure.Repositories
{
    public class UserImageRepository(ApplicationDbContext context) : IUserImageRepository
    {
        public UserImage? GetByUserId(int userId)
        {
            return context.UserImages.AsNoTracking().FirstOrDefault(x => x.UserId == userId);
        }

        public async Task<UserImage?> GetByUserIdAsync(int userId)
        {
            return await context.UserImages.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public void Add(UserImage image)
        {
            context.UserImages.Add(image);
        }

        public async Task AddAsync(UserImage image)
        {
            await context.UserImages.AddAsync(image);
        }

        public void Remove(UserImage image)
        {
            context.UserImages.Remove(image);
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
