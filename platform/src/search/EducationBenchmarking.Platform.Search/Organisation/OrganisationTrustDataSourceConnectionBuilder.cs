using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using EducationBenchmarking.Platform.Infrastructure.Cosmos;
using EducationBenchmarking.Platform.Infrastructure.Search;
using EducationBenchmarking.Platform.Search.Builders;

namespace EducationBenchmarking.Platform.Search.Organisation;

public class OrganisationTrustDataSourceConnectionBuilder : DataSourceConnectionBuilder
{
    public override string Name => SearchResourceNames.DataSources.OrganisationTrust;

    private readonly string _connectionString;
    private readonly string _databaseId;
    private readonly ICollectionService _collectionService;

    public OrganisationTrustDataSourceConnectionBuilder(ICollectionService collectionService, string? connectionString, string? databaseId)
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
                "SELECT VALUE { 'Id': CONCAT('trust-',ToString(c.CompanyNumber)), 'Name': c.TrustOrCompanyName, 'Kind':'trust', 'Identifier': ToString(c.CompanyNumber),'_ts':c._ts } FROM c WHERE c._ts >= @HighWaterMark AND IS_DEFINED(c.CompanyNumber) ORDER BY c._ts"
        };

        var cosmosDbDataSource = new SearchIndexerDataSourceConnection(
            name: Name,
            type: SearchIndexerDataSourceType.CosmosDb,
            connectionString: fullConnString,
            container: container);

        await client.CreateOrUpdateDataSourceConnectionAsync(cosmosDbDataSource);
    }
}