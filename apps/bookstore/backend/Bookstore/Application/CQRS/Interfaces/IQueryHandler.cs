namespace Application.CQRS.Interfaces;

public interface IQueryHandler<TQuery, TResult>
{
    Task<TResult> HandleAsync(TQuery query);
}
