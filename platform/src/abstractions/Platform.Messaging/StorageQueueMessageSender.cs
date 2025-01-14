using System.Diagnostics.CodeAnalysis;
using Azure.Storage.Queues;
using Platform.Json;

namespace Platform.Messaging;

[ExcludeFromCodeCoverage]
public class StorageQueueMessageSender : IMessageSender
{
    private readonly QueueClient _client;

    public StorageQueueMessageSender(string connectionString, string queue)
    {
        _client = new QueueClient(connectionString, queue, new QueueClientOptions
        {
            MessageEncoding = QueueMessageEncoding.None
        });
        _client.CreateIfNotExists();
    }

    public async Task Send<T>(T data)
    {
        await _client.SendMessageAsync(data.ToJson());
    }
}