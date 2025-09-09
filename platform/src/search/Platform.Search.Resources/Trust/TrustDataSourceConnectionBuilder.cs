using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using Platform.Infrastructure;
using Platform.Search.Resources.Builders;

namespace Platform.Search.Resources.Trust;

public class TrustDataSourceConnectionBuilder(string? connectionString) : DataSourceConnectionBuilder
{
    private readonly string _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
    public override string Name => ResourceNames.Search.DataSources.Trust;

    public override async Task Build(SearchIndexerClient client)
    {
        var dataSource = new SearchIndexerDataSourceConnection(
            Name,
            SearchIndexerDataSourceType.AzureSql,
            _connectionString,
            new SearchIndexerDataContainer("VW_TrustSummary"));

        await client.CreateOrUpdateDataSourceConnectionAsync(dataSource);
    }
}