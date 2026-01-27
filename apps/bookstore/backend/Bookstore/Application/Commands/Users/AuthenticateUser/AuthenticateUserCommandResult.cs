using Application.CQRS.Interfaces;

namespace Application.Commands.Users.AuthenticateUser;

public record AuthenticateUserCommandResult : IBookStoreResult
{
    public string JWTToken { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;
}
