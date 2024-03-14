using Web.App.Extensions;

namespace Web.App.Domain.FinancialPlanStages;

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

    public override void SetPlanValues(FinancialPlan plan)
    {
        plan.NumberHeadteacher = NumberHeadteacher;
        plan.NumberDeputyHeadteacher = NumberDeputyHeadteacher;
        plan.NumberNumeracyLead = NumberNumeracyLead;
        plan.NumberLiteracyLead = NumberLiteracyLead;
        plan.NumberHeadSmallCurriculum = NumberHeadSmallCurriculum;
        plan.NumberHeadKs1 = NumberHeadKs1;
        plan.NumberHeadKs2 = NumberHeadKs2;
        plan.NumberSenco = NumberSenco;
        plan.NumberAssistantHeadteacher = NumberAssistantHeadteacher;
        plan.NumberHeadLargeCurriculum = NumberHeadLargeCurriculum;
        plan.NumberPastoralLeader = NumberPastoralLeader;
        plan.NumberOtherMembers = NumberOtherMembers;

        ResetTeachingPeriods(plan);
    }

    private static void ResetTeachingPeriods(FinancialPlan plan)
    {
        if (plan.TeachingPeriodsHeadteacher.Length != plan.NumberHeadteacher.ToInt())
        {
            var val = plan.NumberHeadteacher.ToInt() ?? 0;
            var count = val > 0 ? val : 0;
            plan.TeachingPeriodsHeadteacher = new string[count];
        }

        if (plan.TeachingPeriodsDeputyHeadteacher.Length != plan.NumberDeputyHeadteacher.ToInt())
        {
            var val = plan.NumberDeputyHeadteacher.ToInt() ?? 0;
            var count = val > 0 ? val : 0;
            plan.TeachingPeriodsDeputyHeadteacher = new string[count];
        }

        if (plan.TeachingPeriodsNumeracyLead.Length != plan.NumberNumeracyLead.ToInt())
        {
            var val = plan.NumberNumeracyLead.ToInt() ?? 0;
            var count = val > 0 ? val : 0;
            plan.TeachingPeriodsNumeracyLead = new string[count];
        }

        if (plan.TeachingPeriodsLiteracyLead.Length != plan.NumberLiteracyLead.ToInt())
        {
            var val = plan.NumberLiteracyLead.ToInt() ?? 0;
            var count = val > 0 ? val : 0;
            plan.TeachingPeriodsLiteracyLead = new string[count];
        }

        if (plan.TeachingPeriodsHeadSmallCurriculum.Length != plan.NumberHeadSmallCurriculum.ToInt())
        {
            var val = plan.NumberHeadSmallCurriculum.ToInt() ?? 0;
            var count = val > 0 ? val : 0;
            plan.TeachingPeriodsHeadSmallCurriculum = new string[count];
        }

        if (plan.TeachingPeriodsHeadKs1.Length != plan.NumberHeadKs1.ToInt())
        {
            var val = plan.NumberHeadKs1.ToInt() ?? 0;
            var count = val > 0 ? val : 0;
            plan.TeachingPeriodsHeadKs1 = new string[count];
        }

        if (plan.TeachingPeriodsHeadKs2.Length != plan.NumberHeadKs2.ToInt())
        {
            var val = plan.NumberHeadKs2.ToInt() ?? 0;
            var count = val > 0 ? val : 0;
            plan.TeachingPeriodsHeadKs2 = new string[count];
        }

        if (plan.TeachingPeriodsSenco.Length != plan.NumberSenco.ToInt())
        {
            var val = plan.NumberSenco.ToInt() ?? 0;
            var count = val > 0 ? val : 0;
            plan.TeachingPeriodsSenco = new string[count];
        }

        if (plan.TeachingPeriodsAssistantHeadteacher.Length != plan.NumberAssistantHeadteacher.ToInt())
        {
            var val = plan.NumberAssistantHeadteacher.ToInt() ?? 0;
            var count = val > 0 ? val : 0;
            plan.TeachingPeriodsAssistantHeadteacher = new string[count];
        }

        if (plan.TeachingPeriodsHeadLargeCurriculum.Length != plan.NumberHeadLargeCurriculum.ToInt())
        {
            var val = plan.NumberHeadLargeCurriculum.ToInt() ?? 0;
            var count = val > 0 ? val : 0;
            plan.TeachingPeriodsHeadLargeCurriculum = new string[count];
        }

        if (plan.TeachingPeriodsPastoralLeader.Length != plan.NumberPastoralLeader.ToInt())
        {
            var val = plan.NumberPastoralLeader.ToInt() ?? 0;
            var count = val > 0 ? val : 0;
            plan.TeachingPeriodsPastoralLeader = new string[count];
        }

        if (plan.TeachingPeriodsOtherMembers.Length != plan.NumberOtherMembers.ToInt())
        {
            var val = plan.NumberOtherMembers.ToInt() ?? 0;
            var count = val > 0 ? val : 0;
            plan.TeachingPeriodsOtherMembers = new string[count];
        }
    }
}