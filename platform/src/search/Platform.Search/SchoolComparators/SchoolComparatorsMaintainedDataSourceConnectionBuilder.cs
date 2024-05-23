using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using Platform.Infrastructure.Search;
using Platform.Search.Builders;

namespace Platform.Search.SchoolComparators;

public class SchoolComparatorsMaintainedDataSourceConnectionBuilder : DataSourceConnectionBuilder
{
    public override string Name => SearchResourceNames.DataSources.SchoolComparatorsMaintained;

    private readonly string _connectionString;
    private readonly string _databaseId;

    public SchoolComparatorsMaintainedDataSourceConnectionBuilder(string? connectionString, string? databaseId)
    {
        _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        _databaseId = databaseId ?? throw new ArgumentNullException(nameof(databaseId));
    }

    public override async Task Build(SearchIndexerClient client)
    {
        var fullConnString = $"{_connectionString}Database={_databaseId};";
        const string collection = "Maintained-2022-2023";

        var cosmosDbDataSource = new SearchIndexerDataSourceConnection(
            name: Name,
            type: SearchIndexerDataSourceType.CosmosDb,
            connectionString: fullConnString,
            container: new SearchIndexerDataContainer(collection));

        await client.CreateOrUpdateDataSourceConnectionAsync(cosmosDbDataSource);
    }
}