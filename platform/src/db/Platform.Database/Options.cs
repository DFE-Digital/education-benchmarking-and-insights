using CommandLine;

namespace Platform.Database;

[Verb("options")]
public class Options
{
    [Option('s', "server", Required = true)]
    public string? Server { get; set; }
    
    [Option('d', "database", Required = true)]
    public string? Database { get; set; }
    
    [Option('u', "user", Required = true)]
    public string? User { get; set; }
    
    [Option('p', "password", Required = true)]
    public string? Password { get; set; }
}