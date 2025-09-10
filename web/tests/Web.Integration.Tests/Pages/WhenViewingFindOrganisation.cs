using AngleSharp.Dom;
using Xunit;

namespace Web.Integration.Tests.Pages;

public class WhenViewingFindOrganisation(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    [Fact]
    public async Task CanDisplay()
    {
        var page = await Client.Navigate(Paths.FindOrganisation);

        DocumentAssert.AssertPageUrl(page, Paths.FindOrganisation.ToAbsolute());
        DocumentAssert.TitleAndH1(page, "Find a school, trust or local authority - Financial Benchmarking and Insights Tool - GOV.UK", "What are you looking for?");
    }

    [Theory]
    [InlineData("school", Paths.SchoolSearch)]
    [InlineData("trust", Paths.TrustSearch)]
    [InlineData("local-authority", Paths.LocalAuthoritySearch)]
    public async Task CanChooseOrganisationType(string type, string expectedPath)
    {
        var page = await Client.Navigate(Paths.FindOrganisation);
        var action = page.QuerySelector("button[type='submit']");
        Assert.NotNull(action);

        page = await Client.SubmitForm(page.Forms[0], action, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                { "FindMethod", type }
            });
        });

        DocumentAssert.AssertPageUrl(page, expectedPath.ToAbsolute());
    }

    [Fact]
    public async Task CanDisplayErrorIfTypeChoiceNotMade()
    {
        var page = await Client.Navigate(Paths.FindOrganisation);
        var action = page.QuerySelector("button[type='submit']");
        Assert.NotNull(action);

        page = await Client.SubmitForm(page.Forms[0], action);

        Assert.Equal("Error: Select the type of organisation to search for", page.QuerySelector("#FindMethod-error")?.GetInnerText());
    }
}