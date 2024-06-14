using Web.App.Extensions;

namespace Web.App.Domain.Benchmark.FinancialPlanStages;

public class ManagersPerRoleStage : Stage
{
    public bool ManagementRoleHeadteacher { get; set; }
    public bool ManagementRoleDeputyHeadteacher { get; set; }
    public bool ManagementRoleNumeracyLead { get; set; }
    public bool ManagementRoleLiteracyLead { get; set; }
    public bool ManagementRoleHeadSmallCurriculum { get; set; }
    public bool ManagementRoleHeadKs1 { get; set; }
    public bool ManagementRoleHeadKs2 { get; set; }
    public bool ManagementRoleSenco { get; set; }
    public bool ManagementRoleAssistantHeadteacher { get; set; }
    public bool ManagementRoleHeadLargeCurriculum { get; set; }
    public bool ManagementRolePastoralLeader { get; set; }
    public bool ManagementRoleOtherMembers { get; set; }
    public string? NumberHeadteacher { get; set; }
    public string? NumberDeputyHeadteacher { get; set; }
    public string? NumberNumeracyLead { get; set; }
    public string? NumberLiteracyLead { get; set; }
    public string? NumberHeadSmallCurriculum { get; set; }
    public string? NumberHeadKs1 { get; set; }
    public string? NumberHeadKs2 { get; set; }
    public string? NumberSenco { get; set; }
    public string? NumberAssistantHeadteacher { get; set; }
    public string? NumberHeadLargeCurriculum { get; set; }
    public string? NumberPastoralLeader { get; set; }
    public string? NumberOtherMembers { get; set; }

    public override void SetPlanValues(FinancialPlanInput planInput)
    {
        planInput.NumberHeadteacher = NumberHeadteacher;
        planInput.NumberDeputyHeadteacher = NumberDeputyHeadteacher;
        planInput.NumberNumeracyLead = NumberNumeracyLead;
        planInput.NumberLiteracyLead = NumberLiteracyLead;
        planInput.NumberHeadSmallCurriculum = NumberHeadSmallCurriculum;
        planInput.NumberHeadKs1 = NumberHeadKs1;
        planInput.NumberHeadKs2 = NumberHeadKs2;
        planInput.NumberSenco = NumberSenco;
        planInput.NumberAssistantHeadteacher = NumberAssistantHeadteacher;
        planInput.NumberHeadLargeCurriculum = NumberHeadLargeCurriculum;
        planInput.NumberPastoralLeader = NumberPastoralLeader;
        planInput.NumberOtherMembers = NumberOtherMembers;

        ResetTeachingPeriods(planInput);
    }

    private static void ResetTeachingPeriods(FinancialPlanInput planInput)
    {
        if (planInput.TeachingPeriodsHeadteacher.Length != planInput.NumberHeadteacher.ToInt())
        {
            var val = planInput.NumberHeadteacher.ToInt() ?? 0;
            var count = val > 0 ? val : 0;
            planInput.TeachingPeriodsHeadteacher = new string[count];
        }

        if (planInput.TeachingPeriodsDeputyHeadteacher.Length != planInput.NumberDeputyHeadteacher.ToInt())
        {
            var val = planInput.NumberDeputyHeadteacher.ToInt() ?? 0;
            var count = val > 0 ? val : 0;
            planInput.TeachingPeriodsDeputyHeadteacher = new string[count];
        }

        if (planInput.TeachingPeriodsNumeracyLead.Length != planInput.NumberNumeracyLead.ToInt())
        {
            var val = planInput.NumberNumeracyLead.ToInt() ?? 0;
            var count = val > 0 ? val : 0;
            planInput.TeachingPeriodsNumeracyLead = new string[count];
        }

        if (planInput.TeachingPeriodsLiteracyLead.Length != planInput.NumberLiteracyLead.ToInt())
        {
            var val = planInput.NumberLiteracyLead.ToInt() ?? 0;
            var count = val > 0 ? val : 0;
            planInput.TeachingPeriodsLiteracyLead = new string[count];
        }

        if (planInput.TeachingPeriodsHeadSmallCurriculum.Length != planInput.NumberHeadSmallCurriculum.ToInt())
        {
            var val = planInput.NumberHeadSmallCurriculum.ToInt() ?? 0;
            var count = val > 0 ? val : 0;
            planInput.TeachingPeriodsHeadSmallCurriculum = new string[count];
        }

        if (planInput.TeachingPeriodsHeadKs1.Length != planInput.NumberHeadKs1.ToInt())
        {
            var val = planInput.NumberHeadKs1.ToInt() ?? 0;
            var count = val > 0 ? val : 0;
            planInput.TeachingPeriodsHeadKs1 = new string[count];
        }

        if (planInput.TeachingPeriodsHeadKs2.Length != planInput.NumberHeadKs2.ToInt())
        {
            var val = planInput.NumberHeadKs2.ToInt() ?? 0;
            var count = val > 0 ? val : 0;
            planInput.TeachingPeriodsHeadKs2 = new string[count];
        }

        if (planInput.TeachingPeriodsSenco.Length != planInput.NumberSenco.ToInt())
        {
            var val = planInput.NumberSenco.ToInt() ?? 0;
            var count = val > 0 ? val : 0;
            planInput.TeachingPeriodsSenco = new string[count];
        }

        if (planInput.TeachingPeriodsAssistantHeadteacher.Length != planInput.NumberAssistantHeadteacher.ToInt())
        {
            var val = planInput.NumberAssistantHeadteacher.ToInt() ?? 0;
            var count = val > 0 ? val : 0;
            planInput.TeachingPeriodsAssistantHeadteacher = new string[count];
        }

        if (planInput.TeachingPeriodsHeadLargeCurriculum.Length != planInput.NumberHeadLargeCurriculum.ToInt())
        {
            var val = planInput.NumberHeadLargeCurriculum.ToInt() ?? 0;
            var count = val > 0 ? val : 0;
            planInput.TeachingPeriodsHeadLargeCurriculum = new string[count];
        }

        if (planInput.TeachingPeriodsPastoralLeader.Length != planInput.NumberPastoralLeader.ToInt())
        {
            var val = planInput.NumberPastoralLeader.ToInt() ?? 0;
            var count = val > 0 ? val : 0;
            planInput.TeachingPeriodsPastoralLeader = new string[count];
        }

        if (planInput.TeachingPeriodsOtherMembers.Length != planInput.NumberOtherMembers.ToInt())
        {
            var val = planInput.NumberOtherMembers.ToInt() ?? 0;
            var count = val > 0 ? val : 0;
            planInput.TeachingPeriodsOtherMembers = new string[count];
        }
    }
}