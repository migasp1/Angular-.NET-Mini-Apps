using Application.Commands.Users.AuthenticateUser;
using Application.Commands.Users.RefresToken;
using Application.CQRS.Interfaces;
using Application.Interfaces;
using Domain.Exceptions;

namespace Application.Handlers.Users.RefreshToken;

public class RefreshTokenCommandHandler(
    IJWTTokenService jwtTokenGeneratorService,
    ICryptographyService cryptographyService,
    IUserRepository userRepository
    ) : ICommandHandler<RefreshTokenCommand, AuthenticateUserCommandResult>
{
    public async Task<AuthenticateUserCommandResult> HandleAsync(RefreshTokenCommand command)
    {
        var userEmail = jwtTokenGeneratorService.GetUserEmailFromToken(command.ExpiredJWTToken);

        var user = await userRepository.GetUserByEmail(userEmail)
            ?? throw new NotFoundException("Não foi possível encontrar o utilizador");

        var refreshTokenFromCookie = jwtTokenGeneratorService.GetRefreshTokenFromHttpHeader();

        var hashedRefreshToken = cryptographyService.HashPlainText(refreshTokenFromCookie);

        if (user.RefreshTokenHash != hashedRefreshToken || user.RefreshTokenExpiricyDate <= DateTime.UtcNow)
        {
            throw new UserNotAuthenticatedException("Sessão expirada. Por favor, faça login novamente");
        }

        var jwttoken = jwtTokenGeneratorService.GenerateJWTToken(user);
        var (refreshToken, expirationDate) = jwtTokenGeneratorService.GenerateAndSetRefreshToken();

        user.RefreshTokenHash = cryptographyService.HashPlainText(refreshToken);
        user.RefreshTokenExpiricyDate = expirationDate;

        await userRepository.UpdateUser(user);

        return new AuthenticateUserCommandResult()
        {
            JWTToken = jwttoken
        };
    }
}
