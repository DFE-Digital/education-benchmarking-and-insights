using System.Net;
using AngleSharp.Dom;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Xunit;

namespace Web.Integration.Tests.Pages.Schools.Search;

public class WhenViewingSchoolSearchResults(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    private static SearchResponse<School> SearchResults => new()
    {
        Facets = new Dictionary<string, IList<FacetValueResponseModel>>
        {
            {
                "OverallPhase", new List<FacetValueResponseModel>
                {
                    new()
                    {
                        Value = "Primary",
                        Count = 1
                    },
                    new()
                    {
                        Value = "Secondary",
                        Count = 2
                    }
                }
            }
        },
        TotalResults = 54,
        Page = 1,
        PageSize = 10,
        PageCount = 2,
        Results =
        [
            new School
            {
                URN = "123456",
                SchoolName = "School Name 1",
                AddressStreet = "Street",
                AddressTown = "Town",
                AddressPostcode = "Postcode"
            },
            new School
            {
                URN = "654321",
                SchoolName = "School Name 2",
                AddressStreet = "Street",
                AddressTown = "Town",
                AddressPostcode = "Postcode"
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
            .Navigate(Paths.SchoolSearchResults(term));

        DocumentAssert.AssertPageUrl(page, Paths.SchoolSearchResults(term).ToAbsolute());
        DocumentAssert.TitleAndH1(page, "Search for a school or academy - Financial Benchmarking and Insights Tool - GOV.UK", "Search for a school or academy");
        DocumentAssert.Input(page, "Term", term);
    }

    [Fact]
    public async Task CanSubmitSearch()
    {
        var page = await Client
            .SetupEstablishment(SearchResults)
            .Navigate(Paths.SchoolSearchResults());
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

        DocumentAssert.AssertPageUrl(page, Paths.SchoolSearchResults(term).ToAbsolute());
    }

    [Fact]
    public async Task CanFilterSearch()
    {
        var page = await Client
            .SetupEstablishment(SearchResults)
            .Navigate(Paths.SchoolSearchResults());
        var action = page.QuerySelectorAll("button[type='submit']").Last();
        Assert.NotNull(action);

        const string term = nameof(term);
        const string orderBy = nameof(orderBy);
        const string overallPhase = nameof(overallPhase);
        page = await Client.SubmitForm(page.Forms.Last(), action, f =>
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

        DocumentAssert.AssertPageUrl(page, Paths.SchoolSearchResults(term, orderBy, [overallPhase]).ToAbsolute());
    }

    [Fact]
    public async Task CanPaginateSearch()
    {
        const string term = nameof(term);
        const string orderBy = nameof(orderBy);
        const string overallPhase = nameof(overallPhase);
        var page = await Client
            .SetupEstablishment(SearchResults)
            .Navigate(Paths.SchoolSearchResults(term, orderBy, [overallPhase]));

        var pagination = page.QuerySelectorAll("a.govuk-pagination__link");
        Assert.NotNull(pagination);

        page = await Client.Follow(pagination.ElementAt(0));
        DocumentAssert.AssertPageUrl(page, Paths.SchoolSearchResults(term, orderBy, [overallPhase], 1).ToAbsolute());

        page = await Client.Follow(pagination.ElementAt(1));
        DocumentAssert.AssertPageUrl(page, Paths.SchoolSearchResults(term, orderBy, [overallPhase], 2).ToAbsolute());
    }

    [Fact]
    public async Task CanSelectSearchResult()
    {
        const string urn = "123456";
        var page = await Client
            .SetupEstablishment(SearchResults)
            .Navigate(Paths.SchoolSearchResults());
        var results = page.QuerySelectorAll("ul.govuk-list--result > li > a");
        Assert.NotNull(results);

        page = await Client.Follow(results.ElementAt(0));
        DocumentAssert.AssertPageUrl(page, Paths.SchoolHome(urn).ToAbsolute(), HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task CanDisplayErrorIfMissingSearchTerm()
    {
        var page = await Client.Navigate(Paths.SchoolSearchResults());
        var action = page.QuerySelector("button[type='submit']");
        Assert.NotNull(action);

        page = await Client.SubmitForm(page.Forms[0], action);

        Assert.Equal("Error: Enter a school name or URN to start a search", page.QuerySelector("#Term-error")?.GetInnerText());
    }
}