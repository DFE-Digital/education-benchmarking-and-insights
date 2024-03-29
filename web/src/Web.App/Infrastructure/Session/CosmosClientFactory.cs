using System.Diagnostics.CodeAnalysis;
using Microsoft.Azure.Cosmos;

namespace Web.App.Infrastructure.Session;

[ExcludeFromCodeCoverage]
public static class CosmosClientFactory
{
    public static CosmosClient Create(CosmosCacheSettings settings)
    {
        ArgumentNullException.ThrowIfNull(settings.ConnectionString);

        return new CosmosClient(settings.ConnectionString, new CosmosClientOptions
        {
            ConnectionMode = settings.IsDirect ? ConnectionMode.Direct : ConnectionMode.Gateway
        });
    }
}