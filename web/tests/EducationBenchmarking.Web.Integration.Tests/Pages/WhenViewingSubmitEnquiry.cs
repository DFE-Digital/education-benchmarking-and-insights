using Xunit;

namespace EducationBenchmarking.Web.Integration.Tests.Pages;

public class WhenViewingSubmitEnquiry(BenchmarkingWebAppClient client) : PageBase(client)
{
    [Fact]
    public async Task CanDisplay()
    {
        var page = await Client.Navigate(Paths.SubmitEnquiry);

        var expectedBreadcrumbs = new[] { ("Home", Paths.ServiceHome.ToAbsolute()) };

        DocumentAssert.AssertPageUrl(page, Paths.SubmitEnquiry.ToAbsolute());
        DocumentAssert.Breadcrumbs(page, expectedBreadcrumbs);
        DocumentAssert.TitleAndH1(page, "Education benchmarking and insights - GOV.UK", "Submit an enquiry");
    }
}