
public record AuthenticationResultDTO
{
    public readonly string JWTToken { get; set; }
    public readonly string RefreshToken { get; set; }
}
