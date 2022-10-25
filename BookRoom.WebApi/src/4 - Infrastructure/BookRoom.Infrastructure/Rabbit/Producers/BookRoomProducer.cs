using BookRoom.Domain.Contract.Configurations;
using BookRoom.Domain.Entities;
using BookRoom.Domain.Queue;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BookRoom.Infrastructure.Rabbit.Producers
{
    public class BookRoomProducer : ProducerBase<BookRooms>, IBookRoomProducer
    {
        public BookRoomProducer(
            IOptions<RabbitConfiguration> rabbitConfig, 
            ILogger<ProducerBase<BookRooms>> logger) : base(rabbitConfig, logger)
        {
        }
    }
}
