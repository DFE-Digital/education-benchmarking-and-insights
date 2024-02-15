using Xunit;

namespace EducationBenchmarking.Web.Integration.Tests.Pages;

public class WhenViewingCommercialResources(BenchmarkingWebAppClient client) : PageBase(client)
{
    [Fact]
    public async Task CanDisplay()
    {
        var page = await Client.Navigate(Paths.CommercialResources);

        var expectedBreadcrumbs = new[] { ("Home", Paths.ServiceHome.ToAbsolute()) };

        DocumentAssert.AssertPageUrl(page, Paths.CommercialResources.ToAbsolute());
        DocumentAssert.Breadcrumbs(page, expectedBreadcrumbs);
        DocumentAssert.TitleAndH1(page, "", "Find commercial resources");
    }
}