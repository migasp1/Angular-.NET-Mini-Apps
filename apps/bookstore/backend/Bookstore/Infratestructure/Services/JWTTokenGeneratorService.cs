using Application.Interfaces;
using Bookstore.API.Configurations.Auth.JWTConfigurations;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services;

public class JWTTokenGeneratorService(
    IOptions<JWTSettings> jwtConfigs,
    IHttpContextAccessor httpContextAccessor) : IJWTTokenService
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

    public (string, DateTime) GenerateAndSetRefreshToken()
    {
        var expirationDate = DateTime.UtcNow.AddDays(7);
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = expirationDate
        };

        var refreshToken = Guid.NewGuid().ToString();
        httpContextAccessor.HttpContext!.Response.Cookies.Append("RefreshToken", refreshToken, cookieOptions);

        return (refreshToken, expirationDate);
    }

    public string GetUserEmailFromToken(string token)
    {
        var jwtConfigsValue = jwtConfigs.Value;

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfigsValue.Secret)),
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);

        if (validatedToken is not JwtSecurityToken jwtSecurityToken
            || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            throw new InvalidTokenException("Token inválido");

        return principal.FindFirst(ClaimTypes.Email)!.Value;
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

    public string GetRefreshTokenFromHttpHeader()
    {
        var cookieRefreshToken = httpContextAccessor.HttpContext!.Request.Cookies["RefreshToken"];

        return cookieRefreshToken ?? throw new UnauthorizedAccessException("Não foi possivel validar o refresh token");
    }

    #endregion
}
