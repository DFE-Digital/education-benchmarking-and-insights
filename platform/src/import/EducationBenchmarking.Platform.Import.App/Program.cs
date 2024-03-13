using CommandLine;
using CommandLine.Text;
using EducationBenchmarking.Platform.Import;
using EducationBenchmarking.Platform.Import.App;
using EducationBenchmarking.Platform.Import.Db;
using EducationBenchmarking.Platform.Infrastructure.Cosmos;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

var result = Parser.Default.ParseArguments<ProgramOptions>(args);

await result.MapResult(ImportComparatorSets, _=> HandleErrors(result));

static Task ImportComparatorSets(ProgramOptions options)
{
    var collectionServiceOptions = Options.Create(new CollectionServiceOptions
    {
        DatabaseId = options.CosmosDatabaseId,
        ConnectionString = options.CosmosConnectionString,
        LookupCollectionName = options.LookupCollectionName
    });

    var importOptions = Options.Create(new ComparatorImportServiceOptions
    {
        Sql = new ComparatorImportServiceOptions.SqlOptions
        {
            ConnectionString = options.SqlConnectionString,
            TableName = options.SqlTableName,
            TableKey = options.SqlTableKey
        },
        Cosmos = new ComparatorImportServiceOptions.CosmosOptions
        {
            ConnectionString = options.CosmosConnectionString,
            DatabaseId = options.CosmosDatabaseId,
            LookupCollectionName = options.LookupCollectionName
        }
    });

    var dbOptions = Options.Create(new ComparatorSetLookupDbOptions
    {
        ConnectionString = options.CosmosConnectionString,
        DatabaseId = options.CosmosDatabaseId,
        IsDirect = false,
        ComparatorSetLookupCollectionName = options.LookupCollectionName
    });
    
    var logger = BuilderLogger<ComparatorImportService>();
    var db = new ComparatorSetLookupDb(dbOptions);    
    var service = new ComparatorImportService(importOptions, logger, db);

    return service.Import();
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
            .AddFilter("EducationBenchmarking.Platform.Search.App", LogLevel.Debug)
            .AddConsole();
    });
    return loggerFactory.CreateLogger<T>();
}