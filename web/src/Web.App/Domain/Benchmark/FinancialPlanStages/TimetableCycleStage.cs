namespace Web.App.Domain;

public class TimetableCycleStage : Stage
{
    public string? TimetablePeriods { get; set; }

    public override void SetPlanValues(FinancialPlanInput planInput)
    {
        planInput.TimetablePeriods = TimetablePeriods;
    }
}