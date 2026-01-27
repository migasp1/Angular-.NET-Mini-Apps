using Domain.Entities.Constraints;
using Domain.ErrorCodes;
using Domain.Exceptions.Abstract;

namespace Domain.Exceptions
{
    public class InvalidUserIdException(string message) : DomainException(message, DomainErrorCodes.InvalidUserIdData, StatusCodesConstraints.BadRequest)
    {
    }
}
