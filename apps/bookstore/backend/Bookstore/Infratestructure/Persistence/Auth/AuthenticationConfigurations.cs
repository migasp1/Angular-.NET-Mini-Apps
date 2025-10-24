using Bookstore.API.Configurations.Auth.JWTConfigurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Bookstore.API.Configurations.Auth;

public static class AuthenticationConfigurations
{
    public static void AddJWTAuthentication(
        this IServiceCollection services,
        IOptions<JWTSettings> jwtConfigs,
        IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtConfigs.Value.Issuer,
                    ValidAudience = jwtConfigs.Value.Audience,
                    ValidateLifetime = true,
                    RequireExpirationTime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfigs.Value.Secret))
                };
            });
    }
}
