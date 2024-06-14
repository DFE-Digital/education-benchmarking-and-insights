using FluentValidation;
using Web.App.Domain.Benchmark.FinancialPlanStages;
using Web.App.Extensions;

namespace Web.App.Validators.FinancialPlanStages;

public class TotalExpenditureStageValidator : AbstractValidator<TotalExpenditureStage>
{
    public TotalExpenditureStageValidator()
    {
        RuleFor(p => p.TotalExpenditure)
            .NotEmpty()
            .WithMessage("Enter your total expenditure")
            .Must(x => x.ToInt() is not null)
            .WithMessage("Total expenditure must be a whole number")
            .Must(x => x.ToInt() is >= 0)
            .WithMessage("Total expenditure must be 0 or more");
    }
}