using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
namespace Platform.Api.Benchmark.Responses;

// ReSharper disable UnusedAutoPropertyAccessor.Global
public class MultiResponse
{
    [QueueOutput("%PipelineMessageHub:JobPendingQueue%", Connection = "PipelineMessageHub:ConnectionString")]
    public string[] Messages { get; set; } = [];

    [HttpResult]
    public HttpResponseData? HttpResponse { get; set; }
}