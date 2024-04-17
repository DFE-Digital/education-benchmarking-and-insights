using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;

namespace Platform.Infrastructure.Cosmos;

[ExcludeFromCodeCoverage]
public record CosmosDatabaseOptions
{
    [Required] public string? ConnectionString { get; set; }
    [Required] public string? DatabaseId { get; set; }
    public bool IsDirect { get; set; } = true;
}

[ExcludeFromCodeCoverage]
public class CosmosClientFactory(IOptions<CosmosDatabaseOptions> options) : ICosmosClientFactory
{
    public string DatabaseId => options.Value.DatabaseId ?? throw new ArgumentNullException(nameof(options.Value.DatabaseId));

    public CosmosClient Create()
    {
        ArgumentNullException.ThrowIfNull(options.Value.ConnectionString);

        return new CosmosClient(options.Value.ConnectionString, new CosmosClientOptions
        {
            ConnectionMode = options.Value.IsDirect ? ConnectionMode.Direct : ConnectionMode.Gateway
        });
    }
}

public interface ICosmosClientFactory
{
    string DatabaseId { get; }
    CosmosClient Create();
}