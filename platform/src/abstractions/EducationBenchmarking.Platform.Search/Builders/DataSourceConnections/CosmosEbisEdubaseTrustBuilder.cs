using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using EducationBenchmarking.Platform.Infrastructure.Cosmos;
using EducationBenchmarking.Platform.Infrastructure.Search;

namespace EducationBenchmarking.Platform.Search.Builders.DataSourceConnections;

public class CosmosEbisEdubaseTrustBuilder : DataSourceConnectionBuilder
{
    public override string Name => SearchResourceNames.DataSources.CosmosEbisEdubaseTrust; 
    
    private readonly string _connectionString;
    private readonly string _databaseId;
    private readonly ICollectionService _collectionService;
    
    public CosmosEbisEdubaseTrustBuilder(ICollectionService collectionService, string connectionString, string databaseId)
    {
        _collectionService = collectionService;
        _connectionString = connectionString;
        _databaseId = databaseId;
    }
    
    public override async Task Build(SearchIndexerClient client)
    {
        var fullConnString = $"{_connectionString}Database={_databaseId};";
        var collection = await _collectionService.GetLatestCollection(DataGroups.Edubase);


        var container = new SearchIndexerDataContainer(collection.Name)
        {
            Query =
                "SELECT * FROM c WHERE c._ts >= @HighWaterMark AND IS_DEFINED(c.CompanyNumber) ORDER BY c._ts"
        };
        
        var cosmosDbDataSource = new SearchIndexerDataSourceConnection(
            name: Name,
            type: SearchIndexerDataSourceType.CosmosDb,
            connectionString: fullConnString,
            container: container );
        
        await client.CreateOrUpdateDataSourceConnectionAsync(cosmosDbDataSource);
    }
}