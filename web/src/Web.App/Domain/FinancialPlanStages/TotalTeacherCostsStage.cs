namespace Web.App.Domain.FinancialPlanStages;

public class TotalTeacherCostsStage : Stage
{
    public string? TotalTeacherCosts { get; set; }

    public override void SetPlanValues(FinancialPlan plan)
    {
        plan.TotalTeacherCosts = TotalTeacherCosts;
    }
}