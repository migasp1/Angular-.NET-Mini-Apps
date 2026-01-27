using Application.CQRS.Interfaces;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application.CQRS;

public class CommandDispatcher(IServiceProvider serviceProvider) : ICommandDispatcher
{
    public async Task<TResult> DispatchCommand<TCommand, TResult>(TCommand command)
        where TCommand : IBookstoreCommand<TResult>
        where TResult : IBookStoreResult
    {
        var commandValidator = serviceProvider.GetService<IValidator<TCommand>>();

        if (commandValidator is not null)
        {
            var validationResult = await commandValidator.ValidateAsync(command);
            if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);
        }

        var commandType = typeof(TCommand);
        var handlerType = typeof(ICommandHandler<TCommand, TResult>).MakeGenericType(commandType);
        var handler = serviceProvider.GetService(handlerType);
        var handlerMethod = handlerType.GetMethod("HandleAsync");
        return await (Task<TResult>)handlerMethod!.Invoke(handler, [command!])!;
    }
}
