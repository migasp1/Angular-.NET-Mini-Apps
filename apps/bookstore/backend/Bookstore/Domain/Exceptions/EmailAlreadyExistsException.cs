using Domain.Entities.Constraints;
using Domain.ErrorCodes;
using Domain.Exceptions.Abstract;

namespace Domain.Exceptions;

public class EmailAlreadyExistsException(string message) : DomainException(message, DomainErrorCodes.EmailAlreadyExists, StatusCodesConstraints.Conflict)
{
}
