using Domain.Entities.Constraints;
using Domain.ErrorCodes;
using FluentValidation;

namespace Application.Handlers.Users.RegisterUser;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Continue;

        RuleFor(x => x.Email)
            .NotEmpty()
                .WithMessage("O e-mail tem de estar preenchido")
                .WithErrorCode(DomainErrorCodes.RequiredField)
            .EmailAddress()
                .WithMessage("Formato de e-mail inválido")
                .WithErrorCode(DomainErrorCodes.InvalidEmail)
            .MaximumLength(UserConstraints.EmailMaxLength)
                .WithMessage($"O e-mail não pode exceder os {UserConstraints.EmailMaxLength} caratéres")
                .WithErrorCode(DomainErrorCodes.EmailTooLong);

        RuleFor(x => x.Password)
            .NotEmpty()
                .WithMessage("A palavra-passe tem de estar preenchida")
                .WithErrorCode(DomainErrorCodes.RequiredField)
            .Matches("^(?=.*[a-z])(?=.*[A-Z]).{6,}$")
                .WithMessage("A palavra-passe deve ter pelo menos 6 caracteres e conter letras maiúsculas e minúsculas")
                .WithErrorCode(DomainErrorCodes.InvalidPasswordFormat)
            .MaximumLength(UserConstraints.EmailMaxLength)
                .WithMessage($"A palavra-passe não pode exceder os {UserConstraints.PasswordMaxLength} caratéres")
                .WithErrorCode(DomainErrorCodes.PasswordTooLong);

        RuleFor(x => x.RoleNames)
            .NotEmpty()
            .WithMessage("Tem de indicar pelo menos um papel")
            .WithErrorCode(DomainErrorCodes.RequiredField);
        RuleForEach(x => x.RoleNames)
            .Must(role => RoleConstraints.AllRoles.Contains(role))
            .WithMessage("Papel inválido")
            .WithErrorCode(DomainErrorCodes.InvalidRole);

    }
}
