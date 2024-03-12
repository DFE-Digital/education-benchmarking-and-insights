using CommandLine;
using Platform.Database;

var result = Parser.Default.ParseArguments<Options>(args);

Console.WriteLine("Hello, World!");

namespace Platform.Database
{
    [Verb("options")]
    public class Options
    {
    }
}