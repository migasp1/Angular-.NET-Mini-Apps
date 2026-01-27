namespace Bookstore.API.Configurations.Auth.JWTConfigurations;

public class JWTSettings
{
    public string Secret { get; set; } = default!;
    public string Issuer { get; set; } = default!;
    public string Audience { get; set; } = default!;
    public int ExpiryMinutes { get; set; } = default!;
}
