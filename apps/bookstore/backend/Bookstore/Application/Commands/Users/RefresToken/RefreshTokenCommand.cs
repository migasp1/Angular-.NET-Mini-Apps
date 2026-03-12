using Application.Commands.Users.AuthenticateUser;
using Application.CQRS.Interfaces;

namespace Application.Commands.Users.RefresToken;

public class RefreshTokenCommand : IBookstoreCommand<AuthenticateUserCommandResult>
{
    public string ExpiredJWTToken { get; set; } = string.Empty;
}
