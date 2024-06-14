namespace Web.App.Domain.Benchmark.FinancialPlanStages;

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

    public override void SetPlanValues(FinancialPlanInput planInput)
    {
        planInput.TeachingPeriodsHeadteacher = TeachingPeriodsHeadteacher;
        planInput.TeachingPeriodsDeputyHeadteacher = TeachingPeriodsDeputyHeadteacher;
        planInput.TeachingPeriodsNumeracyLead = TeachingPeriodsNumeracyLead;
        planInput.TeachingPeriodsLiteracyLead = TeachingPeriodsLiteracyLead;
        planInput.TeachingPeriodsHeadSmallCurriculum = TeachingPeriodsHeadSmallCurriculum;
        planInput.TeachingPeriodsHeadKs1 = TeachingPeriodsHeadKs1;
        planInput.TeachingPeriodsHeadKs2 = TeachingPeriodsHeadKs2;
        planInput.TeachingPeriodsSenco = TeachingPeriodsSenco;
        planInput.TeachingPeriodsAssistantHeadteacher = TeachingPeriodsAssistantHeadteacher;
        planInput.TeachingPeriodsHeadLargeCurriculum = TeachingPeriodsHeadLargeCurriculum;
        planInput.TeachingPeriodsPastoralLeader = TeachingPeriodsPastoralLeader;
        planInput.TeachingPeriodsOtherMembers = TeachingPeriodsOtherMembers;

        planInput.IsComplete = true;
        planInput.TargetContactRatio = planInput.TargetContactRatio <= 0 ? 0.78M : planInput.TargetContactRatio;
    }
}