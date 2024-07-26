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
            .With(c => c.Workforce, Fixture.CreateDecimal(101, 200))
            .With(c => c.Teachers, Fixture.CreateDecimal(51, 90))
            .With(c => c.PercentTeacherWithQualifiedStatus, Fixture.CreateDecimal(91, 100))
            .With(c => c.SeniorLeadership, Fixture.CreateDecimal(0, 50))
            .With(c => c.TeachingAssistant, Fixture.CreateDecimal(0, 50))
            .With(c => c.NonClassroomSupportStaff, Fixture.CreateDecimal(0, 50))
            .With(c => c.AuxiliaryStaff, Fixture.CreateDecimal(0, 50))
            .With(c => c.WorkforceHeadcount, Fixture.CreateDecimal(101, 200))
            .Create();

        _formValues = new Dictionary<string, decimal?>
        {
            {
                nameof(WorkforceDataCustomDataViewModel.WorkforceFte), customCensus.Workforce
            },
            {
                nameof(WorkforceDataCustomDataViewModel.TeachersFte), customCensus.Teachers
            },
            {
                nameof(WorkforceDataCustomDataViewModel.QualifiedTeacherPercent), customCensus.PercentTeacherWithQualifiedStatus
            },
            {
                nameof(WorkforceDataCustomDataViewModel.SeniorLeadershipFte), customCensus.SeniorLeadership
            },
            {
                nameof(WorkforceDataCustomDataViewModel.TeachingAssistantsFte), customCensus.TeachingAssistant
            },
            {
                nameof(WorkforceDataCustomDataViewModel.NonClassroomSupportStaffFte), customCensus.NonClassroomSupportStaff
            },
            {
                nameof(WorkforceDataCustomDataViewModel.AuxiliaryStaffFte), customCensus.AuxiliaryStaff
            },
            {
                nameof(WorkforceDataCustomDataViewModel.WorkforceHeadcount), customCensus.WorkforceHeadcount
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
        var action = page.QuerySelector("main .govuk-button");
        Assert.NotNull(action);

        page = await Client.SubmitForm(page.Forms[0], action, f =>
        {
            f.SetFormValues(_formValues.ToDictionary(k => k.Key, v => v.Value?.ToString() ?? string.Empty));
        });

        DocumentAssert.AssertPageUrl(page, Paths.SchoolCustomDataSubmit(school.URN).ToAbsolute());
    }

    [Fact]
    public async Task CanSubmitEmptyCustomValues()
    {
        var (page, school) = await SetupNavigateInitPage();
        AssertPageLayout(page, school);
        var action = page.QuerySelector("main .govuk-button");
        Assert.NotNull(action);

        page = await Client.SubmitForm(page.Forms[0], action);

        DocumentAssert.AssertPageUrl(page, Paths.SchoolCustomDataSubmit(school.URN).ToAbsolute());
    }

    [Fact]
    public async Task ShowsErrorOnInvalidValues()
    {
        var (page, school) = await SetupNavigateInitPage();
        AssertPageLayout(page, school);
        var action = page.QuerySelector("main .govuk-button");
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
            (nameof(WorkforceDataCustomDataViewModel.SeniorLeadershipFte), "Enter senior leadership (full time equivalent) in the correct format"),
            (nameof(WorkforceDataCustomDataViewModel.TeachingAssistantsFte), "Enter teaching assistants (full time equivalent) in the correct format"),
            (nameof(WorkforceDataCustomDataViewModel.NonClassroomSupportStaffFte), "Enter non-classroom support staff - excluding auxiliary staff (full time equivalent) in the correct format"),
            (nameof(WorkforceDataCustomDataViewModel.AuxiliaryStaffFte), "Enter auxiliary staff (full time equivalent) in the correct format"),
            (nameof(WorkforceDataCustomDataViewModel.WorkforceHeadcount), "Enter school workforce (headcount) in the correct format")
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
        /*
        See decision log: temp remove navigation to be review post private beta
        var (page, school) = await SetupNavigateInitPage();

        var anchor = page.QuerySelector(".govuk-back-link");
        page = await Client.Follow(anchor);

        DocumentAssert.AssertPageUrl(page, Paths.SchoolCustomDataNonFinancialData(school.URN).ToAbsolute());*/
    }

    private async Task<(IHtmlDocument page, School school)> SetupNavigateInitPage()
    {
        var school = Fixture.Build<School>()
            .With(x => x.URN, "12345")
            .Create();

        var customDataId = "123";

        var userData = new[]
        {
            new UserData
            {
                Type = "custom-data",
                Id = customDataId
            }
        };

        var customData = new CustomDataSchool
        {
            Id = customDataId,
            URN = school.URN
        };

        var page = await Client.SetupEstablishment(school)
            .SetupUserData(userData)
            .SetupIncome(school, _income)
            .SetupCensus(school, _census)
            .SetupBalance(school)
            .SetupExpenditure(school, _expenditure)
            .SetupSchoolInsight(school, _floorAreaMetric)
            .SetUpCustomData(customData)
            .SetupHttpContextAccessor()
            .Navigate(Paths.SchoolCustomDataWorkforceData(school.URN));

        return (page, school);
    }

    private void AssertPageLayout(IHtmlDocument page, School school)
    {
        DocumentAssert.BackLink(page, "Back", Paths.SchoolCustomDataNonFinancialData(school.URN).ToAbsolute());
        DocumentAssert.TitleAndH1(page, "Customise your data - Financial Benchmarking and Insights Tool - GOV.UK", "Change workforce data");

        var currentValues = page.QuerySelectorAll("span[id^='current-']");
        Assert.Equal(8, currentValues.Length);

        foreach (var currentValue in currentValues)
        {
            var actual = currentValue.TextContent.Trim();
            var field = currentValue.Id?.Split("-").Last() ?? string.Empty;
            var expected = field switch
            {
                nameof(WorkforceDataCustomDataViewModel.WorkforceFte) => _census.Workforce?.ToString("#.0"),
                nameof(WorkforceDataCustomDataViewModel.TeachersFte) => _census.Teachers?.ToString("#.0"),
                nameof(WorkforceDataCustomDataViewModel.QualifiedTeacherPercent) => _census.PercentTeacherWithQualifiedStatus?.ToString("#") + "%",
                nameof(WorkforceDataCustomDataViewModel.SeniorLeadershipFte) => _census.SeniorLeadership?.ToString("#.0"),
                nameof(WorkforceDataCustomDataViewModel.TeachingAssistantsFte) => _census.TeachingAssistant?.ToString("#.0"),
                nameof(WorkforceDataCustomDataViewModel.NonClassroomSupportStaffFte) => _census.NonClassroomSupportStaff?.ToString("#.0"),
                nameof(WorkforceDataCustomDataViewModel.AuxiliaryStaffFte) => _census.AuxiliaryStaff?.ToString("#.0"),
                nameof(WorkforceDataCustomDataViewModel.WorkforceHeadcount) => _census.WorkforceHeadcount?.ToString("#.0"),
                _ => throw new ArgumentOutOfRangeException()
            };

            Assert.True(expected?.Equals(actual), $"{field} expected to be {expected} but found {actual}");
        }
    }
}