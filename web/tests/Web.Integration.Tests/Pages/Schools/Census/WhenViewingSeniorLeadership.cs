using System.Net;
using AngleSharp.Dom;
using System.Net;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App;
using Web.App.Domain;
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

    private async Task<(IHtmlDocument page, School school, SeniorLeadershipGroup[] group)> SetupNavigateInitPage()
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
            .Navigate(Paths.SchoolSeniorLeadership(school.URN));

        return (page, school, group);
    }

    private static void AssertPageLayout(IHtmlDocument page, School school, SeniorLeadershipGroup[] group)
    {
        DocumentAssert.AssertPageUrl(page, Paths.SchoolSeniorLeadership(school.URN).ToAbsolute());
        DocumentAssert.TitleAndH1(page, "Benchmark senior leadership group - Financial Benchmarking and Insights Tool - GOV.UK",
            "Benchmark senior leadership group");

        var dataSourceElement = page.QuerySelector("div[data-test-id='data-source-wrapper']");
        Assert.NotNull(dataSourceElement);
        DocumentAssert.TextEqual(dataSourceElement, "Workforce data is taken from the workforce census. Pupil data is taken from the school census data set in January.");

        var comparatorSetDetailsElement = page.QuerySelector("[data-testid=comparator-set-details]");
        Assert.NotNull(comparatorSetDetailsElement);

        AssertTableSection(page, group);

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
}