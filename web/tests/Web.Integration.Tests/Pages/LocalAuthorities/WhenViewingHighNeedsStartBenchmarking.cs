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
        var (page, authority, authorities) = await SetupNavigateInitPage();

        AssertPageLayout(page, authority, authorities);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string code = "123";
        var page = await Client.SetupEstablishmentWithException()
            .Navigate(Paths.LocalAuthorityHome(code));

        PageAssert.IsProblemPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityHome(code).ToAbsolute(), HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task CanDisplayNotFound()
    {
        const string code = "123";
        var page = await Client.SetupEstablishmentWithNotFound()
            .Navigate(Paths.LocalAuthorityHome(code));

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityHome(code).ToAbsolute(), HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanAddComparators()
    {
        var (page, _, authorities) = await SetupNavigateInitPage();
        var code = authorities.First().Code!;

        var addButton = page.QuerySelector("button[name='action'][value='add']");
        Assert.NotNull(addButton);

        page = await Client.SubmitForm(page.Forms[0], addButton, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                {
                    "LaInput", code
                }
            });
        });

        var selectedTable = page.QuerySelector("#current-comparators-la");
        Assert.NotNull(selectedTable);
        var rows = selectedTable.QuerySelectorAll("tbody > tr");
        Assert.Single(rows);
        Assert.Equal(authorities.First().Name!, rows.Single().QuerySelector("> td")?.TextContent);

        var comparatorSelector = page.QuerySelector("#LaInput");
        Assert.NotNull(comparatorSelector);
        var options = comparatorSelector.QuerySelectorAll("option").Select(q => q.TextContent).ToArray();
        var expectedOptions = new[] { "Choose local authority" }.Concat(authorities
            .Except([authorities.First()])
            .Select(n => n.Name)
            .ToArray());
        Assert.Equal(expectedOptions, options);
    }

    [Fact]
    public async Task CanRemoveComparators()
    {
        var (page, _, authorities) = await SetupNavigateInitPage();
        var code = authorities.First().Code!;

        var addButton = page.QuerySelector("button[name='action'][value='add']");
        Assert.NotNull(addButton);

        page = await Client.SubmitForm(page.Forms[0], addButton, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                {
                    "LaInput", code
                }
            });
        });

        var removeButton = page.QuerySelector($"button[name='action'][value='remove-{code}']");
        Assert.NotNull(removeButton);
        page = await Client.SubmitForm(page.Forms[0], removeButton, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                {
                    "LaInput", code
                }
            });
        });

        var selectedTable = page.QuerySelector("#current-comparators-la");
        Assert.Null(selectedTable);
    }

    [Fact]
    public async Task CanDisplayValidationError()
    {
        var (page, _, _) = await SetupNavigateInitPage();

        var continueButton = page.QuerySelector("button[name='action'][value='continue']");
        Assert.NotNull(continueButton);

        page = await Client.SubmitForm(page.Forms[0], continueButton);

        var errorSummary = page.QuerySelector(".govuk-error-summary");
        Assert.NotNull(errorSummary);
        Assert.Equal("There is a problem\nSelect between 1 and 9 comparator local authorities", errorSummary.GetInnerText().Trim());
    }

    [Fact]
    public async Task CanCancel()
    {
        var (page, authority, _) = await SetupNavigateInitPage();

        var cancelButton = page.QuerySelector("button[name='action'][value='reset']");
        Assert.NotNull(cancelButton);

        page = await Client.SubmitForm(page.Forms[0], cancelButton);

        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityHighNeeds(authority.Code).ToAbsolute());
    }

    private async Task<(IHtmlDocument page, LocalAuthorityStatisticalNeighbours authority, LocalAuthority[] authorities)> SetupNavigateInitPage()
    {
        var authority = Fixture.Build<LocalAuthorityStatisticalNeighbours>()
            .Create();

        var statisticalNeighbours = Fixture.Build<LocalAuthorityStatisticalNeighbour>()
            .CreateMany()
            .ToArray();

        authority.StatisticalNeighbours = statisticalNeighbours;
        var authorities = Fixture.Build<LocalAuthority>().CreateMany().ToArray();

        var page = await Client.SetupEstablishment(authority, authorities)
            .SetupInsights()
            .Navigate(Paths.LocalAuthorityHighNeedsStartBenchmarking(authority.Code));

        return (page, authority, authorities);
    }

    private static void AssertPageLayout(IHtmlDocument page, LocalAuthorityStatisticalNeighbours authority, LocalAuthority[] authorities)
    {
        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityHighNeedsStartBenchmarking(authority.Code).ToAbsolute());

        var expectedBreadcrumbs = new[] { ("Home", Paths.ServiceHome.ToAbsolute()) };
        DocumentAssert.Breadcrumbs(page, expectedBreadcrumbs);

        Assert.NotNull(authority.Name);
        DocumentAssert.TitleAndH1(page, "Choose local authorities to benchmark against - Financial Benchmarking and Insights Tool - GOV.UK", "Choose local authorities to benchmark against");

        var orderedList = page.QuerySelector(".govuk-inset-text > ol.govuk-list--number");
        Assert.NotNull(orderedList);
        var listItems = orderedList.QuerySelectorAll("li").Select(q => q.TextContent).ToArray();
        var expectedListItems = authority.StatisticalNeighbours?
            .OrderBy(n => n.Order)
            .ThenBy(n => n.Name)
            .Select(n => n.Name)
            .ToArray();
        Assert.Equal(expectedListItems, listItems);

        var comparatorSelector = page.QuerySelector("#LaInput");
        Assert.NotNull(comparatorSelector);
        var options = comparatorSelector.QuerySelectorAll("option").Select(q => q.TextContent).ToArray();
        var expectedOptions = new[] { "Choose local authority" }.Concat(authorities
            .Select(n => n.Name)
            .ToArray());
        Assert.Equal(expectedOptions, options);

        var addButton = page.QuerySelector("button[name='action'][value='add']");
        Assert.NotNull(addButton);

        var continueButton = page.QuerySelector("button[name='action'][value='continue']");
        Assert.NotNull(continueButton);

        var cancelButton = page.QuerySelector("button[name='action'][value='reset']");
        Assert.NotNull(cancelButton);
    }
}