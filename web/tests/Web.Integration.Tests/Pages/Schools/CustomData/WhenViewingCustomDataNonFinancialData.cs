using System.Net;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App.Domain;
using Web.App.ViewModels;
using Xunit;
namespace Web.Integration.Tests.Pages.Schools.CustomData;

public class WhenViewingCustomDataNonFinancialData : PageBase<SchoolBenchmarkingWebAppClient>
{
    private readonly Census _census;
    private readonly Census _customCensus;
    private readonly Finances _customFinances;
    private readonly FloorAreaMetric _customFloorAreaMetric;
    private readonly Expenditure _expenditure;
    private readonly Finances _finances;
    private readonly FloorAreaMetric _floorAreaMetric;
    private readonly Dictionary<string, decimal?> _formValues;
    private readonly Income _income;

    public WhenViewingCustomDataNonFinancialData(SchoolBenchmarkingWebAppClient client) : base(client)
    {
        _finances = Fixture.Build<Finances>()
            .Create();

        _customFinances = Fixture.Build<Finances>()
            .Create();

        _income = Fixture.Build<Income>()
            .Create();

        _expenditure = Fixture.Build<Expenditure>()
            .Create();

        _floorAreaMetric = Fixture.Build<FloorAreaMetric>()
            .Create();

        _customFloorAreaMetric = Fixture.Build<FloorAreaMetric>()
            .Create();

        _census = Fixture.Build<Census>()
            .Create();

        _customCensus = Fixture.Build<Census>()
            .Create();

        _formValues = new Dictionary<string, decimal?>
        {
            {
                nameof(NonFinancialDataCustomDataViewModel.NumberOfPupilsFte), _customCensus.NumberOfPupils
            },
            {
                nameof(NonFinancialDataCustomDataViewModel.FreeSchoolMealPercent), _customFinances.FreeSchoolMealPercent
            },
            {
                nameof(NonFinancialDataCustomDataViewModel.SpecialEducationalNeedsPercent), _customFinances.SpecialEducationalNeedsPercent
            },
            {
                nameof(NonFinancialDataCustomDataViewModel.FloorArea), _customFloorAreaMetric.FloorArea
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

        DocumentAssert.AssertPageUrl(page, Paths.SchoolCustomDataWorkforceData(school.Urn).ToAbsolute());
    }

    [Fact]
    public async Task CanSubmitEmptyCustomValues()
    {
        var (page, school) = await SetupNavigateInitPage();
        AssertPageLayout(page, school);
        var action = page.QuerySelector(".govuk-button");
        Assert.NotNull(action);

        page = await Client.SubmitForm(page.Forms[0], action);

        DocumentAssert.AssertPageUrl(page, Paths.SchoolCustomDataWorkforceData(school.Urn).ToAbsolute());
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
        DocumentAssert.AssertPageUrl(page, Paths.SchoolCustomDataWorkforceData(school.Urn).ToAbsolute());
        var anchor = page.QuerySelector(".govuk-back-link");
        page = await Client.Follow(anchor);

        // ... and then back again
        DocumentAssert.AssertPageUrl(page, Paths.SchoolCustomDataNonFinancialData(school.Urn).ToAbsolute());
        var customValues = page.QuerySelectorAll("input").Not("[type='hidden']").ToList();
        Assert.Equal(4, customValues.Count);

        foreach (var customValue in customValues)
        {
            var actual = (customValue as IHtmlInputElement)?.Value;
            var field = customValue.Id ?? string.Empty;
            var expected = field switch
            {
                nameof(NonFinancialDataCustomDataViewModel.NumberOfPupilsFte) => _customCensus.NumberOfPupils.ToString("#.0"),
                nameof(NonFinancialDataCustomDataViewModel.FreeSchoolMealPercent) => _customFinances.FreeSchoolMealPercent.ToString("#.0"),
                nameof(NonFinancialDataCustomDataViewModel.SpecialEducationalNeedsPercent) => _customFinances.SpecialEducationalNeedsPercent.ToString("#.0"),
                _ => _customFloorAreaMetric.FloorArea.ToString()
            };

            Assert.True(expected?.Equals(actual), $"{field} expected to be {expected} but found {actual}");
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

        DocumentAssert.AssertPageUrl(page, Paths.SchoolCustomDataNonFinancialData(school.Urn).ToAbsolute());
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

        DocumentAssert.AssertPageUrl(page, Paths.SchoolCustomDataFinancialData(school.Urn).ToAbsolute());
    }

    private async Task<(IHtmlDocument page, School school)> SetupNavigateInitPage()
    {
        var school = Fixture.Build<School>()
            .With(x => x.Urn, "12345")
            .Create();

        var page = await Client.SetupEstablishment(school)
            .SetupInsights(school, _finances, _expenditure, _floorAreaMetric)
            .SetupIncome(school, _income)
            .SetupCensus(school, _census)
            .SetupHttpContextAccessor()
            .Navigate(Paths.SchoolCustomDataNonFinancialData(school.Urn));

        return (page, school);
    }

    private void AssertPageLayout(IHtmlDocument page, School school)
    {
        DocumentAssert.BackLink(page, "Back", Paths.SchoolCustomDataFinancialData(school.Urn).ToAbsolute());
        DocumentAssert.TitleAndH1(page, "Customise your data - Financial Benchmarking and Insights Tool - GOV.UK", "Change non-financial data");

        var currentValues = page.QuerySelectorAll("span[id^='current-']");
        Assert.Equal(4, currentValues.Length);

        foreach (var currentValue in currentValues)
        {
            var actual = currentValue.TextContent.Trim();
            var field = currentValue.Id?.Split("-").Last() ?? string.Empty;
            var expected = field switch
            {
                nameof(NonFinancialDataCustomDataViewModel.NumberOfPupilsFte) => _census.NumberOfPupils.ToString("#.0"),
                nameof(NonFinancialDataCustomDataViewModel.FreeSchoolMealPercent) => $"{_finances.FreeSchoolMealPercent:#}%",
                nameof(NonFinancialDataCustomDataViewModel.SpecialEducationalNeedsPercent) => $"{_finances.SpecialEducationalNeedsPercent:#}%",
                _ => $"{_floorAreaMetric.FloorArea} square metres"
            };

            Assert.True(expected?.Equals(actual), $"{field} expected to be {expected} but found {actual}");
        }
    }
}