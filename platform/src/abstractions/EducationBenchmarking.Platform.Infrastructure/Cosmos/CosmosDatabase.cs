using System.Diagnostics.CodeAnalysis;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;

namespace EducationBenchmarking.Platform.Infrastructure.Cosmos;

[ExcludeFromCodeCoverage]
public abstract class CosmosDatabase
{
    private readonly CosmosClient _client;
    private readonly string _databaseId;

    protected CosmosDatabase(string connectionString, string databaseId)
    {
        _databaseId = databaseId;
        _client = CosmosClientFactory.Create(connectionString);
    }
    
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
    
    protected async IAsyncEnumerable<T> GetItemEnumerableAsync<T>(string containerId,
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
    
    protected async IAsyncEnumerable<T> GetPagedItemEnumerableAsync<T>(string containerId, int page, int pageSize,
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
    
    protected async Task<int> GetItemCountAsync<T>(string containerId, Func<IQueryable<T>, IQueryable<T>>? withF = null)
    {
        var response = await BuildQueryable(containerId, withF).CountAsync();
        return response.Resource;
    }
    
    private IQueryable<T> BuildQueryable<T>(string containerId, Func<IQueryable<T>, IQueryable<T>>? withF = null)
    {
        var container = _client.GetContainer(_databaseId, containerId);
        return withF != null ? withF(container.GetItemLinqQueryable<T>())
            : container.GetItemLinqQueryable<T>();
    }
}