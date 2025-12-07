using Bookstore.API.Middlewares;

namespace Bookstore.API.Extensions;

public static class PresentationExtensions
{
    public static void AddPresentation(this IServiceCollection services)
    {
        services.AddScoped<GlobalExceptionHandlerMiddleware>();
    }
}
