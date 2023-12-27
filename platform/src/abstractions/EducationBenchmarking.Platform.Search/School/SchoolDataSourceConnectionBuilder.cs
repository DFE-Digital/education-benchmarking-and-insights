using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using EducationBenchmarking.Platform.Infrastructure.Cosmos;
using EducationBenchmarking.Platform.Infrastructure.Search;
using EducationBenchmarking.Platform.Search.Builders;

namespace EducationBenchmarking.Platform.Search.School;

public class SchoolDataSourceConnectionBuilder : DataSourceConnectionBuilder
{
    public override string Name => SearchResourceNames.DataSources.School; 
    
    private readonly string _connectionString;
    private readonly string _databaseId;
    private readonly ICollectionService _collectionService;
    
    public SchoolDataSourceConnectionBuilder(ICollectionService collectionService, string connectionString, string databaseId)
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