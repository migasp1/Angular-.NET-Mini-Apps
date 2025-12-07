namespace Domain.Exceptions.Abstract;

public abstract class DomainException(string message, string domainCode, int statusCode) : Exception(message)
{
    public string DomainCode { get; } = domainCode;
    public int StatusCode { get; } = statusCode;
}
