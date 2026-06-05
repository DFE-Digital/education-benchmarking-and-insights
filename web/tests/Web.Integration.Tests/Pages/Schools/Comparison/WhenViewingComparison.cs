using System.Net;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App;
using Web.App.Domain;
using Web.App.Domain.Charts;
using Web.App.Extensions;
using Web.App.Infrastructure.Apis.Insight;
using Xunit;

namespace Web.Integration.Tests.Pages.Schools.Comparison;

public class WhenViewingComparison(SchoolBenchmarkingWebAppClient client)
    : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    #region Tests

    [Theory]
    [InlineData(EstablishmentTypes.Academies, true, true, true)]
    [InlineData(EstablishmentTypes.Academies, true, true, false)]
    [InlineData(EstablishmentTypes.Academies, true, false, true)]
    [InlineData(EstablishmentTypes.Academies, true, false, false)]
    [InlineData(EstablishmentTypes.Academies, false, true, true)]
    [InlineData(EstablishmentTypes.Academies, false, true, false)]
    [InlineData(EstablishmentTypes.Academies, false, false, true)]
    [InlineData(EstablishmentTypes.Academies, false, false, false)]
    [InlineData(EstablishmentTypes.Maintained, true, true, false)]
    [InlineData(EstablishmentTypes.Maintained, true, true, true)]
    [InlineData(EstablishmentTypes.Maintained, true, false, true)]
    [InlineData(EstablishmentTypes.Maintained, true, false, false)]
    [InlineData(EstablishmentTypes.Maintained, false, true, true)]
    [InlineData(EstablishmentTypes.Maintained, false, true, false)]
    [InlineData(EstablishmentTypes.Maintained, false, false, true)]
    [InlineData(EstablishmentTypes.Maintained, false, false, false)]
    public async Task CanDisplay(string financeType, bool ks4ProgressBandingEnabled, bool filtersEnabled, bool hasProgressIndicators)
    {
        var (page, school, expenditure) = await SetupNavigateInitPage(financeType, ks4ProgressBandingEnabled, filtersEnabled, hasProgressIndicators);

        AssertPageLayout(page, school, expenditure, ks4ProgressBandingEnabled, filtersEnabled, hasProgressIndicators);
    }

    [Theory]
    [InlineData(true, true, true, false)]
    [InlineData(true, true, false, true)]
    [InlineData(true, true, false, false)]
    [InlineData(true, false, true, false)]
    [InlineData(true, false, false, true)]
    [InlineData(true, false, false, false)]
    [InlineData(false, false, true, false)]
    [InlineData(false, false, false, true)]
    [InlineData(false, false, false, false)]

    public async Task CanDisplayCorrectComparatorSetDetails(
        bool ks4ProgressBandingEnabled,
        bool filtersEnabled,
        bool withCustomUserData,
        bool withUserDefinedUserData)
    {
        var (page, school, expenditure) = await SetupNavigateInitPage(
            EstablishmentTypes.Maintained,
            ks4ProgressBandingEnabled,
            filtersEnabled,
            true,
            withCustomUserData,
            withUserDefinedUserData);

        AssertPageLayout(
            page,
            school,
            expenditure,
            ks4ProgressBandingEnabled,
            filtersEnabled,
            true,
            withCustomUserData,
            withUserDefinedUserData);
    }

    [Theory]
    [InlineData(true, true)]
    [InlineData(true, false)]
    [InlineData(false, false)]
    [InlineData(false, true)]
    public async Task CanDisplayCorrectComparatorSetDetailsWhenMissingComparatorSet(bool ks4ProgressBandingEnabled, bool filtersEnabled)
    {
        var (page, school, expenditure) = await SetupNavigateInitPage(
            EstablishmentTypes.Maintained,
            ks4ProgressBandingEnabled,
            filtersEnabled,
            withEmptyComparatorSet: true);

        var comparatorSetDetailsElement = page.QuerySelector("[data-testid=comparator-set-details]"); ;
        Assert.NotNull(comparatorSetDetailsElement);

        AssertMissingComparatorSetCreatedComparatorSetDetailsContent(comparatorSetDetailsElement, school, ks4ProgressBandingEnabled, filtersEnabled);
    }

    [Theory]
    [InlineData(EstablishmentTypes.Academies)]
    [InlineData(EstablishmentTypes.Maintained)]
    public async Task CanNavigateToCurriculumPlanning(string financeType)
    {
        var (page, school, expenditure) = await SetupNavigateInitPage(financeType);

        var liElements = page.QuerySelectorAll("ul.app-links > li");
        var anchor = liElements[0].QuerySelector("h3 > a");
        Assert.NotNull(anchor);

        var newPage = await Client.Follow(anchor);

        DocumentAssert.AssertPageUrl(newPage, Paths.SchoolFinancialPlanning(school.URN).ToAbsolute());
    }

    [Theory]
    [InlineData(EstablishmentTypes.Academies)]
    [InlineData(EstablishmentTypes.Maintained)]
    public async Task CanNavigateToCensusBenchmark(string financeType)
    {
        var (page, school, expenditure) = await SetupNavigateInitPage(financeType);

        var liElements = page.QuerySelectorAll("ul.app-links > li");
        var anchor = liElements[1].QuerySelector("h3 > a");
        Assert.NotNull(anchor);

        var newPage = await Client.Follow(anchor);

        DocumentAssert.AssertPageUrl(newPage, Paths.SchoolCensus(school.URN).ToAbsolute());
    }

    [Theory]
    [InlineData(EstablishmentTypes.Academies)]
    [InlineData(EstablishmentTypes.Maintained)]
    public async Task CanNavigateToCustomData(string financeType)
    {
        var (page, school, expenditure) = await SetupNavigateInitPage(financeType);

        var anchor = page.QuerySelector("#custom-data-link");
        Assert.NotNull(anchor);

        var newPage = await Client.Follow(anchor);

        DocumentAssert.AssertPageUrl(newPage, Paths.SchoolCustomData(school.URN).ToAbsolute());
    }

    [Fact]
    public async Task CanDisplayNotFound()
    {
        const string urn = "123456";
        var page = await Client.SetupEstablishmentWithNotFound()
            .Navigate(Paths.SchoolComparison(urn));

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.SchoolComparison(urn).ToAbsolute(), HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string urn = "123456";
        var page = await Client.SetupEstablishmentWithException()
            .Navigate(Paths.SchoolComparison(urn));

        PageAssert.IsProblemPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.SchoolComparison(urn).ToAbsolute(),
            HttpStatusCode.InternalServerError);
    }

    [Theory]
    [InlineData(EstablishmentTypes.Academies,0, "?viewAs=0&resultAs=0")]
    [InlineData(EstablishmentTypes.Academies,1, "?viewAs=1&resultAs=0")]
    [InlineData(EstablishmentTypes.Maintained,0, "?viewAs=0&resultAs=0")]
    [InlineData(EstablishmentTypes.Maintained,1, "?viewAs=1&resultAs=0")]
    public async Task CanSubmitOptionsForViewAs(string financeType, int viewAs, string expectedQueryParams)
    {
        var (page, school, expenditure) = await SetupNavigateInitPage(financeType);

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
            expenditure,
            viewAs: viewAs,
            expectedQueryParams: expectedQueryParams,
            filtersEnabled: true);
    }

    [Theory]
    [InlineData(EstablishmentTypes.Academies, 0, "?viewAs=0&resultAs=0")]
    [InlineData(EstablishmentTypes.Academies,1, "?viewAs=0&resultAs=1")]
    [InlineData(EstablishmentTypes.Academies,2, "?viewAs=0&resultAs=2")]
    [InlineData(EstablishmentTypes.Academies,3, "?viewAs=0&resultAs=3")]
    [InlineData(EstablishmentTypes.Maintained, 0, "?viewAs=0&resultAs=0")]
    [InlineData(EstablishmentTypes.Maintained,1, "?viewAs=0&resultAs=1")]
    [InlineData(EstablishmentTypes.Maintained,2, "?viewAs=0&resultAs=2")]
    [InlineData(EstablishmentTypes.Maintained,3, "?viewAs=0&resultAs=3")]
    public async Task CanSubmitOptionsForResultsAs(string financeType, int resultAs, string expectedQueryParams)
    {
        var (page, school, expenditure) = await SetupNavigateInitPage(financeType);

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
            expenditure,
            resultAs: resultAs,
            expectedQueryParams: expectedQueryParams,
            filtersEnabled: true);
    }

    [Theory]
    [InlineData(EstablishmentTypes.Academies)]
    [InlineData(EstablishmentTypes.Maintained)]

    public async Task CanDisplayChartWarningWhenChartApiFails(string financeType)
    {
        var (page, school, expenditure) = await SetupNavigateInitPage(financeType, chartApiError: true);

        AssertPageLayout(
            page,
            school,
            expenditure,
            viewAs: (int)Views.ViewAsOptions.Chart,
            chartApiError: true);
    }
    #endregion

    [Theory]
    [InlineData(EstablishmentTypes.Academies, 1, "?selectedSubCategories=1&viewAs=0&resultAs=0")]
    [InlineData(EstablishmentTypes.Academies, 10, "?selectedSubCategories=10&viewAs=0&resultAs=0")]
    [InlineData(EstablishmentTypes.Maintained, 1, "?selectedSubCategories=1&viewAs=0&resultAs=0")]
    [InlineData(EstablishmentTypes.Maintained, 10, "?selectedSubCategories=10&viewAs=0&resultAs=0")]
    public async Task CanSubmitFilterOptionsForSubCategories(string financeType, int expectedSubCategoryId, string expectedQueryParams)
    {
        var (page, school, expenditures) = await SetupNavigateInitPage(financeType);

        var action = page.QuerySelectorAll("button").FirstOrDefault(x => x.TextContent.Trim() == "Apply filters");
        Assert.NotNull(action);

        page = await Client.SubmitForm(page.Forms[0], action, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                { "SelectedSubCategories", expectedSubCategoryId.ToString() }
            });
        });

        AssertPageLayout(
            page,
            school,
            expenditures,
            expectedSubCategories: BuildExpectedSubCategories(expectedSubCategoryId),
            expectedQueryParams: expectedQueryParams);
    }
    #region Methods

    private async Task<(IHtmlDocument page, School school, SchoolExpenditure[] expenditures)> SetupNavigateInitPage(
        string financeType,
        bool ks4ProgressBandingEnabled = true,
        bool filtersEnabled = true,
        bool hasProgressIndicators = true,
        bool withCustomUserData = false,
        bool withUserDefinedUserData = false,
        bool withEmptyComparatorSet = false,
        bool chartApiError = false)
    {
        var school = Fixture.Build<School>()
            .With(x => x.URN, "123456")
            .With(x => x.FinanceType, financeType)
            .With(x => x.TrustCompanyNumber, financeType == EstablishmentTypes.Academies ? "87654321" : string.Empty)
            .Create();

        var characteristics = Fixture.Build<SchoolCharacteristic>()
            .With(x => x.URN, "123456")
            .With(x => x.KS4ProgressBanding, hasProgressIndicators ? "Well above average" : "Below average")
            .CreateMany()
            .ToArray();

        var comparatorSet = Fixture.Build<SchoolComparatorSet>()
            .With(x => x.Building, ["building"])
            .Create();

        var emptyComparatorSet = Fixture.Build<SchoolComparatorSet>()
            .Without(x => x.Building)
            .Without(x => x.Pupil)
            .Create();

        var expenditures = Fixture.Build<SchoolExpenditure>()
            .With(f => f.PeriodCoveredByReturn, 12)
            .CreateMany(3)
            .ToArray();

        var customUserData = new[]
        {
            new UserData
            {
                Type = "custom-data",
                Id = "123",
                Status = "complete"
            }
        };

        var userDefinedSetUserData = new[]
        {
            new UserData
            {
                Type = "comparator-set",
                Id = "456"
            }
        };

        var disabledFeatures = new List<string>();
        if (!ks4ProgressBandingEnabled)
        {
            disabledFeatures.Add(FeatureFlags.KS4ProgressBanding);
        }
        if (!filtersEnabled)
        {
            disabledFeatures.Add(FeatureFlags.SchoolComparisonFilter);
        }

        var horizontalBarChart = new ChartResponse
        {
            Html = "<svg />"
        };

        var client = Client.SetupDisableFeatureFlags(disabledFeatures.ToArray())
            .SetupEstablishment(school)
            .SetupSchool(school)
            .SetupInsights()
            .SetupSchoolInsight(characteristics)
            .SetupExpenditure(school, expenditures: expenditures)
            .SetupUserData(withUserDefinedUserData
                ? userDefinedSetUserData
                : withCustomUserData
                    ? customUserData : null)
            .SetupComparatorSet(school, withEmptyComparatorSet ? emptyComparatorSet : comparatorSet)
            .SetupChartRendering<SchoolComparisonDatum>(horizontalBarChart);

        if (chartApiError)
        {
            Client.SetupChartRenderingWithException<SchoolComparisonDatum>();
        }

        var page = await client.Navigate(Paths.SchoolComparison(school.URN));

        return (page, school, expenditures);
    }

    private static void AssertPageLayout(
        IHtmlDocument page,
        School school,
        SchoolExpenditure[] expenditures,
        bool ks4ProgressBandingEnabled = true,
        bool filtersEnabled = true,
        bool hasProgressIndicators = true,
        bool withCustomUserData = false,
        bool withUserDefinedUserData = false,
        bool chartApiError = false,
        int viewAs = 0,
        int resultAs = 0,
        string expectedQueryParams = "",
        SubCategoryId[]? expectedSubCategories = null)
    {
        var expectedBreadcrumbs = new[]
        {
            ("Home", Paths.ServiceHome.ToAbsolute()),
            ("Your school", Paths.SchoolHome(school.URN).ToAbsolute())
        };

        expectedSubCategories ??= AllSubCategories;

        DocumentAssert.AssertPageUrl(page, $"{Paths.SchoolComparison(school.URN)}{expectedQueryParams}".ToAbsolute());
        DocumentAssert.Breadcrumbs(page, expectedBreadcrumbs);
        DocumentAssert.TitleAndH1(page, "Benchmark spending - Financial Benchmarking and Insights Tool - GOV.UK",
            "Benchmark spending");

        var dataSourceElement = page.QuerySelector("div[data-test-id='data-source-wrapper']");
        Assert.NotNull(dataSourceElement);
        AssertDataSource(dataSourceElement, school, ks4ProgressBandingEnabled, filtersEnabled, hasProgressIndicators);

        var comparatorSetDetailsElement = page.QuerySelector("[data-testid=comparator-set-details]");
        Assert.NotNull(comparatorSetDetailsElement);
        AssertComparatorSetDetails(comparatorSetDetailsElement, school, ks4ProgressBandingEnabled, filtersEnabled, withCustomUserData, withUserDefinedUserData, false);

        AssertComparisonComponent(page, ks4ProgressBandingEnabled, filtersEnabled);

        var toolsListSection = page.GetElementById("benchmarking-and-planning-tools");
        Assert.NotNull(toolsListSection);
        DocumentAssert.Heading2(toolsListSection, "Benchmarking and planning tools");

        var toolsLinks = toolsListSection.ChildNodes.QuerySelectorAll("ul> li > h3 > a").ToList();
        Assert.Equal(2, toolsLinks.Count);

        DocumentAssert.Link(toolsLinks[0], "Curriculum and financial planning",
            Paths.SchoolFinancialPlanning(school.URN).ToAbsolute());
        DocumentAssert.Link(toolsLinks[1], "Benchmark pupil and workforce data", Paths.SchoolCensus(school.URN).ToAbsolute());

        if (filtersEnabled)
        {
            var form = page.QuerySelector(".options-form");
            Assert.NotNull(form);

            AssertFormOptions(form, viewAs, resultAs);

            var filterSection = page.QuerySelector(".app-filter");
            Assert.NotNull(filterSection);

            AssertFilterSection(filterSection, expectedSubCategories);

        }

        if (viewAs == 0)
        {
            AssertChartSection(page, chartApiError);
        }
        else
        {
            AssertTableSection(page, expenditures);
        }
    }

    private static void AssertFormOptions(
        IElement form,
        int viewAs = 0,
        int resultsAs = 0)
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
        var resultAsContainer = form.QuerySelector("#ResultAs");
        Assert.NotNull(resultAsContainer);

        var options = resultAsContainer.QuerySelectorAll("option");
        Assert.Equal(HighNeedsDimensions.AllResultsAsOptions.Length, options.Length);

        foreach (var result in HighNeedsDimensions.AllResultsAsOptions)
        {
            var value = ((int)result).ToString();

            var option = options.FirstOrDefault(o => o.GetAttribute("value") == value);
            Assert.NotNull(option);

            var shouldBeSelected = resultsAs == (int)result;
            var isSelected = option.HasAttribute("selected");

            Assert.Equal(shouldBeSelected, isSelected);
        }
    }

    private static void AssertChartSection(IHtmlDocument page, bool chartError = false)
    {
        var charts = page.QuerySelectorAll(".costs-chart-container");
        Assert.NotNull(charts);

        foreach (var chartSection in charts)
        {
            var chartSvg = chartSection.QuerySelector(".ssr-chart");
            var chartWarning = chartSection.QuerySelector(".ssr-chart-warning");

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
    }

    private static void AssertTableSection(IHtmlDocument page, SchoolExpenditure[] expenditures)
    {
        var sections = page.QuerySelectorAll("[data-testid=\"subcategory-section\"]");
        Assert.NotEmpty(sections);

        foreach (var section in sections)
        {
            var match = Sections.Single(s => section.Id!.Contains(s.Id));

            var rows = section.QuerySelectorAll("tbody tr");
            Assert.Equal(expenditures.Length, rows.Length);

            foreach (var row in rows)
            {
                var cells = row.TableCells();
                var expected = expenditures.Single(p => p.SchoolName == cells[0]);

                Assert.Equal(cells[0], expected.SchoolName);
                Assert.Equal(cells[1], expected.LAName);
                Assert.Equal(cells[2], expected.SchoolType);
                Assert.Equal(cells[3], expected.TotalPupils.ToSimpleDisplay());
                var value = match.Select(expected);
                Assert.Equal(cells[4], value?.ToCurrency());
            }
        }
    }

    private static void AssertFilterSection(
        IElement filterSection,
        SubCategoryId[] expectedSubCategories)
    {
        var applyFiltersButton = filterSection.QuerySelectorAll("button")
            .FirstOrDefault(x => x.TextContent.Trim() == "Apply filters");
        Assert.NotNull(applyFiltersButton);

        foreach (var subCategory in AllSubCategories)
        {
            var checkbox = filterSection.QuerySelector($"input[type='checkbox'][value='{subCategory.Id}']");
            Assert.NotNull(checkbox);

            if (expectedSubCategories.Length < AllSubCategories.Length)
            {
                var shouldBeChecked = expectedSubCategories.Any(e => e.Id == subCategory.Id);
                var isChecked = checkbox.HasAttribute("checked");

                if (shouldBeChecked)
                {
                    Assert.True(isChecked);
                }
                else
                {
                    Assert.False(isChecked);
                }
            }
        }
    }

    private static void AssertComparisonComponent(
        IHtmlDocument page,
        bool ks4ProgressBandingEnabled,
        bool filtersEnabled)
    {
        if (filtersEnabled)
        {
            // TODO: implement this test with layout assertions once the filters/options etc are implemented
            //AssertFilteredComparisonComponent(page);
            return;
        }

        var id = ks4ProgressBandingEnabled
            ? "compare-your-costs-2"
            : "compare-your-costs";

        var comparisonComponent = page.GetElementById(id);
        Assert.NotNull(comparisonComponent);
    }

    private static void AssertDataSource(IElement dataSourceElement, School school, bool ks4ProgressBandingEnabled, bool filtersEnabled, bool hasProgressIndicators)
    {
        var isAcademy = school.IsPartOfTrust;
        var urn = school.URN;
        Assert.NotNull(urn);

        if (ks4ProgressBandingEnabled)
        {
            if (hasProgressIndicators)
                AssertWithIndicators(dataSourceElement, urn, isAcademy);
            else
                AssertNoIndicators(dataSourceElement, isAcademy);

            return;
        }

        if (filtersEnabled)
        {
            AssertNoIndicators(dataSourceElement, isAcademy);
            return;
        }

        AssertNoBanding(dataSourceElement, isAcademy);
    }

    private static void AssertNoBanding(IElement dataSourceElement, bool isAcademy)
    {
        const string additionalText = "Benchmark your spending against similar schools.";
        if (isAcademy)
        {
            SchoolDocumentAssert.AssertAcademyNoBanding(dataSourceElement, additionalText);
        }
        else
        {
            SchoolDocumentAssert.AssertMaintainedSchoolNoBanding(dataSourceElement, additionalText);
        }
    }

    private static void AssertNoIndicators(IElement dataSourceElement, bool isAcademy)
    {
        if (isAcademy)
        {
            if (isAcademy)
                SchoolDocumentAssert.AssertAcademyNoIndicators(dataSourceElement);
        }
        else
        {
            SchoolDocumentAssert.AssertMaintainedSchoolNoIndicators(dataSourceElement);
        }
    }

    private static void AssertWithIndicators(IElement dataSourceElement, string urn, bool isAcademy)
    {
        if (isAcademy)
        {
            SchoolDocumentAssert.AssertAcademyWithIndicators(dataSourceElement, urn);
        }
        else
        {
            SchoolDocumentAssert.AssertMaintainedSchoolWithIndicators(dataSourceElement, urn);
        }
    }

    private static void AssertComparatorSetDetails(IElement comparatorSetDetailsElement, School school, bool ks4ProgressBandingEnabled, bool filtersEnabled, bool withCustomUserData, bool withUserDefinedUserData, bool emptyComparatorSet)
    {
        if (withCustomUserData)
        {
            AssertCustomDataCreatedComparatorSetDetailsContent(comparatorSetDetailsElement, school, ks4ProgressBandingEnabled, filtersEnabled);
        }
        else if (withUserDefinedUserData)
        {
            AssertUserDefinedSetCreatedComparatorSetDetailsContent(comparatorSetDetailsElement, school, ks4ProgressBandingEnabled, filtersEnabled);
        }
        else
        {
            AssertComparatorSetDetailsDefaultContent(comparatorSetDetailsElement, school, ks4ProgressBandingEnabled, filtersEnabled);
        }
    }

    private static void AssertComparatorSetDetailsDefaultContent(IElement comparatorSetDetailsElement, School school, bool ks4ProgressBandingEnabled, bool filtersEnabled)
    {
        if (ks4ProgressBandingEnabled || filtersEnabled)
        {
            var introParagraphs = comparatorSetDetailsElement.QuerySelectorAll("p.govuk-body");
            Assert.Equal(2, introParagraphs.Length);
            Assert.Equal("To make the data easier to read, we have:", introParagraphs[0].TextContent.Trim());
            Assert.Equal("You can:", introParagraphs[1].TextContent.Trim());

            var listItems = comparatorSetDetailsElement.QuerySelectorAll("ul[data-testid='actions-list'] li");
            Assert.Equal(3, listItems.Length);

            DocumentAssert.TextEqual(listItems[0], "view the 2 sets of similar schools we've chosen to benchmark this school's spending against", true);
            DocumentAssert.Link(listItems[0].QuerySelector("a"), "view the 2 sets of similar schools we've chosen", Paths.SchoolComparators(school.URN).ToAbsolute());

            DocumentAssert.TextEqual(listItems[1], "create or save your own set of schools to benchmark against", true);
            DocumentAssert.Link(listItems[1].QuerySelector("a"), "create or save your own set of schools", Paths.SchoolComparatorsCreate(school.URN).ToAbsolute());

            DocumentAssert.TextEqual(listItems[2], "change the data for this school", true);
            DocumentAssert.Link(listItems[2].QuerySelector("a"), "change the data", Paths.SchoolCustomData(school.URN).ToAbsolute());
        }
        else
        {
            var paragraphs = comparatorSetDetailsElement.QuerySelectorAll("p.govuk-body");
            Assert.Equal(3, paragraphs.Length);

            DocumentAssert.TextEqual(paragraphs[0], "We've chosen 2 sets of similar schools to benchmark this school's spending against.", true);
            DocumentAssert.Link(paragraphs[0].QuerySelector("a"), "We've chosen 2 sets of similar schools", Paths.SchoolComparators(school.URN).ToAbsolute());

            DocumentAssert.TextEqual(paragraphs[1], "Create or save your own set of schools to benchmark against");
            DocumentAssert.Link(paragraphs[1].QuerySelector("a"), "Create or save your own set of schools to benchmark against", Paths.SchoolComparatorsCreate(school.URN).ToAbsolute());

            DocumentAssert.TextEqual(paragraphs[2], "Change the data for this school");
            DocumentAssert.Link(paragraphs[2].QuerySelector("a"), "Change the data for this school", Paths.SchoolCustomData(school.URN).ToAbsolute());
        }
    }

    private static void AssertMissingComparatorSetCreatedComparatorSetDetailsContent(IElement comparatorSetDetailsElement, School school, bool ks4ProgressBandingEnabled, bool filtersEnabled)
    {
        if (ks4ProgressBandingEnabled || filtersEnabled)
        {
            var introParagraphs = comparatorSetDetailsElement.QuerySelectorAll("p.govuk-body");
            Assert.Equal(2, introParagraphs.Length);
            DocumentAssert.TextEqual(introParagraphs[0], "There is not enough information available to create a set of similar schools.", true);
            DocumentAssert.TextEqual(introParagraphs[1], "You can:");

            var list = comparatorSetDetailsElement.QuerySelector("ul.govuk-list.govuk-list--bullet");
            Assert.NotNull(list);
            var listItems = list.QuerySelectorAll("li");
            Assert.Equal(2, listItems.Length);

            DocumentAssert.TextEqual(listItems[0], "create or save your own set of schools to benchmark against", true);
            DocumentAssert.Link(listItems[0].QuerySelector("a"), "create or save your own set of schools", Paths.SchoolComparatorsCreate(school.URN).ToAbsolute());

            DocumentAssert.TextEqual(listItems[1], "change the data for this school", true);
            DocumentAssert.Link(listItems[1].QuerySelector("a"), "change the data", Paths.SchoolCustomData(school.URN).ToAbsolute());
        }
        else
        {
            var warning = comparatorSetDetailsElement.QuerySelector("div.govuk-warning-text strong.govuk-warning-text__text");
            Assert.NotNull(warning);
            DocumentAssert.TextEqual(warning, "Warning There isn't enough information available to create a set of similar schools.", true);

            var paragraphs = comparatorSetDetailsElement.QuerySelectorAll("p.govuk-body");
            Assert.NotNull(paragraphs);
            Assert.Equal(2, paragraphs.Length);

            DocumentAssert.TextEqual(paragraphs[0], "Create or save your own set of schools to benchmark against");
            DocumentAssert.Link(paragraphs[0].QuerySelector("a"), "Create or save your own set of schools to benchmark against", Paths.SchoolComparatorsCreate(school.URN).ToAbsolute());

            DocumentAssert.TextEqual(paragraphs[1], "Change the data for this school");
            DocumentAssert.Link(paragraphs[1].QuerySelector("a"), "Change the data for this school", Paths.SchoolCustomData(school.URN).ToAbsolute());
        }
    }

    private static void AssertUserDefinedSetCreatedComparatorSetDetailsContent(IElement comparatorSetDetailsElement, School school, bool ks4ProgressBandingEnabled, bool filtersEnabled)
    {
        if (ks4ProgressBandingEnabled || filtersEnabled)
        {
            var paragraphs = comparatorSetDetailsElement.QuerySelectorAll(":scope > p.govuk-body");
            Assert.NotNull(paragraphs);
            Assert.Equal(3, paragraphs.Length);

            DocumentAssert.TextEqual(paragraphs[0], "You are now comparing with your chosen schools.");
            DocumentAssert.TextEqual(paragraphs[1], "To make the data easier to read, we have:");
            DocumentAssert.TextEqual(paragraphs[2], "Change your similar schools.");
            DocumentAssert.Link(paragraphs[2].QuerySelector("a"), "Change your similar schools.", Paths.SchoolComparatorsUserDefined(school.URN).ToAbsolute());
        }
        else
        {
            var paragraphs = comparatorSetDetailsElement.QuerySelectorAll("p.govuk-body");
            Assert.NotNull(paragraphs);
            Assert.Equal(2, paragraphs.Length);

            DocumentAssert.TextEqual(paragraphs[0], "You are now comparing with your chosen schools.");
            DocumentAssert.TextEqual(paragraphs[1], "Change your similar schools");
            DocumentAssert.Link(paragraphs[1].QuerySelector("a"), "Change your similar schools", Paths.SchoolComparatorsUserDefined(school.URN).ToAbsolute());
        }
    }

    private static void AssertCustomDataCreatedComparatorSetDetailsContent(IElement comparatorSetDetailsElement, School school, bool ks4ProgressBandingEnabled, bool filtersEnabled)
    {
        if (ks4ProgressBandingEnabled || filtersEnabled)
        {
            var paragraphs = comparatorSetDetailsElement.QuerySelectorAll(":scope > p.govuk-body");
            Assert.NotNull(paragraphs);
            Assert.Equal(3, paragraphs.Length);

            DocumentAssert.TextEqual(paragraphs[0], "The information displayed is the originally reported data, not the customised data that was provided.");
            DocumentAssert.TextEqual(paragraphs[1], "To make the data easier to read, we have:");
            DocumentAssert.TextEqual(paragraphs[2], "You can:");

            var listItems = comparatorSetDetailsElement.QuerySelectorAll("ul[data-testid='actions-list'] li");
            Assert.Equal(2, listItems.Length);

            DocumentAssert.TextEqual(listItems[0], "view the 2 sets of similar schools we've chosen to benchmark this school's spending against", true);
            DocumentAssert.Link(listItems[0].QuerySelector("a"), "view the 2 sets of similar schools we've chosen", Paths.SchoolComparators(school.URN).ToAbsolute());

            DocumentAssert.TextEqual(listItems[1], "view custom data set");
            DocumentAssert.Link(listItems[1].QuerySelector("a"), "view custom data set", Paths.SchoolCustomisedData(school.URN).ToAbsolute());
        }
        else
        {
            var paragraphs = comparatorSetDetailsElement.QuerySelectorAll("p.govuk-body");
            Assert.NotNull(paragraphs);
            Assert.Equal(3, paragraphs.Length);

            DocumentAssert.TextEqual(paragraphs[0], "We've chosen 2 sets of similar schools to benchmark this school's spending against.", true);
            DocumentAssert.Link(paragraphs[0].QuerySelector("a"), "We've chosen 2 sets of similar schools", Paths.SchoolComparators(school.URN).ToAbsolute());

            DocumentAssert.TextEqual(paragraphs[1], "The information displayed is the originally reported data, not the customised data that was provided.");

            DocumentAssert.TextEqual(paragraphs[2], "View custom data set");
            DocumentAssert.Link(paragraphs[2].QuerySelector("a"), "View custom data set", Paths.SchoolCustomisedData(school.URN).ToAbsolute());
        }
    }

    private static readonly SubCategoryId[] AllSubCategories =
        Enumerable.Range(0, 41).Select(i => new SubCategoryId(i)).ToArray();

    private record SubCategoryId(int Id);

    private static SubCategoryId[] BuildExpectedSubCategories(params int[]? ids)
    {
        if (ids is null || ids.Length == 0)
        {
            return AllSubCategories;
        }

        return ids.Select(id => AllSubCategories.First(c => c.Id == id)).ToArray();
    }

    private static readonly (string Id, Func<SchoolExpenditure, decimal?> Select)[] Sections =
    [
        ("school-subcategory-total-expenditure",
            s => s.TotalExpenditure),
        ("school-subcategory-total-teaching-and-teaching-support-staff-costs",
            s => s.TotalTeachingSupportStaffCosts),
        ("school-subcategory-teaching-staff-costs",
            s => s.TeachingStaffCosts),
        ("school-subcategory-supply-teaching-staff-costs",
            s => s.SupplyTeachingStaffCosts),
        ("school-subcategory-educational-consultancy-costs",
            s => s.EducationalConsultancyCosts),
        ("school-subcategory-educational-support-staff-costs",
            s => s.EducationSupportStaffCosts),
        ("school-subcategory-agency-supply-teaching-staff-costs",
            s => s.AgencySupplyTeachingStaffCosts),
        ("school-subcategory-total-non-educational-support-staff-costs",
            s => s.TotalNonEducationalSupportStaffCosts),
        ("school-subcategory-administrative-and-clerical-staff-costs",
            s => s.AdministrativeClericalStaffCosts),
        ("school-subcategory-auditors-costs",
            s => s.AuditorsCosts),
        ("school-subcategory-other-staff-costs",
            s => s.OtherStaffCosts),
        ("school-subcategory-professional-services-non-curriculum-costs",
            s => s.ProfessionalServicesNonCurriculumCosts),
        ("school-subcategory-total-educational-supplies-costs",
            s => s.TotalEducationalSuppliesCosts),
        ("school-subcategory-examination-fees-costs",
            s => s.ExaminationFeesCosts),
        ("school-subcategory-learning-resources-not-ict-equipment-costs",
            s => s.LearningResourcesNonIctCosts),
        ("school-subcategory-educational-learning-resources-costs",
            s => s.LearningResourcesIctCosts),
        ("school-subcategory-total-premises-staff-and-service-costs",
            s => s.TotalPremisesStaffServiceCosts),
        ("school-subcategory-cleaning-and-caretaking-costs",
            s => s.CleaningCaretakingCosts),
        ("school-subcategory-maintenance-of-premises-costs",
            s => s.MaintenancePremisesCosts),
        ("school-subcategory-other-occupation-costs",
            s => s.OtherOccupationCosts),
        ("school-subcategory-premises-staff-costs",
            s => s.PremisesStaffCosts),
        ("school-subcategory-total-utilities-costs",
            s => s.TotalUtilitiesCosts),
        ("school-subcategory-energy-costs",
            s => s.EnergyCosts),
        ("school-subcategory-water-and-sewerage-costs",
            s => s.WaterSewerageCosts),
        ("school-subcategory-administrative-supplies-non-educational",
            s => s.AdministrativeSuppliesNonEducationalCosts),
        ("school-subcategory-total-catering-costs-gross",
            s => s.TotalGrossCateringCosts),
        ("school-subcategory-total-catering-costs-net",
            s => s.TotalNetCateringCosts),
        ("school-subcategory-catering-staff-costs",
            s => s.CateringStaffCosts),
        ("school-subcategory-catering-supplies-costs",
            s => s.CateringSuppliesCosts),
        ("school-subcategory-total-other-costs",
            s => s.TotalOtherCosts),
        ("school-subcategory-other-insurance-premiums-costs",
            s => s.OtherInsurancePremiumsCosts),
        ("school-subcategory-grounds-maintenance-costs",
            s => s.GroundsMaintenanceCosts),
        ("school-subcategory-indirect-employee-expenses",
            s => s.IndirectEmployeeExpenses),
        ("school-subcategory-interest-charges-for-loan-and-bank",
            s => s.InterestChargesLoanBank),
        ("school-subcategory-pfi-charges",
            s => s.PrivateFinanceInitiativeCharges),
        ("school-subcategory-rent-and-rates-costs",
            s => s.RentRatesCosts),
        ("school-subcategory-special-facilities-costs",
            s => s.SpecialFacilitiesCosts),
        ("school-subcategory-staff-development-and-training-costs",
            s => s.StaffDevelopmentTrainingCosts),
        ("school-subcategory-staff-related-insurance-costs",
            s => s.StaffRelatedInsuranceCosts),
        ("school-subcategory-supply-teacher-insurance-costs",
            s => s.SupplyTeacherInsurableCosts),
        ("school-subcategory-community-focused-school-staff-maintained-schools-only",
            s => s.CommunityFocusedSchoolStaff),
        ("school-subcategory-community-focused-school-costs-maintained-schools-only",
            s => s.CommunityFocusedSchoolCosts)
    ];

    #endregion
}
