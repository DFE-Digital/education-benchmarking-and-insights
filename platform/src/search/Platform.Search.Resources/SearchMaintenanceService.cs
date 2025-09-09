using System.ComponentModel.DataAnnotations;
using Azure;
using Azure.Search.Documents.Indexes;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Platform.Search.Resources.Builders;
using Platform.Search.Resources.LocalAuthority;
using Platform.Search.Resources.School;
using Platform.Search.Resources.SchoolComparators;
using Platform.Search.Resources.Trust;
using Platform.Search.Resources.TrustComparators;

namespace Platform.Search.Resources;

public class SearchMaintenanceServiceOptions
{
    [Required]
    public SqlOptions? Sql { get; set; }

    [Required]
    public string? Name { get; set; }

    [Required]
    public string? Key { get; set; }

    public Uri SearchEndPoint => new($"https://{Name}.search.windows.net/");
    public AzureKeyCredential SearchCredentials => new(Key ?? throw new ArgumentNullException());

    public class SqlOptions
    {
        [Required]
        public string? ConnectionString { get; set; }
    }
}

public class SearchMaintenanceService
{
    private readonly SearchIndexClient _indexClient;
    private readonly SearchIndexerClient _indexerClient;
    private readonly ILogger<SearchMaintenanceService> _logger;
    private readonly SearchMaintenanceServiceOptions _options;
    private int _failureCount;

    public SearchMaintenanceService(IOptions<SearchMaintenanceServiceOptions> options, ILogger<SearchMaintenanceService> logger)
    {
        _logger = logger;
        _options = options.Value;
        _indexerClient = new SearchIndexerClient(_options.SearchEndPoint, _options.SearchCredentials);
        _indexClient = new SearchIndexClient(_options.SearchEndPoint, _options.SearchCredentials);
    }

    public async Task Rebuild()
    {
        //Remove all indexers, data sources and indexes
        await RemoveIndexers();
        await RemoveDataSourcesConnections();
        await RemoveIndexes();

        //Build all indexers, data sources and indexes
        await BuildIndexes();
        await BuildDataSourcesConnections();
        await BuildIndexers();

        if (_failureCount > 0)
        {
            throw new Exception($"Failed to rebuild search indexes. {_failureCount} error{(_failureCount == 1 ? string.Empty : "s")} detected.");
        }
    }

    public Task Reset() => throw new NotImplementedException();

    private async Task RemoveDataSourcesConnections()
    {
        var connections = await _indexerClient.GetDataSourceConnectionNamesAsync();

        if (connections.HasValue)
        {
            foreach (var connection in connections.Value)
            {
                if (!await RemoveDataSourcesConnection(connection))
                {
                    _failureCount++;
                }
            }
        }
    }

    private async Task<bool> RemoveDataSourcesConnection(string connection)
    {
        try
        {
            await _indexerClient.DeleteDataSourceConnectionAsync(connection);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to delete data source connection : {Connection}", connection);
            return false;
        }

        return true;
    }

    private async Task RemoveIndexers()
    {
        var indexers = await _indexerClient.GetIndexerNamesAsync();

        if (indexers.HasValue)
        {
            foreach (var indexer in indexers.Value)
            {
                if (!await RemoveIndexer(indexer))
                {
                    _failureCount++;
                }
            }
        }
    }

    private async Task<bool> RemoveIndexer(string indexer)
    {
        try
        {
            await _indexerClient.DeleteIndexerAsync(indexer);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to delete indexer : {Indexer}", indexer);
            return false;
        }

        return true;
    }

    private async Task RemoveIndexes()
    {
        var indexes = await _indexClient.GetIndexNamesAsync().ToArrayAsync();

        foreach (var index in indexes)
        {
            if (!await RemoveIndex(index))
            {
                _failureCount++;
            }
        }
    }

    private async Task<bool> RemoveIndex(string index)
    {
        try
        {
            await _indexClient.DeleteIndexAsync(index);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to delete index : {Index}", index);
            return false;
        }

        return true;
    }

    private async Task BuildIndexes()
    {
        var builders = new IndexBuilder[] { new TrustIndexBuilder(), new SchoolIndexBuilder(), new LocalAuthorityIndexBuilder(), new SchoolComparatorsIndexBuilder(), new TrustComparatorsIndexBuilder() };

        foreach (var builder in builders)
        {
            if (!await BuildIndex(builder))
            {
                _failureCount++;
            }
        }
    }

    private async Task<bool> BuildIndex(IndexBuilder builder)
    {
        try
        {
            await builder.Build(_indexClient);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to build index : {Index}", builder.Name);
            return false;
        }

        return true;
    }

    private async Task BuildDataSourcesConnections()
    {
        ArgumentNullException.ThrowIfNull(_options.Sql);

        var builders = new DataSourceConnectionBuilder[]
        {
            new SchoolDataSourceConnectionBuilder(_options.Sql.ConnectionString), new TrustDataSourceConnectionBuilder(_options.Sql.ConnectionString), new LocalAuthorityDataSourceConnectionBuilder(_options.Sql.ConnectionString),
            new SchoolComparatorsDataSourceConnectionBuilder(_options.Sql.ConnectionString), new TrustComparatorsDataSourceConnectionBuilder(_options.Sql.ConnectionString)
        };

        foreach (var builder in builders)
        {
            if (!await BuildDataSourcesConnection(builder))
            {
                _failureCount++;
            }
        }
    }

    private async Task<bool> BuildDataSourcesConnection(DataSourceConnectionBuilder builder)
    {
        try
        {
            await builder.Build(_indexerClient);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to build data source connection : {Connection}", builder.Name);
            return false;
        }

        return true;
    }

    private async Task BuildIndexers()
    {
        var builders = new IndexerBuilder[] { new SchoolIndexerBuilder(), new TrustIndexerBuilder(), new LocalAuthorityIndexerBuilder(), new SchoolComparatorsIndexerBuilder(), new TrustComparatorsIndexerBuilder() };

        foreach (var builder in builders)
        {
            if (!await BuildIndexer(builder))
            {
                _failureCount++;
            }
        }
    }

    private async Task<bool> BuildIndexer(IndexerBuilder builder)
    {
        try
        {
            await builder.Build(_indexerClient);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to build indexer : {Indexer}", builder.Name);
            return false;
        }

        return true;
    }
}