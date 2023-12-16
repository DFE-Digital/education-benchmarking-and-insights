using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using EducationBenchmarking.Platform.Infrastructure.Cosmos;

namespace EducationBenchmarking.Platform.Search.Builders.DataSourceConnections;

public class CosmosEbisEdubaseBuilder : DataSourceConnectionBuilder
{
    public override string Name => Names.DataSources.CosmosEbisEdubase; 
    
    private readonly string _connectionString;
    private readonly string _databaseId;
    private readonly ICollectionService _collectionService;
    
    public CosmosEbisEdubaseBuilder(ICollectionService collectionService, string connectionString, string databaseId)
    {
        _collectionService = collectionService;
        _connectionString = connectionString;
        _databaseId = databaseId;
    }
    
    public override async Task Build(SearchIndexerClient client)
    {
        var fullConnString = $"{_connectionString}Database={_databaseId};";
        var collection = await _collectionService.GetLatestCollection(DataGroups.Edubase);
        
        var cosmosDbDataSource = new SearchIndexerDataSourceConnection(
            name: Name,
            type: SearchIndexerDataSourceType.CosmosDb,
            connectionString: fullConnString,
            container: new SearchIndexerDataContainer(collection.Name));
        
        await client.CreateOrUpdateDataSourceConnectionAsync(cosmosDbDataSource);
    }
}