using BookRoom.Domain.Contract.Configurations;
using BookRoom.Domain.Queue;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace BookRoom.Infrastructure.Rabbit.Consumers
{
    public abstract class ConsumerBase<TMessage> : IConsumer<TMessage> 
        where TMessage : INotification
    {
        private readonly RabbitConfiguration _rabbitConfiguration;
        private readonly ILogger<ConsumerBase<TMessage>> _logger;
        private readonly IConnection _connection;
        private readonly AsyncRetryPolicy _retry;
        private readonly IMediator _mediator;
        public ConsumerBase(
            IOptions<RabbitConfiguration> rabbitConfig,
            ILogger<ConsumerBase<TMessage>> logger,
            IMediator mediator)
        {
            _rabbitConfiguration = rabbitConfig.Value;

            _connection = new ConnectionFactory()
            {
                Uri = new Uri(_rabbitConfiguration.Connection)
            }.CreateConnection();

            _logger = logger;
            _retry = Policy.Handle<Exception>()
                .WaitAndRetryAsync(3, retryAttemp => TimeSpan.FromSeconds(Math.Pow(3, retryAttemp)));
            
            _mediator = mediator;
        }
        public void Handle(object model, BasicDeliverEventArgs deliverArgs)
        {
            var body = deliverArgs.Body.ToArray();
            var envelope = Encoding.UTF8.GetString(body);
            var message = JsonConvert.DeserializeObject<TMessage>(envelope);

            _mediator.Publish(message).GetAwaiter().GetResult();
        }

        public async Task StartReadingAsync(CancellationToken cancellation)
        {
            try
            {
                var queueName = typeof(TMessage).Name;

                var chanel = _connection.CreateModel();
                chanel.QueueDeclare(queue: queueName,
                                     durable: _rabbitConfiguration.Durable,
                                     exclusive: _rabbitConfiguration.Exclusive,
                                     autoDelete: _rabbitConfiguration.AutoDelete,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(chanel);

                consumer.Received += Handle;

                chanel.BasicConsume(queue: queueName,
                                     autoAck: true,
                                     consumer: consumer);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Consumer} - {Method} - Exception Thrown", this.GetType().Name, nameof(StartReadingAsync));
                //Implement and call a queue resilence right here for not loosing messages
            }
        }

        public async Task StopReadingAsync(CancellationToken cancellation)
        {
            _connection.Close();
            _connection.Dispose();
        }
    }
}
