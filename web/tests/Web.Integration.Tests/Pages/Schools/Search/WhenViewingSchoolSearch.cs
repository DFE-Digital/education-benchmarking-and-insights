using System.Net;
using AngleSharp.Dom;
using Xunit;

namespace Web.Integration.Tests.Pages.Schools.Search;

public class WhenViewingSchoolSearch(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    [Theory]
    [InlineData(null)]
    [InlineData("term")]
    public async Task CanDisplay(string? term)
    {
        var page = await Client.Navigate(Paths.SchoolSearch(term));

        DocumentAssert.AssertPageUrl(page, Paths.SchoolSearch(term).ToAbsolute());
        DocumentAssert.TitleAndH1(page, "Search for a school or academy - Financial Benchmarking and Insights Tool - GOV.UK", "Search for a school or academy");
        DocumentAssert.Input(page, "Term", term);
    }

    [Fact]
    public async Task CanSubmitSearch()
    {
        var page = await Client.Navigate(Paths.SchoolSearch());
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

        DocumentAssert.AssertPageUrl(page, Paths.SchoolSearch(term).ToAbsolute());
    }

    [Fact]
    public async Task CanFilterSearch()
    {
        var page = await Client.Navigate(Paths.SchoolSearch());
        var action = page.QuerySelector("button[type='submit']");
        Assert.NotNull(action);

        const string term = nameof(term);
        const string orderBy = nameof(orderBy);
        const string overallPhase = nameof(overallPhase);
        page = await Client.SubmitForm(page.Forms[0], action, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                {
                    "Term", term
                },
                {
                    "OrderBy", orderBy
                },
                {
                    "OverallPhase", overallPhase
                }
            });
        });

        DocumentAssert.AssertPageUrl(page, Paths.SchoolSearch(term, null, orderBy, overallPhase).ToAbsolute());
    }

    [Fact]
    public async Task CanClearFilterSearch()
    {
        const string term = nameof(term);
        const string orderBy = nameof(orderBy);
        const string overallPhase = nameof(overallPhase);
        var page = await Client.Navigate(Paths.SchoolSearch(term, null, orderBy, overallPhase));
        var action = page.QuerySelector("button[name='action'][value='reset']");
        Assert.NotNull(action);

        page = await Client.SubmitForm(page.Forms[0], action, _ => { });
        DocumentAssert.AssertPageUrl(page, Paths.SchoolSearch(term).ToAbsolute());
    }

    [Fact]
    public async Task CanPaginateSearch()
    {
        const string term = nameof(term);
        const string orderBy = nameof(orderBy);
        const string overallPhase = nameof(overallPhase);
        var page = await Client.Navigate(Paths.SchoolSearch(term, null, orderBy, overallPhase));

        var pagination = page.QuerySelectorAll("a.govuk-pagination__link");
        Assert.NotNull(pagination);

        page = await Client.Follow(pagination.ElementAt(0));
        DocumentAssert.AssertPageUrl(page, Paths.SchoolSearch(term, null, orderBy, overallPhase, 1).ToAbsolute());

        page = await Client.Follow(pagination.ElementAt(1));
        DocumentAssert.AssertPageUrl(page, Paths.SchoolSearch(term, null, orderBy, overallPhase, 2).ToAbsolute());
    }

    [Fact]
    public async Task CanSelectSearchResult()
    {
        const string urn = "123456";
        var page = await Client
            .SetupEstablishmentWithNotFound()
            .Navigate(Paths.SchoolSearch());
        var results = page.QuerySelectorAll("ul.govuk-list--result > li > a");
        Assert.NotNull(results);

        page = await Client.Follow(results.ElementAt(0));
        DocumentAssert.AssertPageUrl(page, Paths.SchoolHome(urn).ToAbsolute(), HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanDisplayErrorIfMissingSearchTerm()
    {
        var page = await Client.Navigate(Paths.SchoolSearch());
        var action = page.QuerySelector("button[type='submit']");
        Assert.NotNull(action);

        page = await Client.SubmitForm(page.Forms[0], action);

        Assert.Equal("Error: Enter a school name or URN to start a search", page.QuerySelector("#Term-error")?.GetInnerText());
    }
}