using EducationBenchmarking.Web.Domain.FinancialPlanStages;
using FluentValidation;

namespace EducationBenchmarking.Web.Validators.FinancialPlanStages;

public class TotalTeacherCostsStageValidator : AbstractValidator<TotalTeacherCostsStage>
{
    public TotalTeacherCostsStageValidator()
    {
        RuleFor(p => p.TotalTeacherCosts)
            .NotEmpty()
            .WithMessage("Enter your total teacher costs")
            .GreaterThanOrEqualTo(0)
            .WithMessage("Total teacher costs must be 0 or more");
    }
}