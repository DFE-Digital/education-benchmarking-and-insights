using CommandLine;
using CommandLine.Text;
using Platform.Search;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Platform.Infrastructure.Cosmos;
using Platform.Search.App;

var result = Parser.Default.ParseArguments<ProgramOptions>(args);

await result.MapResult(RebuildIndexes, _ => HandleErrors(result));

static Task RebuildIndexes(ProgramOptions options)
{
    var collectionServiceOptions = Options.Create(new CollectionServiceOptions
    {
        DatabaseId = options.CosmosDatabaseId,
        ConnectionString = options.CosmosConnectionString,
        LookupCollectionName = options.LookupCollectionName
    });

    var searchOptions = Options.Create(new SearchMaintenanceServiceOptions
    {
        Key = options.SearchKey,
        Name = options.SearchName,
        Cosmos = new SearchMaintenanceServiceOptions.CosmosOptions
        {
            ConnectionString = options.CosmosConnectionString,
            DatabaseId = options.CosmosDatabaseId
        },
        Storage = new SearchMaintenanceServiceOptions.StorageOptions
        {
            ConnectionString = options.PlatformStorageConnectionString,
            LocalAuthoritiesContainer = options.LocalAuthoritiesContainer,
        }
    });

    var logger = BuilderLogger<SearchMaintenanceService>();
    var collectionService = new CollectionService(collectionServiceOptions);
    var service = new SearchMaintenanceService(searchOptions, logger, collectionService);

    return service.Rebuild();
}

static Task HandleErrors<T>(ParserResult<T> result)
{
    Console.WriteLine(HelpText.RenderUsageText(result));
    return Task.CompletedTask;
}

static ILogger<T> BuilderLogger<T>()
{
    using var loggerFactory = LoggerFactory.Create(builder =>
    {
        builder
            .AddFilter("Microsoft", LogLevel.Warning)
            .AddFilter("System", LogLevel.Warning)
            .AddFilter("Platform.Search.App", LogLevel.Debug)
            .AddConsole();
    });
    return loggerFactory.CreateLogger<T>();
}