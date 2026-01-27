using Application.Commands.Users.AuthenticateUser;
using Application.CQRS.Interfaces;

namespace Application.Handlers.Users.AuthenticateUser;

public class AuthenticateUserCommand : IBookstoreCommand<AuthenticateUserCommandResult>
{
    public string? Email { get; set; }
    public string? Password { get; set; }
}
