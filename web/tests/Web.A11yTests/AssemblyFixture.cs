using Xunit.Abstractions;
using Xunit.Sdk;

[assembly: Xunit.TestFramework("Web.A11yTests.AssemblyFixture", "Web.A11yTests")]

namespace Web.A11yTests;

public sealed class AssemblyFixture : XunitTestFramework, IDisposable
{
    public AssemblyFixture(IMessageSink messageSink) : base(messageSink)
    {
        var exitCode = Microsoft.Playwright.Program.Main(["install", "--with-deps", "chromium"]);
        if (exitCode != 0)
        {
            throw new Exception($"Playwright exited with code {exitCode}");
        }
    }

    public new void Dispose()
    {
        base.Dispose();
    }
}