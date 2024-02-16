using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;

namespace EducationBenchmarking.Platform.Infrastructure.Cosmos;

public class CosmosDatabaseOptions
{
    [Required] public string? ConnectionString { get; set; }
    [Required] public string? DatabaseId { get; set; }
    public bool IsDirect { get; set; } = true;
}


[ExcludeFromCodeCoverage]
public abstract class CosmosDatabase
{
    private readonly CosmosDatabaseOptions _options;
    private readonly CosmosClient _client;

    protected CosmosDatabase(CosmosDatabaseOptions options)
    {
        _options = options;
        _client = CosmosClientFactory.Create(options.ConnectionString, options.IsDirect);
    }

    protected Task<ItemResponse<T>> ReadItemAsync<T>(string containerId, string id, string partitionKey)
    {
        var container = _client.GetContainer(_options.DatabaseId, containerId);
        return container.ReadItemAsync<T>(id, new PartitionKey(partitionKey));
    }

    protected Task<ResponseMessage> ReadItemStreamAsync(string containerId, string id, string partitionKey)
    {
        var container = _client.GetContainer(_options.DatabaseId, containerId);
        return container.ReadItemStreamAsync(id, new PartitionKey(partitionKey));
    }

    protected async Task UpsertItemAsync<T>(string containerId, T item, PartitionKey partitionKey)
    {
        var container = _client.GetContainer(_options.DatabaseId, containerId);
        await container.UpsertItemAsync(item, partitionKey);
    }

    protected async Task DeleteItemAsync<T>(string containerId, string identifier, PartitionKey partitionKey)
    {
        var container = _client.GetContainer(_options.DatabaseId, containerId);
        await container.DeleteItemAsync<T>(identifier, partitionKey);
    }

    protected async IAsyncEnumerable<T> ItemEnumerableAsync<T>(string? containerId,
        Func<IQueryable<T>, IQueryable<T>>? withF = null)
    {
        var queryable = BuildQueryable(containerId, withF);

        using (var feedIterator = queryable.ToFeedIterator())
        {
            while (feedIterator.HasMoreResults)
            {
                foreach (var item in await feedIterator.ReadNextAsync())
                {
                    {
                        yield return item;
                    }
                }
            }
        }
    }

    protected async IAsyncEnumerable<T> PagedItemEnumerableAsync<T>(string? containerId, int page, int pageSize,
        Func<IQueryable<T>, IQueryable<T>>? withF = null)
    {
        var start = (page - 1) * pageSize;
        var queryable = BuildQueryable(containerId, withF);
        var pagedQueryable = queryable.Skip(start).Take(pageSize);

        using (var feedIterator = pagedQueryable.ToFeedIterator())
        {
            while (feedIterator.HasMoreResults)
            {
                foreach (var item in await feedIterator.ReadNextAsync())
                {
                    {
                        yield return item;
                    }
                }
            }
        }
    }

    protected async Task<int> ItemCountAsync<T>(string? containerId, Func<IQueryable<T>, IQueryable<T>>? withF = null)
    {
        var response = await BuildQueryable(containerId, withF).CountAsync();
        return response.Resource;
    }

    private IQueryable<T> BuildQueryable<T>(string? containerId, Func<IQueryable<T>, IQueryable<T>>? withF = null)
    {
        ArgumentNullException.ThrowIfNull(containerId);
        var container = _client.GetContainer(_options.DatabaseId, containerId);
        return withF != null ? withF(container.GetItemLinqQueryable<T>())
            : container.GetItemLinqQueryable<T>();
    }
}