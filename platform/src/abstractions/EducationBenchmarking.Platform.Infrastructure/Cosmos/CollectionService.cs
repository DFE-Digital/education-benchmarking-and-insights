using Microsoft.Extensions.Options;

namespace EducationBenchmarking.Platform.Infrastructure.Cosmos;

public interface ICollectionService
{
    string GetLatestCollection(string dataGroup);
}

public class CollectionServiceOptions
{
    public string ConnectionString { get; set; }
    public string DatabaseId { get; set; }
    public string LookupCollectionName { get; set; }
}

public class CollectionService : CosmosDatabase, ICollectionService
{
    private readonly CollectionServiceOptions _options;
    private DataCollection[]? _collections;

    public DataCollection[] ActiveCollections
    {
        get
        {
            if (_collections == null)
            {
                _collections = GetItemEnumerableAsync<DataCollection>(
                        _options.LookupCollectionName,
                        q => q.Where(x => x.Active == "Y"))
                    .ToArrayAsync().GetAwaiter().GetResult();
            }

            return _collections;
        }
    }

    public CollectionService(IOptions<CollectionServiceOptions> options) : base(options.Value.ConnectionString, options.Value.DatabaseId)
    {
        _options = options.Value;
    }

    private IEnumerable<string> GetActiveCollections(string dataGroup)
    {
        var result = ActiveCollections.Where(c => c.DataGroup == dataGroup)
            .Select(c => c.Name)
            .ToArray();
        return result;
    }

    public string GetLatestCollection(string dataGroup)
    {
        var activeCollections = GetActiveCollections(dataGroup);

        return activeCollections.MaxBy(o => o.Split('-').First()) 
               ?? throw new ArgumentException("Collection not found for the data group");
    }
    
}

