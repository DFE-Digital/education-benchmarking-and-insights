using EducationBenchmarking.Web.Domain.FinancialPlanStages;
using FluentValidation;

namespace EducationBenchmarking.Web.Validators.FinancialPlanStages;

public class TotalEducationSupportStageValidator : AbstractValidator<TotalEducationSupportStage>
{
    public TotalEducationSupportStageValidator()
    {
        RuleFor(p => p.EducationSupportStaffCosts)
            .NotEmpty()
            .WithMessage("Enter your total education support staff costs")
            .GreaterThanOrEqualTo(0)
            .WithMessage("Total education support staff costs must be 0 or more");
    }
}