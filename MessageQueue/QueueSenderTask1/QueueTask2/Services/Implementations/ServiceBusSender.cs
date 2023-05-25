using Azure.Messaging.ServiceBus;
using System.Text.Json;

namespace QueueTask2.Services.Implementations
{
    public class ServiceBusSender
    {
        private readonly ServiceBusClient _client;
        private readonly Azure.Messaging.ServiceBus.ServiceBusSender _clientSender;
        private const string QUEUE_NAME = "queue-1";

        public ServiceBusSender(IConfiguration configuration)
        {
            string? connectionString = configuration.GetConnectionString("ServiceBusConnectionString");
            _client = new ServiceBusClient(connectionString);
            _clientSender = _client.CreateSender(QUEUE_NAME);
        }

        public async Task SendMessage(object payload)
        {
            string messagePayload = JsonSerializer.Serialize(payload);
            ServiceBusMessage message = new(messagePayload);
            await _clientSender.SendMessageAsync(message).ConfigureAwait(false);
        }
    }
}
