using System.Diagnostics.CodeAnalysis;
using Microsoft.Azure.Cosmos;

namespace EducationBenchmarking.Platform.Infrastructure.Cosmos;

[ExcludeFromCodeCoverage]
public static class CosmosClientFactory
{
    public static CosmosClient Create(string connectionString, bool isDirect)
    {
        return new CosmosClient(connectionString, new CosmosClientOptions
        {
            ConnectionMode = isDirect ? ConnectionMode.Direct : ConnectionMode.Gateway
        });
    }
}