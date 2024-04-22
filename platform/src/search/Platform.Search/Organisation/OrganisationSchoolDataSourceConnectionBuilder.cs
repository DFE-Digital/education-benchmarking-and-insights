using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using Platform.Domain;
using Platform.Search.Builders;

namespace Platform.Search.Organisation;

public class OrganisationSchoolDataSourceConnectionBuilder : DataSourceConnectionBuilder
{
    public override string Name => SearchResourceNames.DataSources.OrganisationSchool;

    private readonly string _connectionString;
    private readonly string _databaseId;

    public OrganisationSchoolDataSourceConnectionBuilder(string? connectionString, string? databaseId)
    {
        _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        _databaseId = databaseId ?? throw new ArgumentNullException(nameof(databaseId));
    }

    public override async Task Build(SearchIndexerClient client)
    {
        const string collection = "GIAS";
        var fullConnString = $"{_connectionString}Database={_databaseId};";

        var container = new SearchIndexerDataContainer(collection)
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
            container: container);

        await client.CreateOrUpdateDataSourceConnectionAsync(cosmosDbDataSource);
    }
}