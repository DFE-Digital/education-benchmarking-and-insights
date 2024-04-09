using System.Diagnostics.CodeAnalysis;
using Microsoft.Azure.Cosmos;

namespace Web.App.Infrastructure.Cosmos;

[ExcludeFromCodeCoverage]
public static class ClientFactory
{
    public static CosmosClient Create(Settings settings)
    {
        ArgumentNullException.ThrowIfNull(settings.ConnectionString);

        return new CosmosClient(settings.ConnectionString, new CosmosClientOptions
        {
            ConnectionMode = settings.IsDirect ? ConnectionMode.Direct : ConnectionMode.Gateway
        });
    }
}