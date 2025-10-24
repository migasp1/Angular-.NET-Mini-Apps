namespace Infrastructure.Services;

public interface ICryptographyService
{
    string HashPlainText(string plainText);
}
