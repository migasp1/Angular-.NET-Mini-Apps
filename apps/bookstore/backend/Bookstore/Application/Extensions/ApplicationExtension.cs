using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class ApplicationExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services.AddValidatorsFromAssembly(typeof(ApplicationExtension).Assembly);
    }
}
