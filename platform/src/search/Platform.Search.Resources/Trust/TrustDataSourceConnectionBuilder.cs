using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using Platform.Infrastructure;
using Platform.Search.Resources.Builders;

namespace Platform.Search.Resources.Trust;

public class TrustDataSourceConnectionBuilder(string? connectionString) : DataSourceConnectionBuilder
{
    public override string Name => ResourceNames.Search.DataSources.Trust;
    private readonly string _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));

    public override async Task Build(SearchIndexerClient client)
    {
        var dataSource = new SearchIndexerDataSourceConnection(
            name: Name,
            type: SearchIndexerDataSourceType.AzureSql,
            connectionString: _connectionString,
            container: new SearchIndexerDataContainer("VW_TrustSummary"));

        await client.CreateOrUpdateDataSourceConnectionAsync(dataSource);
    }
}