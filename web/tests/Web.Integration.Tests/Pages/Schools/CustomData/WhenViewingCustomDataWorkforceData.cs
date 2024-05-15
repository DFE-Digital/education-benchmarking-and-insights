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
    private readonly Expenditure _expenditure;
    private readonly Finances _finances;
    private readonly FloorAreaMetric _floorAreaMetric;
    private readonly Dictionary<string, decimal?> _formValues;
    private readonly Income _income;

    public WhenViewingCustomDataWorkforceData(SchoolBenchmarkingWebAppClient client) : base(client)
    {
        _finances = Fixture.Build<Finances>()
            .Create();

        _income = Fixture.Build<Income>()
            .Create();

        _expenditure = Fixture.Build<Expenditure>()
            .Create();

        _floorAreaMetric = Fixture.Build<FloorAreaMetric>()
            .Create();

        _census = Fixture.Build<Census>()
            .Create();

        var customCensus = Fixture.Build<Census>()
            .With(c => c.WorkforceFte, Fixture.CreateDecimal(101, 200))
            .With(c => c.TeachersFte, Fixture.CreateDecimal(51, 100))
            .With(c => c.SeniorLeadershipFte, Fixture.CreateDecimal(0, 50))
            .Create();

        _formValues = new Dictionary<string, decimal?>
        {
            {
                nameof(WorkforceDataCustomDataViewModel.WorkforceFte), customCensus.NumberOfPupils
            },
            {
                nameof(WorkforceDataCustomDataViewModel.TeachersFte), customCensus.TeachersFte
            },
            {
                nameof(WorkforceDataCustomDataViewModel.SeniorLeadershipFte), customCensus.SeniorLeadershipFte
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

        DocumentAssert.AssertPageUrl(page, Paths.SchoolCustomData(school.Urn).ToAbsolute());
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

        DocumentAssert.AssertPageUrl(page, Paths.SchoolCustomDataWorkforceData(school.Urn).ToAbsolute());
        DocumentAssert.SummaryErrors(
            page,
            (nameof(WorkforceDataCustomDataViewModel.WorkforceFte), "Enter school workforce (full time equivalent) in the correct format"),
            (nameof(WorkforceDataCustomDataViewModel.TeachersFte), "Enter number of teachers (full time equivalent) in the correct format"),
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

        DocumentAssert.AssertPageUrl(page, Paths.SchoolCustomDataNonFinancialData(school.Urn).ToAbsolute());
    }

    private async Task<(IHtmlDocument page, School school)> SetupNavigateInitPage()
    {
        var school = Fixture.Build<School>()
            .With(x => x.Urn, "12345")
            .Create();

        var page = await Client.SetupEstablishment(school)
            .SetupInsights(school, _finances, _income, _expenditure, _floorAreaMetric)
            .SetupCensus(school, _census)
            .SetupHttpContextAccessor()
            .Navigate(Paths.SchoolCustomDataWorkforceData(school.Urn));

        return (page, school);
    }

    private void AssertPageLayout(IHtmlDocument page, School school)
    {
        DocumentAssert.BackLink(page, "Back", Paths.SchoolCustomDataNonFinancialData(school.Urn).ToAbsolute());
        DocumentAssert.TitleAndH1(page, "Customise your data - Financial Benchmarking and Insights Tool - GOV.UK", "Change workforce data");

        var currentValues = page.QuerySelectorAll("span[id^='current-']");
        Assert.Equal(3, currentValues.Length);

        foreach (var currentValue in currentValues)
        {
            var actual = currentValue.TextContent.Trim();
            var field = currentValue.Id?.Split("-").Last() ?? string.Empty;
            var expected = field switch
            {
                nameof(WorkforceDataCustomDataViewModel.WorkforceFte) => _census.WorkforceFte,
                nameof(WorkforceDataCustomDataViewModel.TeachersFte) => _census.TeachersFte,
                _ => _census.SeniorLeadershipFte
            };

            Assert.True(expected?.ToString("#.0").Equals(actual), $"{field} expected to be {expected} but found {actual}");
        }
    }
}