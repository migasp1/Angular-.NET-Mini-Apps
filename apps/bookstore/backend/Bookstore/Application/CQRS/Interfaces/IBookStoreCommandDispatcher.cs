namespace Application.CQRS.Interfaces;

public interface IBookStoreCommandDispatcher
{
    Task<TResult> DispatchCommand<TResult>(IBookstoreCommand<TResult> command)
        where TResult : IBookStoreResult;
}
