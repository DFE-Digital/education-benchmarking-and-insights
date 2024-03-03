using EducationBenchmarking.Web.Domain.FinancialPlanStages;
using FluentValidation;

namespace EducationBenchmarking.Web.Validators.FinancialPlanStages;

public class PrePopulateDataStageValidator : AbstractValidator<PrePopulateDataStage>
{
    public PrePopulateDataStageValidator()
    {
        RuleFor(p => p.UseFigures)
            .Must(x => x.HasValue)
            .WithMessage("Select yes if you want to use these figures");
    }
}