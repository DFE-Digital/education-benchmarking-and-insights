using System;
using System.Threading.Tasks;
using Azure.Storage.Queues;
using Microsoft.Extensions.Options;
using Platform.Domain.Messages;
using Platform.Functions.Extensions;

namespace Platform.Orchestrator;

public interface IJobStartMessageSender
{
    Task Send(PipelineMessage input);
}

public record JobStartMessageSenderOptions
{
    public string? ConnectionString { get; set; }
    public string? JobStartQueue { get; set; }
}

public class JobStartMessageSender : IJobStartMessageSender
{
    private readonly QueueClient _client;

    public JobStartMessageSender(IOptions<JobStartMessageSenderOptions> options)
    {
        ArgumentNullException.ThrowIfNull(options.Value.ConnectionString);
        ArgumentNullException.ThrowIfNull(options.Value.JobStartQueue);

        _client = new QueueClient(options.Value.ConnectionString, options.Value.JobStartQueue, new QueueClientOptions
        {
            MessageEncoding = QueueMessageEncoding.Base64
        });
    }

    public async Task Send(PipelineMessage input)
    {
        await _client.SendMessageAsync(new BinaryData(input.ToJsonByteArray()));
    }
}