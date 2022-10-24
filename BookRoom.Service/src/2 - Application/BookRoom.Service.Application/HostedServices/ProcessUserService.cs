using BookRoom.Service.Domain.Queue;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics.CodeAnalysis;

namespace BookRoom.Service.Application.HostedServices
{
    [ExcludeFromCodeCoverage]
    public class ProcessUserService : IHostedService
    {
        private readonly IUserConsumer _consumer;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public ProcessUserService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _consumer = _serviceScopeFactory.CreateScope().ServiceProvider.GetService<IUserConsumer>();
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
