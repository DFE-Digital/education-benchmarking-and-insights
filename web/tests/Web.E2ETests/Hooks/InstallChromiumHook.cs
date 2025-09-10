using Microsoft.Playwright;

namespace Web.E2ETests.Hooks;

[Binding]
public class InstallChromiumHook
{
    [BeforeTestRun]
    public static void BeforeTestRun()
    {
        var exitCode = Program.Main(["install", "chromium"]);
        if (exitCode != 0)
        {
            throw new Exception($"Playwright exited with code {exitCode}");
        }
    }
}