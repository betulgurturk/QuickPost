using Application.Common.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
namespace Infrastructure.Services
{
    public class RabbitMQService : IQueueService
    {
        private readonly IConnectionFactory _connectionFactory;
        public RabbitMQService(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public Task Receive()
        {
            throw new NotImplementedException();
        }
        public async Task Send(Guid UserId, string Message)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: "hello", durable: false, exclusive: false, autoDelete: false,
                arguments: null);

            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new ReturnMessage() { Message = Message, UserId = UserId }));
            await channel.BasicPublishAsync(exchange: string.Empty, routingKey: "hello", body: body);
        }


        public class ReturnMessage
        {
            public required Guid UserId { get; set; }
            public required string Message { get; set; }
        }
    }
}
