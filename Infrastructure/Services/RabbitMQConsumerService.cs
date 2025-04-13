using Application.Common.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class RabbitMQConsumerService(IConnectionFactory connectionFactory) : IMessageConsumer
    {
        private readonly IConnectionFactory _connectionFactory = connectionFactory;
        IConnection _connection;
        IChannel channel;
        
        public async Task StartConsumingAsync(CancellationToken cancellationToken = default)
        {
            _connection = await _connectionFactory.CreateConnectionAsync();
            channel = await _connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: "hello", durable: false, exclusive: false, autoDelete: false,
                arguments: null);

            Console.WriteLine(" [*] Waiting for messages.");

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($" [x] Received {message}");
                return Task.CompletedTask;
            };

            await channel.BasicConsumeAsync("hello", autoAck: true, consumer: consumer);

        }
    }
}
