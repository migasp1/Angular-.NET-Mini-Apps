using Domain.Entities.Constraints;
using Domain.ErrorCodes;
using Domain.Exceptions.Abstract;

namespace Domain.Exceptions;

public class InvalidJWTSettingsException(string message) : DomainException(message, DomainErrorCodes.InvalidJWTSettings, StatusCodesConstraints.InternalError)
{
}
