using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Tudo_list.Infrastructure.Context;

namespace Tudo_list.Infrastructure.CrossCutting.Ioc
{
    public static class ServicesCollectionExtensions
    {
        public static IServiceCollection AddMapper(this IServiceCollection servicesCollection, string connectionString)
        {
            servicesCollection.AddDbContext<ApplicationDbContext>(options =>
            {
                const string envServer = "DB_SERVER";
                const string envDatabase = "DB_DATABASE";

                options.UseSqlServer(connectionString);
            });

            return servicesCollection;
        }

        public static IServiceCollection AddDatabaseContext(this IServiceCollection servicesCollection)
        {
            servicesCollection.TryAddScoped<ApplicationDbContext>();

            return servicesCollection;
        }
    }
}
