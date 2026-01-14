using System.Net;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App.Domain;
using Web.App.Domain.Charts;
using Web.App.ViewModels;
using Xunit;

namespace Web.Integration.Tests.Pages.Schools.Census;

public class WhenViewingSeniorLeadership(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    [Fact]
    public async Task CanDisplay()
    {
        var (page, school, group) = await SetupNavigateInitPage();

        AssertPageLayout(page, school, group);
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

        page = await Client.SubmitForm(page.Forms[0], action, f =>
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

        page = await Client.SubmitForm(page.Forms[0], action, f =>
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

    private async Task<(IHtmlDocument page, School school, SeniorLeadershipGroup[] group)> SetupNavigateInitPage(string queryParams = "")
    {
        var school = Fixture.Build<School>()
            .With(x => x.URN, "123456")
            .Create();

        var comparatorSet = Fixture.Build<SchoolComparatorSet>()
            .With(c => c.Pupil, Fixture.CreateMany<string>().ToArray())
            .Without(c => c.Building)
            .Create();

        var group = Fixture.Build<SeniorLeadershipGroup>()
            .CreateMany()
            .ToArray();
        group.ElementAt(0).URN = school.URN;

        Assert.NotNull(school.URN);

        var page = await Client
            .SetupInsights()
            .SetupSchool(school, group)
            .SetupComparatorSet(school, comparatorSet)
            .Navigate($"{Paths.SchoolSeniorLeadership(school.URN)}{queryParams}");

        return (page, school, group);
    }

    private static void AssertPageLayout(
        IHtmlDocument page,
        School school,
        SeniorLeadershipGroup[] group,
        int viewAs = 0,
        int resultAs = 0,
        string expectedQueryParams = "")
    {
        DocumentAssert.AssertPageUrl(page, $"{Paths.SchoolSeniorLeadership(school.URN)}{expectedQueryParams}".ToAbsolute());
        DocumentAssert.TitleAndH1(page, "Benchmark senior leadership group - Financial Benchmarking and Insights Tool - GOV.UK",
            "Benchmark senior leadership group");

        var dataSourceElement = page.QuerySelector("div[data-test-id='data-source-wrapper']");
        Assert.NotNull(dataSourceElement);
        DocumentAssert.TextEqual(dataSourceElement, "Workforce data is taken from the workforce census. Pupil data is taken from the school census data set in January.");

        var comparatorSetDetailsElement = page.QuerySelector("[data-testid=comparator-set-details]");
        Assert.NotNull(comparatorSetDetailsElement);

        var form = page.QuerySelector(".actions-form");
        Assert.NotNull(form);

        AssertFormOptions(form, viewAs, resultAs);

        if (viewAs == 0)
        {
            // TODO: update with assertions for chart svg once implemented
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
            Assert.Equal(decimal.Parse(cells[2]), expected.TotalPupils);
            Assert.Equal(decimal.Parse(cells[3]), expected.SeniorLeadership);
            Assert.Equal(decimal.Parse(cells[4]), expected.HeadTeacher);
            Assert.Equal(decimal.Parse(cells[5]), expected.DeputyHeadTeacher);
            Assert.Equal(decimal.Parse(cells[6]), expected.AssistantHeadTeacher);
            Assert.Equal(decimal.Parse(cells[7]), expected.LeadershipNonTeacher);
        }
    }

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
}