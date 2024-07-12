using System.Net;
using AngleSharp.Html.Dom;
using AutoFixture;
using AutoFixture.Dsl;
using Moq;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Xunit;
namespace Web.Integration.Tests.Pages.Schools.FinancialPlanning;

public class WhenViewingPlanningManagementRoles(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    private static readonly int CurrentYear =
        DateTime.UtcNow.Month < 9 ? DateTime.UtcNow.Year - 1 : DateTime.UtcNow.Year;

    [Theory]
    [InlineData(EstablishmentTypes.Academies, OverallPhaseTypes.Primary)]
    [InlineData(EstablishmentTypes.Academies, OverallPhaseTypes.Secondary)]
    [InlineData(EstablishmentTypes.Maintained, OverallPhaseTypes.Primary)]
    [InlineData(EstablishmentTypes.Maintained, OverallPhaseTypes.Secondary)]
    public async Task CanDisplay(string financeType, string overallPhase)
    {
        var (page, school) = await SetupNavigateInitPage(financeType, overallPhase);

        AssertPageLayout(page, school);
    }

    [Fact]
    // [InlineData(EstablishmentTypes.Academies)]
    // [InlineData(EstablishmentTypes.Maintained)]
    public async Task CanNavigateBack()
    {
        /*
         See decision log: temp remove navigation to be review post private beta
         var (page, school) = await SetupNavigateInitPage(financeType, overallPhase);

        var anchor = page.QuerySelector(".govuk-back-link");
        page = await Client.Follow(anchor);

        DocumentAssert.AssertPageUrl(page,
            Paths.SchoolFinancialPlanningOtherTeachingPeriods(school.URN, CurrentYear).ToAbsolute());*/
    }

    [Fact]
    public async Task CanDisplayNotFound()
    {
        const string urn = "12345";
        const int year = 2024;

        var page = await Client.SetupEstablishmentWithNotFound()
            .Navigate(Paths.SchoolFinancialPlanningManagementRoles(urn, year));

        var expectedUrl = Paths.SchoolFinancialPlanningManagementRoles(urn, year).ToAbsolute();
        DocumentAssert.AssertPageUrl(page, expectedUrl, HttpStatusCode.NotFound);
        PageAssert.IsNotFoundPage(page);
    }

    [Fact]
    public async Task CanDisplayNotFoundOnSubmit()
    {
        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Academies, OverallPhaseTypes.Primary);
        var action = page.QuerySelector("main .govuk-button");

        Assert.NotNull(action);

        Client.SetupFinancialPlan();

        page = await Client.SubmitForm(page.Forms[0], action);

        Client.FinancialPlanApi.Verify(api => api.UpsertAsync(It.IsAny<PutFinancialPlanRequest>()), Times.Never);

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page,
            Paths.SchoolFinancialPlanningManagementRoles(school.URN, CurrentYear).ToAbsolute(),
            HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string urn = "12345";
        const int year = 2024;
        var page = await Client.SetupEstablishmentWithException()
            .Navigate(Paths.SchoolFinancialPlanningManagementRoles(urn, year));

        var expectedUrl = Paths.SchoolFinancialPlanningManagementRoles(urn, year).ToAbsolute();
        DocumentAssert.AssertPageUrl(page, expectedUrl, HttpStatusCode.InternalServerError);
        PageAssert.IsProblemPage(page);
    }

    [Theory]
    [InlineData(true, true, true, true, true, true, true, true)]
    [InlineData(false, true, false, true, false, true, false, true)]
    [InlineData(true, false, true, false, true, false, true, false)]
    [InlineData(true, false, false, false, false, false, false, false)]
    [InlineData(false, true, false, false, false, false, false, false)]
    [InlineData(false, false, true, false, false, false, false, false)]
    [InlineData(false, false, false, true, false, false, false, false)]
    [InlineData(false, false, false, false, true, false, false, false)]
    [InlineData(false, false, false, false, false, true, false, false)]
    [InlineData(false, false, false, false, false, false, true, false)]
    [InlineData(false, false, false, false, false, false, false, true)]
    public async Task CanSubmitPrimary(bool managementRoleHeadteacher, bool managementRoleDeputyHeadteacher,
        bool managementRoleNumeracyLead, bool managementRoleLiteracyLead, bool managementRoleHeadSmallCurriculum,
        bool managementRoleHeadKs1, bool managementRoleHeadKs2, bool managementRoleSenco)
    {
        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Academies, OverallPhaseTypes.Primary);
        AssertPageLayout(page, school);
        var action = page.QuerySelector("main .govuk-button");
        Assert.NotNull(action);

        page = await Client.SubmitForm(page.Forms[0], action, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                {
                    "ManagementRoleHeadteacher", managementRoleHeadteacher.ToString()
                },
                {
                    "ManagementRoleDeputyHeadteacher", managementRoleDeputyHeadteacher.ToString()
                },
                {
                    "ManagementRoleNumeracyLead", managementRoleNumeracyLead.ToString()
                },
                {
                    "ManagementRoleLiteracyLead", managementRoleLiteracyLead.ToString()
                },
                {
                    "ManagementRoleHeadSmallCurriculum", managementRoleHeadSmallCurriculum.ToString()
                },
                {
                    "ManagementRoleHeadKs1", managementRoleHeadKs1.ToString()
                },
                {
                    "ManagementRoleHeadKs2", managementRoleHeadKs2.ToString()
                },
                {
                    "ManagementRoleSenco", managementRoleSenco.ToString()
                }
            });
        });

        Client.FinancialPlanApi.Verify(api => api.UpsertAsync(It.IsAny<PutFinancialPlanRequest>()), Times.Once);

        DocumentAssert.AssertPageUrl(page,
            Paths.SchoolFinancialPlanningManagersPerRole(school.URN, CurrentYear).ToAbsolute());
    }

    [Theory]
    [InlineData(true, true, true, true, true, true, true, true)]
    [InlineData(false, true, false, true, false, true, false, true)]
    [InlineData(true, false, true, false, true, false, true, false)]
    [InlineData(true, false, false, false, false, false, false, false)]
    [InlineData(false, true, false, false, false, false, false, false)]
    [InlineData(false, false, true, false, false, false, false, false)]
    [InlineData(false, false, false, true, false, false, false, false)]
    [InlineData(false, false, false, false, true, false, false, false)]
    [InlineData(false, false, false, false, false, true, false, false)]
    [InlineData(false, false, false, false, false, false, true, false)]
    [InlineData(false, false, false, false, false, false, false, true)]
    public async Task CanSubmitSecondary(bool managementRoleHeadteacher, bool managementRoleDeputyHeadteacher,
        bool managementRoleAssistantHeadteacher, bool managementRoleHeadLargeCurriculum, bool managementRoleHeadSmallCurriculum,
        bool managementRoleSenco, bool managementRolePastoralLeader, bool managementRoleOtherMembers)
    {
        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Academies, OverallPhaseTypes.Secondary);
        AssertPageLayout(page, school);
        var action = page.QuerySelector("main .govuk-button");
        Assert.NotNull(action);

        page = await Client.SubmitForm(page.Forms[0], action, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                {
                    "ManagementRoleHeadteacher", managementRoleHeadteacher.ToString()
                },
                {
                    "ManagementRoleDeputyHeadteacher", managementRoleDeputyHeadteacher.ToString()
                },
                {
                    "ManagementRoleAssistantHeadteacher", managementRoleAssistantHeadteacher.ToString()
                },
                {
                    "ManagementRoleHeadLargeCurriculum", managementRoleHeadLargeCurriculum.ToString()
                },
                {
                    "ManagementRoleHeadSmallCurriculum", managementRoleHeadSmallCurriculum.ToString()
                },
                {
                    "ManagementRoleSenco", managementRoleSenco.ToString()
                },
                {
                    "ManagementRolePastoralLeader", managementRolePastoralLeader.ToString()
                },
                {
                    "ManagementRoleOtherMembers", managementRoleOtherMembers.ToString()
                }
            });
        });

        Client.FinancialPlanApi.Verify(api => api.UpsertAsync(It.IsAny<PutFinancialPlanRequest>()), Times.Once);

        DocumentAssert.AssertPageUrl(page,
            Paths.SchoolFinancialPlanningManagersPerRole(school.URN, CurrentYear).ToAbsolute());
    }

    [Theory]
    [InlineData(true, true, true, true, true, true, true, true)]
    [InlineData(false, true, false, true, false, true, false, true)]
    [InlineData(true, false, true, false, true, false, true, false)]
    [InlineData(true, false, false, false, false, false, false, false)]
    [InlineData(false, true, false, false, false, false, false, false)]
    [InlineData(false, false, true, false, false, false, false, false)]
    [InlineData(false, false, false, true, false, false, false, false)]
    [InlineData(false, false, false, false, true, false, false, false)]
    [InlineData(false, false, false, false, false, true, false, false)]
    [InlineData(false, false, false, false, false, false, true, false)]
    [InlineData(false, false, false, false, false, false, false, true)]
    public async Task CanDisplayWithPreviousValuePrimary(bool managementRoleHeadteacher, bool managementRoleDeputyHeadteacher,
        bool managementRoleNumeracyLead, bool managementRoleLiteracyLead, bool managementRoleHeadSmallCurriculum,
        bool managementRoleHeadKs1, bool managementRoleHeadKs2, bool managementRoleSenco)
    {
        var composer = Fixture.Build<FinancialPlanInput>()
            .With(x => x.ManagementRoleHeadteacher, managementRoleHeadteacher)
            .With(x => x.ManagementRoleDeputyHeadteacher, managementRoleDeputyHeadteacher)
            .With(x => x.ManagementRoleNumeracyLead, managementRoleNumeracyLead)
            .With(x => x.ManagementRoleLiteracyLead, managementRoleLiteracyLead)
            .With(x => x.ManagementRoleHeadSmallCurriculum, managementRoleHeadSmallCurriculum)
            .With(x => x.ManagementRoleHeadKs1, managementRoleHeadKs1)
            .With(x => x.ManagementRoleHeadKs2, managementRoleHeadKs2)
            .With(x => x.ManagementRoleSenco, managementRoleSenco);

        var (page, _) =
            await SetupNavigateInitPage(EstablishmentTypes.Academies, OverallPhaseTypes.Primary, composer);

        var checkboxes = page.QuerySelector(".govuk-checkboxes");
        Assert.NotNull(checkboxes);

        var options = new[]
        {
            ("ManagementRoleHeadteacher", "Headteacher", managementRoleHeadteacher),
            ("ManagementRoleDeputyHeadteacher", "Deputy headteacher", managementRoleDeputyHeadteacher),
            ("ManagementRoleNumeracyLead", "Numeracy lead", managementRoleNumeracyLead),
            ("ManagementRoleLiteracyLead", "Literacy lead", managementRoleLiteracyLead),
            ("ManagementRoleHeadSmallCurriculum", "Head of small curriculum area", managementRoleHeadSmallCurriculum),
            ("ManagementRoleHeadKs1", "Head of KS1", managementRoleHeadKs1),
            ("ManagementRoleHeadKs2", "Head of KS2", managementRoleHeadKs2),
            ("ManagementRoleSenco", "Special educational needs coordinator (SENCO)", managementRoleSenco)
        };

        DocumentAssert.Checkboxes(checkboxes, options);
    }

    [Theory]
    [InlineData(true, true, true, true, true, true, true, true)]
    [InlineData(false, true, false, true, false, true, false, true)]
    [InlineData(true, false, true, false, true, false, true, false)]
    [InlineData(true, false, false, false, false, false, false, false)]
    [InlineData(false, true, false, false, false, false, false, false)]
    [InlineData(false, false, true, false, false, false, false, false)]
    [InlineData(false, false, false, true, false, false, false, false)]
    [InlineData(false, false, false, false, true, false, false, false)]
    [InlineData(false, false, false, false, false, true, false, false)]
    [InlineData(false, false, false, false, false, false, true, false)]
    [InlineData(false, false, false, false, false, false, false, true)]
    public async Task CanDisplayWithPreviousValueSecondary(bool managementRoleHeadteacher, bool managementRoleDeputyHeadteacher,
        bool managementRoleAssistantHeadteacher, bool managementRoleHeadLargeCurriculum, bool managementRoleHeadSmallCurriculum,
        bool managementRoleSenco, bool managementRolePastoralLeader, bool managementRoleOtherMembers)
    {
        var composer = Fixture.Build<FinancialPlanInput>()
            .With(x => x.ManagementRoleHeadteacher, managementRoleHeadteacher)
            .With(x => x.ManagementRoleDeputyHeadteacher, managementRoleDeputyHeadteacher)
            .With(x => x.ManagementRoleAssistantHeadteacher, managementRoleAssistantHeadteacher)
            .With(x => x.ManagementRoleHeadLargeCurriculum, managementRoleHeadLargeCurriculum)
            .With(x => x.ManagementRoleHeadSmallCurriculum, managementRoleHeadSmallCurriculum)
            .With(x => x.ManagementRoleSenco, managementRoleSenco)
            .With(x => x.ManagementRolePastoralLeader, managementRolePastoralLeader)
            .With(x => x.ManagementRoleOtherMembers, managementRoleOtherMembers);

        var (page, _) =
            await SetupNavigateInitPage(EstablishmentTypes.Academies, OverallPhaseTypes.Secondary, composer);

        var checkboxes = page.QuerySelector(".govuk-checkboxes");
        Assert.NotNull(checkboxes);

        var options = new[]
        {
            ("ManagementRoleHeadteacher", "Headteacher", managementRoleHeadteacher),
            ("ManagementRoleDeputyHeadteacher", "Deputy headteacher", managementRoleDeputyHeadteacher),
            ("ManagementRoleAssistantHeadteacher", "Assistant headteacher", managementRoleAssistantHeadteacher),
            ("ManagementRoleHeadLargeCurriculum", "Head of large curriculum area", managementRoleHeadLargeCurriculum),
            ("ManagementRoleHeadSmallCurriculum", "Head of small curriculum area", managementRoleHeadSmallCurriculum),
            ("ManagementRoleSenco", "Special education needs coordinator (SENCO)", managementRoleSenco),
            ("ManagementRolePastoralLeader", "Pastoral leader", managementRolePastoralLeader),
            ("ManagementRoleOtherMembers", "Other members of management or leadership staff", managementRoleOtherMembers)
        };

        DocumentAssert.Checkboxes(checkboxes, options);
    }

    [Fact]
    public async Task ShowsErrorOnInvalidSubmit()
    {

        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Academies, OverallPhaseTypes.Primary);
        AssertPageLayout(page, school);
        var action = page.QuerySelector("main .govuk-button");
        Assert.NotNull(action);

        page = await Client.SubmitForm(page.Forms[0], action, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                {
                    "ManagementRoleHeadteacher", ""
                },
                {
                    "ManagementRoleDeputyHeadteacher", ""
                },
                {
                    "ManagementRoleNumeracyLead", ""
                },
                {
                    "ManagementRoleLiteracyLead", ""
                },
                {
                    "ManagementRoleHeadSmallCurriculum", ""
                },
                {
                    "ManagementRoleHeadKs1", ""
                },
                {
                    "ManagementRoleHeadKs2", ""
                },
                {
                    "ManagementRoleSenco", ""
                }
            });
            ;
        });

        Client.FinancialPlanApi.Verify(api => api.UpsertAsync(It.IsAny<PutFinancialPlanRequest>()), Times.Never);

        DocumentAssert.AssertPageUrl(page, Paths.SchoolFinancialPlanningManagementRoles(school.URN, CurrentYear).ToAbsolute());
        DocumentAssert.FormErrors(page, ("management-roles", "Select which management roles have teaching responsibilities"));
    }

    private async Task<(IHtmlDocument page, School school)> SetupNavigateInitPage(string financeType, string overallPhase, IPostprocessComposer<FinancialPlanInput>? planComposer = null)
    {
        var school = Fixture.Build<School>()
            .With(x => x.URN, "12345")
            .With(x => x.FinanceType, financeType)
            .With(x => x.OverallPhase, overallPhase)
            .Create();

        planComposer ??= Fixture.Build<FinancialPlanInput>();

        var plan = planComposer
            .With(x => x.Urn, school.URN)
            .With(x => x.Year, CurrentYear)
            .Create();

        var page = await Client.SetupEstablishment(school)
            .SetupFinancialPlan(plan)
            .Navigate(Paths.SchoolFinancialPlanningManagementRoles(school.URN, CurrentYear));

        return (page, school);
    }

    private static void AssertPageLayout(IHtmlDocument page, School school)
    {
        DocumentAssert.BackLink(page, "Back",
            Paths.SchoolFinancialPlanningOtherTeachingPeriods(school.URN, CurrentYear).ToAbsolute());
        DocumentAssert.TitleAndH1(page,
            "Management roles with teaching responsibilties - Financial Benchmarking and Insights Tool - GOV.UK",
            "Management roles with teaching responsibilties");
    }
}