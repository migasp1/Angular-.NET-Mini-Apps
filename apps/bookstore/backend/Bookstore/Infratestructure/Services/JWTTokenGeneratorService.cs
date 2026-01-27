using Application.Interfaces;
using Bookstore.API.Configurations.Auth.JWTConfigurations;
using Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services;

public class JWTTokenGeneratorService(IOptions<JWTSettings> jwtConfigs, ICryptographyService cryptographyService) : IJWTTokenGeneratorService
{
    public string GenerateJWTToken(User user)
    {
        var jwtConfigsValue = jwtConfigs.Value;

        var token = new JwtSecurityToken(
            issuer: jwtConfigsValue.Issuer,
            audience: jwtConfigsValue.Audience,
            claims: GetClaims(user),
            expires: DateTime.UtcNow.AddMinutes(jwtConfigsValue.ExpiryMinutes),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfigsValue.Secret)),
                SecurityAlgorithms.HmacSha256
            )
        );

        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        return cryptographyService.HashPlainText(Guid.NewGuid().ToString());
    }


    #region Private methods

    private Claim[] GetClaims(User user)
    {
        var claims = new List<Claim>()
        {
            new(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email!),
        };

        claims.AddRange(user.Roles!.Select(r => new Claim(ClaimTypes.Role, r.Name)));

        return [.. claims];
    }

    #endregion
}
