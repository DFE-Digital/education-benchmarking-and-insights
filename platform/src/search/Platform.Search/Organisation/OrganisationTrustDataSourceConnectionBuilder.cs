using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using Platform.Infrastructure.Search;
using Platform.Search.Builders;

namespace Platform.Search.Organisation;

public class OrganisationTrustDataSourceConnectionBuilder : DataSourceConnectionBuilder
{
    public override string Name => SearchResourceNames.DataSources.OrganisationTrust;

    private readonly string _connectionString;
    private readonly string _databaseId;

    public OrganisationTrustDataSourceConnectionBuilder(string? connectionString, string? databaseId)
    {
        _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        _databaseId = databaseId ?? throw new ArgumentNullException(nameof(databaseId));
    }

    public override async Task Build(SearchIndexerClient client)
    {
        var fullConnString = $"{_connectionString}Database={_databaseId};";
        const string collection = "GIAS";

        var container = new SearchIndexerDataContainer(collection)
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