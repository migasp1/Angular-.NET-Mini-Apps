using Application.CQRS.Interfaces;

namespace Application.Handlers.Users.AuthenticateUser;

public class AuthenticateUserCommand : IBookstoreCommand
{
    public string? Email { get; set; }
    public string? Password { get; set; }
}
