using System.Diagnostics.CodeAnalysis;
using Azure;
using Azure.Core.Pipeline;
using Azure.Search.Documents;
using Azure.Search.Documents.Models;
using Microsoft.Extensions.Options;
using Platform.Infrastructure;

namespace Platform.Search;

public interface IIndexClient
{
    Task<Response<SearchResults<T>>> SearchAsync<T>(string? searchText, SearchOptions options);
    Task<Response<T>> GetDocumentAsync<T>(string? key);
    Task<Response<SuggestResults<T>>> SuggestAsync<T>(string? searchText, string? suggesterName, SuggestOptions options, CancellationToken cancellationToken = default);
}

[ExcludeFromCodeCoverage]
public abstract class IndexClient(Uri endpoint, AzureKeyCredential credential, string indexName, IIndexTelemetryService telemetry) : IIndexClient
{
    private readonly SearchClient _client = new(endpoint, indexName, credential);

    public async Task<Response<SearchResults<T>>> SearchAsync<T>(string? searchText, SearchOptions options)
    {
        var id = Guid.NewGuid();

        Response<SearchResults<T>> response;
        using (HttpPipeline.CreateClientRequestIdScope(id.ToString()))
        {
            response = await _client.SearchAsync<T>(searchText, options);
        }

        var searchResults = response.Value;
        telemetry.TrackEvent(
            new SearchTelemetryProperties(id, _client.ServiceName, _client.IndexName, options.Facets, options.Filter, searchText, searchResults.TotalCount));

        return response;
    }


    public Task<Response<T>> GetDocumentAsync<T>(string? key) => _client.GetDocumentAsync<T>(key);

    public async Task<Response<SuggestResults<T>>> SuggestAsync<T>(string? searchText, string? suggesterName, SuggestOptions options, CancellationToken cancellationToken = default)
    {
        var id = Guid.NewGuid();
        Response<SuggestResults<T>> response;
        using (HttpPipeline.CreateClientRequestIdScope(id.ToString()))
        {
            response = await _client.SuggestAsync<T>(searchText, suggesterName, options, cancellationToken);
        }

        var props = new SuggestTelemetryProperties(
            id,
            _client.ServiceName,
            _client.IndexName,
            suggesterName,
            options.Select,
            options.Filter,
            searchText,
            response.Value.Results.Count);

        telemetry.TrackEvent(props);

        return response;
    }
}

[ExcludeFromCodeCoverage]
public class SchoolIndexClient(IOptions<PlatformSearchOptions> options, IIndexTelemetryService telemetry)
    : IndexClient(options.Value.Endpoint, options.Value.Credential, ResourceNames.Search.Indexes.School, telemetry);

[ExcludeFromCodeCoverage]
public class TrustIndexClient(IOptions<PlatformSearchOptions> options, IIndexTelemetryService telemetry)
    : IndexClient(options.Value.Endpoint, options.Value.Credential, ResourceNames.Search.Indexes.Trust, telemetry);

[ExcludeFromCodeCoverage]
public class LocalAuthorityIndexClient(IOptions<PlatformSearchOptions> options, IIndexTelemetryService telemetry)
    : IndexClient(options.Value.Endpoint, options.Value.Credential, ResourceNames.Search.Indexes.LocalAuthority, telemetry);

[ExcludeFromCodeCoverage]
public class SchoolComparatorsIndexClient(IOptions<PlatformSearchOptions> options, IIndexTelemetryService telemetry)
    : IndexClient(options.Value.Endpoint, options.Value.Credential, ResourceNames.Search.Indexes.SchoolComparators, telemetry);

[ExcludeFromCodeCoverage]
public class TrustComparatorsIndexClient(IOptions<PlatformSearchOptions> options, IIndexTelemetryService telemetry)
    : IndexClient(options.Value.Endpoint, options.Value.Credential, ResourceNames.Search.Indexes.TrustComparators, telemetry);