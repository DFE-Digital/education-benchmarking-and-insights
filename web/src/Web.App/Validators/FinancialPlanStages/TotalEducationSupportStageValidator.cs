using FluentValidation;
using Web.App.Domain.FinancialPlanStages;

namespace Web.App.Validators.FinancialPlanStages;

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