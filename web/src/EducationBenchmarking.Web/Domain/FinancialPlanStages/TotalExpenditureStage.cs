namespace EducationBenchmarking.Web.Domain.FinancialPlanStages;

public class TotalExpenditureStage : Stage
{
    public decimal? TotalExpenditure { get; set; }

    public override void SetPlanValues(FinancialPlan plan)
    {
        plan.TotalExpenditure = TotalExpenditure;
    }
}