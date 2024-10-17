using DbUp.Builder;
using DbUp.Engine;

namespace Core.Database;

public static class DbUpExtensions
{

    public static UpgradeEngineBuilder SetTimeout(this UpgradeEngineBuilder builder, int seconds)
    {
        builder.Configure(x => x.ScriptExecutor.ExecutionTimeoutSeconds = seconds);
        return builder;
    }

    public static void Execute(this UpgradeEngine engine)
    {
        var result = engine.PerformUpgrade();

        if (!result.Successful)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine(result.Error);
            Console.ResetColor();
            Environment.Exit(-1);
        }
    }
}