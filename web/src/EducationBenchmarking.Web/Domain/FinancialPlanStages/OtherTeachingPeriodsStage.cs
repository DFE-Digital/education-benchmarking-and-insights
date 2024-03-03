namespace EducationBenchmarking.Web.Domain.FinancialPlanStages;

public class OtherTeachingPeriodsStage : Stage
{
    public string? Action { get; set; }
    public List<OtherTeachingPeriod> OtherTeachingPeriods { get; set; } = new();

    public override void SetPlanValues(FinancialPlan plan)
    {
        var periods = OtherTeachingPeriods
            .Select(period => new FinancialPlan.OtherTeachingPeriod
            {
                PeriodName = period.PeriodName,
                PeriodsPerTimetable = period.PeriodsPerTimetable
            })
            .ToList();

        plan.OtherTeachingPeriods = periods;
    }

    public class OtherTeachingPeriod
    {
        public string? PeriodName { get; set; }
        public string? PeriodsPerTimetable { get; set; }
    }
}