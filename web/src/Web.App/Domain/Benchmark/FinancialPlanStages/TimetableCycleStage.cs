namespace Web.App.Domain.Benchmark.FinancialPlanStages;

public class TimetableCycleStage : Stage
{
    public string? TimetablePeriods { get; set; }

    public override void SetPlanValues(FinancialPlanInput planInput)
    {
        planInput.TimetablePeriods = TimetablePeriods;
    }
}