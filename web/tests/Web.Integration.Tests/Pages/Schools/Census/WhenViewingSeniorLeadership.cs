using System.Net;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App.Domain;
using Web.App.Domain.Charts;
using Xunit;

namespace Web.Integration.Tests.Pages.Schools.Census;

public class WhenViewingSeniorLeadership(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    [Theory]
    [InlineData(true, true)]
    [InlineData(false, true)]
    [InlineData(true, false)]
    [InlineData(false, false)]
    public async Task CanDisplay(bool chartError, bool withUserDefinedUserData)
    {
        var (page, school, group) = await SetupNavigateInitPage(
            chartApiException: chartError,
            withUserDefinedUserData: withUserDefinedUserData);

        AssertPageLayout(page, school, group, chartError: chartError, withUserDefinedUserData: withUserDefinedUserData);
    }

    [Fact]
    public async Task CanDisplayNotFound()
    {
        const string urn = "123456";
        var page = await Client.SetupSchoolWithNotFound()
            .Navigate(Paths.SchoolSeniorLeadership(urn));

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.SchoolSeniorLeadership(urn).ToAbsolute(), HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string urn = "123456";
        var page = await Client.SetupSchoolWithException()
            .Navigate(Paths.SchoolSeniorLeadership(urn));

        PageAssert.IsProblemPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.SchoolSeniorLeadership(urn).ToAbsolute(),
            HttpStatusCode.InternalServerError);
    }

    [Theory]
    [InlineData(0, "?viewAs=0&resultAs=0")]
    [InlineData(1, "?viewAs=1&resultAs=0")]
    public async Task CanSubmitOptionsForViewAs(int viewAs, string expectedQueryParams)
    {
        var (page, school, group) = await SetupNavigateInitPage(expectedQueryParams);

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
            school,
            group,
            viewAs: viewAs,
            expectedQueryParams: expectedQueryParams);
    }

    [Theory]
    [InlineData(0, "?viewAs=0&resultAs=0")]
    [InlineData(1, "?viewAs=0&resultAs=1")]
    public async Task CanSubmitOptionsForResultsAs(int resultAs, string expectedQueryParams)
    {
        var (page, school, group) = await SetupNavigateInitPage(expectedQueryParams);

        var action = page.QuerySelectorAll("button").FirstOrDefault(x => x.TextContent.Trim() == "Apply");
        Assert.NotNull(action);
        var form = action.Closest("form");
        Assert.NotNull(form);
        page = await Client.SubmitForm(form, action, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                { "ResultAs", resultAs.ToString() }
            });
        });

        AssertPageLayout(
            page,
            school,
            group,
            resultAs: resultAs,
            expectedQueryParams: expectedQueryParams);
    }

    [Fact]
    public async Task CanDisplayCorrectComparatorSetDetailsWhenMissingComparatorSet()
    {
        var (page, school, _) = await SetupNavigateInitPage(withMissingComparatorSet: true);

        var comparatorSetDetailsElement = page.QuerySelector("[data-testid=comparator-set-details]"); ;
        Assert.NotNull(comparatorSetDetailsElement);

        AssertMissingComparatorSetCreatedComparatorSetDetailsContent(comparatorSetDetailsElement, school);
    }

    [Fact]
    public async Task CanDownloadPageData()
    {
        var (page, school, _) = await SetupNavigateInitPage();

        var action = page.QuerySelectorAll("button").FirstOrDefault(x => x.TextContent.Trim() == "Download page data");
        Assert.NotNull(action);
        var form = action.Closest("form");
        Assert.NotNull(form);
        page = await Client.SubmitForm(form, action);

        DocumentAssert.AssertPageUrl(page, Paths.SchoolSeniorLeadershipDownload(school.URN).ToAbsolute());
    }

    [Fact]
    public async Task MissingTableDataCorrectlyShownAsZero()
    {
        var (page, school, group) = await SetupNavigateInitPage(
            queryParams: "?viewAs=1&resultAs=0",
            withMissingGroupValues: true);

        AssertPageLayout(
            page,
            school,
            group,
            viewAs: 1,
            resultAs: 0,
            expectedQueryParams: "?viewAs=1&resultAs=0");
    }

    private async Task<(IHtmlDocument page, School school, SeniorLeadershipGroup[] group)> SetupNavigateInitPage(
        string queryParams = "",
        bool chartApiException = false,
        bool withUserDefinedUserData = false,
        bool withMissingComparatorSet = false,
        bool withMissingGroupValues = false)
    {
        const string chartSvg = "<svg />";

        var school = Fixture.Build<School>()
            .With(x => x.URN, "123456")
            .Create();

        var comparatorSet = Fixture.Build<SchoolComparatorSet>()
            .With(c => c.Pupil, Fixture.CreateMany<string>().ToArray())
            .Without(c => c.Building)
            .Create();

        var emptyComparatorSet = Fixture.Build<SchoolComparatorSet>()
            .Without(x => x.Building)
            .Without(x => x.Pupil)
            .Create();

        var userDefinedSetUserData = new[]
        {
            new UserData
            {
                Type = "comparator-set",
                Id = "456"
            }
        };

        SeniorLeadershipGroup[] group;

        if (!withMissingGroupValues)
        {
            group = Fixture.Build<SeniorLeadershipGroup>()
                .CreateMany()
                .ToArray();
            group.ElementAt(0).URN = school.URN;
        }
        else
        {
            var otherSchools = Fixture.Build<SeniorLeadershipGroup>()
                .CreateMany(2)
                .ToList();

            var selectedSchool = Fixture.Build<SeniorLeadershipGroup>()
                .Without(x => x.SeniorLeadership)
                .Without(x => x.HeadTeacher)
                .Without(x => x.DeputyHeadTeacher)
                .Without(x => x.AssistantHeadTeacher)
                .Without(x => x.LeadershipNonTeacher)
                .Create();

            group = otherSchools.Append(selectedSchool).ToArray();
        }

        Assert.NotNull(school.URN);

        var client = Client.SetupInsights()
            .SetupSchool(school, group)
            .SetupUserData(withUserDefinedUserData ? userDefinedSetUserData : null)
            .SetupComparatorSet(school, withMissingComparatorSet ? emptyComparatorSet : comparatorSet)
            .SetupSingleChartRendering<SeniorLeadershipGroup>(chartSvg);

        if (chartApiException)
        {
            client.SetupChartRenderingWithException<SeniorLeadershipGroup>();
        }

        var page = await client.Navigate($"{Paths.SchoolSeniorLeadership(school.URN)}{queryParams}");

        return (page, school, group);
    }

    private static void AssertPageLayout(
        IHtmlDocument page,
        School school,
        SeniorLeadershipGroup[] group,
        int viewAs = 0,
        int resultAs = 0,
        string expectedQueryParams = "",
        bool chartError = false,
        bool withUserDefinedUserData = false)
    {
        DocumentAssert.AssertPageUrl(page, $"{Paths.SchoolSeniorLeadership(school.URN)}{expectedQueryParams}".ToAbsolute());
        DocumentAssert.TitleAndH1(page, "Benchmark senior leadership group - Financial Benchmarking and Insights Tool - GOV.UK",
            "Benchmark senior leadership group");

        var dataSourceElement = page.QuerySelector("div[data-test-id='data-source-wrapper']");
        Assert.NotNull(dataSourceElement);
        DocumentAssert.TextEqual(dataSourceElement, "Workforce data is taken from the workforce census. Pupil data is taken from the school census data set in January.");

        var comparatorSetDetailsElement = page.QuerySelector("[data-testid=comparator-set-details]");
        Assert.NotNull(comparatorSetDetailsElement);
        AssertComparatorSetDetails(comparatorSetDetailsElement, school, withUserDefinedUserData);

        var form = page.QuerySelector(".actions-form");
        Assert.NotNull(form);

        AssertFormOptions(form, viewAs, resultAs);

        if (viewAs == 0)
        {
            AssertChartSection(page, chartError);
        }
        else
        {
            AssertTableSection(page, group);
        }

        var toolsSection = page.GetElementById("benchmarking-and-planning-tools");
        DocumentAssert.Heading2(toolsSection, "Benchmarking and planning tools");

        var toolsLinks = toolsSection?.ChildNodes.QuerySelectorAll("ul> li > h3 > a").ToList();
        Assert.Equal(3, toolsLinks?.Count);

        DocumentAssert.Link(toolsLinks?.ElementAtOrDefault(0), "Curriculum and financial planning", Paths.SchoolFinancialPlanning(school.URN).ToAbsolute());
        DocumentAssert.Link(toolsLinks?.ElementAtOrDefault(1), "Benchmark spending", Paths.SchoolComparison(school.URN).ToAbsolute());
        DocumentAssert.Link(toolsLinks?.ElementAtOrDefault(2), "Benchmark pupil and workforce data", Paths.SchoolCensus(school.URN).ToAbsolute());
    }

    private static void AssertTableSection(IHtmlDocument page, SeniorLeadershipGroup[] group)
    {
        var table = page.QuerySelector(".govuk-table");
        Assert.NotNull(table);

        var rows = table.QuerySelectorAll("tbody tr");
        Assert.Equal(group.Length, rows.Length);

        foreach (var row in rows)
        {
            var cells = row.TableCells();

            var expected = group.FirstOrDefault(g => g.SchoolName == cells[0]);
            Assert.NotNull(expected);

            var schoolLink = row.QuerySelector(".govuk-link");

            Assert.NotNull(expected.SchoolName);
            DocumentAssert.Link(schoolLink, expected.SchoolName, Paths.SchoolHome(expected.URN).ToAbsolute());

            Assert.Equal(cells[1], expected.LAName);
            Assert.Equal(decimal.Parse(cells[2]), AsDecimalOrZero(expected.TotalPupils));
            Assert.Equal(decimal.Parse(cells[3]), AsDecimalOrZero(expected.SeniorLeadership));
            Assert.Equal(decimal.Parse(cells[4]), AsDecimalOrZero(expected.HeadTeacher));
            Assert.Equal(decimal.Parse(cells[5]), AsDecimalOrZero(expected.DeputyHeadTeacher));
            Assert.Equal(decimal.Parse(cells[6]), AsDecimalOrZero(expected.AssistantHeadTeacher));
            Assert.Equal(decimal.Parse(cells[7]), AsDecimalOrZero(expected.LeadershipNonTeacher));
        }
    }

    private static decimal AsDecimalOrZero(decimal? value) => value ?? 0;

    private static void AssertFormOptions(
        IElement form,
        int viewAs = 0,
        int resultAs = 0)
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

        var resultAsSelect = form.QuerySelector("select[name='ResultAs']");
        Assert.NotNull(resultAsSelect);

        var optionEls = resultAsSelect.QuerySelectorAll("option");
        Assert.Equal(CensusDimensions.All.Length, optionEls.Length);

        foreach (var option in CensusDimensions.All)
        {
            var value = ((int)option).ToString();

            var optionEl = optionEls.FirstOrDefault(x => x.GetAttribute("value") == value);
            Assert.NotNull(optionEl);

            var isSelected = optionEl.HasAttribute("selected");
            var shouldBeSelected = resultAs == (int)option;

            Assert.Equal(shouldBeSelected, isSelected);
        }

        var applyButton = form.QuerySelector("button.govuk-button--secondary");
        Assert.NotNull(applyButton);
        DocumentAssert.TextEqual(applyButton, "Apply");
    }

    private static void AssertChartSection(IHtmlDocument page, bool chartError)
    {
        var chartSvg = page.QuerySelector(".ssr-chart");
        var chartWarning = page.QuerySelector(".ssr-chart-warning");

        if (chartError)
        {
            Assert.NotNull(chartWarning);
            Assert.Null(chartSvg);
        }
        else
        {
            Assert.NotNull(chartSvg);
            Assert.Null(chartWarning);
        }
    }

    private static void AssertComparatorSetDetails(IElement comparatorSetDetailsElement, School school, bool withUserDefinedUserData)
    {
        if (withUserDefinedUserData)
        {
            AssertUserDefinedSetCreatedComparatorSetDetailsContent(comparatorSetDetailsElement, school);
        }
        else
        {
            AssertComparatorSetDetailsDefaultContent(comparatorSetDetailsElement, school);
        }
    }

    private static void AssertComparatorSetDetailsDefaultContent(IElement comparatorSetDetailsElement, School school)
    {
        var listItems = comparatorSetDetailsElement.QuerySelectorAll("ul[data-testid='actions-list'] li");
        Assert.Equal(2, listItems.Length);

        DocumentAssert.TextEqual(listItems[0], "view the set of similar schools we've chosen to benchmark this school's pupil and workforce data against", true);
        DocumentAssert.Link(listItems[0].QuerySelector("a"), "view the set of similar schools we've chosen", Paths.SchoolComparatorsWorkforce(school.URN).ToAbsolute());

        DocumentAssert.TextEqual(listItems[1], "create or save your own set of schools to benchmark against", true);
        DocumentAssert.Link(listItems[1].QuerySelector("a"), "create or save your own set of schools", Paths.SchoolComparatorsCreate(school.URN).ToAbsolute());
    }

    private static void AssertUserDefinedSetCreatedComparatorSetDetailsContent(IElement comparatorSetDetailsElement, School school)
    {
        var paragraphs = comparatorSetDetailsElement.QuerySelectorAll("p.govuk-body");
        Assert.NotNull(paragraphs);
        Assert.Equal(2, paragraphs.Length);

        DocumentAssert.TextEqual(paragraphs[0], "You are now comparing with your chosen schools.");
        DocumentAssert.TextEqual(paragraphs[1], "Change your similar schools.");
        DocumentAssert.Link(paragraphs[1].QuerySelector("a"), "Change your similar schools.", Paths.SchoolComparatorsUserDefined(school.URN).ToAbsolute());
    }

    private static void AssertMissingComparatorSetCreatedComparatorSetDetailsContent(IElement comparatorSetDetailsElement, School school)
    {
        var paragraphs = comparatorSetDetailsElement.QuerySelectorAll("p.govuk-body");
        Assert.Equal(2, paragraphs.Length);
        DocumentAssert.TextEqual(paragraphs[0], "There is not enough information available to create a set of similar schools.", true);
        DocumentAssert.TextEqual(paragraphs[1], "create or save your own set of schools to benchmark against", true);
        DocumentAssert.Link(paragraphs[1].QuerySelector("a"), "create or save your own set of schools", Paths.SchoolComparatorsCreate(school.URN).ToAbsolute());
    }
}