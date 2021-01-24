
namespace WorkPlanning.Infrastructure.Configuration
{
    public class AppConfig
    {
        public string SqlConnectionString { get; set; }
        public string ServiceBusConnectionString { get; set; }
        public string ServiceBusQueueName { get; set; }
    }
}
