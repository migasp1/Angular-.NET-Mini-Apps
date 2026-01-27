namespace Application.Interfaces;

public interface IRoleRepository
{
    Task CreateRole(string name, int userId);
}
