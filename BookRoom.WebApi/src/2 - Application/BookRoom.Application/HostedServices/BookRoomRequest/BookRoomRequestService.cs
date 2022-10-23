using BookRoom.Domain.Queue;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using static System.Formats.Asn1.AsnWriter;

namespace BookRoom.Application.HostedServices.BookRoomRequest
{
    public class BookRoomRequestService : IHostedService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IBookRoomRequestConsumer _consumer;
        public BookRoomRequestService(
            IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _consumer = _serviceScopeFactory.CreateScope().ServiceProvider.GetService<IBookRoomRequestConsumer>();
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {            
            await _consumer.StartReadingAsync(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _consumer.StopReadingAsync(cancellationToken);
        }
    }
}
