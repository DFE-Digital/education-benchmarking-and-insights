using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Options;

namespace EducationBenchmarking.Platform.Infrastructure.Cosmos;

public interface ICollectionService
{
    Task<DataCollection> GetLatestCollection(string dataGroup);
}

public class CollectionServiceOptions
{
    [Required] public string ConnectionString { get; set; }
    [Required] public string DatabaseId { get; set; }
    [Required] public string LookupCollectionName { get; set; }
}

public class CollectionService : CosmosDatabase, ICollectionService
{
    private readonly CollectionServiceOptions _options;

    public CollectionService(IOptions<CollectionServiceOptions> options) : base(options.Value.ConnectionString,
        options.Value.DatabaseId)
    {
        _options = options.Value;
    }

    private async Task<DataCollection[]> GetAllActiveCollections()
    {
        return await GetItemEnumerableAsync<DataCollection>(
                _options.LookupCollectionName,
                q => q.Where(x => x.Active == "Y"))
            .ToArrayAsync();
    }


    private async Task<IEnumerable<DataCollection>> GetActiveCollectionsForDataGroup(string dataGroup)
    {
        var groups = await GetAllActiveCollections();
        return groups.Where(c => c.DataGroup == dataGroup)
            .ToArray();
    }

    public async Task<DataCollection> GetLatestCollection(string dataGroup)
    {
        var activeCollections = await GetActiveCollectionsForDataGroup(dataGroup);

        return activeCollections.MaxBy(o => o.Name.Split('-').First())
               ?? throw new ArgumentException("Collection not found for the data group");
    }
}