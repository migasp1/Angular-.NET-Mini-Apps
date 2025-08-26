namespace Domain.Exceptions.Abstract;

public abstract class DomainException(string message, string code) : Exception(message)
{
    public string Code { get; private set; } = code;
}
