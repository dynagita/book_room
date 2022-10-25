namespace BookRoom.Readness.Domain.Contract.UseCases
{
    public interface IUseCaseOnlyResponse<TResponse>
    {
        Task<TResponse> HandleAsync(CancellationToken cancellationToken);

        
    }
}
