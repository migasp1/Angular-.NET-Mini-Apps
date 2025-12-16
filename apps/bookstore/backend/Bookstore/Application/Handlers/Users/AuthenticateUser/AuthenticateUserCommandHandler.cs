using Application.Commands.Users.AuthenticateUser;
using Application.CQRS.Interfaces;
using Application.Interfaces;
using Domain.Exceptions;

namespace Application.Handlers.Users.AuthenticateUser;

public class AuthenticateUserCommandHandler(
    IUserRepository userRepository,
    ICryptographyService cryptographyService,
    IJWTTokenGeneratorService jWTTokenGeneratorService) : ICommandHandler<AuthenticateUserCommand, AuthenticateUserResult>
{
    public async Task<AuthenticateUserResult> HandleAsync(AuthenticateUserCommand command)
    {
        var user = await userRepository.GetUserByEmail(command.Email!) ?? throw new UserNotAuthenticatedException("Credenciais inválidas");

        var (PasswordHash, PasswordSalt) = cryptographyService.GetPasswordData(command.Password!);

        if (user.PasswordHash != PasswordHash && user.PasswordSalt != PasswordSalt)
            throw new UserNotAuthenticatedException("Credenciais inválidas");

        var jwttoken = jWTTokenGeneratorService.GenerateJWTToken(user);
        var refreshToken = jWTTokenGeneratorService.GenerateRefreshToken();

        user.RefreshTokenHash = refreshToken;
        user.RefreshTokenExpiricyDate = DateTime.UtcNow;

        await userRepository.UpdateUser(user);

        return new AuthenticateUserResult()
        {
            JWTToken = jwttoken,
            RefreshToken = refreshToken
        };
    }
}
