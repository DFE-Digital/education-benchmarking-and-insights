using Xunit;

namespace Web.Integration.Tests.Pages;

public class WhenViewingAccessibility(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    [Fact]
    public async Task CanDisplay()
    {
        var page = await Client.Navigate(Paths.Accessibility);

        DocumentAssert.AssertPageUrl(page, Paths.Accessibility.ToAbsolute());
        DocumentAssert.BackLink(page, "Back", $"{Paths.Accessibility.ToAbsolute()}#");
        DocumentAssert.TitleAndH1(page, "Financial Benchmarking and Insights Tool - GOV.UK", "Accessibility");
    }
}