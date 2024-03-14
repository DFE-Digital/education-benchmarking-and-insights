using FluentValidation;
using Web.App.Domain.FinancialPlanStages;
using Web.App.Extensions;

namespace Web.App.Validators.FinancialPlanStages;

public class TeachingAssistantFiguresStageValidator : AbstractValidator<TeachingAssistantFiguresStage>
{
    public TeachingAssistantFiguresStageValidator()
    {
        When(p => p.PupilsMixedReceptionYear1.ToInt() > 0, () =>
        {
            RuleFor(p => p.AssistantsMixedReceptionYear1)
                .NotEmpty()
                .WithMessage("Enter your teaching assistant figures for reception and year 1")
                .Must(x => x is >= 0)
                .WithMessage("Teaching assistant figures for reception and year 1 must be 0 or more");
        });

        When(p => p.PupilsMixedYear1Year2.ToInt() > 0, () =>
        {
            RuleFor(p => p.AssistantsMixedYear1Year2)
                .NotEmpty()
                .WithMessage("Enter your teaching assistant figures for year 1 and year 2")
                .Must(x => x is >= 0)
                .WithMessage("Teaching assistant figures for year 1 and year 2 must be 0 or more");
        });

        When(p => p.PupilsMixedYear2Year3.ToInt() > 0, () =>
        {
            RuleFor(p => p.AssistantsMixedYear2Year3)
                .NotEmpty()
                .WithMessage("Enter your teaching assistant figures for year 2 and year 3")
                .Must(x => x is >= 0)
                .WithMessage("Teaching assistant figures for year 2 and year 3 must be 0 or more");
        });

        When(p => p.PupilsMixedYear3Year4.ToInt() > 0, () =>
        {
            RuleFor(p => p.AssistantsMixedYear3Year4)
                .NotEmpty()
                .WithMessage("Enter your teaching assistant figures for year 3 and year 4")
                .Must(x => x is >= 0)
                .WithMessage("Teaching assistant figures for year 3 and year 4 must be 0 or more");
        });

        When(p => p.PupilsMixedYear4Year5.ToInt() > 0, () =>
        {
            RuleFor(p => p.AssistantsMixedYear4Year5)
                .NotEmpty()
                .WithMessage("Enter your teaching assistant figures for year 4 and year 5")
                .Must(x => x is >= 0)
                .WithMessage("Teaching assistant figures for year 4 and year 5 must be 0 or more");
        });

        When(p => p.PupilsMixedYear5Year6.ToInt() > 0, () =>
        {
            RuleFor(p => p.AssistantsMixedYear5Year6)
                .NotEmpty()
                .WithMessage("Enter your teaching assistant figures for year 5 and year 6")
                .Must(x => x is >= 0)
                .WithMessage("Teaching assistant figures for year 5 and year 6 must be 0 or more");
        });

        When(p => p.PupilsNursery > 0, () =>
        {
            RuleFor(p => p.AssistantsNursery)
                .NotEmpty()
                .WithMessage("Enter your teaching assistant figures for nursery")
                .Must(x => x is >= 0)
                .WithMessage("Teaching assistant figures for nursery must be 0 or more");
        });

        When(p => p.PupilsReception.ToInt() > 0, () =>
        {
            RuleFor(p => p.AssistantsReception)
                .NotEmpty()
                .WithMessage("Enter your teaching assistant figures for reception")
                .Must(x => x is >= 0)
                .WithMessage("Teaching assistant figures for reception must be 0 or more");
        });

        When(p => p.PupilsYear1.ToInt() > 0, () =>
        {
            RuleFor(p => p.AssistantsYear1)
                .NotEmpty()
                .WithMessage("Enter your teaching assistant figures for year 1")
                .Must(x => x is >= 0)
                .WithMessage("Teaching assistant figures for year 1 must be 0 or more");
        });

        When(p => p.PupilsYear2.ToInt() > 0, () =>
        {
            RuleFor(p => p.AssistantsYear2)
                .NotEmpty()
                .WithMessage("Enter your teaching assistant figures for year 2")
                .Must(x => x is >= 0)
                .WithMessage("Teaching assistant figures for year 2 must be 0 or more");
        });

        When(p => p.PupilsYear3.ToInt() > 0, () =>
        {
            RuleFor(p => p.AssistantsYear3)
                .NotEmpty()
                .WithMessage("Enter your teaching assistant figures for year 3")
                .Must(x => x is >= 0)
                .WithMessage("Teaching assistant figures for year 3 must be 0 or more");
        });

        When(p => p.PupilsYear4.ToInt() > 0, () =>
        {
            RuleFor(p => p.AssistantsYear4)
                .NotEmpty()
                .WithMessage("Enter your teaching assistant figures for year 4")
                .Must(x => x is >= 0)
                .WithMessage("Teaching assistant figures for year 4 must be 0 or more");
        });

        When(p => p.PupilsYear5.ToInt() > 0, () =>
        {
            RuleFor(p => p.AssistantsYear5)
                .NotEmpty()
                .WithMessage("Enter your teaching assistant figures for year 5")
                .Must(x => x is >= 0)
                .WithMessage("Teaching assistant figures for year 5 must be 0 or more");
        });

        When(p => p.PupilsYear6.ToInt() > 0, () =>
        {
            RuleFor(p => p.AssistantsYear6)
                .NotEmpty()
                .WithMessage("Enter your teaching assistant figures for year 6")
                .Must(x => x is >= 0)
                .WithMessage("Teaching assistant figures for year 6 must be 0 or more");
        });
    }
}