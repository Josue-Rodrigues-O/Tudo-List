using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tudo_list.Infrastructure.Context;
using Tudo_list.Infrastructure.CrossCutting.Ioc;
using Tudo_List.Domain.Core.Interfaces.Services;
using Tudo_List.Test.Mock;

namespace Tudo_List.Test
{
    public abstract class UnitTest
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ApplicationDbContext _context;

        public UnitTest()
        {
            var serviceCollection = GetServiceCollection();
            _serviceProvider = serviceCollection.BuildServiceProvider();
            _context = _serviceProvider.GetRequiredService<ApplicationDbContext>();
        }

        private static ServiceCollection GetServiceCollection()
        {
            var services = new ServiceCollection();

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
            });

            services.AddScoped<ApplicationDbContext>();

            services.AddDomainServices();
            services.AddApplicationSecrets();
            services.AddAutoMapper();
            services.AddRepositories();
            services.AddApplicationServices();

            services.AddScoped<ICurrentUserService, CurrentUserServiceMock>();

            return services;
        }

        protected void InitializeInMemoryDatabase<T>(IEnumerable<T> collection) where T : class
        {
            foreach (var item in collection)
            {
                _context.Add(item);
            }

            _context.SaveChanges();
        }
    }
}