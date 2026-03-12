using Domain.Entities;

namespace Application.Interfaces;

public interface IJWTTokenService
{
    string GenerateJWTToken(User user);
    (string, DateTime) GenerateAndSetRefreshToken();
    string GetUserEmailFromToken(string token);
    string GetRefreshTokenFromHttpHeader();
}
