using Xunit;

namespace Web.Integration.Tests.Pages;

public class WhenViewingLandingPage(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    [Fact]
    public async Task CanDisplay()
    {
        var page = await Client.Navigate(Paths.ServiceHome);

        DocumentAssert.AssertPageUrl(page, Paths.ServiceHome.ToAbsolute());
        DocumentAssert.TitleAndH1(page, "Home - Financial Benchmarking and Insights Tool - GOV.UK", "Financial Benchmarking and Insights Tool");

        var startButton = page.GetElementsByClassName("govuk-button--start").FirstOrDefault();
        Assert.NotNull(startButton);
        DocumentAssert.PrimaryCta(startButton, "Start now", Paths.FindOrganisation);
    }

    [Fact]
    public async Task CanNavigateToFindOrganisation()
    {
        var page = await Client.Navigate(Paths.ServiceHome);
        var startButton = page.GetElementsByClassName("govuk-button--start").FirstOrDefault();
        Assert.NotNull(startButton);

        page = await Client.Follow(startButton);

        DocumentAssert.AssertPageUrl(page, Paths.FindOrganisation.ToAbsolute());
    }
}