using CommandLine;

namespace Platform.Database;

[Verb("options")]
public class Options
{
    [Option('c', "connectionString", Required = true)]
    public string? ConnectionString { get; set; }
}