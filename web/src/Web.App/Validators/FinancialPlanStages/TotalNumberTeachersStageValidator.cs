using FluentValidation;
using Web.App.Domain;
namespace Web.App.Validators.FinancialPlanStages;

public class TotalNumberTeachersStageValidator : AbstractValidator<TotalNumberTeachersStage>
{
    public TotalNumberTeachersStageValidator()
    {
        RuleFor(p => p.TotalNumberOfTeachersFte)
            .NotEmpty()
            .WithMessage("Enter the number of full-time equivalent teachers you have")
            .GreaterThanOrEqualTo(1)
            .WithMessage("Number of full-time equivalent teachers must be 1 or more");
    }
}