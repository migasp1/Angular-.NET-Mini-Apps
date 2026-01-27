using Application.Handlers.Users.RegisterUser;
using Domain.Entities;

namespace Application.Interfaces;

public interface IUserMappings
{
    User MapRegisterUserCommandToUser(RegisterUserCommand registerUserCommand);
}
