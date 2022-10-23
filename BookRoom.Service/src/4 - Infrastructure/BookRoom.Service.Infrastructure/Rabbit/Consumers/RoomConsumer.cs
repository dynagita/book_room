using BookRoom.Domain.Contract.Configurations;
using BookRoom.Service.Domain.Contract.Notifications;
using BookRoom.Service.Domain.Queue;
using BookRoom.Services.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BookRoom.Service.Infrastructure.Rabbit.Consumers
{
    public class RoomConsumer : ConsumerBase<RoomNotification>, IRoomConsumer
    {
        public RoomConsumer(
            IOptions<RabbitConfiguration> rabbitConfig, 
            ILogger<ConsumerBase<RoomNotification>> logger, 
            IMediator mediator) : base(rabbitConfig, logger, mediator)
        {
        }

        protected override string GetQueueName()
        {
            return $"{nameof(RoomNotification)}";
        }
    }
}
