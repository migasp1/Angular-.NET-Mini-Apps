namespace Application.CQRS.Interfaces;

public interface ICommandDispatcher
{
    Task DispatchCommand<TCommand>(TCommand command) where TCommand : IBookstoreCommand;
}
