using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace WorkPlanning.Services.AzureServiceBus
{
    public class ServiceBusHostedService : IHostedService
    {
        private readonly ServiceBusClient _serviceBusClient;
        private ServiceBusProcessor _processor;

        public ServiceBusHostedService(ServiceBusClient serviceBusClient)
        {
            _serviceBusClient = serviceBusClient;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _processor = _serviceBusClient.CreateProcessor("test");

            _processor.ProcessMessageAsync += ProcessMessageAsync;
            _processor.ProcessErrorAsync += ProcessErrorAsync;

            await _processor.StartProcessingAsync(cancellationToken);
        }

        private Task ProcessErrorAsync(ProcessErrorEventArgs arg)
        {
            return Task.CompletedTask;
        }

        private Task ProcessMessageAsync(ProcessMessageEventArgs arg)
        {
            // perform your business logic to process messages

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return _processor.CloseAsync(cancellationToken: cancellationToken);
        }
    }
}
