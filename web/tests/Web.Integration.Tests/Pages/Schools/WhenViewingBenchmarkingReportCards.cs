using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.XPath;
using AutoFixture;
using Web.App.Domain;
using Xunit;
namespace Web.Integration.Tests.Pages.Schools;

public class WhenViewingBenchmarkingReportCards(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    [Theory]
    [InlineData(EstablishmentTypes.Academies, false, false, 12)]
    [InlineData(EstablishmentTypes.Maintained, false, false, 12)]
    [InlineData(EstablishmentTypes.Maintained, true, true, 12)]
    public async Task CanDisplay(
        string financeType,
        bool isPartOfFederation,
        bool isLeadSchoolInFederation,
        int? periodCoveredByReturn)
    {
        var (page, school, _, _) = await SetupNavigateInitPage(financeType, isPartOfFederation, isLeadSchoolInFederation, periodCoveredByReturn);
        AssertPageLayout(page, school);
    }

    [Theory]
    [InlineData(EstablishmentTypes.Academies)]
    [InlineData(EstablishmentTypes.Maintained)]
    public async Task CanDisplayIntroductionSection(string financeType)
    {
        var (page, school, _, _) = await SetupNavigateInitPage(financeType);
        AssertIntroductionSection(page, school);
    }

    [Theory]
    [InlineData(EstablishmentTypes.Academies)]
    [InlineData(EstablishmentTypes.Maintained)]
    public async Task CanDisplayKeyInformationSection(string financeType)
    {
        var (page, school, balance, _) = await SetupNavigateInitPage(financeType);
        AssertKeyInformationSection(page, school, balance);
    }

    [Theory]
    [InlineData(EstablishmentTypes.Academies)]
    [InlineData(EstablishmentTypes.Maintained)]
    public async Task CanDisplayPriorityAreasAllSchoolsSection(string financeType)
    {
        var (page, school, _, _) = await SetupNavigateInitPage(financeType);
        AssertPriorityAreasAllSchoolsSection(page, school);
    }

    [Theory]
    [InlineData(EstablishmentTypes.Academies)]
    [InlineData(EstablishmentTypes.Maintained)]
    public async Task CanDisplayPriorityAreasOtherSection(string financeType)
    {
        var (page, school, _, _) = await SetupNavigateInitPage(financeType);
        AssertPriorityAreasOtherSection(page, school);
    }

    [Theory]
    [InlineData(EstablishmentTypes.Academies)]
    [InlineData(EstablishmentTypes.Maintained)]
    public async Task CanDisplayPupilWorkforceMetrics(string financeType)
    {
        var (page, school, _, census) = await SetupNavigateInitPage(financeType);
        AssertPupilWorkforceMetrics(page, school, census);
    }

    [Theory]
    [InlineData(EstablishmentTypes.Academies)]
    [InlineData(EstablishmentTypes.Maintained)]
    public async Task CanDisplayNextStepsSection(string financeType)
    {
        var (page, school, _, _) = await SetupNavigateInitPage(financeType);
        AssertNextStepsSection(page, school);
    }

    [Theory]
    [InlineData(EstablishmentTypes.Academies)]
    [InlineData(EstablishmentTypes.Maintained)]
    public async Task CanDisplayWhoYouAreComparedWithSection(string financeType)
    {
        var (page, _, _, _) = await SetupNavigateInitPage(financeType);
        AssertWhoYouAreComparedWithSection(page);
    }

    [Theory]
    [InlineData(EstablishmentTypes.Maintained, true, false, 12, "this is a non lead school in a federation")]
    [InlineData(EstablishmentTypes.Maintained, true, true, null, "expenditure data is not available for this school")]
    [InlineData(EstablishmentTypes.Maintained, true, true, 9, "this school does not have data for the entire period")]
    public async Task CanDisplayNotAvailableWarning(
        string financeType,
        bool isPartOfFederation,
        bool isLeadSchoolInFederation,
        int? periodCoveredByReturn,
        string commentary)
    {
        var (page, school, _, _) = await SetupNavigateInitPage(financeType, isPartOfFederation, isLeadSchoolInFederation, periodCoveredByReturn);
        AssertWarning(page, school, commentary);
    }

    private async Task<(IHtmlDocument page, School school, SchoolBalance balance, Census)> SetupNavigateInitPage(
        string financeType,
        bool isPartOfFederation = false,
        bool isLeadSchoolInFederation = false,
        int? periodCoveredByReturn = 12)
    {
        const string urn = "12345";
        var school = Fixture.Build<School>()
            .With(x => x.URN, urn)
            .With(x => x.TrustCompanyNumber, financeType == EstablishmentTypes.Academies ? "12345678" : string.Empty)
            .With(x => x.FinanceType, financeType)
            .With(x => x.FederationLeadURN, isPartOfFederation ? isLeadSchoolInFederation ? urn : "54321" : string.Empty)
            .Create();

        var balance = Fixture.Build<SchoolBalance>()
            .With(x => x.SchoolName, school.SchoolName)
            .With(x => x.URN, school.URN)
            .With(x => x.PeriodCoveredByReturn, periodCoveredByReturn)
            .Create();

        RagRating[] ratings =
        [
            new()
            {
                Category = Category.TeachingStaff,
                RAG = "red"
            },
            new()
            {
                Category = Category.NonEducationalSupportStaff,
                RAG = "amber"
            },
            new()
            {
                Category = Category.EducationalSupplies,
                RAG = "green"
            },
            new()
            {
                Category = Category.EducationalIct,
                RAG = "red"
            },
            new()
            {
                Category = Category.PremisesStaffServices,
                RAG = "amber"
            },
            new()
            {
                Category = Category.Utilities,
                RAG = "green"
            },
            new()
            {
                Category = Category.AdministrativeSupplies,
                RAG = "red"
            },
            new()
            {
                Category = Category.CateringStaffServices,
                RAG = "amber"
            },
            new()
            {
                Category = Category.Other,
                RAG = "red"
            }
        ];

        var censuses = Fixture.Build<Census>().CreateMany().ToArray();
        censuses.First().URN = school.URN;

        var comparatorSet = Fixture.Create<SchoolComparatorSet>();

        var page = await Client.SetupEstablishment(school)
            .SetupMetricRagRating(ratings)
            .SetupInsights()
            .SetupExpenditure(school)
            .SetupBalance(balance)
            .SetupCensus(censuses)
            .SetupComparatorSet(school, comparatorSet)
            .Navigate(Paths.SchoolBenchmarkingReportCards(school.URN));

        return (page, school, balance, censuses.First());
    }

    private static void AssertPageLayout(IHtmlDocument page, School school)
    {
        DocumentAssert.AssertPageUrl(page, Paths.SchoolBenchmarkingReportCards(school.URN).ToAbsolute());
        DocumentAssert.TitleAndH1(page, "Benchmarking report cards - Financial Benchmarking and Insights Tool - GOV.UK", school.SchoolName!);
    }

    private static void AssertIntroductionSection(IHtmlDocument page, School school)
    {
        var introductionSection = page.Body.SelectSingleNode("//main/div/section[1]");
        var innerText = string.Join(
            Environment.NewLine,
            introductionSection.ChildNodes
                .QuerySelectorAll("p")
                .Select(e => string.Join(" ", e.ChildNodes.Select(n => n.TextContent.Trim())).Trim()));

        if (school.FinanceType == EstablishmentTypes.Academies)
        {
            Assert.Contains("Academies Accounts Return", innerText);
            Assert.Contains("School Workforce Census for 2021 - 2022", innerText);
        }
        else
        {
            Assert.Contains("Consistent Financial Reporting return", innerText);
            Assert.Contains("School Workforce Census for 2020 - 2021", innerText);
        }

        var link = introductionSection.ChildNodes.QuerySelector("a");
        Assert.NotNull(link);
        Assert.Equal(Paths.SchoolHome(school.URN), link.GetAttribute("href"));
    }

    private static void AssertKeyInformationSection(IHtmlDocument page, School school, SchoolBalance balance)
    {
        var keyInformationSection = page.Body.SelectSingleNode("//main/div/section[2]");
        DocumentAssert.Heading2(keyInformationSection, "Key information about your school");

        var headlineFiguresTexts = keyInformationSection.ChildNodes
            .QuerySelectorAll(".app-headline-figures")
            .Select(e => string.Join(" ", e.ChildNodes.Select(n => n.TextContent.Trim())).Trim());
        Assert.Equal([
            $"In year balance  £{balance.InYearBalance}",
            $"Revenue reserve  £{balance.RevenueReserve}",
            $"Ofsted rating  {school.OfstedDescription}",
            $"School phase  {school.OverallPhase}"
        ], headlineFiguresTexts);

        var link = keyInformationSection.ChildNodes.QuerySelector("a");
        Assert.NotNull(link);
        Assert.Equal(Paths.SchoolComparison(school.URN), link.GetAttribute("href"));
    }

    private static void AssertPriorityAreasAllSchoolsSection(IHtmlDocument page, School school)
    {
        var priorityAreasAllSchoolsSection = page.Body.SelectSingleNode("//main/div/section[3]");
        DocumentAssert.Heading2(priorityAreasAllSchoolsSection, "Your spend in priority areas for all schools");

        var header3Texts = priorityAreasAllSchoolsSection.ChildNodes
            .QuerySelectorAll("h3")
            .Select(e => string.Join(" ", e.ChildNodes.Select(n => n.TextContent.Trim())).Trim());
        Assert.Equal(["Teaching and Teaching support staff", "Administrative supplies", "Non-educational support staff"], header3Texts);

        var links = priorityAreasAllSchoolsSection.ChildNodes.QuerySelectorAll("a");
        Assert.Equal(2, links.Length);
        Assert.Equal(Paths.SchoolComparators(school.URN), links.First().GetAttribute("href"));
        Assert.Equal(Paths.SchoolComparison(school.URN), links.Last().GetAttribute("href"));
    }

    private static void AssertPriorityAreasOtherSection(IHtmlDocument page, School school)
    {
        var priorityAreasOtherSection = page.Body.SelectSingleNode("//main/div/section[4]");
        DocumentAssert.Heading2(priorityAreasOtherSection, "Other top spending priorities for your school");

        var header3Texts = priorityAreasOtherSection.ChildNodes
            .QuerySelectorAll("h3")
            .Select(e => string.Join(" ", e.ChildNodes.Select(n => n.TextContent.Trim())).Trim());
        Assert.Equal(["Educational ICT", "Catering staff and supplies", "Premises staff and services"], header3Texts);

        var links = priorityAreasOtherSection.ChildNodes.QuerySelectorAll("a");
        Assert.Equal(2, links.Length);
        Assert.Equal(Paths.SchoolComparators(school.URN), links.First().GetAttribute("href"));
        Assert.Equal(Paths.SchoolComparison(school.URN), links.Last().GetAttribute("href"));
    }

    private static void AssertPupilWorkforceMetrics(IHtmlDocument page, School school, Census census)
    {
        var pupilWorkforceMetricsSection = page.Body.SelectSingleNode("//main/div/section[5]");
        DocumentAssert.Heading2(pupilWorkforceMetricsSection, "Pupil and workforce metrics");

        var headlineFiguresTexts = pupilWorkforceMetricsSection.ChildNodes
            .QuerySelectorAll(".app-headline-figures")
            .Select(e => string.Join(" ", e.ChildNodes.Select(n => n.TextContent.Trim())).Trim());
        Assert.Equal([
            $"{census.Teachers}  Pupils per teacher",
            $"{census.SeniorLeadership}  Pupils per senior leadership role"
        ], headlineFiguresTexts);

        var link = pupilWorkforceMetricsSection.ChildNodes.QuerySelector("a");
        Assert.NotNull(link);
        Assert.Equal(Paths.SchoolCensus(school.URN), link.GetAttribute("href"));
    }

    private static void AssertNextStepsSection(IHtmlDocument page, School school)
    {
        var nextStepsSection = page.Body.SelectSingleNode("//main/div/section[6]");
        DocumentAssert.Heading2(nextStepsSection, "Next steps");

        var link = nextStepsSection.ChildNodes.QuerySelector("a");
        Assert.NotNull(link);
        Assert.Equal(Paths.SchoolHome(school.URN), link.GetAttribute("href"));
    }

    private static void AssertWhoYouAreComparedWithSection(IHtmlDocument page)
    {
        var whoYouAreComparedWithSection = page.Body.SelectSingleNode("//main/div/section[7]");
        DocumentAssert.Heading2(whoYouAreComparedWithSection, "Who you are compared with");
    }

    private static void AssertWarning(IHtmlDocument page, School school, string commentary)
    {
        var body = page.Body.SelectSingleNode("//main/div");
        var warning = body.ChildNodes.QuerySelectorAll("div").ElementAt(2).GetInnerText();

        Assert.Contains(commentary, warning);

        var link = body.ChildNodes.QuerySelector("a");
        Assert.NotNull(link);
        Assert.Equal(Paths.SchoolHome(school.URN), link.GetAttribute("href"));
    }
}