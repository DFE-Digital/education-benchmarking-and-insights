using CommandLine;

var result = Parser.Default.ParseArguments<Options>(args);

Console.WriteLine("Hello, World!");

[Verb("options")]
public class Options
{
}