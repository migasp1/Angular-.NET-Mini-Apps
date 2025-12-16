using Domain.Entities;

namespace Application.Interfaces;

public interface IUserRepository
{
    Task CreateUser(User user);
    Task UpdateUser(User user);
    Task<User?> GetUserByEmail(string userEmail);
}
