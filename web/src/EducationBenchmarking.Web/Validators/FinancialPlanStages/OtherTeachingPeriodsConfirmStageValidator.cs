using EducationBenchmarking.Web.Domain.FinancialPlanStages;
using FluentValidation;

namespace EducationBenchmarking.Web.Validators.FinancialPlanStages
{
    public class OtherTeachingPeriodsConfirmStageValidator : AbstractValidator<OtherTeachingPeriodsConfirmStage>
    {
        public OtherTeachingPeriodsConfirmStageValidator()
        {
            RuleFor(p => p.Proceed)
                .Must(x => x.HasValue)
                .WithMessage("Please select yes or no")
                .WithName("Proceed");
        }
    }
}