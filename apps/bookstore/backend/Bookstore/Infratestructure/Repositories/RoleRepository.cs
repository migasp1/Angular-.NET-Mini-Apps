using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories;

public class RoleRepository(BookStoreDbContext bookStoreDb) : IRoleRepository
{
    public async Task CreateRole(string name, int userId)
    {
        await bookStoreDb.AddAsync(new Role
        {
            Name = name
        });
        await SaveDbChangesAsync();
    }

    #region Privete methods

    private async Task SaveDbChangesAsync() => await bookStoreDb.SaveChangesAsync();

    #endregion
}
