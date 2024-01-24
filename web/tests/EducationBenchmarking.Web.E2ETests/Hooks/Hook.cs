using Microsoft.Playwright;

namespace EducationBenchmarking.Web.E2ETests.Hooks;

[Binding]
public class Hooks
{
    [BeforeTestRun]
    public static void InstallBrowsers()
    {
        var exitCode = Program.Main(["install", "chromium"]);
        if (exitCode != 0)
        {
            throw new Exception($"Playwright exited with code {exitCode}");
        }
    }
}