using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using Platform.Infrastructure;
using Platform.Search.Resources.Builders;

namespace Platform.Search.Resources.School;

public class SchoolDataSourceConnectionBuilder : DataSourceConnectionBuilder
{
    public override string Name => ResourceNames.Search.DataSources.School;
    private readonly string _connectionString;

    public SchoolDataSourceConnectionBuilder(string? connectionString)
    {
        _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
    }

    public override async Task Build(SearchIndexerClient client)
    {
        var dataSource = new SearchIndexerDataSourceConnection(
            name: Name,
            type: SearchIndexerDataSourceType.AzureSql,
            connectionString: _connectionString,
            container: new SearchIndexerDataContainer("School"));

        await client.CreateOrUpdateDataSourceConnectionAsync(dataSource);
    }
}