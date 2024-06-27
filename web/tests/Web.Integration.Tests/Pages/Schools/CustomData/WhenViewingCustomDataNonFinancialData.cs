using System.Net;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App.Domain;
using Web.App.Extensions;
using Web.App.ViewModels;
using Xunit;
namespace Web.Integration.Tests.Pages.Schools.CustomData;

public class WhenViewingCustomDataNonFinancialData : PageBase<SchoolBenchmarkingWebAppClient>
{
    private readonly Census _census;
    private readonly Census _customCensus;
    private readonly SchoolCharacteristic _customFloorAreaMetric;
    private readonly SchoolExpenditure _expenditure;
    private readonly SchoolCharacteristic _floorAreaMetric;
    private readonly Dictionary<string, decimal?> _formValues;
    private readonly SchoolIncome _income;

    public WhenViewingCustomDataNonFinancialData(SchoolBenchmarkingWebAppClient client) : base(client)
    {
        _expenditure = Fixture.Build<SchoolExpenditure>()
            .Create();

        _income = Fixture.Build<SchoolIncome>()
            .Create();

        _expenditure = Fixture.Build<SchoolExpenditure>()
            .Create();

        _floorAreaMetric = Fixture.Build<SchoolCharacteristic>()
            .Create();

        _customFloorAreaMetric = Fixture.Build<SchoolCharacteristic>()
            .Create();

        _census = Fixture.Build<Census>()
            .Create();

        _customCensus = Fixture.Build<Census>()
            .Create();

        _formValues = new Dictionary<string, decimal?>
        {
            {
                nameof(NonFinancialDataCustomDataViewModel.NumberOfPupilsFte), _customCensus.TotalPupils
            },
            {
                nameof(NonFinancialDataCustomDataViewModel.FreeSchoolMealPercent), _customFloorAreaMetric.PercentFreeSchoolMeals
            },
            {
                nameof(NonFinancialDataCustomDataViewModel.SpecialEducationalNeedsPercent), _customFloorAreaMetric.PercentSpecialEducationNeeds
            },
            {
                nameof(NonFinancialDataCustomDataViewModel.FloorArea), _customFloorAreaMetric.TotalInternalFloorArea
            }
        };
    }

    [Fact]
    public async Task CanDisplay()
    {
        var (page, school) = await SetupNavigateInitPage();

        AssertPageLayout(page, school);
    }

    [Fact]
    public async Task CanSubmitValidCustomValues()
    {
        var (page, school) = await SetupNavigateInitPage();
        AssertPageLayout(page, school);
        var action = page.QuerySelector(".govuk-button");
        Assert.NotNull(action);

        page = await Client.SubmitForm(page.Forms[0], action, f =>
        {
            f.SetFormValues(_formValues.ToDictionary(k => k.Key, v => v.Value?.ToString() ?? string.Empty));
        });

        DocumentAssert.AssertPageUrl(page, Paths.SchoolCustomDataWorkforceData(school.URN).ToAbsolute());
    }

    [Fact]
    public async Task CanSubmitEmptyCustomValues()
    {
        var (page, school) = await SetupNavigateInitPage();
        AssertPageLayout(page, school);
        var action = page.QuerySelector(".govuk-button");
        Assert.NotNull(action);

        page = await Client.SubmitForm(page.Forms[0], action);

        DocumentAssert.AssertPageUrl(page, Paths.SchoolCustomDataWorkforceData(school.URN).ToAbsolute());
    }

    [Fact]
    public async Task SetsAndGetsValidCustomValuesToSession()
    {
        var (page, school) = await SetupNavigateInitPage();
        AssertPageLayout(page, school);
        var action = page.QuerySelector(".govuk-button");
        Assert.NotNull(action);

        page = await Client.SubmitForm(page.Forms[0], action, f =>
        {
            f.SetFormValues(_formValues.ToDictionary(k => k.Key, v => v.Value?.ToString() ?? string.Empty));
        });

        // go forward...
        DocumentAssert.AssertPageUrl(page, Paths.SchoolCustomDataWorkforceData(school.URN).ToAbsolute());
        var anchor = page.QuerySelector(".govuk-back-link");
        page = await Client.Follow(anchor);

        // ... and then back again
        DocumentAssert.AssertPageUrl(page, Paths.SchoolCustomDataNonFinancialData(school.URN).ToAbsolute());
        var customValues = page.QuerySelectorAll("input").Not("[type='hidden']").ToList();
        Assert.Equal(4, customValues.Count);

        foreach (var customValue in customValues)
        {
            var actual = (customValue as IHtmlInputElement)?.Value;
            var field = customValue.Id ?? string.Empty;
            var expected = field switch
            {
                nameof(NonFinancialDataCustomDataViewModel.NumberOfPupilsFte) => _customCensus.TotalPupils.ToSimpleDisplay(),
                nameof(NonFinancialDataCustomDataViewModel.FreeSchoolMealPercent) => _customFloorAreaMetric.PercentFreeSchoolMeals.ToPercent().TrimEnd('%'),
                nameof(NonFinancialDataCustomDataViewModel.SpecialEducationalNeedsPercent) => _customFloorAreaMetric.PercentSpecialEducationNeeds.ToPercent().TrimEnd('%'),
                nameof(NonFinancialDataCustomDataViewModel.FloorArea) => _customFloorAreaMetric.TotalInternalFloorArea.ToSimpleDisplay(),
                _ => throw new ArgumentOutOfRangeException()
            };

            Assert.True(expected.Equals(actual), $"{field} expected to be {expected} but found {actual}");
        }
    }

