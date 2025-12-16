namespace Application.CQRS.Interfaces;

public interface ICommandDispatcher
{
    Task<TResult> DispatchCommand<TCommand, TResult>(TCommand command)
        where TCommand : IBookstoreCommand<TResult>
        where TResult : IBookStoreResult;
}
