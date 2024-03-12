namespace Web.App.Domain.FinancialPlanStages
{
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

        public override void SetPlanValues(FinancialPlan plan)
        {
            plan.ManagementRoleHeadteacher = ManagementRoleHeadteacher;
            plan.ManagementRoleDeputyHeadteacher = ManagementRoleDeputyHeadteacher;
            plan.ManagementRoleNumeracyLead = ManagementRoleNumeracyLead;
            plan.ManagementRoleLiteracyLead = ManagementRoleLiteracyLead;
            plan.ManagementRoleHeadSmallCurriculum = ManagementRoleHeadSmallCurriculum;
            plan.ManagementRoleHeadKs1 = ManagementRoleHeadKs1;
            plan.ManagementRoleHeadKs2 = ManagementRoleHeadKs2;
            plan.ManagementRoleSenco = ManagementRoleSenco;
            plan.ManagementRoleAssistantHeadteacher = ManagementRoleAssistantHeadteacher;
            plan.ManagementRoleHeadLargeCurriculum = ManagementRoleHeadLargeCurriculum;
            plan.ManagementRolePastoralLeader = ManagementRolePastoralLeader;
            plan.ManagementRoleOtherMembers = ManagementRoleOtherMembers;

            plan.NumberHeadteacher = ManagementRoleHeadteacher ? plan.NumberHeadteacher : null;
            plan.NumberDeputyHeadteacher = ManagementRoleDeputyHeadteacher ? plan.NumberDeputyHeadteacher : null;
            plan.NumberNumeracyLead = ManagementRoleNumeracyLead ? plan.NumberNumeracyLead : null;
            plan.NumberLiteracyLead = ManagementRoleLiteracyLead ? plan.NumberLiteracyLead : null;
            plan.NumberHeadSmallCurriculum = ManagementRoleHeadSmallCurriculum ? plan.NumberHeadSmallCurriculum : null;
            plan.NumberHeadKs1 = ManagementRoleHeadKs1 ? plan.NumberHeadKs1 : null;
            plan.NumberHeadKs2 = ManagementRoleHeadKs2 ? plan.NumberHeadKs2 : null;
            plan.NumberSenco = ManagementRoleSenco ? plan.NumberSenco : null;
            plan.NumberAssistantHeadteacher = ManagementRoleAssistantHeadteacher ? plan.NumberAssistantHeadteacher : null;
            plan.NumberHeadLargeCurriculum = ManagementRoleHeadLargeCurriculum ? plan.NumberHeadLargeCurriculum : null;
            plan.NumberPastoralLeader = ManagementRolePastoralLeader ? plan.NumberPastoralLeader : null;
            plan.NumberOtherMembers = ManagementRoleOtherMembers ? plan.NumberOtherMembers : null;
        }
    }
}