using EducationBenchmarking.Web.Extensions;

namespace EducationBenchmarking.Web.Domain.FinancialPlanStages;

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
            plan.TeachingPeriodsHeadteacher = new string[plan.NumberHeadteacher.ToInt() ?? 0];
        }

        if (plan.TeachingPeriodsDeputyHeadteacher.Length != plan.NumberDeputyHeadteacher.ToInt())
        {
            plan.TeachingPeriodsDeputyHeadteacher = new string[plan.NumberDeputyHeadteacher.ToInt() ?? 0];
        }

        if (plan.TeachingPeriodsNumeracyLead.Length != plan.NumberNumeracyLead.ToInt())
        {
            plan.TeachingPeriodsNumeracyLead = new string[plan.NumberNumeracyLead.ToInt() ?? 0];
        }

        if (plan.TeachingPeriodsLiteracyLead.Length != plan.NumberLiteracyLead.ToInt())
        {
            plan.TeachingPeriodsLiteracyLead = new string[plan.NumberLiteracyLead.ToInt() ?? 0];
        }

        if (plan.TeachingPeriodsHeadSmallCurriculum.Length != plan.NumberHeadSmallCurriculum.ToInt())
        {
            plan.TeachingPeriodsHeadSmallCurriculum = new string[plan.NumberHeadSmallCurriculum.ToInt() ?? 0];
        }

        if (plan.TeachingPeriodsHeadKs1.Length != plan.NumberHeadKs1.ToInt())
        {
            plan.TeachingPeriodsHeadKs1 = new string[plan.NumberHeadKs1.ToInt() ?? 0];
        }

        if (plan.TeachingPeriodsHeadKs2.Length != plan.NumberHeadKs2.ToInt())
        {
            plan.TeachingPeriodsHeadKs2 = new string[plan.NumberHeadKs2.ToInt() ?? 0];
        }

        if (plan.TeachingPeriodsSenco.Length != plan.NumberSenco.ToInt())
        {
            plan.TeachingPeriodsSenco = new string[plan.NumberSenco.ToInt() ?? 0];
        }

        if (plan.TeachingPeriodsAssistantHeadteacher.Length != plan.NumberAssistantHeadteacher.ToInt())
        {
            plan.TeachingPeriodsAssistantHeadteacher = new string[plan.NumberAssistantHeadteacher.ToInt() ?? 0];
        }

        if (plan.TeachingPeriodsHeadLargeCurriculum.Length != plan.NumberHeadLargeCurriculum.ToInt())
        {
            plan.TeachingPeriodsHeadLargeCurriculum = new string[plan.NumberHeadLargeCurriculum.ToInt() ?? 0];
        }

        if (plan.TeachingPeriodsPastoralLeader.Length != plan.NumberPastoralLeader.ToInt())
        {
            plan.TeachingPeriodsPastoralLeader = new string[plan.NumberPastoralLeader.ToInt() ?? 0];
        }

        if (plan.TeachingPeriodsOtherMembers.Length != plan.NumberOtherMembers.ToInt())
        {
            plan.TeachingPeriodsOtherMembers = new string[plan.NumberOtherMembers.ToInt() ?? 0];
        }
    }
}