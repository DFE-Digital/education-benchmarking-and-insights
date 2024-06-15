using FluentValidation;
using Web.App.Domain;
using Web.App.Extensions;

namespace Web.App.Validators.FinancialPlanStages;

public class TotalIncomeStageValidator : AbstractValidator<TotalIncomeStage>
{
    public TotalIncomeStageValidator()
    {
        RuleFor(p => p.TotalIncome)
            .NotEmpty()
            .WithMessage("Enter your total income")
            .Must(x => x.ToInt() is not null)
            .WithMessage("Total income must be a whole number")
            .Must(x => x.ToInt() is >= 0)
            .WithMessage("Total income must be 0 or more");
    }
}