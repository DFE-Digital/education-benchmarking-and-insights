using System.Net;
using AngleSharp.Dom;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Xunit;

namespace Web.Integration.Tests.Pages.Trusts.Search;

public class WhenViewingTrustSearchResults(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
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
                CompanyNumber = "123456",
                TrustName = "Trust Name 1"
            },
            new TrustSummary
            {
                CompanyNumber = "654321",
                TrustName = "Trust Name 2"
            }
        ]
    };

    [Theory]
    [InlineData(null)]
    [InlineData("term")]
    public async Task CanDisplay(string? term)
    {
        var page = await Client
            .SetupEstablishment(SearchResults)
            .Navigate(Paths.TrustSearchResults(term));

        DocumentAssert.AssertPageUrl(page, Paths.TrustSearchResults(term).ToAbsolute());
        DocumentAssert.TitleAndH1(page, "Search for a trust - Financial Benchmarking and Insights Tool - GOV.UK", "Search for a trust");
        DocumentAssert.Input(page, "Term", term);
    }

    [Fact]
    public async Task CanSubmitSearch()
    {
        var page = await Client
            .SetupEstablishment(SearchResults)
            .Navigate(Paths.TrustSearchResults());
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

        DocumentAssert.AssertPageUrl(page, Paths.TrustSearchResults(term).ToAbsolute());
    }

    [Fact]
    public async Task CanFilterSearch()
    {
        var page = await Client
            .SetupEstablishment(SearchResults)
            .Navigate(Paths.TrustSearchResults());
        var action = page.QuerySelectorAll("button[type='submit']").Last();
        Assert.NotNull(action);

        const string term = nameof(term);
        const string orderBy = nameof(orderBy);
        page = await Client.SubmitForm(page.Forms.Last(), action, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                {
                    "Term", term
                },
                {
                    "OrderBy", orderBy
                }
            });
        });

        DocumentAssert.AssertPageUrl(page, Paths.TrustSearchResults(term, orderBy).ToAbsolute());
    }

    [Fact]
    public async Task CanClearFilteredSearch()
    {
        const string term = nameof(term);
        const string orderBy = nameof(orderBy);
        var page = await Client
            .SetupEstablishment(SearchResults)
            .Navigate(Paths.TrustSearchResults(term, orderBy));
        var action = page.QuerySelector("a.govuk-link:contains('Clear filters')");
        Assert.NotNull(action);

        page = await Client.Follow(action);

        DocumentAssert.AssertPageUrl(page, Paths.TrustSearchResults(term).ToAbsolute());
    }

    [Fact]
    public async Task CanPaginateSearch()
    {
        const string term = nameof(term);
        const string orderBy = nameof(orderBy);
        var page = await Client
            .SetupEstablishment(SearchResults)
            .Navigate(Paths.TrustSearchResults(term, orderBy));

        var pagination = page.QuerySelectorAll("a.govuk-pagination__link");
        Assert.NotNull(pagination);

        page = await Client.Follow(pagination.ElementAt(0));
        DocumentAssert.AssertPageUrl(page, Paths.TrustSearchResults(term, orderBy, 1).ToAbsolute());

        page = await Client.Follow(pagination.ElementAt(1));
        DocumentAssert.AssertPageUrl(page, Paths.TrustSearchResults(term, orderBy, 2).ToAbsolute());
    }

    [Fact]
    public async Task CanSelectSearchResult()
    {
        const string companyNumber = "123456";
        var page = await Client
            .SetupEstablishment(SearchResults)
            .Navigate(Paths.TrustSearchResults());
        var results = page.QuerySelectorAll("ul.govuk-list--result > li > a");
        Assert.NotNull(results);

        page = await Client.Follow(results.ElementAt(0));
        DocumentAssert.AssertPageUrl(page, Paths.TrustHome(companyNumber).ToAbsolute(), HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task CanDisplayErrorIfMissingSearchTerm()
    {
        var page = await Client.Navigate(Paths.TrustSearchResults());
        var action = page.QuerySelectorAll("button[type='submit']").First();
        Assert.NotNull(action);

        page = await Client.SubmitForm(page.Forms.First(), action);

        Assert.Equal("Error: Enter a trust name or company number to start a search", page.QuerySelector("#Term-error")?.GetInnerText());
    }

    [Fact]
    public async Task CanDisplayWarningIfNoResultsFound()
    {
        var page = await Client
            .SetupEstablishment(new SearchResponse<TrustSummary>())
            .Navigate(Paths.TrustSearchResults());
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

        Assert.Equal("We couldn't find any trusts matching your search criteria.", page.QuerySelector("#search-warning")?.GetInnerText());
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string term = nameof(term);
        var page = await Client
            .SetupEstablishmentWithException()
            .Navigate(Paths.TrustSearchResults(term));

        PageAssert.IsProblemPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.TrustSearchResults(term).ToAbsolute(), HttpStatusCode.InternalServerError);
    }
}