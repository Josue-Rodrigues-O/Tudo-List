using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tudo_list.Infrastructure.Configuration;
using Tudo_list.Infrastructure.Configuration.Constants;
using Tudo_list.Infrastructure.Context;
using Tudo_list.Infrastructure.Repositories;
using Tudo_List.Application;
using Tudo_List.Application.Interfaces.Applications;
using Tudo_List.Application.Interfaces.Services;
using Tudo_List.Application.Mappers;
using Tudo_List.Application.Services;
using Tudo_List.Domain.Core.Interfaces.Configuration;
using Tudo_List.Domain.Core.Interfaces.Factories;
using Tudo_List.Domain.Core.Interfaces.Repositories;
using Tudo_List.Domain.Core.Interfaces.Services;
using Tudo_List.Domain.Entities;
using Tudo_List.Domain.Services;
using Tudo_List.Domain.Services.Factories;
using Tudo_List.Domain.Services.Validation;

namespace Tudo_list.Infrastructure.CrossCutting.Ioc
{
    public static class ServicesCollectionExtensions
    {
        public static IServiceCollection AddAutoMapper(this IServiceCollection servicesCollection)
        {
            var mappingConfig = new MapperConfiguration(config => 
            {
                config.AddProfile(new DtoToUserMapping());
                config.AddProfile(new DtoToTodoListItemMapping());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            servicesCollection.AddSingleton(mapper);

            return servicesCollection;
        }

        public static IServiceCollection AddDatabaseContext(this IServiceCollection servicesCollection, ConfigurationManager config)
        {
            servicesCollection.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString(SecretsKeys.SqlServerConnectionString));
            });

            servicesCollection.AddScoped<ApplicationDbContext>();

            return servicesCollection;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection servicesCollection)
        {
            servicesCollection.AddScoped<IUserRepository, UserRepository>();
            servicesCollection.AddScoped<ITodoListItemRepository, TodoListItemRepository>();

            return servicesCollection;
        }

        public static IServiceCollection AddDomainServices(this IServiceCollection servicesCollection)
        {
            servicesCollection.AddScoped<IUserService, UserService>();
            servicesCollection.AddScoped<ITodoListItemService, TodoListItemService>();
            servicesCollection.AddTransient<IPasswordStrategyFactory, PasswordStrategyFactory>();

            return servicesCollection;
        }
        
        public static IServiceCollection AddApplicationServices(this IServiceCollection servicesCollection)
        {
            servicesCollection.AddScoped<IUserApplication, UserApplication>();
            servicesCollection.AddScoped<ITodoListItemApplication, TodoListItemApplication>();
            servicesCollection.AddScoped<IAuthService, AuthService>();
            servicesCollection.AddScoped<ICurrentUserService, CurrentUserService>();
            servicesCollection.AddScoped<IValidator<TodoListItem>, TodoListItemValidator>();
            servicesCollection.AddTransient<ITokenService, TokenService>();

            return servicesCollection;
        }
        
        public static IServiceCollection AddApplicationSecrets(this IServiceCollection servicesCollection)
        {
            servicesCollection.AddScoped<ISecrets, Secrets>();

            return servicesCollection;
        }
    }
}
