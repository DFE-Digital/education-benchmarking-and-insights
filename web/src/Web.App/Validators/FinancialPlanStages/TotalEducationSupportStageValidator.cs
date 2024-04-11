using FluentValidation;
using Web.App.Domain.FinancialPlanStages;
using Web.App.Extensions;

namespace Web.App.Validators.FinancialPlanStages;

public class TotalEducationSupportStageValidator : AbstractValidator<TotalEducationSupportStage>
{
    public TotalEducationSupportStageValidator()
    {
        RuleFor(p => p.EducationSupportStaffCosts)
            .NotEmpty()
            .WithMessage("Enter your total education support staff costs")
            .Must(x => x.ToInt() is not null)
            .WithMessage("Total education support staff costs must be a whole number")
            .Must(x => x.ToInt() is >= 0)
            .WithMessage("Total education support staff costs must be 0 or more");
    }
}