using System.Net;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App.Domain.Charts;
using Web.App.Domain.LocalAuthorities;
using Xunit;

namespace Web.Integration.Tests.Pages.LocalAuthorities;

public class WhenViewingEducationHealthCarePlans(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    [Fact]
    public async Task CanDisplay()
    {
        var (page, authority, plans) = await SetupNavigateInitPage();

        AssertPageLayout(page, authority, plans);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string code = "123";
        var page = await Client.SetupLocalAuthorityEndpointsWithException()
            .Navigate(Paths.LocalAuthorityEducationHealthCarePlans(code));

        PageAssert.IsProblemPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityEducationHealthCarePlans(code).ToAbsolute(), HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task CanDisplayNotFoundForEstablishment()
    {
        const string code = "123";
        var page = await Client.SetupLocalAuthorityEndpointsWithNotFound()
            .Navigate(Paths.LocalAuthorityEducationHealthCarePlans(code));

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.LocalAuthorityEducationHealthCarePlans(code).ToAbsolute(), HttpStatusCode.NotFound);
    }

    [Theory]
    [InlineData(0, "?viewAs=0")]
    [InlineData(1, "?viewAs=1")]
    public async Task CanSubmitOptionsForViewAs(int viewAs, string expectedQueryParams)
    {
        var (page, authority, plans) = await SetupNavigateInitPage(expectedQueryParams);

        var action = page.QuerySelectorAll("button").FirstOrDefault(x => x.TextContent.Trim() == "Apply");
        Assert.NotNull(action);
        var form = action.Closest("form");
        Assert.NotNull(form);
        page = await Client.SubmitForm(form, action, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                { "ViewAs", viewAs.ToString() }
            });
        });

        AssertPageLayout(
            page,
            authority,
            plans,
            viewAs: viewAs,
            expectedQueryParams: expectedQueryParams);
    }

    [Fact]
    public async Task CanDownloadPageData()
    {
        var (page, authority, _) = await SetupNavigateInitPage();

        var anchor = page.QuerySelectorAll("a.govuk-button")
            .FirstOrDefault(x => x.TextContent.Trim() == "Download page data");
        Assert.NotNull(anchor);

        var newPage = await Client.Follow(anchor);

        DocumentAssert.AssertPageUrl(newPage, Paths.LocalAuthorityEducationHealthCarePlansDownload(authority.Code).ToAbsolute());
    }

    private async Task<(
        IHtmlDocument page,
        LocalAuthority authority,
        EducationHealthCarePlans[] plans)> SetupNavigateInitPage(
        string queryParams = "")
    {
        var authority = Fixture.Build<LocalAuthority>()
            .With(a => a.Code, "123")
            .Create();

        var plans = Fixture.Build<EducationHealthCarePlans>()
            .CreateMany(3).ToArray();

        Assert.NotNull(authority.Code);

        var client = Client.SetupInsights()
            .SetupLocalAuthorityEndpoints(authority, plans)
            .SetupLocalAuthoritiesComparators(authority.Code, ["123", "124"]);

        var page = await client.Navigate($"{Paths.LocalAuthorityEducationHealthCarePlans(authority.Code)}{queryParams}");

        return (page, authority, plans);
    }

    private static void AssertPageLayout(
        IHtmlDocument page,
        LocalAuthority authority,
        EducationHealthCarePlans[] plans,
        int viewAs = 0,
        string expectedQueryParams = "")
    {
        DocumentAssert.AssertPageUrl(page, $"{Paths.LocalAuthorityEducationHealthCarePlans(authority.Code)}{expectedQueryParams}".ToAbsolute());

        Assert.NotNull(authority.Name);
        DocumentAssert.TitleAndH1(page, "Benchmark education, health care plans - Financial Benchmarking and Insights Tool - GOV.UK", "Benchmark education, health care plans");

        var form = page.QuerySelector(".actions-form");
        Assert.NotNull(form);

        AssertFormOptions(form, viewAs);

        if (viewAs == 0)
        {
            //TODO: asserts chart once implemented
            Assert.True(true);
        }
        else
        {
            AssertTableSection(page, plans);
        }
    }

    private static void AssertFormOptions(
        IElement form,
        int viewAs = 0)
    {
        var viewAsContainer = form.QuerySelector("#ViewAs");
        Assert.NotNull(viewAsContainer);

        var radioInputs = viewAsContainer.QuerySelectorAll("input[type='radio']");
        Assert.Equal(Views.All.Length, radioInputs.Length);

        foreach (var view in Views.All)
        {
            var value = ((int)view).ToString();

            var input = radioInputs.FirstOrDefault(x => x.GetAttribute("value") == value);
            Assert.NotNull(input);

            var shouldBeChecked = viewAs == (int)view;
            var isChecked = input.HasAttribute("checked");

            Assert.Equal(shouldBeChecked, isChecked);
        }
    }

    private static void AssertTableSection(IHtmlDocument page, EducationHealthCarePlans[] plans)
    {
        var tables = page.QuerySelectorAll(".govuk-table");
        Assert.NotNull(tables);

        foreach (var table in tables)
        {
            var heading = table.QuerySelector("thead th:nth-child(2)")!.TextContent.Trim();

            // get required prop from table heading to later extract expected value from plan
            Assert.True(HeadingToValue.TryGetValue(heading, out var extractor),
                $"Unknown heading: {heading}");

            var rows = table.QuerySelectorAll("tbody tr");
            Assert.Equal(plans.Length, rows.Length);

            foreach (var row in rows)
            {
                var cells = row.TableCells();

                var expected = plans.FirstOrDefault(g => g.Name == cells[0]);
                Assert.NotNull(expected);

                Assert.Equal(cells[0], expected.Name);
                Assert.Equal(cells[1], extractor(expected).ToString());
                Assert.Equal(cells[2], expected.TotalPupils.ToString());
            }
        }
    }

    private static readonly Dictionary<string, Func<EducationHealthCarePlans, decimal?>> HeadingToValue =
        new()
        {
            ["Total pupils with EHC plans (per 1000 pupils)"] = p => p.Total,
            ["EHC plans in Mainstream schools or academies (per 1000 pupils)"] = p => p.Mainstream,
            ["EHC plans in Resourced provision or SEN units (per 1000 pupils)"] = p => p.Resourced,
            ["EHC plans in Maintained special school or special academies (per 1000 pupils)"] = p => p.Special,
            ["EHC plans in NMSS or independent schools (per 1000 pupils)"] = p => p.Independent,
            ["EHC plans in Hospital schools or alternative provisions (per 1000 pupils)"] = p => p.Hospital,
            ["EHC plans in Post 16 (per 1000 pupils)"] = p => p.Post16,
            ["EHC plans in other types of provisions (per 1000 pupils)"] = p => p.Other
        };
}