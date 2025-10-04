namespace Application.Handlers.Users;

public class RegisterUserCommand
{
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? Role { get; set; }
}
