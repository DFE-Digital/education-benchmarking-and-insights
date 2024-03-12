using FluentValidation;
using Web.App.Domain.FinancialPlanStages;
using Web.App.Extensions;

namespace Web.App.Validators.FinancialPlanStages
{
    public class OtherTeachingPeriodsStageValidator : AbstractValidator<OtherTeachingPeriodsStage>
    {
        public OtherTeachingPeriodsStageValidator()
        {
            RuleForEach(p => p.OtherTeachingPeriods).ChildRules(period =>
            {
                period.When(p => !string.IsNullOrEmpty(p.PeriodsPerTimetable), () =>
                {
                    period.RuleFor(p => p.PeriodName)
                        .NotEmpty()
                        .WithMessage("Enter name of teaching period");
                });

                period.When(p => !string.IsNullOrEmpty(p.PeriodName), () =>
                {
                    period.RuleFor(p => p.PeriodsPerTimetable)
                        .NotEmpty()
                        .WithMessage("Enter number of periods per timetable cycle")
                        .Must(x => x.ToInt() is not null)
                        .WithMessage("Number of periods per timetable cycle must be a whole number")
                        .Must(x => x.ToInt() is > 0)
                        .WithMessage("Number of periods per timetable cycle must be 1 or more");
                });
            }).WithName("OtherTeachingPeriods");
        }
    }
}