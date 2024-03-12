using FluentValidation;
using Web.App.Domain.FinancialPlanStages;

namespace Web.App.Validators.FinancialPlanStages
{
    public class TotalExpenditureStageValidator : AbstractValidator<TotalExpenditureStage>
    {
        public TotalExpenditureStageValidator()
        {
            RuleFor(p => p.TotalExpenditure)
                .NotEmpty()
                .WithMessage("Enter your total expenditure")
                .GreaterThanOrEqualTo(0)
                .WithMessage("Total expenditure must be 0 or more");
        }
    }
}