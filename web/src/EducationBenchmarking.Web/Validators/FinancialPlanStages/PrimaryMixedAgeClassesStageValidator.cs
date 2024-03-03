using EducationBenchmarking.Web.Domain.FinancialPlanStages;
using FluentValidation;

namespace EducationBenchmarking.Web.Validators.FinancialPlanStages;

public class PrimaryMixedAgeClassesStageValidator : AbstractValidator<PrimaryMixedAgeClassesStage>
{
    public PrimaryMixedAgeClassesStageValidator()
    {
        RuleFor(p => p)
            .Must(x => x.MixedAgeReceptionYear1 || x.MixedAgeYear1Year2
                                                || x.MixedAgeYear2Year3 || x.MixedAgeYear3Year4 ||
                                                x.MixedAgeYear4Year5 || x.MixedAgeYear5Year6)
            .WithMessage("Select which years have mixed age classes")
            .WithName("MixedAgeClasses");
    }
}