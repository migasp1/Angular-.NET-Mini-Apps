using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository(BookStoreDbContext bookStoreDb) : IUserRepository
{
    public async Task CreateUser(User user)
    {
        await bookStoreDb.AddAsync(user);
        await SaveDbChangesAsync();
    }

    public async Task<User?> GetUserByEmail(string userEmail) => await bookStoreDb.Users.SingleOrDefaultAsync(x => x.Email == userEmail);

    #region Private methods

    private async Task SaveDbChangesAsync() => await bookStoreDb.SaveChangesAsync();

    #endregion
}
