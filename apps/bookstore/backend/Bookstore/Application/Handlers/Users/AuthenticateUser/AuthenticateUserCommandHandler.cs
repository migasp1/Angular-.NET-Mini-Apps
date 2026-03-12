using Application.Commands.Users.AuthenticateUser;
using Application.CQRS.Interfaces;
using Application.Interfaces;
using Domain.Exceptions;

namespace Application.Handlers.Users.AuthenticateUser;

public class AuthenticateUserCommandHandler(
    IUserRepository userRepository,
    ICryptographyService cryptographyService,
    IJWTTokenService jWTTokenGeneratorService) : ICommandHandler<AuthenticateUserCommand, AuthenticateUserCommandResult>
{
    public async Task<AuthenticateUserCommandResult> HandleAsync(AuthenticateUserCommand command)
    {
        var user = await userRepository.GetUserByEmail(command.Email!) ?? throw new UserNotAuthenticatedException("Credenciais inválidas");

        var isAuthenticated = cryptographyService.IsValidPassword(command.Password!, user.PasswordHash, user.PasswordSalt);

        if (!isAuthenticated)
            throw new UserNotAuthenticatedException("Credenciais inválidas");

        var jwttoken = jWTTokenGeneratorService.GenerateJWTToken(user);
        var (refreshToken, expirationDate) = jWTTokenGeneratorService.GenerateAndSetRefreshToken();

        user.RefreshTokenHash = cryptographyService.HashPlainText(refreshToken);
        user.RefreshTokenExpiricyDate = expirationDate;

        await userRepository.UpdateUser(user);

        return new AuthenticateUserCommandResult()
        {
            JWTToken = jwttoken
        };
    }
}
