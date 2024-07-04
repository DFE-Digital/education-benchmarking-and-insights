using Xunit;

namespace Web.Integration.Tests.Pages;

public class WhenViewingContact(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    [Fact]
    public async Task CanDisplay()
    {
        var page = await Client.Navigate(Paths.Contact);

        DocumentAssert.AssertPageUrl(page, Paths.Contact.ToAbsolute());
        DocumentAssert.BackLink(page, "Back", $"{Paths.Contact.ToAbsolute()}#");
        DocumentAssert.TitleAndH1(page, "Financial Benchmarking and Insights Tool - GOV.UK", "Contact us");
    }
}