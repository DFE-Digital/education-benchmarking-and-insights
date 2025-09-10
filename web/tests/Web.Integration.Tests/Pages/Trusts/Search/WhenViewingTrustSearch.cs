using AngleSharp.Dom;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Xunit;

namespace Web.Integration.Tests.Pages.Trusts.Search;

public class WhenViewingTrustSearch(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    private static SearchResponse<TrustSummary> SearchResults => new()
    {
        TotalResults = 54,
        Page = 1,
        PageSize = 10,
        PageCount = 2,
        Results =
        [
            new TrustSummary
            {
                CompanyNumber = "12345678",
                TrustName = "Trust Name 1"
            },
            new TrustSummary
            {
                CompanyNumber = "87654321",
                TrustName = "Trust Name 2"
            }
        ]
    };

    [Fact]
    public async Task CanDisplay()
    {
        var page = await Client.Navigate(Paths.TrustSearch);

        DocumentAssert.AssertPageUrl(page, Paths.TrustSearch.ToAbsolute());
        DocumentAssert.TitleAndH1(page, "Search for a trust - Financial Benchmarking and Insights Tool - GOV.UK", "Search for a trust");
    }

    [Fact]
    public async Task CanSubmitSearch()
    {
        var page = await Client
            .SetupEstablishment(SearchResults)
            .Navigate(Paths.TrustSearch);
        var action = page.QuerySelectorAll("button[type='submit']").First();
        Assert.NotNull(action);

        const string term = nameof(term);
        page = await Client.SubmitForm(page.Forms.First(), action, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                { "Term", term }
            });
        });

        DocumentAssert.AssertPageUrl(page, Paths.TrustSearchResults(term).ToAbsolute());
    }

    [Fact]
    public async Task CanDisplayErrorIfMissingSearchTerm()
    {
        var page = await Client.Navigate(Paths.TrustSearch);
        var action = page.QuerySelector("button[type='submit']");
        Assert.NotNull(action);

        page = await Client.SubmitForm(page.Forms[0], action);

        Assert.Equal("Error: Enter a trust name or company number to start a search", page.QuerySelector("#Term-error")?.GetInnerText());
    }
}