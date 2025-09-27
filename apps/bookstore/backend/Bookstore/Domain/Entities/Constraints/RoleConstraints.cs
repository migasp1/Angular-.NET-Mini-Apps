namespace Domain.Entities.Constraints;

public static class RoleConstraints
{
    public const string User = "User";
    public const string Admin = "Admin";

    public static readonly string[] AllRoles = [User, Admin];
}
