using System.Diagnostics.CodeAnalysis;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;

namespace Platform.Infrastructure.Cosmos;


[ExcludeFromCodeCoverage]
public abstract class CosmosDatabase(ICosmosClientFactory factory)
{
    private readonly CosmosClient _client = factory.Create();
    private readonly string _databaseId = factory.DatabaseId;

    protected Task<ItemResponse<T>> ReadItemAsync<T>(string containerId, string id, string partitionKey)
    {
        var container = _client.GetContainer(_databaseId, containerId);
        return container.ReadItemAsync<T>(id, new PartitionKey(partitionKey));
    }

    protected Task<ResponseMessage> ReadItemStreamAsync(string containerId, string id, string partitionKey)
    {
        var container = _client.GetContainer(_databaseId, containerId);
        return container.ReadItemStreamAsync(id, new PartitionKey(partitionKey));
    }

    protected async Task UpsertItemAsync<T>(string containerId, T item, PartitionKey partitionKey)
    {
        var container = _client.GetContainer(_databaseId, containerId);
        await container.UpsertItemAsync(item, partitionKey);
    }

    protected async Task DeleteItemAsync<T>(string containerId, string identifier, PartitionKey partitionKey)
    {
        var container = _client.GetContainer(_databaseId, containerId);
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
        var container = _client.GetContainer(_databaseId, containerId);
        return withF != null ? withF(container.GetItemLinqQueryable<T>())
            : container.GetItemLinqQueryable<T>();
    }
}