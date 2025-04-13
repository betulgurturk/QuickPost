using Application.Common.Interfaces;
using System.Threading;

namespace QuickPostApi.Services
{
    public class BackgroundServices : BackgroundService
    {

        private readonly IMessageConsumer _messageConsumer;
        public BackgroundServices(IMessageConsumer messageConsumer)
        {
            _messageConsumer = messageConsumer;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _messageConsumer.StartConsumingAsync(stoppingToken);
        }

       
    }
}
