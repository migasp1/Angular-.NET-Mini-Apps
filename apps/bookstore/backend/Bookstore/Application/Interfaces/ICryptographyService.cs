namespace Application.Interfaces;

public interface ICryptographyService
{
    (string PasswordHash, string PasswordSalt) GetPasswordData(string plainText);
    string HashPlainText(string plainText);
}
