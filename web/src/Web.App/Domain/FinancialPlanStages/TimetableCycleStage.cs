namespace Web.App.Domain.FinancialPlanStages
{
    public class TimetableCycleStage : Stage
    {
        public string? TimetablePeriods { get; set; }

        public override void SetPlanValues(FinancialPlan plan)
        {
            plan.TimetablePeriods = TimetablePeriods;
        }
    }
}