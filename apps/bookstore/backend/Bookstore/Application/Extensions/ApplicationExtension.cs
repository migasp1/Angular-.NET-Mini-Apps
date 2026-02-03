using Application.Commands.Users.AuthenticateUser;
using Application.CQRS;
using Application.CQRS.Interfaces;
using Application.Handlers.Users.AuthenticateUser;
using Application.Handlers.Users.RegisterUser;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class ApplicationExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IBookStoreCommandDispatcher, CommandDispatcher>();
        services.AddScoped<ICommandHandler<RegisterUserCommand, Unit>, RegisterUserCommandHandler>();
        services.AddScoped<ICommandHandler<AuthenticateUserCommand, AuthenticateUserCommandResult>, AuthenticateUserCommandHandler>();

        return services.AddValidatorsFromAssembly(typeof(ApplicationExtension).Assembly);
    }
}
