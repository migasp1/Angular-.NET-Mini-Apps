using Domain.ErrorCodes;
using Domain.Exceptions.Abstract;

namespace Domain.Exceptions;

public class InvalidIsbnException(string message) : DomainException(message, DomainErrorCodes.InvalidIsbn)
{
}
