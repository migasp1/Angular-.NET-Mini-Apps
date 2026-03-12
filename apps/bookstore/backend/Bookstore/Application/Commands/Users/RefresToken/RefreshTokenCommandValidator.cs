using Domain.ErrorCodes;
using FluentValidation;

namespace Application.Commands.Users.RefresToken;

public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenCommandValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Continue;

        RuleFor(x => x.ExpiredJWTToken)
            .NotEmpty()
                .WithMessage("Token em falta")
                .WithErrorCode(DomainErrorCodes.RequiredField);
    }
}
