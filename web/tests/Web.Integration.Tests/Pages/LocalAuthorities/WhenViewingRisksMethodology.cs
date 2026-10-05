using Xunit;

namespace Web.Integration.Tests.Pages.LocalAuthorities;

public class WhenViewingRisksMethodology(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    [Fact]
    public async Task CanDisplay()
    {
        const string code = "123";
        var page = await Client.Navigate(Paths.LocalAuthorityRisksMethodology(code));

        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityRisksMethodology(code).ToAbsolute());
        DocumentAssert.TitleAndH1(page, "Risk score methodology - Financial Benchmarking and Insights Tool - GOV.UK", "Risk score methodology");
    }
}
