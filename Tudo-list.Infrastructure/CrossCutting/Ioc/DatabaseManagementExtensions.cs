using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Tudo_list.Infrastructure.Context;

namespace Tudo_list.Infrastructure.CrossCutting.Ioc
{
    public static class DatabaseManagementExtensions
    {
        public static void EnsureDatabaseCreated(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            var serviceDb = scope.ServiceProvider.GetService<ApplicationDbContext>();

            serviceDb!.Database.EnsureCreated();
        }
    }
}
