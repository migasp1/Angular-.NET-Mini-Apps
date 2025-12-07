namespace Application.Handlers.Users.AuthenticateUser;

public class AuthenticateUserCommand
{
    public string? Email { get; set; }
    public string? Password { get; set; }
}
