using BookRoom.Domain.Contract.Configurations;
using BookRoom.Domain.Contract.Notification.BookRooms;
using BookRoom.Domain.Queue;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BookRoom.Infrastructure.Rabbit.Producers
{
    public class BookRoomRequestProducer : ProducerBase<BookRoomNotification>, IBookRoomRequestProducer
    {
        public BookRoomRequestProducer(
            IOptions<RabbitConfiguration> rabbitConfig, 
            ILogger<ProducerBase<BookRoomNotification>> logger) : base(rabbitConfig, logger)
        {
        }
    }
}
