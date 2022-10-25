using BookRoom.Domain.Contract.Configurations;
using BookRoom.Domain.Entities;
using BookRoom.Domain.Queue;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BookRoom.Infrastructure.Rabbit.Producers
{
    public class RoomProducer : ProducerBase<Room>, IRoomProducer
    {
        public RoomProducer(
            IOptions<RabbitConfiguration> rabbitConfig, 
            ILogger<ProducerBase<Room>> logger) : base(rabbitConfig, logger)
        {
        }
    }
}
