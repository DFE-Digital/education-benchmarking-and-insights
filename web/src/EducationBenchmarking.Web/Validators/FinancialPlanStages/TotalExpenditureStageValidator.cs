using EducationBenchmarking.Web.Domain.FinancialPlanStages;
using FluentValidation;

namespace EducationBenchmarking.Web.Validators.FinancialPlanStages;

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