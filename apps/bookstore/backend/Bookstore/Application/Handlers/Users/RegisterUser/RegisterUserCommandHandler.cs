using Application.CQRS;
using Application.CQRS.Interfaces;
using Application.Interfaces;
using Domain.Exceptions;

namespace Application.Handlers.Users.RegisterUser;

public class RegisterUserCommandHandler(
    IUserRepository userRepository,
    IUserMappings userMappings
    ) : ICommandHandler<RegisterUserCommand, Unit>
{
    public async Task<Unit> HandleAsync(RegisterUserCommand command)
    {
        var existingUser = await userRepository.GetUserByEmail(command.Email!);
        if (existingUser is not null)
            throw new EmailAlreadyExistsException("O e-mail já se encontra em uso");

        var mappedUser = userMappings.MapRegisterUserCommandToUser(command);

        await userRepository.CreateUser(mappedUser);

        return Unit.Value;
    }
}
