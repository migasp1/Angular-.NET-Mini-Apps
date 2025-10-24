namespace Domain.ErrorCodes;

public static class DomainErrorCodes
{
    public const string InvalidIsbn = "INVALID_ISBN";
    public const string InvalidPrice = "INVALID_PRICE";
    public const string InvalidCurrency = "UNSUPPORTED_CURRENCY";
    public const string InvalidJWTSettings = "INVALID_JWT_SETTINGS";
    public const string RequiredField = "REQUIRED_FIELD";
    public const string InvalidEmail = "IVALID_EMAIL";
    public const string EmailTooLong = "EMAIL_TOO_LONG";
    public const string PasswordTooLong = "PASSWORD_TOO_LONG";
    public const string InvalidPasswordFormat = "INVALID_PASSWORD_FORMAT";
    public const string InvalidRole = "INVALID_ROLE";
}
