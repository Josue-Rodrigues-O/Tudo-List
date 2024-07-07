using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Tudo_list.Infrastructure.Context;
using Tudo_list.Infrastructure.Repositories;
using Tudo_List.Application;
using Tudo_List.Application.Interfaces.Applications;
using Tudo_List.Application.Interfaces.Services;
using Tudo_List.Application.Mappers;
using Tudo_List.Application.Services;
using Tudo_List.Domain.Core.Interfaces.Factories;
using Tudo_List.Domain.Core.Interfaces.Repositories;
using Tudo_List.Domain.Core.Interfaces.Services;
using Tudo_List.Domain.Services;
using Tudo_List.Domain.Services.Factories;

namespace Tudo_list.Infrastructure.CrossCutting.Ioc
{
    public static class ServicesCollectionExtensions
    {
        public static IServiceCollection AddAutoMapper(this IServiceCollection servicesCollection)
        {
            var mappingConfig = new MapperConfiguration(x => 
            {
                x.AddProfile(new DtoToUserMapping());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            servicesCollection.AddSingleton(mapper);

            return servicesCollection;
        }

        public static IServiceCollection AddDatabaseContext(this IServiceCollection servicesCollection, string connectionString)
        {
            servicesCollection.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            servicesCollection.TryAddScoped<ApplicationDbContext>();

            return servicesCollection;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection servicesCollection)
        {
            servicesCollection.TryAddScoped<IUserRepository, UserRepository>();

            return servicesCollection;
        }

        public static IServiceCollection AddDomainServices(this IServiceCollection servicesCollection)
        {
            servicesCollection.TryAddScoped<IUserService, UserService>();
            servicesCollection.TryAddTransient<IPasswordStrategyFactory, PasswordStrategyFactory>();

            return servicesCollection;
        }
        
        public static IServiceCollection AddApplicationServices(this IServiceCollection servicesCollection)
        {
            servicesCollection.TryAddScoped<IUserApplication, UserApplication>();
            servicesCollection.TryAddTransient<ITokenService, TokenService>();
            servicesCollection.TryAddScoped<IAuthService, AuthService>();

            return servicesCollection;
        }

    }
}
