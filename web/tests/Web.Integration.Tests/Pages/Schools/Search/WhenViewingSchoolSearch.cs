using AngleSharp.Dom;
using Xunit;

namespace Web.Integration.Tests.Pages.Schools.Search;

public class WhenViewingSchoolSearch(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    [Fact]
    public async Task CanDisplay()
    {
        var page = await Client.Navigate(Paths.SchoolSearch);

        DocumentAssert.AssertPageUrl(page, Paths.SchoolSearch.ToAbsolute());
        DocumentAssert.TitleAndH1(page, "Search for a school or academy - Financial Benchmarking and Insights Tool - GOV.UK", "Search for a school or academy");
    }

    [Fact]
    public async Task CanSubmitSearch()
    {
        var page = await Client.Navigate(Paths.SchoolSearch);
        var action = page.QuerySelector("button[type='submit']");
        Assert.NotNull(action);

        const string term = nameof(term);
        page = await Client.SubmitForm(page.Forms[0], action, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                {
                    "Term", term
                }
            });
        });

        DocumentAssert.AssertPageUrl(page, Paths.SchoolSearchResults(term).ToAbsolute());
    }

    [Fact]
    public async Task CanDisplayErrorIfMissingSearchTerm()
    {
        var page = await Client.Navigate(Paths.SchoolSearch);
        var action = page.QuerySelector("button[type='submit']");
        Assert.NotNull(action);

        page = await Client.SubmitForm(page.Forms[0], action);

        Assert.Equal("Error: Enter a school name or URN to start a search", page.QuerySelector("#Term-error")?.GetInnerText());
    }
}