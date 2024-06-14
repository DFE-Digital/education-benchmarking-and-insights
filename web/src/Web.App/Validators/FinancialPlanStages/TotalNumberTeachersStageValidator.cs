using FluentValidation;
using Web.App.Domain.Benchmark.FinancialPlanStages;

namespace Web.App.Validators.FinancialPlanStages;

public class TotalNumberTeachersStageValidator : AbstractValidator<TotalNumberTeachersStage>
{
    public TotalNumberTeachersStageValidator()
    {
        RuleFor(p => p.TotalNumberOfTeachersFte)
            .NotEmpty()
            .WithMessage("Enter your number of full-time equivalent teachers")
            .GreaterThanOrEqualTo(1)
            .WithMessage("Number of full-time equivalent teachers must be 1 or more");
    }
}