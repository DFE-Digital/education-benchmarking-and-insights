using Xunit;

namespace Web.Integration.Tests.Pages;

public class WhenViewingCookies(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    [Fact]
    public async Task CanDisplay()
    {
        var page = await Client.Navigate(Paths.Cookies);

        DocumentAssert.AssertPageUrl(page, Paths.Cookies.ToAbsolute());
        DocumentAssert.BackLink(page, "Back", $"{Paths.Cookies.ToAbsolute()}#");
        DocumentAssert.TitleAndH1(page, "Financial Benchmarking and Insights Tool - GOV.UK", "Cookies");
    }
}