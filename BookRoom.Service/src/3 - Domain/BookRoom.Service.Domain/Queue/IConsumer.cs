using MediatR;
using RabbitMQ.Client.Events;

namespace BookRoom.Service.Domain.Queue
{
    public interface IConsumer<in TMessage>
        where TMessage : INotification
    {
        Task StopReadingAsync(CancellationToken cancellation);
        Task StartReadingAsync(CancellationToken cancellation);
        void Handle(object model, BasicDeliverEventArgs deliverArgs);
    }
}
