using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using Platform.Infrastructure.Search;
using Platform.Search.Builders;

namespace Platform.Search.School;

public class SchoolDataSourceConnectionBuilder : DataSourceConnectionBuilder
{
    public override string Name => SearchResourceNames.DataSources.School;
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