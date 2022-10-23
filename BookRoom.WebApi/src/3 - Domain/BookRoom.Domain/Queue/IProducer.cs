namespace BookRoom.Domain.Queue
{
    public interface IProducer<T>
    {
        Task SendAsync(T message, CancellationToken cancellationToken);


    }
}
