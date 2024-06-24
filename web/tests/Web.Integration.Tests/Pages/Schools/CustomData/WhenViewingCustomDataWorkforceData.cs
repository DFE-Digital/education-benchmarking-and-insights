using System.Net;
using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App.Domain;
using Web.App.ViewModels;
using Xunit;
namespace Web.Integration.Tests.Pages.Schools.CustomData;

public class WhenViewingCustomDataWorkforceData : PageBase<SchoolBenchmarkingWebAppClient>
{
    private readonly Census _census;
    private readonly SchoolExpenditure _expenditure;
    private readonly SchoolCharacteristic _floorAreaMetric;
    private readonly Dictionary<string, decimal?> _formValues;
    private readonly SchoolIncome _income;

    public WhenViewingCustomDataWorkforceData(SchoolBenchmarkingWebAppClient client) : base(client)
    {
        _income = Fixture.Build<SchoolIncome>()
            .Create();

        _expenditure = Fixture.Build<SchoolExpenditure>()
            .Create();

        _floorAreaMetric = Fixture.Build<SchoolCharacteristic>()
            .Create();

        _census = Fixture.Build<Census>()
            .Create();

        var customCensus = Fixture.Build<Census>()
            .With(c => c.WorkforceFTE, Fixture.CreateDecimal(101, 200))
            .With(c => c.TeachersFTE, Fixture.CreateDecimal(51, 90))
            .With(c => c.PercentTeacherWithQualifiedStatus, Fixture.CreateDecimal(91, 100))
            .With(c => c.SeniorLeadershipFTE, Fixture.CreateDecimal(0, 50))
            .Create();

        _formValues = new Dictionary<string, decimal?>
        {
            {
                nameof(WorkforceDataCustomDataViewModel.WorkforceFte), customCensus.WorkforceFTE
            },
            {
                nameof(WorkforceDataCustomDataViewModel.TeachersFte), customCensus.TeachersFTE
            },
            {
                nameof(WorkforceDataCustomDataViewModel.QualifiedTeacherPercent), customCensus.PercentTeacherWithQualifiedStatus
            },
            {
                nameof(WorkforceDataCustomDataViewModel.SeniorLeadershipFte), customCensus.SeniorLeadershipFTE
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

        DocumentAssert.AssertPageUrl(page, Paths.SchoolCustomData(school.URN).ToAbsolute());
    }

    [Fact]
    public async Task CanSubmitEmptyCustomValues()
    {
        var (page, school) = await SetupNavigateInitPage();
        AssertPageLayout(page, school);
        var action = page.QuerySelector(".govuk-button");
        Assert.NotNull(action);

        page = await Client.SubmitForm(page.Forms[0], action);

        DocumentAssert.AssertPageUrl(page, Paths.SchoolCustomData(school.URN).ToAbsolute());
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

        DocumentAssert.AssertPageUrl(page, Paths.SchoolCustomDataWorkforceData(school.URN).ToAbsolute());
        DocumentAssert.SummaryErrors(
            page,
            (nameof(WorkforceDataCustomDataViewModel.WorkforceFte), "Enter school workforce (full time equivalent) in the correct format"),
            (nameof(WorkforceDataCustomDataViewModel.TeachersFte), "Enter number of teachers (full time equivalent) in the correct format"),
            (nameof(WorkforceDataCustomDataViewModel.QualifiedTeacherPercent), "Enter teachers with qualified teacher status in the correct format"),
            (nameof(WorkforceDataCustomDataViewModel.SeniorLeadershipFte), "Enter senior leadership (full time equivalent) in the correct format")
        );
    }

    [Fact]
    public async Task CanDisplayNotFound()
    {
        const string urn = "12345";
        var page = await Client.SetupEstablishmentWithNotFound()
            .Navigate(Paths.SchoolCustomDataWorkforceData(urn));

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.SchoolCustomDataWorkforceData(urn).ToAbsolute(), HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string urn = "12345";
        var page = await Client.SetupEstablishmentWithException()
            .Navigate(Paths.SchoolCustomDataWorkforceData(urn));

        PageAssert.IsProblemPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.SchoolCustomDataWorkforceData(urn).ToAbsolute(), HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task CanNavigateBack()
    {
        var (page, school) = await SetupNavigateInitPage();

        var anchor = page.QuerySelector(".govuk-back-link");
        page = await Client.Follow(anchor);

        DocumentAssert.AssertPageUrl(page, Paths.SchoolCustomDataNonFinancialData(school.URN).ToAbsolute());
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
            .SetUpCustomData()
            .SetupHttpContextAccessor()
            .Navigate(Paths.SchoolCustomDataWorkforceData(school.URN));

        return (page, school);
    }

    private void AssertPageLayout(IHtmlDocument page, School school)
    {
        DocumentAssert.BackLink(page, "Back", Paths.SchoolCustomDataNonFinancialData(school.URN).ToAbsolute());
        DocumentAssert.TitleAndH1(page, "Customise your data - Financial Benchmarking and Insights Tool - GOV.UK", "Change workforce data");

        var currentValues = page.QuerySelectorAll("span[id^='current-']");
        Assert.Equal(4, currentValues.Length);

        foreach (var currentValue in currentValues)
        {
            var actual = currentValue.TextContent.Trim();
            var field = currentValue.Id?.Split("-").Last() ?? string.Empty;
            var expected = field switch
            {
                nameof(WorkforceDataCustomDataViewModel.WorkforceFte) => _census.WorkforceFTE?.ToString("#.0"),
                nameof(WorkforceDataCustomDataViewModel.TeachersFte) => _census.TeachersFTE?.ToString("#.0"),
                nameof(WorkforceDataCustomDataViewModel.QualifiedTeacherPercent) => _census.PercentTeacherWithQualifiedStatus?.ToString("#") + "%",
                _ => _census.SeniorLeadershipFTE?.ToString("#.0")
            };

            Assert.True(expected?.Equals(actual), $"{field} expected to be {expected} but found {actual}");
        }
    }
}