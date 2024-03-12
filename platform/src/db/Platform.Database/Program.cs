using System.Reflection;
using CommandLine;
using CommandLine.Text;
using DbUp;
using Platform.Database;

var result = Parser.Default.ParseArguments<Options>(args);
await result.MapResult(Deploy, _ => HandleErrors(result));
return;

static Task Deploy(Options options)
{
    DeployChanges.To
     .SqlDatabase(options.ConnectionString)
     .JournalToSqlTable("dbo", "SchemaVersions")
     .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly(), s => s.StartsWith("Platform.Database.Scripts"))
     .LogToConsole()
     .Build()
     .Execute();

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Success!");
    Console.ResetColor();

    return Task.CompletedTask;
}

static Task HandleErrors<T>(ParserResult<T> result)
{
    Console.WriteLine(HelpText.RenderUsageText(result));

    return Task.CompletedTask;
}