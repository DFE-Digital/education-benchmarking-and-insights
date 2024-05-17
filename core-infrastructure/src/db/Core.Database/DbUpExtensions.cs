using DbUp.Engine;

namespace Platform.Database;

public static class DbUpExtensions
{
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