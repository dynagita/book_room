using BookRoom.Domain.Contract.Configurations;
using BookRoom.Service.Domain.Contract.Notifications;
using BookRoom.Service.Domain.Queue;
using BookRoom.Services.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BookRoom.Service.Infrastructure.Rabbit.Consumers
{
    public class BookRoomConsumer : ConsumerBase<BookRoomsNotification>, IBookRoomConsumer
    {
        public BookRoomConsumer(
            IOptions<RabbitConfiguration> rabbitConfig, 
            ILogger<ConsumerBase<BookRoomsNotification>> logger, 
            IMediator mediator) : base(rabbitConfig, logger, mediator)
        {
        }

        protected override string GetQueueName()
        {
            return $"{nameof(BookRoomsNotification)}";
        }
    }
}
