using CommandLine;

namespace EducationBenchmarking.Platform.Import.App;

[Verb("options")]
public class ProgramOptions
{
    [Option('s', "sqlConnectionString", Required = true)]
    public string? SqlConnectionString { get; set; }

    [Option('t', "sqlTableName", Required = true)]
    public string? SqlTableName { get; set; }

    [Option('k', "sqlTableKey", Required = true)]
    public string? SqlTableKey { get; set; }

    [Option('c', "cosmosConnectionString", Required = true)]
    public string? CosmosConnectionString { get; set; }

    [Option('d', "cosmosDatabaseId", Required = true)]
    public string? CosmosDatabaseId { get; set; }

    [Option('l', "lookupCollectionName", Required = true)]
    public string? LookupCollectionName { get; set; }
}