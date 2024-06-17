using System;
using System.Threading.Tasks;
using Azure.Storage.Queues;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Orchestrator;

public interface IJobStartMessageSender
{
    Task Send(PipelineStartMessage input);
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
            MessageEncoding = QueueMessageEncoding.None
        });
    }

    public async Task Send(PipelineStartMessage input)
    {
        await _client.SendMessageAsync(input.ToJson());
    }
}