using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using Platform.Infrastructure;
using Platform.Search.Resources.Builders;

namespace Platform.Search.Resources.LocalAuthority;

public class LocalAuthorityDataSourceConnectionBuilder(string? connectionString) : DataSourceConnectionBuilder
{
    public override string Name => ResourceNames.Search.DataSources.LocalAuthority;

    private readonly string _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));

    public override async Task Build(SearchIndexerClient client)
    {
        var dataSource = new SearchIndexerDataSourceConnection(
            name: Name,
            type: SearchIndexerDataSourceType.AzureSql,
            connectionString: _connectionString,
            container: new SearchIndexerDataContainer("VW_LocalAuthoritySummary"));

        await client.CreateOrUpdateDataSourceConnectionAsync(dataSource);
    }
}