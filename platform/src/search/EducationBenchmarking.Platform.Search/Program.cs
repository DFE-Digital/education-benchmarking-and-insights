using CommandLine;
using EducationBenchmarking.Platform.Search;

var result = Parser.Default.ParseArguments<Options>(args);

Console.WriteLine("Hello, World!");

namespace EducationBenchmarking.Platform.Search
{
    [Verb("options")]
    public class Options
    {
    }
}