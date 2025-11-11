using Application.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;

namespace Infrastructure.Services;

public class CurrentUserService(IHttpContextAccessor httpContext, IUserRepository userRepository) : ICurrentUserService
{
    public async Task<User> ValidateAndGetCurrentLoggedUser()
    {
        // Just in case this method is called in some place where the authentication was bypassed (ex: AllowAnonymous enpoint)
        if (httpContext.HttpContext?.User?.Identity is { IsAuthenticated: false }) throw new UserNotAuthenticatedException("Não foi possível autenticar o utilizador");

        var userEmailCaim = httpContext.HttpContext?.User.FindFirst(JwtRegisteredClaimNames.Email)?.Value;

        return userEmailCaim is null
            ? throw new InvalidUserIdException("Não foi possível obter o utilizador a partir do token")
            : await userRepository.GetUserByEmail(userEmailCaim) ?? throw new InvalidUserIdException("Não foi possível encontrar o utilizador");
    }

}
