namespace Domain.Entities;

public class Role
{
    public int RoleId { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; } = default!;
}
