using Application.Handlers.Users.RegisterUser;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Handlers.Users.Profiles;

public class UserMappings(ICryptographyService cryptographyService) : IUserMappings
{
    public User MapRegisterUserCommandToUser(RegisterUserCommand command)
    {
        (string passwordHash, string passwordSalt) = cryptographyService.GetPasswordData(command.Password!);
        return new User()
        {
            Email = command.Email,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Roles = [ ..command.RoleNames!.Select(name => new Role() {
                Name = name
            })]
        };
    }
}
