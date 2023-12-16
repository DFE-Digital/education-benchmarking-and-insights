using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using EducationBenchmarking.Platform.Infrastructure.Cosmos;

namespace EducationBenchmarking.Platform.Search.Builders.DataSourceConnections;

public class CosmosEbisEdubaseSchoolBuilder : DataSourceConnectionBuilder
{
    public override string Name => Names.DataSources.CosmosEbisEdubaseSchool; 
    
    private readonly string _connectionString;
    private readonly string _databaseId;
    private readonly ICollectionService _collectionService;
    
    public CosmosEbisEdubaseSchoolBuilder(ICollectionService collectionService, string connectionString, string databaseId)
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
                "SELECT VALUE { 'id': CONCAT('school-',ToString(c.URN)), 'name': c.EstablishmentName, 'kind':'school', '_ts':c._ts } FROM c WHERE c._ts >= @HighWaterMark ORDER BY c._ts"
        };
        
        var cosmosDbDataSource = new SearchIndexerDataSourceConnection(
            name: Name,
            type: SearchIndexerDataSourceType.CosmosDb,
            connectionString: fullConnString,
            container: container );
        
        await client.CreateOrUpdateDataSourceConnectionAsync(cosmosDbDataSource);
    }
}