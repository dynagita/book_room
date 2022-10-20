namespace BookRoom.Domain.Contract.UseCases
{
    public interface IUseCaseBase<in TRequest, TResponse>
    {
        Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken);
    }
}
