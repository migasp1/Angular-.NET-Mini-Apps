namespace Domain.ErrorCodes;

public static class DomainErrorCodes
{
    public const string InvalidIsbn = "INVALID_ISBN";
    public const string InvalidPrice = "INVALID_PRICE";
    public const string InvalidCurrency = "UNSUPPORTED_CURRENCY";
    public const string InvalidJWTSettings = "INVALID_JWT_SETTINGS";
    public const string RequiredField = "REQUIRED_FIELD";
    public const string InvalidEmail = "INVALID_EMAIL";
    public const string EmailTooLong = "EMAIL_TOO_LONG";
    public const string PasswordTooLong = "PASSWORD_TOO_LONG";
    public const string InvalidPasswordFormat = "INVALID_PASSWORD_FORMAT";
    public const string InvalidRole = "INVALID_ROLE";
    public const string InvalidUserIdData = "INVALID_USER_ID_DATA";
    public const string UserNotAuthenticated = "USER_NOT_AUTHENTICATED";
    public const string EmailAlreadyExists = "Email_ALREADY_EXISTS";
}
