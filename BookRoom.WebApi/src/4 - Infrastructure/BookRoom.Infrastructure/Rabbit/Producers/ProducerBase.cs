using BookRoom.Domain.Contract.Configurations;
using BookRoom.Domain.Queue;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using System.Text;

namespace BookRoom.Infrastructure.Rabbit.Producers
{
    public class ProducerBase<T> : IProducer<T>
    {
        private readonly RabbitConfiguration _rabbitConfiguration;
        private readonly ILogger<ProducerBase<T>> _logger;
        private readonly IConnection _connection;
        private readonly AsyncRetryPolicy _retry;
        public ProducerBase(
            IOptions<RabbitConfiguration> rabbitConfig, 
            ILogger<ProducerBase<T>> logger)
        {
            _rabbitConfiguration = rabbitConfig.Value;

            _connection = new ConnectionFactory()
            {
                Uri = new Uri(_rabbitConfiguration.Connection)
            }.CreateConnection();

            _logger = logger;
            _retry = Policy.Handle<Exception>()
                .WaitAndRetryAsync(3, retryAttemp => TimeSpan.FromSeconds(Math.Pow(3, retryAttemp)));
        }
        public Task SendAsync(T message, CancellationToken cancellationToken)
        {
            try
            {
                _retry.ExecuteAsync(async () =>
                {
                    var queueName = typeof(T).Name;

                    var channel = _connection.CreateModel();
                    channel.QueueDeclare(
                        queue: queueName,
                        durable: _rabbitConfiguration.Durable,
                        exclusive: _rabbitConfiguration.Exclusive,
                        autoDelete: _rabbitConfiguration.AutoDelete,
                        arguments: null
                        );

                    var jsonMessage = JsonConvert.SerializeObject(message);

                    var serializedMessage = Encoding.UTF8.GetBytes(jsonMessage);

                    channel.BasicPublish(exchange: "",
                                        routingKey: queueName,
                                        basicProperties: null,
                                        body: serializedMessage);

                    return Task.CompletedTask;
                }).GetAwaiter().GetResult();                
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "{Producer} - {Method} - Exception Thrown", this.GetType().Name, nameof(SendAsync));
                //Implement and call a queue resilence right here for not loosing messages
            }

            return Task.CompletedTask;
        }        
    }
}
