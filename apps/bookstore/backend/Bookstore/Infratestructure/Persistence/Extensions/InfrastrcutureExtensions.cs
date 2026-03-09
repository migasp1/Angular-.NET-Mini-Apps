using Application.Handlers.Users.Profiles;
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


        // Register the services 
        services.AddScoped<ICryptographyService, CryptographyService>();
        services.AddScoped<IJWTTokenService, JWTTokenGeneratorService>();

        // Register mappings 
        services.AddScoped<IUserMappings, UserMappings>();

        // Register the repos
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
    }
}
