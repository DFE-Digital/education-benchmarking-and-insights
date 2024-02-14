using Xunit;

namespace EducationBenchmarking.Web.Integration.Tests.Pages;

public class WhenViewingLandingPage(BenchmarkingWebAppClient client) : PageBase(client)
{
    [Fact]
    public async Task CanDisplay()
    {
        var page = await Client.Navigate(Paths.ServiceHome);

        var expectedBreadcrumbs = new[] { ("Home", Paths.ServiceHome.ToAbsolute()) };

        DocumentAssert.AssertPageUrl(page, Paths.ServiceHome.ToAbsolute());
        DocumentAssert.Breadcrumbs(page, expectedBreadcrumbs);
        DocumentAssert.TitleAndH1(page, "Home - Education benchmarking and insights - GOV.UK", "Education benchmarking and insights");

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