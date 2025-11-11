using Bookstore.API.Configurations.Auth.JWTConfigurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Bookstore.API.Configurations.Auth;

public static class AuthenticationConfigurations
{
    public static void AddJWTAuthentication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var jwtConfigs = configuration.GetSection("jwtSettings").Get<JWTSettings>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtConfigs.Issuer,
                    ValidAudience = jwtConfigs.Audience,
                    ValidateLifetime = true,
                    RequireExpirationTime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfigs.Secret))
                };
            });
    }
}
