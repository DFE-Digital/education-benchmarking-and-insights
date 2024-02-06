using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using EducationBenchmarking.Platform.Infrastructure.Cosmos;
using EducationBenchmarking.Platform.Infrastructure.Search;
using EducationBenchmarking.Platform.Search.Builders;

namespace EducationBenchmarking.Platform.Search.Organisation;

public class OrganisationSchoolDataSourceConnectionBuilder : DataSourceConnectionBuilder
{
    public override string Name => SearchResourceNames.DataSources.OrganisationSchool; 
    
    private readonly string _connectionString;
    private readonly string _databaseId;
    private readonly ICollectionService _collectionService;
    
    public OrganisationSchoolDataSourceConnectionBuilder(ICollectionService collectionService, string? connectionString, string? databaseId)
    {
        _collectionService = collectionService;
        _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        _databaseId = databaseId ?? throw new ArgumentNullException(nameof(databaseId));
    }
    
    public override async Task Build(SearchIndexerClient client)
    {
        var fullConnString = $"{_connectionString}Database={_databaseId};";
        var collection = await _collectionService.LatestCollection(DataGroups.Edubase);


        var container = new SearchIndexerDataContainer(collection.Name)
        {
            Query =
                "SELECT VALUE { " +
                "'Id': CONCAT('school-',ToString(c.URN)), " +
                "'Name': c.EstablishmentName, " +
                "'Kind':'school', " +
                "'Identifier': c.URN," +
                "'Street': c.Street," +
                "'Locality': c.Locality," +
                "'Address3': c.Address3," +
                "'Town': c.Town," +
                "'County': c.County," +
                "'Postcode': c.Postcode," +
                "'_ts':c._ts }" +
                " FROM c WHERE c._ts >= @HighWaterMark ORDER BY c._ts"
        };
        
        var cosmosDbDataSource = new SearchIndexerDataSourceConnection(
            name: Name,
            type: SearchIndexerDataSourceType.CosmosDb,
            connectionString: fullConnString,
            container: container );
        
        await client.CreateOrUpdateDataSourceConnectionAsync(cosmosDbDataSource);
    }
}