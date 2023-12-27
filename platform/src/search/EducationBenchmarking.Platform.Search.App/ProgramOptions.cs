using CommandLine;

namespace EducationBenchmarking.Platform.Search.App;

[Verb("options")]
public class ProgramOptions
{
    [Option('s', "searchName", Required = true)]
    public string SearchName { get; set; }
    
    [Option('k', "searchKey", Required = true)]
    public string SearchKey { get; set; }
    
    [Option('c', "cosmosConnectionString", Required = true)]
    public string CosmosConnectionString { get; set; }
    
    [Option('d', "cosmosDatabaseId", Required = true)]
    public string CosmosDatabaseId { get; set; }
    
    [Option('l', "lookupCollectionName", Required = true)]
    public string LookupCollectionName { get; set; }
    
    [Option('p', "platformStorageConnectionString", Required = true)]
    public string PlatformStorageConnectionString { get; set; }
    
    [Option('a', "localAuthoritiesContainer", Required = true)]
    public string LocalAuthoritiesContainer { get; set; }
}