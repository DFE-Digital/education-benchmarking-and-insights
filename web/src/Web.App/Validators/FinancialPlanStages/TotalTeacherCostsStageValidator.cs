using FluentValidation;
using Web.App.Domain.FinancialPlanStages;

namespace Web.App.Validators.FinancialPlanStages
{
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
}