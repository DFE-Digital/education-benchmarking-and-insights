using Xunit;

namespace EducationBenchmarking.Web.Integration.Tests.Pages;

public class WhenViewingServiceHelp(BenchmarkingWebAppClient client) : PageBase(client)
{
    [Fact]
    public async Task CanDisplay()
    {
        var page = await Client.Navigate(Paths.ServiceHelp);

        var expectedBreadcrumbs = new[] { ("Home", Paths.ServiceHome.ToAbsolute()) };

        DocumentAssert.AssertPageUrl(page, Paths.ServiceHelp.ToAbsolute());
        DocumentAssert.Breadcrumbs(page, expectedBreadcrumbs);
        DocumentAssert.TitleAndH1(page, "", "Help with this service");
    }
}