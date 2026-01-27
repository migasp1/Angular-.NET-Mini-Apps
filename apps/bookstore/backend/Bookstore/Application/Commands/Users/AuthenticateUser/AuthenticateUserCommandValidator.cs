using FluentValidation;

namespace Application.Handlers.Users.AuthenticateUser;

public class AuthenticateUserCommandValidator : AbstractValidator<AuthenticateUserCommand>
{
    public AuthenticateUserCommandValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Continue;
    }
}
