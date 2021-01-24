using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using WorkPlanning.Infrastructure.Configuration;

namespace WorkPlanning.Services.AzureServiceBus
{
    public class AzureServiceBusClient : IAzureServiceBusClient, IAsyncDisposable
    {
        private readonly string _queueName;

        private readonly ServiceBusClient _client;

        public AzureServiceBusClient(IOptions<AppConfig> appConfig)
        {
            if (_client == null)
                _client = new ServiceBusClient(appConfig.Value.ServiceBusConnectionString);
            _queueName = appConfig.Value.ServiceBusQueueName;
        }

        public async Task SendMessageAsync(ServiceBusMessage message)
        {
            await using var sender = _client.CreateSender(_queueName);
            await sender.SendMessageAsync(message);
        }

        public async Task<ServiceBusReceivedMessage> ReceiveMessageAsync()
        {
            await using var receiver = _client.CreateReceiver(_queueName);
            return await receiver.ReceiveMessageAsync();
        }

        public ValueTask DisposeAsync()
        {
            return _client.DisposeAsync();
        }
    }
}
