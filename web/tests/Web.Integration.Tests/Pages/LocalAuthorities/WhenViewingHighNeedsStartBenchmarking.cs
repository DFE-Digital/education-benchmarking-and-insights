using System.Net;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App.Domain;
using Xunit;

namespace Web.Integration.Tests.Pages.LocalAuthorities;

public class WhenViewingHighNeedsStartBenchmarking(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
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

        var neighboursSelected = page.QuerySelector("#current-comparators-neighbours");
        Assert.NotNull(neighboursSelected);
        var neighbourCards = neighboursSelected.QuerySelectorAll(".app-removable-card");
        Assert.Equal(neighbourAuthorities.Length, neighbourCards.Length);

        var othersSelected = page.QuerySelector("#current-comparators-others");
        Assert.Null(othersSelected);

        var addButton = page.QuerySelector("button[name='action'][value='add']");
        Assert.NotNull(addButton);

        page = await Client.SubmitForm(page.Forms[0], addButton, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                { "LaInput", code }
            });
        });

        neighboursSelected = page.QuerySelector("#current-comparators-neighbours");
        Assert.NotNull(neighboursSelected);
        neighbourCards = neighboursSelected.QuerySelectorAll(".app-removable-card");
        Assert.Equal(neighbourAuthorities.Length, neighbourCards.Length);

        othersSelected = page.QuerySelector("#current-comparators-others");
        Assert.NotNull(othersSelected);
        var otherCards = othersSelected.QuerySelectorAll(".app-removable-card");
        Assert.Equal(1, otherCards.Length);

        var comparatorSelector = page.QuerySelector("#LaInput");
        Assert.NotNull(comparatorSelector);
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

    /*
    [Fact]
    public async Task CannotAddTwentiethComparator()
    {
        string[] selectedLAs = [
            "LA01", "LA02", "LA03", "LA04", "LA05", "LA06", "LA07", "LA08", "LA09", "LA10",
            "LA11", "LA12", "LA13", "LA14", "LA15", "LA16", "LA17", "LA18", "LA19"
        ];
        
        var (page, _, authorities) = await SetupNavigateInitPage(selectedLAs);
        var code = authorities.First().Code!;

        var addButton = page.QuerySelector("button[name='action'][value='add']");
        Assert.NotNull(addButton);

        page = await Client.SubmitForm(page.Forms[0], addButton, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                { "LaInput", code }
            });
        });

        var errorSummary = page.QuerySelector(".govuk-error-summary");
        Assert.NotNull(errorSummary);
        Assert.Equal("There is a problem\nSelect between 1 and 19 comparator local authorities", errorSummary.GetInnerText().Trim());
    }
    */

    [Fact]
    public async Task CanRemoveOtherComparators()
    {
        var (page, _, neighbourAuthorities, otherAuthorities) = await SetupNavigateInitPage();
        var code = otherAuthorities.First().Code!;

        var addButton = page.QuerySelector("button[name='action'][value='add']");
        Assert.NotNull(addButton);

        var othersSelected = page.QuerySelector("#current-comparators-others");
        Assert.Null(othersSelected);

        page = await Client.SubmitForm(page.Forms[0], addButton, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                { "LaInput", code }
            });
        });

        othersSelected = page.QuerySelector("#current-comparators-others");
        Assert.NotNull(othersSelected);

        var othersCards = othersSelected.QuerySelectorAll(".app-removable-card");
        Assert.Equal(1, othersCards.Length);


        var removeButton = page.QuerySelector($"button[name='action'][value='remove-{code}']");
        Assert.NotNull(removeButton);

        page = await Client.SubmitForm(page.Forms[0], removeButton, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                { "LaInput", code }
            });
        });

        var neighboursSelected = page.QuerySelector("#current-comparators-neighbours");
        Assert.NotNull(neighboursSelected);

        var neighboursCards = neighboursSelected.QuerySelectorAll(".app-removable-card");
        Assert.Equal(neighbourAuthorities.Length, neighboursCards.Length);
    }

    [Fact]
    public async Task CanRemoveNeighbourComparators()
    {
        var (page, _, neighbourAuthorities, _) = await SetupNavigateInitPage();
        var code = neighbourAuthorities.First().Code!;

        var addButton = page.QuerySelector("button[name='action'][value='add']");
        Assert.NotNull(addButton);

        page = await Client.SubmitForm(page.Forms[0], addButton, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                { "LaInput", code }
            });
        });

        var neighboursSelected = page.QuerySelector("#current-comparators-neighbours");
        Assert.NotNull(neighboursSelected);

        var neighboursCards = neighboursSelected.QuerySelectorAll(".app-removable-card");
        Assert.Equal(neighbourAuthorities.Length, neighboursCards.Length);

        var removeButton = page.QuerySelector($"button[name='action'][value='remove-{code}']");
        Assert.NotNull(removeButton);

        page = await Client.SubmitForm(page.Forms[0], removeButton, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                { "LaInput", code }
            });
        });

        neighboursSelected = page.QuerySelector("#current-comparators-neighbours");
        Assert.NotNull(neighboursSelected);

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
            var removeButton = page.QuerySelector($"button[name='action'][value='remove-{code}']");
            Assert.NotNull(removeButton);

            page = await Client.SubmitForm(page.Forms[0], removeButton, f =>
            {
                f.SetFormValues(new Dictionary<string, string>
                {
                    { "LaInput", code }
                });
            });
        }

        var continueButton = page.QuerySelector("button[name='action'][value='continue']");
        Assert.NotNull(continueButton);

        page = await Client.SubmitForm(page.Forms[0], continueButton);

        var errorSummary = page.QuerySelector(".govuk-error-summary");
        Assert.NotNull(errorSummary);
        Assert.Equal("There is a problem\nSelect between 1 and 19 comparator local authorities", errorSummary.GetInnerText().Trim());
    }

    [Fact]
    public async Task CanContinue()
    {
        var (page, authority, _, otherAuthorities) = await SetupNavigateInitPage(["code1"]);
        var code = otherAuthorities.First().Code!;

        var addButton = page.QuerySelector("button[name='action'][value='add']");
        Assert.NotNull(addButton);

        page = await Client.SubmitForm(page.Forms[0], addButton, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                { "LaInput", code }
            });
        });

        var continueButton = page.QuerySelector("button[name='action'][value='continue']");
        Assert.NotNull(continueButton);

        page = await Client.SubmitForm(page.Forms[0], continueButton);
        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityHighNeedsBenchmarking(authority.Code).ToAbsolute());
    }

    [Fact]
    public async Task CanDisplayNeighboursPreSelected()
    {
        var (page, authority, neighbourAuthorities, _) = await SetupNavigateInitPage();

        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityHighNeedsStartBenchmarking(authority.Code).ToAbsolute());

        var selectedNeighbours = page.QuerySelector("#current-comparators-neighbours");
        Assert.NotNull(selectedNeighbours);
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

        var backLink = page.QuerySelector("a.govuk-back-link") as IHtmlElement;
        Assert.NotNull(backLink);

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
        var addButton = page.QuerySelector("button[name='action'][value='add']");
        Assert.NotNull(addButton);

        page = await Client.SubmitForm(page.Forms[0], addButton, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                { "LaInput", code }
            });
        });

        var removeAllButton = page.QuerySelector("button[name='action'][value='clear']");
        Assert.NotNull(removeAllButton);

        page = await Client.SubmitForm(page.Forms[0], removeAllButton);

        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityHighNeedsStartBenchmarking(authority.Code).ToAbsolute());

        removeAllButton = page.QuerySelector("button[name='action'][value='clear']");
        Assert.Null(removeAllButton);

        var neighboursSelected = page.QuerySelector("#current-comparators-neighbours");
        Assert.Null(neighboursSelected);
        var othersSelected = page.QuerySelector("#current-comparators-others");
        Assert.Null(othersSelected);
    }

    [Fact]
    public async Task CanResetToNeighboursComparators()
    {
        var (page, authority, neighbourAuthorities, otherAuthorities) = await SetupNavigateInitPage();

        var code = otherAuthorities.First().Code!;
        var addButton = page.QuerySelector("button[name='action'][value='add']");
        Assert.NotNull(addButton);

        page = await Client.SubmitForm(page.Forms[0], addButton, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                { "LaInput", code }
            });
        });

        var resetButton = page.QuerySelector("button[name='action'][value='reset']");
        Assert.NotNull(resetButton);

        page = await Client.SubmitForm(page.Forms[0], resetButton);

        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityHighNeedsStartBenchmarking(authority.Code).ToAbsolute());

        var neighboursSelected = page.QuerySelector("#current-comparators-neighbours");
        Assert.NotNull(neighboursSelected);
        var cards = neighboursSelected.QuerySelectorAll(".app-removable-card");
        Assert.NotNull(cards);
        Assert.Equal(neighbourAuthorities.Length, cards.Length);

        var othersSelected = page.QuerySelector("#current-comparators-others");
        Assert.Null(othersSelected);
    }

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

        var comparatorSelector = page.QuerySelector("#LaInput");
        Assert.NotNull(comparatorSelector);
        var options = comparatorSelector.QuerySelectorAll("option").Select(q => q.TextContent).ToArray();
        var expectedOptions = new[]
        {
            "Choose local authority"
        }.Concat(authorities
            .Select(n => n.Name)
            .ToArray());
        Assert.Equal(expectedOptions, options);

        var addButton = page.QuerySelector("button[name='action'][value='add']");
        Assert.NotNull(addButton);

        var continueButton = page.QuerySelector("button[name='action'][value='continue']");
        Assert.NotNull(continueButton);

        var removeAllButton = page.QuerySelector("button[name='action'][value='clear']");
        Assert.NotNull(removeAllButton);

        var resetButton = page.QuerySelector("button[name='action'][value='reset']");
        Assert.NotNull(resetButton);
    }
}