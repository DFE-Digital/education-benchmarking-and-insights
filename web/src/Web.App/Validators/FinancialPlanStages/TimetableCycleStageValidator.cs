using FluentValidation;
using Web.App.Domain;

namespace Web.App.Validators.FinancialPlanStages;

public class TimetableCycleStageValidator : AbstractValidator<TimetableCycleStage>
{
    public TimetableCycleStageValidator()
    {
        RuleFor(p => p.TimetablePeriods)
            .NotEmpty()
            .WithMessage("Enter the number of periods in one timetable cycle")
            .Must(x => int.TryParse(x, out _))
            .WithMessage("Number of periods in one timetable cycle must be a whole number")
            .Must(x => int.TryParse(x, out var val) && val > 0)
            .WithMessage("Number of periods in one timetable cycle must be 1 or more");
    }
}