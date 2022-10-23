using MediatR;

namespace BookRoom.Service.Domain.Contract.UseCases
{
    public interface IUseCaseBase<in T>
        where T : INotification
    {
        Task HandleAsync(T request, CancellationToken cancellationToken);
    }
}
