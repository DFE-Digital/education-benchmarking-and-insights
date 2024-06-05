using CommandLine;

namespace Platform.Search.App;

[Verb("options")]
public class ProgramOptions
{
    [Option('s', "searchName", Required = true)]
    public string? SearchName { get; set; }

    [Option('k', "searchKey", Required = true)]
    public string? SearchKey { get; set; }

    [Option('c', "sqlConnectionString", Required = true)]
    public string? SqlConnectionString { get; set; }
}