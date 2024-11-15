using System;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using Microsoft.Extensions.Options;
namespace Platform.Orchestrator.Search;

public interface ISearchIndexerClient
{
    Uri Endpoint { get; }
    Task<Response?> RunIndexerAsync(string indexerName, CancellationToken cancellationToken = default);
}

public sealed class SearchIndexerClient(IOptions<PipelineSearchOptions> options) : ISearchIndexerClient
{
    private Azure.Search.Documents.Indexes.SearchIndexerClient Client => new(options.Value.SearchEndPoint, options.Value.SearchCredentials);
    public Uri Endpoint => Client.Endpoint;
    public Task<Response?> RunIndexerAsync(string indexerName, CancellationToken cancellationToken = default) => Client.RunIndexerAsync(indexerName, cancellationToken);
}