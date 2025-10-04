using Bookstore.API.Configurations.Auth.JWTConfigurations;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Bookstore.API.Configurations.Auth;

public static class AuthenticationConfigurations
{
    public static void AddJWTAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        JWTSettings settings = configuration.GetSection("JWTSettings").Get<JWTSettings>() ??
            throw new InvalidJWTSettingsException("Não foi possível obter a configuração do token JWT");

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = settings.Issuer,
                    ValidAudience = settings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Secret))
                };
            });
    }
}
