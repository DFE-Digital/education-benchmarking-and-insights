using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Options;

namespace Platform.Infrastructure.Cosmos;

[ExcludeFromCodeCoverage]
public record CollectionServiceOptions : CosmosDatabaseOptions
{
    [Required] public string? LookupCollectionName { get; set; }
}

[ExcludeFromCodeCoverage]
public class CollectionService(IOptions<CollectionServiceOptions> options)
    : CosmosDatabase(options.Value), ICollectionService
{
    private readonly CollectionServiceOptions _options = options.Value;

    private async Task<DataCollection[]> AllActiveCollections()
    {
        ArgumentNullException.ThrowIfNull(_options.LookupCollectionName);

        return await ItemEnumerableAsync<DataCollection>(
                _options.LookupCollectionName,
                q => q.Where(x => x.Active == "Y"))
            .ToArrayAsync();
    }


    private async Task<IEnumerable<DataCollection>> ActiveCollectionsForDataGroup(string dataGroup)
    {
        var groups = await AllActiveCollections();
        return groups.Where(c => c.DataGroup == dataGroup)
            .ToArray();
    }

    public async Task<DataCollection> LatestCollection(string dataGroup)
    {
        var activeCollections = await ActiveCollectionsForDataGroup(dataGroup);

        return activeCollections.MaxBy(o => o.Name?.Split('-').First())
               ?? throw new ArgumentException("Collection not found for the data group");
    }
}

public interface ICollectionService
{
    Task<DataCollection> LatestCollection(string dataGroup);
}