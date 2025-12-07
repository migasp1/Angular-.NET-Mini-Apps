using Domain.Entities.Constraints;
using Domain.ErrorCodes;
using Domain.Exceptions.Abstract;

namespace Domain.Exceptions;

public class UserNotAuthenticatedException(string message) : DomainException(message, DomainErrorCodes.UserNotAuthenticated, StatusCodesConstraints.Unauthorized)
{
}
