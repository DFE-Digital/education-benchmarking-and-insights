using System.Text.RegularExpressions;
using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App.Domain;
using Web.App.Extensions;
using Xunit;

namespace Web.Integration.Tests.Pages.Schools.Comparators;

public partial class WhenViewingComparators(SchoolBenchmarkingWebAppClient client)
    : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task CanDisplay(bool hasUserData)
    {
        var (page, school, _, _) = await SetupNavigateInitPage(hasUserData: hasUserData);
        AssertPageLayout(page, school, hasUserData);
    }

    [Theory]
    [InlineData(OverallPhaseTypes.Primary)]
    [InlineData(OverallPhaseTypes.Special)]
    public async Task CanDisplayRunningCostCategoriesTab(string overallPhase)
    {
        var (page, school, comparatorSet, characteristics) = await SetupNavigateInitPage(overallPhase);
        AssertRunningCostCategoriesTab(page, school, comparatorSet, characteristics);
    }

    [Fact]
    public async Task CanDisplayBuildingCostCategoriesTab()
    {
        var (page, school, comparatorSet, characteristics) = await SetupNavigateInitPage();
        AssertBuildingCostCategories(page, school, comparatorSet, characteristics);
    }

    private async Task<(IHtmlDocument page, School school, SchoolComparatorSet comparatorSet, SchoolCharacteristic[] characteristics)> SetupNavigateInitPage(string overallPhase = OverallPhaseTypes.Primary, bool hasUserData = false)
    {
        var school = Fixture.Build<School>()
            .With(x => x.URN, "123456")
            .With(x => x.OverallPhase, overallPhase)
            .Create();

        const int comparators = 3;
        var comparatorSet = Fixture.Build<SchoolComparatorSet>()
            .With(c => c.Pupil, Fixture.CreateMany<string>(comparators).ToArray())
            .With(c => c.Building, Fixture.CreateMany<string>(comparators).ToArray())
            .Create();
        var characteristics = Fixture.Build<SchoolCharacteristic>()
            .CreateMany(comparators)
            .ToArray();
        characteristics.First().URN = school.URN;

        UserData[]? userData = null;
        if (hasUserData)
        {
            var customData = Fixture.Build<UserData>()
                .With(u => u.Type, "custom-data")
                .Create();
            userData = [customData];
        }

        var page = await Client
            .SetupEstablishment(school)
            .SetupSchoolInsight(characteristics)
            .SetupUserData(userData)
            .SetupComparatorSet(school, comparatorSet)
            .Navigate(Paths.SchoolComparators(school.URN));

        return (page, school, comparatorSet, characteristics);
    }

    private static void AssertPageLayout(IHtmlDocument page, School school, bool hasUserData)
    {
        DocumentAssert.TitleAndH1(page,
            "Schools we chose for benchmarking - Financial Benchmarking and Insights Tool - GOV.UK",
            "Schools we chose for benchmarking");

        var action = page.QuerySelector("a.govuk-link:contains('Choose your own set of schools')");

        if (hasUserData)
        {
            Assert.Null(action);
        }
        else
        {
            DocumentAssert.Link(action, "Choose your own set of schools", Paths.SchoolComparatorsCreate(school.URN).ToAbsolute());
        }
    }

    private static void AssertRunningCostCategoriesTab(IHtmlDocument page, School school, SchoolComparatorSet comparatorSet, SchoolCharacteristic[] characteristics)
    {
        var tab = page.QuerySelector("#running");
        Assert.NotNull(tab);

        var intro = tab.QuerySelectorAll(".govuk-body").FirstOrDefault();
        Assert.NotNull(intro);
        Assert.Contains($"We've chosen {comparatorSet.Pupil.Length} similar schools", intro.TextContent);

        var howList = tab.QuerySelectorAll(".govuk-list").ElementAtOrDefault(0);
        Assert.NotNull(howList);
        var howListItems = howList.QuerySelectorAll("li");
        Assert.Equal(OverallPhaseTypes.SendCharacteristicsPhases.Contains(school.OverallPhase) ? 5 : 3, howListItems.Length);

        var senList = tab.QuerySelectorAll(".govuk-list").ElementAtOrDefault(1);
        if (OverallPhaseTypes.SendCharacteristicsPhases.Contains(school.OverallPhase))
        {
            Assert.NotNull(senList);
            var senListItems = senList.QuerySelectorAll("li");
            Assert.Equal(4, senListItems.Length);
        }
        else
        {
            Assert.Null(senList);
        }

        var table = tab.QuerySelector(".govuk-table");
        Assert.NotNull(table);
        var tableRows = table.QuerySelectorAll("tbody > tr");
        Assert.Equal(characteristics.Length, tableRows.Length);

        for (var i = 0; i < characteristics.Length; i++)
        {
            var comparator = characteristics.ElementAt(i);
            var actualRow = tableRows.ElementAt(i);

            Assert.Equal(
                $"{comparator.SchoolName} {(comparator.URN == school.URN ? "(Your chosen school)" : comparator.Address)} {comparator.OverallPhase} {comparator.TotalPupils} {comparator.PercentSpecialEducationNeeds}% {comparator.PercentFreeSchoolMeals}%",
                MultipleWhitespaceRegex().Replace(actualRow.TextContent.Trim(), " "));
        }
    }

    private static void AssertBuildingCostCategories(IHtmlDocument page, School school, SchoolComparatorSet comparatorSet, SchoolCharacteristic[] characteristics)
    {
        var tab = page.QuerySelector("#building");
        Assert.NotNull(tab);

        var intro = tab.QuerySelectorAll(".govuk-body").FirstOrDefault();
        Assert.NotNull(intro);
        Assert.Contains($"We've chosen {comparatorSet.Building.Length} similar schools", intro.TextContent);

        var howList = tab.QuerySelectorAll(".govuk-list").ElementAtOrDefault(0);
        Assert.NotNull(howList);
        var howListItems = howList.QuerySelectorAll("li");
        Assert.Equal(5, howListItems.Length);

        var table = tab.QuerySelector(".govuk-table");
        Assert.NotNull(table);
        var tableRows = table.QuerySelectorAll("tbody > tr");
        Assert.Equal(characteristics.Length, tableRows.Length);

        for (var i = 0; i < characteristics.Length; i++)
        {
            var comparator = characteristics.ElementAt(i);
            var actualRow = tableRows.ElementAt(i);

            Assert.Equal(
                $"{comparator.SchoolName} {(comparator.URN == school.URN ? "(Your chosen school)" : comparator.Address)} {comparator.OverallPhase} {comparator.TotalPupils} {comparator.TotalInternalFloorArea:F1} {comparator.BuildingAverageAge.ToAge()}",
                MultipleWhitespaceRegex().Replace(actualRow.TextContent.Trim(), " "));
        }
    }

    [GeneratedRegex("\\s{2,}")]
    private static partial Regex MultipleWhitespaceRegex();
}