using Xunit;

namespace Web.Integration.Tests.Pages;

public class WhenViewingPrivacy(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    [Fact]
    public async Task CanDisplay()
    {
        var page = await Client.Navigate(Paths.Privacy);

        DocumentAssert.AssertPageUrl(page, Paths.Privacy.ToAbsolute());
        DocumentAssert.BackLink(page, "Back", $"{Paths.Privacy.ToAbsolute()}#");
        DocumentAssert.TitleAndH1(page, "Financial Benchmarking and Insights Tool - GOV.UK", "Privacy");
    }
}