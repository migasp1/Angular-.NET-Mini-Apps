using Application.CQRS;
using Application.CQRS.Interfaces;
using Application.Handlers.Users.Profiles;
using Application.Handlers.Users.RegisterUser;
using Application.Interfaces;
using Bookstore.API.Configurations.Auth.JWTConfigurations;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure.Persistence.Extensions;

public static class InfrastrcutureExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));
        services.Configure<DatabaseSettings>(configuration.GetSection("DatabaseSettings"));

        services.AddDbContext<BookStoreDbContext>((provider, options) =>
        {
            var dbSettings = provider.GetRequiredService<IOptions<DatabaseSettings>>().Value;

            if (dbSettings.Provider == "SqlServer")
            {
                options.UseSqlServer(
                    dbSettings.ConnectionString,
                    x => x.MigrationsAssembly("Infrastructure")
                );
            }
            else
            {
                // Add another provider in the future if I'm feeling excentric
            }
        });

        services.AddScoped<ICommandDispatcher, CommandDispatcher>();

        // Register the services 
        services.AddScoped<ICryptographyService, CryptographyService>();

        // Register mappings 
        services.AddScoped<IUserMappings, UserMappings>();

        // Register the repos
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();

        // Register the commands (could be implemented with scanning, but for sake of simplicity, I chose this approach)
        services.AddScoped<ICommandHandler<RegisterUserCommand>, RegisterUserCommandHandler>();
    }
}
