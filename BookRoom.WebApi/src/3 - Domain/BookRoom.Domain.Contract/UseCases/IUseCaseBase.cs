using MediatR;

namespace BookRoom.Domain.Contract.UseCases
{
    public interface IUseCaseBase<in TRequest, TResponse>
    {
        Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken);
    }

    public interface IUseCaseBase<in TKey, in TRequest, TResponse>
    {
        Task<TResponse> HandleAsync(TKey key, TRequest request, CancellationToken cancellationToken);
    }

    public interface IUseCaseBase<TRequest>
    {
        Task HandleAsync(TRequest request, CancellationToken cancellationToken);
    }
}
