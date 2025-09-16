namespace Domain.Entities;

public class User
{
    public int UserId { get; set; }
    public string? Email { get; set; }
    public string PasswordHash { get; set; } = default!;
    public string PasswordSalt { get; set; } = default!;
    public string Role { get; set; } = default!;
}
