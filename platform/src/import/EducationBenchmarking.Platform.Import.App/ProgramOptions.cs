using CommandLine;

namespace EducationBenchmarking.Platform.Import.App;

[Verb("options")]
public class ProgramOptions
{
    [Option('s', "sqlConnectionString", Required = true)]
    public string? SqlConnectionString { get; set; }

    [Option('n', "sqlComparatorTableName", Required = true)]
    public string? SqlComparatorTableName { get; set; }

    [Option('k', "sqlComparatorTableKey", Required = true)]
    public string? SqlComparatorTableKey { get; set; }
    
    [Option('e', "sqlEntityTableName", Required = true)]
    public string? SqlEntityTableName { get; set; }
    
    [Option('t', "sqlEntityTableKey", Required = true)]
    public string? SqlEntityTableKey { get; set; }

    [Option('c', "cosmosConnectionString", Required = true)]
    public string? CosmosConnectionString { get; set; }

    [Option('d', "cosmosDatabaseId", Required = true)]
    public string? CosmosDatabaseId { get; set; }

    [Option('l', "lookupCollectionName", Required = true)]
    public string? LookupCollectionName { get; set; }
}