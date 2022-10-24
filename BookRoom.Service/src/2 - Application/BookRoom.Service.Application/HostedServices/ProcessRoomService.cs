using BookRoom.Service.Domain.Queue;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics.CodeAnalysis;

namespace BookRoom.Service.Application.HostedServices
{
    [ExcludeFromCodeCoverage]
    public class ProcessRoomService : IHostedService
    {
        private readonly IRoomConsumer _consumer;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public ProcessRoomService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _consumer = _serviceScopeFactory.CreateScope().ServiceProvider.GetService<IRoomConsumer>();
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
