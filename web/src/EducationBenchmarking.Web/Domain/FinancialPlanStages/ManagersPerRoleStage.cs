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
    }
}