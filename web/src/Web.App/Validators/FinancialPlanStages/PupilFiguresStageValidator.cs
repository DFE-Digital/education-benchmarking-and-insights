using FluentValidation;
using Web.App.Domain.FinancialPlanStages;
using Web.App.Extensions;

namespace Web.App.Validators.FinancialPlanStages;

public class PupilFiguresStageValidator : AbstractValidator<PupilFiguresStage>
{
    public PupilFiguresStageValidator()
    {
        RuleFor(p => p)
            .Must(HasPupilFigures)
            .WithMessage("Enter pupil figures for at least one year")
            .WithName("PupilFigures");

        When(p => !string.IsNullOrEmpty(p.PupilsYear7), () =>
        {
            RuleFor(p => p.PupilsYear7)
                .Must(x => x.ToInt() is not null)
                .WithMessage("Pupil figures for year 7 must be a whole number")
                .Must(x => x.ToInt() is >= 0)
                .WithMessage("Pupil figures for year 7 must be 0 or more");
        });

        When(p => !string.IsNullOrEmpty(p.PupilsYear8), () =>
        {
            RuleFor(p => p.PupilsYear8)
                .Must(x => x.ToInt() is not null)
                .WithMessage("Pupil figures for year 8 must be a whole number")
                .Must(x => x.ToInt() is >= 0)
                .WithMessage("Pupil figures for year 8 must be 0 or more");
        });

        When(p => !string.IsNullOrEmpty(p.PupilsYear9), () =>
        {
            RuleFor(p => p.PupilsYear9)
                .Must(x => x.ToInt() is not null)
                .WithMessage("Pupil figures for year 9 must be a whole number")
                .Must(x => x.ToInt() is >= 0)
                .WithMessage("Pupil figures for year 9 must be 0 or more");
        });

        When(p => !string.IsNullOrEmpty(p.PupilsYear10), () =>
        {
            RuleFor(p => p.PupilsYear10)
                .Must(x => x.ToInt() is not null)
                .WithMessage("Pupil figures for year 10 must be a whole number")
                .Must(x => x.ToInt() is >= 0)
                .WithMessage("Pupil figures for year 10 must be 0 or more");
        });

        When(p => !string.IsNullOrEmpty(p.PupilsYear11), () =>
        {
            RuleFor(p => p.PupilsYear11)
                .Must(x => x.ToInt() is not null)
                .WithMessage("Pupil figures for year 11 must be a whole number")
                .Must(x => x.ToInt() is >= 0)
                .WithMessage("Pupil figures for year 11 must be 0 or more");
        });

        When(p => p.PupilsYear12 is not null, () =>
        {
            RuleFor(p => p.PupilsYear12)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Pupil figures for year 12 must be 0 or more");
        });

        When(p => p.PupilsYear13 is not null, () =>
        {
            RuleFor(p => p.PupilsYear13)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Pupil figures for year 13 must be 0 or more");
        });
    }

    private static bool HasPupilFigures(PupilFiguresStage model)
    {
        return model.PupilsYear7.ToInt() is > 0 ||
               model.PupilsYear8.ToInt() is > 0 ||
               model.PupilsYear9.ToInt() is > 0 ||
               model.PupilsYear10.ToInt() is > 0 ||
               model.PupilsYear11.ToInt() is > 0 ||
               model.PupilsYear12 is > 0 ||
               model.PupilsYear13 is > 0;
    }
}