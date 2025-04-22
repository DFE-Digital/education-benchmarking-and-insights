using System.Net;
using AngleSharp.Dom;
using Xunit;

namespace Web.Integration.Tests.Pages.Trusts.Search;

public class WhenViewingTrustSearch(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
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
            .Navigate(Paths.TrustSearch);
        var action = page.QuerySelectorAll("button[type='submit']").First();
        Assert.NotNull(action);

        const string term = nameof(term);
        page = await Client.SubmitForm(page.Forms.First(), action, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                {
                    "Term", term
                }
            });
        });

        DocumentAssert.AssertPageUrl(page, Paths.TrustSearchResults(term).ToAbsolute(), HttpStatusCode.NotFound);
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