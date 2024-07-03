using FluentValidation;
using Web.App.Domain;
using Web.App.Extensions;
namespace Web.App.Validators.FinancialPlanStages;

public class TotalTeacherCostsStageValidator : AbstractValidator<TotalTeacherCostsStage>
{
    public TotalTeacherCostsStageValidator()
    {
        RuleFor(p => p.TotalTeacherCosts)
            .NotEmpty()
            .WithMessage("Enter your total spend on teaching staff")
            .Must(x => x.ToInt() is not null)
            .WithMessage("Total teacher costs must be a whole number")
            .Must(x => x.ToInt() is >= 0)
            .WithMessage("Total teacher costs must be 0 or more");
    }
}