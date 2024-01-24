using Microsoft.Playwright;

namespace EducationBenchmarking.Web.A11yTests.Hooks;

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