using System.Text.Json;
using Confluent.Kafka;

namespace AlfaTest;

public static class KafkaProducerService
{
    public static async Task SendMessage(NumberInfo number)
    {
        const string bootstrapServers = "kafka:29092";
        
        var config = new ProducerConfig()
        {
            BootstrapServers = bootstrapServers
        };
        
        using var producer = new ProducerBuilder<Null, string>(config).Build();
        var message = JsonSerializer.Serialize(number);
        
        await producer.ProduceAsync("primes", new Message<Null, string> { Value = message });
    }
}