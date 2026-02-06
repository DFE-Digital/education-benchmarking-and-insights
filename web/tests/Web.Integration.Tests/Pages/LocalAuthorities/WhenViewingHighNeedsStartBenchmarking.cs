using System.Net;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App.Domain;
using Xunit;

namespace Web.Integration.Tests.Pages.LocalAuthorities;

public class WhenViewingHighNeedsStartBenchmarking(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    #region Tests

    [Fact]
    public async Task CanDisplay()
    {
        var (page, authority, _, otherAuthorities) = await SetupNavigateInitPage();

        AssertPageLayout(page, authority, otherAuthorities);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string code = "123";
        var page = await Client.SetupEstablishmentWithException()
            .Navigate(Paths.LocalAuthorityHighNeedsBenchmarking(code));

        PageAssert.IsProblemPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityHighNeedsBenchmarking(code).ToAbsolute(), HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task CanDisplayNotFound()
    {
        const string code = "123";
        var page = await Client.SetupEstablishmentWithNotFound()
            .Navigate(Paths.LocalAuthorityHighNeedsBenchmarking(code));

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityHighNeedsBenchmarking(code).ToAbsolute(), HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanAddOtherComparators()
    {
        var (page, _, neighbourAuthorities, otherAuthorities) = await SetupNavigateInitPage();
        var code = otherAuthorities.First().Code!;

        var neighboursSelected = page.GetElementAndAssert("#current-comparators-neighbours", Assert.NotNull);
        var neighbourCards = neighboursSelected.QuerySelectorAll(".app-removable-card");
        Assert.Equal(neighbourAuthorities.Length, neighbourCards.Length);

        page.GetElementAndAssert("#current-comparators-others", Assert.Null);

        var addButton = page.GetElementAndAssert("button[name='action'][value='add']", Assert.NotNull);

        page = await Client.SubmitForm(page.Forms[0], addButton, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                { "LaInput", code }
            });
        });

        neighboursSelected = page.GetElementAndAssert("#current-comparators-neighbours", Assert.NotNull);
        neighbourCards = neighboursSelected.QuerySelectorAll(".app-removable-card");
        Assert.Equal(neighbourAuthorities.Length, neighbourCards.Length);

        var othersSelected = page.GetElementAndAssert("#current-comparators-others", Assert.NotNull);
        var otherCards = othersSelected.QuerySelectorAll(".app-removable-card");
        Assert.Equal(1, otherCards.Length);

        var comparatorSelector = page.GetElementAndAssert("#LaInput", Assert.NotNull);
        var options = comparatorSelector.QuerySelectorAll("option").Select(q => q.TextContent).ToArray();
        var expectedOptions = new[]
        {
            "Choose local authority"
        }.Concat(otherAuthorities
            .Except([otherAuthorities.First()])
            .OrderBy(n => n.Name)
            .Select(n => n.Name)
            .ToArray());

        Assert.Equal(expectedOptions, options);
    }

    [Fact]
    public async Task CannotAddTwentiethComparator()
    {
        var selectedLAs = new List<string>();
        for (var x = 0; x < 19; x++)
        {
            // the LAs w/code 209+ are the otherAuthorities
            selectedLAs.Add((x + 209).ToString());
        }

        var (page, _, neighbourAuthorities, otherAuthorities) = await SetupNavigateInitPage(selectedLAs.ToArray());
        var addLACode = neighbourAuthorities.First().Code!; // make sure we try to add an LA that isn't already selected

        var addButton = page.GetElementAndAssert("button[name='action'][value='add']", Assert.NotNull);

        page = await Client.SubmitForm(page.Forms[0], addButton, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                { "LaInput", addLACode },
                { "Selected", selectedLAs.ToString()! }
            });
        });

        var errorSummary = page.GetElementAndAssert(".govuk-error-summary", Assert.NotNull);
        Assert.Equal("There is a problem\nSelect up to 19 comparator local authorities", errorSummary.GetInnerText().Trim());
    }

    [Fact]
    public async Task CanRemoveOtherComparators()
    {
        var (page, _, neighbourAuthorities, otherAuthorities) = await SetupNavigateInitPage();
        var code = otherAuthorities.First().Code!;

        var addButton = page.GetElementAndAssert("button[name='action'][value='add']", Assert.NotNull);

        page.GetElementAndAssert("#current-comparators-others", Assert.Null);

        page = await Client.SubmitForm(page.Forms[0], addButton, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                { "LaInput", code }
            });
        });

        var othersSelected = page.GetElementAndAssert("#current-comparators-others", Assert.NotNull);
        var othersCards = othersSelected.QuerySelectorAll(".app-removable-card");
        Assert.Equal(1, othersCards.Length);

        var removeButton = page.GetElementAndAssert($"button[name='action'][value='remove-{code}']", Assert.NotNull);

        page = await Client.SubmitForm(page.Forms[0], removeButton, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                { "LaInput", code }
            });
        });

        var neighboursSelected = page.GetElementAndAssert("#current-comparators-neighbours", Assert.NotNull);

        var neighboursCards = neighboursSelected.QuerySelectorAll(".app-removable-card");
        Assert.Equal(neighbourAuthorities.Length, neighboursCards.Length);
    }

    [Fact]
    public async Task CanRemoveNeighbourComparators()
    {
        var (page, _, neighbourAuthorities, _) = await SetupNavigateInitPage();
        var code = neighbourAuthorities.First().Code!;

        var addButton = page.GetElementAndAssert("button[name='action'][value='add']", Assert.NotNull);

        page = await Client.SubmitForm(page.Forms[0], addButton, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                { "LaInput", code }
            });
        });

        var neighboursSelected = page.GetElementAndAssert("#current-comparators-neighbours", Assert.NotNull);

        var neighboursCards = neighboursSelected.QuerySelectorAll(".app-removable-card");
        Assert.Equal(neighbourAuthorities.Length, neighboursCards.Length);

        var removeButton = page.GetElementAndAssert($"button[name='action'][value='remove-{code}']", Assert.NotNull);

        page = await Client.SubmitForm(page.Forms[0], removeButton, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                { "LaInput", code }
            });
        });

        neighboursSelected = page.GetElementAndAssert("#current-comparators-neighbours", Assert.NotNull);

        neighboursCards = neighboursSelected.QuerySelectorAll(".app-removable-card");
        Assert.Equal(neighbourAuthorities.Length - 1, neighboursCards.Length);
    }

    [Fact]
    public async Task CanDisplayValidationError()
    {
        var (page, _, neighbourAuthorities, _) = await SetupNavigateInitPage();

        // remove each neighbour from the pre-selected LAs
        foreach (var neighbour in neighbourAuthorities)
        {
            var code = neighbour.Code;
            Assert.NotNull(code);
            var removeButton = page.GetElementAndAssert($"button[name='action'][value='remove-{code}']", Assert.NotNull);

            page = await Client.SubmitForm(page.Forms[0], removeButton, f =>
            {
                f.SetFormValues(new Dictionary<string, string>
                {
                    { "LaInput", code }
                });
            });
        }

        var continueButton = page.GetElementAndAssert("button[name='action'][value='continue']", Assert.NotNull);

        page = await Client.SubmitForm(page.Forms[0], continueButton);

        var errorSummary = page.GetElementAndAssert(".govuk-error-summary", Assert.NotNull);
        Assert.Equal("There is a problem\nSelect between 1 and 19 comparator local authorities", errorSummary.GetInnerText().Trim());
    }

    [Fact]
    public async Task CanContinue()
    {
        var (page, authority, _, otherAuthorities) = await SetupNavigateInitPage(["code1"]);
        var code = otherAuthorities.First().Code!;

        var addButton = page.GetElementAndAssert("button[name='action'][value='add']", Assert.NotNull);

        page = await Client.SubmitForm(page.Forms[0], addButton, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                { "LaInput", code }
            });
        });

        var continueButton = page.GetElementAndAssert("button[name='action'][value='continue']", Assert.NotNull);

        page = await Client.SubmitForm(page.Forms[0], continueButton);
        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityHighNeedsBenchmarking(authority.Code).ToAbsolute());
    }

    [Fact]
    public async Task CanDisplayNeighboursPreSelected()
    {
        var (page, authority, neighbourAuthorities, _) = await SetupNavigateInitPage();

        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityHighNeedsStartBenchmarking(authority.Code).ToAbsolute());

        var selectedNeighbours = page.GetElementAndAssert("#current-comparators-neighbours", Assert.NotNull);
        var cards = selectedNeighbours.QuerySelectorAll(".app-removable-card");
        Assert.NotNull(cards);
        Assert.Equal(neighbourAuthorities.Length, cards.Length);

        foreach (var neighbour in neighbourAuthorities)
        {
            var card = cards.SingleOrDefault(r => r.QuerySelector("p")?.TextContent.Trim() == neighbour.Name);
            Assert.NotNull(card);
        }
    }

    [Theory]
    [InlineData(null)]
    [InlineData("benchmarking")]
    public async Task CanNavigateBack(string? referrer)
    {
        var (page, authority, _, _) = await SetupNavigateInitPage(["code1"], referrer);

        var backLink = page.GetElementAndAssert("a.govuk-back-link", Assert.NotNull);

        var newPage = await Client.Follow(backLink);

        var expectedPage = string.IsNullOrEmpty(referrer)
            ? Paths.LocalAuthorityHome(authority.Code).ToAbsolute()
            : Paths.LocalAuthorityHighNeedsBenchmarking(authority.Code).ToAbsolute();

        DocumentAssert.AssertPageUrl(newPage, expectedPage);
    }

    [Fact]
    public async Task CanRemoveAllComparators()
    {
        var (page, authority, _, otherAuthorities) = await SetupNavigateInitPage();

        var code = otherAuthorities.First().Code!;
        var addButton = page.GetElementAndAssert("button[name='action'][value='add']", Assert.NotNull);

        page = await Client.SubmitForm(page.Forms[0], addButton, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                { "LaInput", code }
            });
        });

        var removeAllButton = page.GetElementAndAssert("button[name='action'][value='clear']", Assert.NotNull);

        page = await Client.SubmitForm(page.Forms[0], removeAllButton);

        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityHighNeedsStartBenchmarking(authority.Code).ToAbsolute());

        page.GetElementAndAssert("button[name='action'][value='clear']", Assert.Null);
        page.GetElementAndAssert("#current-comparators-neighbours", Assert.Null);
        page.GetElementAndAssert("#current-comparators-others", Assert.Null);
    }

    [Fact]
    public async Task CanResetToNeighboursComparators()
    {
        var (page, authority, neighbourAuthorities, otherAuthorities) = await SetupNavigateInitPage();

        var code = otherAuthorities.First().Code!;
        var addButton = page.GetElementAndAssert("button[name='action'][value='add']", Assert.NotNull);

        page = await Client.SubmitForm(page.Forms[0], addButton, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                { "LaInput", code }
            });
        });

        var resetButton = page.GetElementAndAssert("button[name='action'][value='reset']", Assert.NotNull);

        page = await Client.SubmitForm(page.Forms[0], resetButton);

        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityHighNeedsStartBenchmarking(authority.Code).ToAbsolute());

        var neighboursSelected = page.GetElementAndAssert("#current-comparators-neighbours", Assert.NotNull);
        var cards = neighboursSelected.QuerySelectorAll(".app-removable-card");
        Assert.NotNull(cards);
        Assert.Equal(neighbourAuthorities.Length, cards.Length);

        page.GetElementAndAssert("#current-comparators-others", Assert.Null);
    }

    #endregion

    #region Helper Methods

    private async Task<(
        IHtmlDocument page,
        LocalAuthorityStatisticalNeighbours authority,
        LocalAuthority[] neighbourAuthorities,
        LocalAuthority[] otherAuthorities)>
        SetupNavigateInitPage(string[]? comparators = null, string? referrer = null)
    {
        var authority = Fixture.Build<LocalAuthorityStatisticalNeighbours>()
            .With(a => a.Code, "123")
            .Create();

        var codes = Enumerable.Range(200, 30)
            .Select(i => i.ToString())
            .ToArray();

        var statisticalNeighbours = codes.Take(9)
            .Select(code => new LocalAuthorityStatisticalNeighbour
            {
                Code = code,
                Name = $"neighbour{code}"
            })
            .ToArray();

        authority.StatisticalNeighbours = statisticalNeighbours;

        var neighbourAuthorities = statisticalNeighbours
            .Select(n => new LocalAuthority
            {
                Code = n.Code,
                Name = n.Name
            })
            .ToArray();

        var otherAuthorities = codes.Skip(9)
            .Select(code => new LocalAuthority
            {
                Code = code,
                Name = $"other{code}"
            })
            .ToArray();

        var authorities = neighbourAuthorities
            .Concat(otherAuthorities).ToArray();

        var page = await Client.SetupEstablishment(authority, authorities)
            .SetupInsights()
            .SetupLocalAuthoritiesComparators(authority.Code!, comparators ?? [])
            .Navigate(Paths.LocalAuthorityHighNeedsStartBenchmarking(authority.Code, referrer));

        return (page, authority, neighbourAuthorities, otherAuthorities);
    }

    private static void AssertPageLayout(IHtmlDocument page, LocalAuthorityStatisticalNeighbours authority, LocalAuthority[] authorities)
    {
        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityHighNeedsStartBenchmarking(authority.Code).ToAbsolute());

        var expectedBreadcrumbs = new[]
        {
            ("Home", Paths.ServiceHome.ToAbsolute())
        };
        DocumentAssert.Breadcrumbs(page, expectedBreadcrumbs);

        Assert.NotNull(authority.Name);
        DocumentAssert.TitleAndH1(page, "Choose local authorities to compare high needs spending - Financial Benchmarking and Insights Tool - GOV.UK", "Choose local authorities to compare high needs spending");

        var comparatorSelector = page.GetElementAndAssert("#LaInput", Assert.NotNull);
        var options = comparatorSelector.QuerySelectorAll("option").Select(q => q.TextContent).ToArray();
        var expectedOptions = new[]
        {
            "Choose local authority"
        }.Concat(authorities
            .Select(n => n.Name)
            .ToArray());
        Assert.Equal(expectedOptions, options);

        page.GetElementAndAssert("button[name='action'][value='add']", Assert.NotNull);
        page.GetElementAndAssert("button[name='action'][value='continue']", Assert.NotNull);
        page.GetElementAndAssert("button[name='action'][value='clear']", Assert.NotNull);
        page.GetElementAndAssert("button[name='action'][value='reset']", Assert.NotNull);
    }

    #endregion
}