    [Fact]
    public async Task ShowsErrorOnInvalidValues()
    {
        var (page, school) = await SetupNavigateInitPage();
        AssertPageLayout(page, school);
        var action = page.QuerySelector(".govuk-button");
        Assert.NotNull(action);

        page = await Client.SubmitForm(page.Forms[0], action, f =>
        {
            f.SetFormValues(_formValues.ToDictionary(k => k.Key, _ => "invalid"));
        });

        DocumentAssert.AssertPageUrl(page, Paths.SchoolCustomDataNonFinancialData(school.URN).ToAbsolute());
        DocumentAssert.SummaryErrors(
            page,
            (nameof(NonFinancialDataCustomDataViewModel.NumberOfPupilsFte), "Enter number of pupils (full time equivalent) in the correct format"),
            (nameof(NonFinancialDataCustomDataViewModel.FreeSchoolMealPercent), "Enter pupils eligible for free school meals (FSM) in the correct format"),
            (nameof(NonFinancialDataCustomDataViewModel.SpecialEducationalNeedsPercent), "Enter pupils with special educational needs (SEN) in the correct format"),
            (nameof(NonFinancialDataCustomDataViewModel.FloorArea), "Enter gross internal floor area in the correct format")
        );
    }

    [Fact]
    public async Task CanDisplayNotFound()
    {
        const string urn = "12345";
        var page = await Client.SetupEstablishmentWithNotFound()
            .Navigate(Paths.SchoolCustomDataNonFinancialData(urn));

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.SchoolCustomDataNonFinancialData(urn).ToAbsolute(), HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string urn = "12345";
        var page = await Client.SetupEstablishmentWithException()
            .Navigate(Paths.SchoolCustomDataNonFinancialData(urn));

        PageAssert.IsProblemPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.SchoolCustomDataNonFinancialData(urn).ToAbsolute(), HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task CanNavigateBack()
    {
        var (page, school) = await SetupNavigateInitPage();

        var anchor = page.QuerySelector(".govuk-back-link");
        page = await Client.Follow(anchor);

        DocumentAssert.AssertPageUrl(page, Paths.SchoolCustomDataFinancialData(school.URN).ToAbsolute());
    }

    private async Task<(IHtmlDocument page, School school)> SetupNavigateInitPage()
    {
        var school = Fixture.Build<School>()
            .With(x => x.URN, "12345")
            .Create();

        var page = await Client.SetupEstablishment(school)
            .SetupIncome(school, _income)
            .SetupCensus(school, _census)
            .SetupBalance(school)
            .SetupExpenditure(school, _expenditure)
            .SetupSchoolInsight(school, _floorAreaMetric)
            .SetupHttpContextAccessor()
            .Navigate(Paths.SchoolCustomDataNonFinancialData(school.URN));

        return (page, school);
    }

    private void AssertPageLayout(IHtmlDocument page, School school)
    {
        DocumentAssert.BackLink(page, "Back", Paths.SchoolCustomDataFinancialData(school.URN).ToAbsolute());
        DocumentAssert.TitleAndH1(page, "Customise your data - Financial Benchmarking and Insights Tool - GOV.UK", "Change non-financial data");

        var currentValues = page.QuerySelectorAll("span[id^='current-']");
        Assert.Equal(4, currentValues.Length);

        foreach (var currentValue in currentValues)
        {
            var actual = currentValue.TextContent.Trim();
            var field = currentValue.Id?.Split("-").Last() ?? string.Empty;
            var expected = field switch
            {
                nameof(NonFinancialDataCustomDataViewModel.NumberOfPupilsFte) => $"{_census.TotalPupils:#.0}",
                nameof(NonFinancialDataCustomDataViewModel.FreeSchoolMealPercent) => $"{_floorAreaMetric.PercentFreeSchoolMeals:#}%",
                nameof(NonFinancialDataCustomDataViewModel.SpecialEducationalNeedsPercent) => $"{_floorAreaMetric.PercentSpecialEducationNeeds:#}%",
                _ => $"{_floorAreaMetric.TotalInternalFloorArea:N0} square metres"
            };

            Assert.True(expected.Equals(actual), $"{field} expected to be {expected} but found {actual}");
        }
    }
}