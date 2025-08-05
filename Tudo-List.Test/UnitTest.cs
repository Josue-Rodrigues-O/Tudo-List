using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections;
using Tudo_list.Infrastructure.Context;
using Tudo_list.Infrastructure.CrossCutting.Ioc;
using Tudo_List.Domain.Core.Interfaces.Configuration;
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
            _serviceProvider = GetServiceCollection().BuildServiceProvider();
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
            services.AddScoped<ISecrets, SecretsMock>();

            return services;
        }

        protected void SaveInMemoryDatabase<T>(T entity) where T : class
        {
            if (entity is IEnumerable collection)
            {
                foreach (var item in collection)
                {
                    _context.Add(item);
                }
            }
            else
            {
                _context.Add(entity);
            }

            _context.SaveChanges();
        }
    }
}