namespace Domain.Entities.Constraints;

public static class StatusCodesConstraints
{
    public const int BadRequest = 400;
    public const int Conflict = 401;
    public const int Unauthorized = 401;
    public const int NotFound = 404;
    public const int InternalError = 500;
}
