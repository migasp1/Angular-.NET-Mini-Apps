using Domain.Entities;

namespace Application.Interfaces;

public interface ICurrentUserService
{
    Task<User> ValidateAndGetCurrentLoggedUser();
}
