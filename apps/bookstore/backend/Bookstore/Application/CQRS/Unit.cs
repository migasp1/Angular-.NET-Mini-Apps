using Application.CQRS.Interfaces;

namespace Application.CQRS;

public readonly struct Unit : IBookStoreResult
{
    public static readonly Unit Value = new();
}
