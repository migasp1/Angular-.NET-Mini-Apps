using Application.CQRS.Interfaces;

namespace Application.Handlers.Users.AuthenticateUser;

public class AuthenticateUserCommandHandler : ICommandHandler<AuthenticateUserCommand>
{
    public Task HandleAsync(AuthenticateUserCommand command)
    {
        throw new NotImplementedException();
    }
}
