using BookRoom.Domain.Contract.Configurations;
using BookRoom.Service.Domain.Contract.Notifications;
using BookRoom.Service.Domain.Queue;
using BookRoom.Services.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BookRoom.Service.Infrastructure.Rabbit.Consumers
{
    public class UserConsumer : ConsumerBase<UserNotification>, IUserConsumer
    {
        public UserConsumer(
            IOptions<RabbitConfiguration> rabbitConfig, 
            ILogger<ConsumerBase<UserNotification>> logger, 
            IMediator mediator) : base(rabbitConfig, logger, mediator)
        {
        }

        protected override string GetQueueName()
        {
            return $"{nameof(UserNotification)}";
        }
    }
}
