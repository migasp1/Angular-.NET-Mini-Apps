namespace Application.CQRS.Interfaces;

public interface IBookstoreCommand<TResult> where TResult : IBookStoreResult
{
}
