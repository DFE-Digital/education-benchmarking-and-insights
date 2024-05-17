using CommandLine;

namespace Core.Database;

[Verb("options")]
public class Options
{
    [Option('c', "connectionString", Required = true)]
    public string? ConnectionString { get; set; }
}