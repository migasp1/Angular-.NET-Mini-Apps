namespace Application.Interfaces;

public interface ICryptographyService
{
    (string PasswordHash, string PasswordSalt) GetPasswordData(string plainText);
    bool IsValidPassword(string plainText, string storedPasswordB64, string storedSaltB64);
    string HashPlainText(string plainText);
}
