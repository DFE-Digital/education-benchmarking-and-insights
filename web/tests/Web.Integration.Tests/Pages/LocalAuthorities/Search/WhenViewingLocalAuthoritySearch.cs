using System.Net;
using AngleSharp.Dom;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Xunit;

namespace Web.Integration.Tests.Pages.LocalAuthorities.Search;

public class WhenViewingLocalAuthoritySearch(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    private static SearchResponse<LocalAuthoritySummary> SearchResults => new()
    {
        TotalResults = 54,
        Page = 1,
        PageSize = 10,
        PageCount = 2,
        Results =
        [
            new LocalAuthoritySummary
            {
                Code = "123456",
                Name = "LA Name 1"
            },
            new LocalAuthoritySummary
            {
                Code = "654321",
                Name = "LA Name 2"
            }
        ]
    };

    [Fact]
    public async Task CanDisplay()
    {
        var page = await Client.Navigate(Paths.LocalAuthoritySearch);

        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthoritySearch.ToAbsolute());
        DocumentAssert.TitleAndH1(page, "Search for a local authority - Financial Benchmarking and Insights Tool - GOV.UK", "Search for a local authority");
    }

    [Fact]
    public async Task CanSubmitSearch()
    {
        var page = await Client
            .SetupEstablishment(SearchResults)
            .Navigate(Paths.LocalAuthoritySearch);
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

        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthoritySearchResults(term).ToAbsolute(), HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanDisplayErrorIfMissingSearchTerm()
    {
        var page = await Client.Navigate(Paths.LocalAuthoritySearch);
        var action = page.QuerySelector("button[type='submit']");
        Assert.NotNull(action);

        page = await Client.SubmitForm(page.Forms[0], action);

        Assert.Equal("Error: Enter a local authority name or code to start a search", page.QuerySelector("#Term-error")?.GetInnerText());
    }
}