using Application.CQRS;
using Application.CQRS.Interfaces;

namespace Application.Handlers.Users.RegisterUser;

public class RegisterUserCommand : IBookstoreCommand<Unit>
{
    public string? Email { get; set; }
    public string? Password { get; set; }
    public List<string>? RoleNames { get; set; }
}
