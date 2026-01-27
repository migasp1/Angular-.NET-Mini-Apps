using Application.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Services;

public class CryptographyService : ICryptographyService
{
    private readonly int NumberOfIterations = 10000; // baixo, só a título de exemplo
    public (string PasswordHash, string PasswordSalt) GetPasswordData(string plainText)
    {
        var saltBytes = new byte[32];
        RandomNumberGenerator.Fill(saltBytes);

        var hashBytes = Rfc2898DeriveBytes.Pbkdf2(plainText, saltBytes, NumberOfIterations, HashAlgorithmName.SHA3_256, 32);

        return (Convert.ToBase64String(hashBytes), Convert.ToBase64String(saltBytes));
    }

    public bool IsValidPassword(string plainText, string storedPasswordB64, string storedSaltB64)
    {
        var saltBytes = Convert.FromBase64String(storedSaltB64);
        var storedPasswordBytes = Convert.FromBase64String(storedPasswordB64);
        var calculatedPasswordBytes = Rfc2898DeriveBytes.Pbkdf2(plainText, saltBytes, NumberOfIterations, HashAlgorithmName.SHA3_256, 32);

        return CryptographicOperations.FixedTimeEquals(calculatedPasswordBytes, storedPasswordBytes);
    }

    public string HashPlainText(string plainText) => Convert.ToBase64String(SHA3_256.HashData(UTF8Encoding.UTF8.GetBytes(plainText)));
}
