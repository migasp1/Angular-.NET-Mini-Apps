using Domain.Entities.Constraints;
using Domain.ErrorCodes;
using Domain.Exceptions.Abstract;

namespace Domain.Exceptions;

public class InvalidPriceException(string message) : DomainException(message, DomainErrorCodes.InvalidPrice, StatusCodesConstraints.BadRequest)
{
}
