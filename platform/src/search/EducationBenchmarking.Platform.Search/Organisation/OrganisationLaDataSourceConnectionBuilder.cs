using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using EducationBenchmarking.Platform.Infrastructure.Search;
using EducationBenchmarking.Platform.Search.Builders;

namespace EducationBenchmarking.Platform.Search.Organisation;

public class OrganisationLaDataSourceConnectionBuilder : DataSourceConnectionBuilder
{
    public override string Name => SearchResourceNames.DataSources.OrganisationLa;

    private readonly string _container;
    private readonly string _connectionString;
    
    public OrganisationLaDataSourceConnectionBuilder(string? connectionString, string? container)
    {
        _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        _container = container ?? throw new ArgumentNullException(nameof(container));
    }
    
    public override async Task Build(SearchIndexerClient client)
    {
        var dataSource = new SearchIndexerDataSourceConnection(
            name: Name,
            type: SearchIndexerDataSourceType.AzureBlob,
            connectionString: _connectionString,
            container: new SearchIndexerDataContainer(_container) );
        
        await client.CreateOrUpdateDataSourceConnectionAsync(dataSource);
    }
}