namespace Web.App.Domain.Benchmark.FinancialPlanStages;

public class ManagementRolesStage : Stage
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

    public override void SetPlanValues(FinancialPlanInput planInput)
    {
        planInput.ManagementRoleHeadteacher = ManagementRoleHeadteacher;
        planInput.ManagementRoleDeputyHeadteacher = ManagementRoleDeputyHeadteacher;
        planInput.ManagementRoleNumeracyLead = ManagementRoleNumeracyLead;
        planInput.ManagementRoleLiteracyLead = ManagementRoleLiteracyLead;
        planInput.ManagementRoleHeadSmallCurriculum = ManagementRoleHeadSmallCurriculum;
        planInput.ManagementRoleHeadKs1 = ManagementRoleHeadKs1;
        planInput.ManagementRoleHeadKs2 = ManagementRoleHeadKs2;
        planInput.ManagementRoleSenco = ManagementRoleSenco;
        planInput.ManagementRoleAssistantHeadteacher = ManagementRoleAssistantHeadteacher;
        planInput.ManagementRoleHeadLargeCurriculum = ManagementRoleHeadLargeCurriculum;
        planInput.ManagementRolePastoralLeader = ManagementRolePastoralLeader;
        planInput.ManagementRoleOtherMembers = ManagementRoleOtherMembers;

        planInput.NumberHeadteacher = ManagementRoleHeadteacher ? planInput.NumberHeadteacher : null;
        planInput.NumberDeputyHeadteacher = ManagementRoleDeputyHeadteacher ? planInput.NumberDeputyHeadteacher : null;
        planInput.NumberNumeracyLead = ManagementRoleNumeracyLead ? planInput.NumberNumeracyLead : null;
        planInput.NumberLiteracyLead = ManagementRoleLiteracyLead ? planInput.NumberLiteracyLead : null;
        planInput.NumberHeadSmallCurriculum = ManagementRoleHeadSmallCurriculum ? planInput.NumberHeadSmallCurriculum : null;
        planInput.NumberHeadKs1 = ManagementRoleHeadKs1 ? planInput.NumberHeadKs1 : null;
        planInput.NumberHeadKs2 = ManagementRoleHeadKs2 ? planInput.NumberHeadKs2 : null;
        planInput.NumberSenco = ManagementRoleSenco ? planInput.NumberSenco : null;
        planInput.NumberAssistantHeadteacher = ManagementRoleAssistantHeadteacher ? planInput.NumberAssistantHeadteacher : null;
        planInput.NumberHeadLargeCurriculum = ManagementRoleHeadLargeCurriculum ? planInput.NumberHeadLargeCurriculum : null;
        planInput.NumberPastoralLeader = ManagementRolePastoralLeader ? planInput.NumberPastoralLeader : null;
        planInput.NumberOtherMembers = ManagementRoleOtherMembers ? planInput.NumberOtherMembers : null;
    }
}