using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using Azure;
using Azure.Identity;
using Microsoft.Azure.Cosmos;
namespace Web.App.Infrastructure.Cosmos;

[ExcludeFromCodeCoverage]
public static class ClientFactory
{
    public static CosmosClient Create(Settings settings)
    {
        ArgumentNullException.ThrowIfNull(settings.ConnectionString);
        var options = new CosmosClientOptions
        {
            ConnectionMode = settings.IsDirect ? ConnectionMode.Direct : ConnectionMode.Gateway
        };

        var key = GetAccountKey(settings.ConnectionString);
        var endpoint = GetAccountEndpoint(settings.ConnectionString);
        return string.IsNullOrWhiteSpace(key)
            ? new CosmosClient(endpoint, new DefaultAzureCredential(), options)
            : new CosmosClient(endpoint, new AzureKeyCredential(key), options);
    }

    // from CosmosClientOptions.cs
    private static string? GetAccountEndpoint(string connectionString) => GetValueFromConnectionString(connectionString, "AccountEndpoint");

    private static string? GetAccountKey(string connectionString) => GetValueFromConnectionString(connectionString, "AccountKey");

    private static string? GetValueFromConnectionString(string connectionString, string keyName)
    {
        var builder = new DbConnectionStringBuilder
        {
            ConnectionString = connectionString
        };
        if (!builder.TryGetValue(keyName, out var value))
        {
            return null;
        }

        var keyNameValue = value as string;
        if (!string.IsNullOrEmpty(keyNameValue))
        {
            return keyNameValue;
        }

        throw new ArgumentException("The connection string is missing a required property: " + keyName);
    }
}