using Domain.Entities;

namespace Application.Interfaces;

public interface IJWTTokenGeneratorService
{
    string GenerateJWTToken(User user);
    string GenerateRefreshToken();
}
