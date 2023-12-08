using CommandLine;
using EducationBenchmarking.Platform.Database;

var result = Parser.Default.ParseArguments<Options>(args);

Console.WriteLine("Hello, World!");

namespace EducationBenchmarking.Platform.Database
{
    [Verb("options")]
    public class Options
    {
    }
}