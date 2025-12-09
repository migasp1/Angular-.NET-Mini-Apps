using Application.CQRS;
using Application.CQRS.Interfaces;
using Application.Handlers.Users.RegisterUser;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class ApplicationExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICommandDispatcher, CommandDispatcher>();

        // Register the commands (could be implemented with scanning, but for sake of simplicity, I chose this approach)
        services.AddScoped<ICommandHandler<RegisterUserCommand>, RegisterUserCommandHandler>();

        return services.AddValidatorsFromAssembly(typeof(ApplicationExtension).Assembly);
    }
}
