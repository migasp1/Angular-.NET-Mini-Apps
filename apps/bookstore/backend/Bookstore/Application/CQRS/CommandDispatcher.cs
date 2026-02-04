using Application.CQRS.Interfaces;
using FluentValidation;

namespace Application.CQRS;

public class CommandDispatcher(IServiceProvider serviceProvider) : IBookStoreCommandDispatcher
{
    public async Task<TResult> DispatchCommand<TResult>(IBookstoreCommand<TResult> command)
        where TResult : IBookStoreResult
    {
        var validatorType = typeof(IValidator<>).MakeGenericType(command.GetType());
        var commandValidator = serviceProvider.GetService(validatorType) as IValidator;

        if (commandValidator is not null)
        {
            var validationContext = new ValidationContext<object>(command);
            var validationResult = await commandValidator.ValidateAsync(validationContext);
            if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);
        }

        var handlerType = typeof(ICommandHandler<,>).MakeGenericType(command.GetType(), typeof(TResult));
        var handler = serviceProvider.GetService(handlerType);
        var handlerMethod = handlerType.GetMethod("HandleAsync");
        return await (Task<TResult>)handlerMethod!.Invoke(handler, [command!])!;
    }
}
