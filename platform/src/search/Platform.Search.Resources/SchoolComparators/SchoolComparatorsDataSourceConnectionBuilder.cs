using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using Platform.Infrastructure;
using Platform.Search.Resources.Builders;

namespace Platform.Search.Resources.SchoolComparators;

public class SchoolComparatorsDataSourceConnectionBuilder : DataSourceConnectionBuilder
{
    public override string Name => ResourceNames.Search.DataSources.SchoolComparators;
    private readonly string _connectionString;

    public SchoolComparatorsDataSourceConnectionBuilder(string? connectionString)
    {
        _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
    }

    public override async Task Build(SearchIndexerClient client)
    {
        var dataSource = new SearchIndexerDataSourceConnection(
            name: Name,
            type: SearchIndexerDataSourceType.AzureSql,
            connectionString: _connectionString,
            container: new SearchIndexerDataContainer("SchoolCharacteristic"));

        await client.CreateOrUpdateDataSourceConnectionAsync(dataSource);
    }
}