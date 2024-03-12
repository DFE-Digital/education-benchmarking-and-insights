namespace Web.App.Domain.FinancialPlanStages
{
    public class TeachingPeriodsManagerStage : Stage
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
        public string[] TeachingPeriodsHeadteacher { get; set; } = Array.Empty<string>();
        public string[] TeachingPeriodsDeputyHeadteacher { get; set; } = Array.Empty<string>();
        public string[] TeachingPeriodsNumeracyLead { get; set; } = Array.Empty<string>();
        public string[] TeachingPeriodsLiteracyLead { get; set; } = Array.Empty<string>();
        public string[] TeachingPeriodsHeadSmallCurriculum { get; set; } = Array.Empty<string>();
        public string[] TeachingPeriodsHeadKs1 { get; set; } = Array.Empty<string>();
        public string[] TeachingPeriodsHeadKs2 { get; set; } = Array.Empty<string>();
        public string[] TeachingPeriodsSenco { get; set; } = Array.Empty<string>();
        public string[] TeachingPeriodsAssistantHeadteacher { get; set; } = Array.Empty<string>();
        public string[] TeachingPeriodsHeadLargeCurriculum { get; set; } = Array.Empty<string>();
        public string[] TeachingPeriodsPastoralLeader { get; set; } = Array.Empty<string>();
        public string[] TeachingPeriodsOtherMembers { get; set; } = Array.Empty<string>();

        public override void SetPlanValues(FinancialPlan plan)
        {
            plan.TeachingPeriodsHeadteacher = TeachingPeriodsHeadteacher;
            plan.TeachingPeriodsDeputyHeadteacher = TeachingPeriodsDeputyHeadteacher;
            plan.TeachingPeriodsNumeracyLead = TeachingPeriodsNumeracyLead;
            plan.TeachingPeriodsLiteracyLead = TeachingPeriodsLiteracyLead;
            plan.TeachingPeriodsHeadSmallCurriculum = TeachingPeriodsHeadSmallCurriculum;
            plan.TeachingPeriodsHeadKs1 = TeachingPeriodsHeadKs1;
            plan.TeachingPeriodsHeadKs2 = TeachingPeriodsHeadKs2;
            plan.TeachingPeriodsSenco = TeachingPeriodsSenco;
            plan.TeachingPeriodsAssistantHeadteacher = TeachingPeriodsAssistantHeadteacher;
            plan.TeachingPeriodsHeadLargeCurriculum = TeachingPeriodsHeadLargeCurriculum;
            plan.TeachingPeriodsPastoralLeader = TeachingPeriodsPastoralLeader;
            plan.TeachingPeriodsOtherMembers = TeachingPeriodsOtherMembers;

            plan.IsComplete = true;
            plan.TargetContactRatio = plan.TargetContactRatio <= 0 ? 0.78M : plan.TargetContactRatio;
        }
    }
}