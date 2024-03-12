namespace Web.App.Domain.FinancialPlanStages
{
    public class TotalTeacherCostsStage : Stage
    {
        public decimal? TotalTeacherCosts { get; set; }

        public override void SetPlanValues(FinancialPlan plan)
        {
            plan.TotalTeacherCosts = TotalTeacherCosts;
        }
    }
}