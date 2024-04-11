namespace Web.App.Domain.FinancialPlanStages;

public class TotalExpenditureStage : Stage
{
    public string? TotalExpenditure { get; set; }

    public override void SetPlanValues(FinancialPlan plan)
    {
        plan.TotalExpenditure = TotalExpenditure;
    }
}