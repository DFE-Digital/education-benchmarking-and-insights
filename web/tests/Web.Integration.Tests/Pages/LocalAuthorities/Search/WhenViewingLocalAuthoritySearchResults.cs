using System.Net;
using AngleSharp.Dom;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Xunit;

namespace Web.Integration.Tests.Pages.LocalAuthorities.Search;

public class WhenViewingLocalAuthoritySearchResults(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
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

    [Theory]
    [InlineData(null)]
    [InlineData("term")]
    public async Task CanDisplay(string? term)
    {
        var page = await Client
            .SetupEstablishment(SearchResults)
            .Navigate(Paths.LocalAuthoritySearchResults(term));

        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthoritySearchResults(term).ToAbsolute());
        DocumentAssert.TitleAndH1(page, "Search for a local authority - Financial Benchmarking and Insights Tool - GOV.UK", "Search for a local authority");
        DocumentAssert.Input(page, "Term", term);
    }

    [Fact]
    public async Task CanSubmitSearch()
    {
        var page = await Client
            .SetupEstablishment(SearchResults)
            .Navigate(Paths.LocalAuthoritySearchResults());
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

        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthoritySearchResults(term).ToAbsolute());
    }

    [Fact]
    public async Task CanFilterSearch()
    {
        var page = await Client
            .SetupEstablishment(SearchResults)
            .Navigate(Paths.LocalAuthoritySearchResults());
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

        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthoritySearchResults(term, orderBy).ToAbsolute());
    }

    [Fact]
    public async Task CanClearFilteredSearch()
    {
        const string term = nameof(term);
        const string orderBy = nameof(orderBy);
        var page = await Client
            .SetupEstablishment(SearchResults)
            .Navigate(Paths.LocalAuthoritySearchResults(term, orderBy));
        var action = page.QuerySelector("a.govuk-link:contains('Clear filters')");
        Assert.NotNull(action);

        page = await Client.Follow(action);

        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthoritySearchResults(term).ToAbsolute());
    }

    [Fact]
    public async Task CanPaginateSearch()
    {
        const string term = nameof(term);
        const string orderBy = nameof(orderBy);
        var page = await Client
            .SetupEstablishment(SearchResults)
            .Navigate(Paths.LocalAuthoritySearchResults(term, orderBy));

        var pagination = page.QuerySelectorAll("a.govuk-pagination__link");
        Assert.NotNull(pagination);

        page = await Client.Follow(pagination.ElementAt(0));
        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthoritySearchResults(term, orderBy, 1).ToAbsolute());

        page = await Client.Follow(pagination.ElementAt(1));
        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthoritySearchResults(term, orderBy, 2).ToAbsolute());
    }

    [Fact]
    public async Task CanSelectSearchResult()
    {
        const string code = "123456";
        var page = await Client
            .SetupEstablishment(SearchResults)
            .Navigate(Paths.LocalAuthoritySearchResults());
        var results = page.QuerySelectorAll("ul.govuk-list--result > li > a");
        Assert.NotNull(results);

        page = await Client.Follow(results.ElementAt(0));
        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityHome(code).ToAbsolute(), HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task CanDisplayErrorIfMissingSearchTerm()
    {
        var page = await Client.Navigate(Paths.LocalAuthoritySearchResults());
        var action = page.QuerySelectorAll("button[type='submit']").First();
        Assert.NotNull(action);

        page = await Client.SubmitForm(page.Forms.First(), action);

        Assert.Equal("Error: Enter a local authority name or code to start a search", page.QuerySelector("#Term-error")?.GetInnerText());
    }

    [Fact]
    public async Task CanDisplayWarningIfNoResultsFound()
    {
        var page = await Client
            .SetupEstablishment(new SearchResponse<LocalAuthoritySummary>())
            .Navigate(Paths.LocalAuthoritySearchResults());
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

        Assert.Equal("We couldn't find any local authorities matching your search criteria.", page.QuerySelector("#search-warning")?.GetInnerText());
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string term = nameof(term);
        var page = await Client
            .SetupEstablishmentWithException()
            .Navigate(Paths.LocalAuthoritySearchResults(term));

        PageAssert.IsProblemPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthoritySearchResults(term).ToAbsolute(), HttpStatusCode.InternalServerError);
    }
}