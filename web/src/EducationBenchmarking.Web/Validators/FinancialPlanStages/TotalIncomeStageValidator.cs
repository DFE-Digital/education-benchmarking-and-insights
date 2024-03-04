using EducationBenchmarking.Web.Domain.FinancialPlanStages;
using FluentValidation;

namespace EducationBenchmarking.Web.Validators.FinancialPlanStages;

public class TotalIncomeStageValidator : AbstractValidator<TotalIncomeStage>
{
    public TotalIncomeStageValidator()
    {
        RuleFor(p => p.TotalIncome)
            .NotEmpty()
            .WithMessage("Enter your total income")
            .GreaterThanOrEqualTo(0)
            .WithMessage("Total income must be 0 or more");
    }
}