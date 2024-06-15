namespace Web.App.Domain;

public class OtherTeachingPeriodsStage : Stage
{
    public string? Action { get; set; }
    public List<OtherTeachingPeriod> OtherTeachingPeriods { get; set; } = new();

    public override void SetPlanValues(FinancialPlanInput planInput)
    {
        var periods = OtherTeachingPeriods
            .Select(period => new FinancialPlanInput.OtherTeachingPeriod
            {
                PeriodName = period.PeriodName,
                PeriodsPerTimetable = period.PeriodsPerTimetable
            })
            .ToList();

        planInput.OtherTeachingPeriods = periods;
    }

    public class OtherTeachingPeriod
    {
        public string? PeriodName { get; set; }
        public string? PeriodsPerTimetable { get; set; }
    }
}