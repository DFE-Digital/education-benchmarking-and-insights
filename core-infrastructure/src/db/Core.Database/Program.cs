using System.Reflection;
using CommandLine;
using CommandLine.Text;
using Core.Database;
using DbUp;
using DbUp.Helpers;

var result = Parser.Default.ParseArguments<Options>(args);
await result.MapResult(Deploy, _ => HandleErrors(result));
return;

static Task Deploy(Options options)
{
    //Deploy core structure
    DeployChanges.To
     .SqlDatabase(options.ConnectionString)
     .JournalToSqlTable("dbo", "SchemaVersions")
     .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly(), s => s.StartsWith("Core.Database.Scripts"))
     .SetTimeout(60 * 10)
     .LogToConsole()
     .Build()
     .Execute();

    //Deploy views
    DeployChanges.To
           .SqlDatabase(options.ConnectionString)
           .JournalTo(new NullJournal())
           .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly(), s => s.StartsWith("Core.Database.Views"))
           .SetTimeout(60 * 10)
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