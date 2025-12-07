using Domain.Entities.Constraints;
using Domain.Exceptions.Abstract;
using Domain.VOs;
using FluentValidation;
using System.Text.Json;
using Error = Domain.VOs.Error;

namespace Bookstore.API.Middlewares;

public class GlobalExceptionHandlerMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (Exception ex)
        {
            await HandleException(context, ex);
        }
    }

    #region private methods

    private async Task HandleException(HttpContext context, Exception ex)
    {
        var responseBody = new ErrorResponse();
        var statusCode = ex switch
        {
            ValidationException validationException => ProcessValidationException(validationException, responseBody),
            DomainException domainException => ProcessDomainException(domainException, responseBody),
            _ => ProcessUnhandledException(responseBody),
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsync(JsonSerializer.Serialize(responseBody));
    }

    private int ProcessValidationException(ValidationException exception, ErrorResponse respondeBody)
    {
        respondeBody.Errors = [.. exception.Errors.Select(error => new Error()
        {
            Property = error.PropertyName,
            Message = error.ErrorMessage,
            Code = error.ErrorCode
        })];

        return StatusCodesConstraints.BadRequest;
    }

    private int ProcessDomainException(DomainException exception, ErrorResponse respondeBody)
    {
        respondeBody.Errors = [new Error() {
            Message = exception.Message,
            Code = exception.DomainCode
        }];

        return exception.StatusCode;
    }

    private int ProcessUnhandledException(ErrorResponse respondeBody)
    {
        respondeBody.Errors = [new Error() {
            Message = "Oops, algo correu mal. Por favor tente novamente mais tarde"
        }];

        return StatusCodesConstraints.InternalError;
    }

    #endregion
}
