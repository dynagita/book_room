using BookRoom.Domain.Contract.Configurations;
using BookRoom.Domain.Entities;
using BookRoom.Domain.Queue;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BookRoom.Infrastructure.Rabbit.Producers
{
    public class UserProducer : ProducerBase<User>, IUserProducer
    {
        public UserProducer(
            IOptions<RabbitConfiguration> rabbitConfig, 
            ILogger<ProducerBase<User>> logger) : base(rabbitConfig, logger)
        {
        }
    }
}
