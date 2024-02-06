using System.ComponentModel.DataAnnotations;
using Azure;
using Azure.Search.Documents.Indexes;
using EducationBenchmarking.Platform.Infrastructure.Cosmos;
using EducationBenchmarking.Platform.Search.Builders;
using EducationBenchmarking.Platform.Search.Organisation;
using EducationBenchmarking.Platform.Search.School;
using EducationBenchmarking.Platform.Search.Trust;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EducationBenchmarking.Platform.Search;

public interface ISearchMaintenanceService
{
    Task Rebuild();
    Task Reset();
}

public class SearchMaintenanceServiceOptions
{
    [Required] public CosmosOptions? Cosmos { get; set; }
    [Required] public StorageOptions? Storage { get; set; }
    [Required] public string? Name { get; set; }
    [Required] public string? Key { get; set; }
    
    public Uri SearchEndPoint => new($"https://{Name}.search.windows.net/");
    public AzureKeyCredential SearchCredentials => new(Key ?? throw new ArgumentNullException());
    
    public class CosmosOptions
    {
        [Required] public string? ConnectionString { get; set; }
        [Required] public string? DatabaseId { get; set; } 
    }
    
    public class StorageOptions
    {
        [Required] public string? ConnectionString { get; set; }
        [Required] public string? LocalAuthoritiesContainer { get; set; } 
    }
}

public class SearchMaintenanceService : ISearchMaintenanceService
{
    private readonly SearchIndexerClient _indexerClient;
    private readonly SearchIndexClient _indexClient;
    private readonly ILogger<SearchMaintenanceService> _logger;
    private readonly SearchMaintenanceServiceOptions _options;
    private readonly ICollectionService _collectionService;

    public SearchMaintenanceService(IOptions<SearchMaintenanceServiceOptions> options, ILogger<SearchMaintenanceService> logger, ICollectionService collectionService)
    {
        _logger = logger;
        _collectionService = collectionService;
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
    }

    public Task Reset()
    {
        throw new NotImplementedException();
    }

    private async Task RemoveDataSourcesConnections()
    {
        var connections = await _indexerClient.GetDataSourceConnectionNamesAsync();

        if (connections.HasValue)
        {
            foreach (var connection in connections.Value)
            {
                await RemoveDataSourcesConnection(connection);
            }
        }
    }

    private async Task RemoveDataSourcesConnection(string connection)
    {
        try
        {
            await _indexerClient.DeleteDataSourceConnectionAsync(connection);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to delete data source connection : {Connection}", connection);
        }
    }
    
    private async Task RemoveIndexers()
    {
        var indexers = await _indexerClient.GetIndexerNamesAsync();

        if (indexers.HasValue)
        {
            foreach (var indexer in indexers.Value)
            {
                await RemoveIndexer(indexer);
            }
        }
    }

    private async Task RemoveIndexer(string indexer)
    {
        try
        {
            await _indexerClient.DeleteIndexerAsync(indexer);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to delete indexer : {Indexer}", indexer);
        }
    }
    
    private async Task RemoveIndexes()
    {
        var indexes = await _indexClient.GetIndexNamesAsync().ToArrayAsync();
        
            foreach (var index in indexes)
            {
                await RemoveIndex(index);
            }
    }

    private async Task RemoveIndex(string index)
    {
        try
        {
            await _indexClient.DeleteIndexAsync(index);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to delete index : {Index}", index);
        }
    }
    
    private async Task BuildIndexes()
    {
        var builders = new IndexBuilder[] 
        {
            new OrganisationIndexBuilder(),
            new TrustIndexBuilder(),
            new SchoolIndexBuilder()
        };

        foreach (var builder in builders)
        {
            await BuildIndex(builder);
        }
    }

    private async Task BuildIndex(IndexBuilder builder)
    {
        try
        {
            await builder.Build(_indexClient);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to build index : {Index}", builder.Name);
        }
    }

    private async Task BuildDataSourcesConnections()
    {
        ArgumentNullException.ThrowIfNull(_options.Cosmos);
        ArgumentNullException.ThrowIfNull(_options.Storage);
        
        var builders = new DataSourceConnectionBuilder[]
        {
            new OrganisationSchoolDataSourceConnectionBuilder(_collectionService, _options.Cosmos.ConnectionString, _options.Cosmos.DatabaseId),
            new OrganisationTrustDataSourceConnectionBuilder(_collectionService, _options.Cosmos.ConnectionString, _options.Cosmos.DatabaseId),
            new OrganisationLaDataSourceConnectionBuilder(_options.Storage.ConnectionString, _options.Storage.LocalAuthoritiesContainer),
            new SchoolDataSourceConnectionBuilder(_collectionService, _options.Cosmos.ConnectionString, _options.Cosmos.DatabaseId),
            new TrustDataSourceConnectionBuilder(_collectionService, _options.Cosmos.ConnectionString, _options.Cosmos.DatabaseId)
        };

        foreach (var builder in builders)
        {
            await BuildDataSourcesConnection(builder);
        }
    }

    private async Task BuildDataSourcesConnection(DataSourceConnectionBuilder builder)
    {
        try
        {
            await builder.Build(_indexerClient);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to build data source connection : {Connection}", builder.Name);
        }
    }
    
    private async Task BuildIndexers()
    {
        var builders = new IndexerBuilder[]
        {
            new SchoolIndexerBuilder(),
            new TrustIndexerBuilder(),
            new OrganisationSchoolIndexerBuilder(),
            new OrganisationTrustIndexerBuilder(),
            new OrganisationLaIndexerBuilder()
        };

        foreach (var builder in builders)
        {
            await BuildIndexer(builder);
        }
    }

    private async Task BuildIndexer(IndexerBuilder builder)
    {
        try
        {
            await builder.Build(_indexerClient);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to build indexer : {Indexer}", builder.Name);
        }
    }
}