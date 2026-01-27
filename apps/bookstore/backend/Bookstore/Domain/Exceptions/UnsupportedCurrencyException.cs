using Domain.Entities.Constraints;
using Domain.ErrorCodes;
using Domain.Exceptions.Abstract;

namespace Domain.Exceptions;

public class UnsupportedCurrencyException(string message) : DomainException(message, DomainErrorCodes.InvalidCurrency, StatusCodesConstraints.BadRequest)
{
}
