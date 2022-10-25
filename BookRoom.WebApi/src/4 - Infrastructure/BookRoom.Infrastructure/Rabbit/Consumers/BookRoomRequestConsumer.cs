using BookRoom.Domain.Contract.Configurations;
using BookRoom.Domain.Contract.Notification.BookRooms;
using BookRoom.Domain.Queue;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BookRoom.Infrastructure.Rabbit.Consumers
{
    public class BookRoomRequestConsumer : ConsumerBase<BookRoomNotification>, IBookRoomRequestConsumer
    {
        public BookRoomRequestConsumer(
            IOptions<RabbitConfiguration> rabbitConfig, 
            ILogger<ConsumerBase<BookRoomNotification>> logger, 
            IMediator mediator) : base(rabbitConfig, logger, mediator)
        {
        }
    }
}
