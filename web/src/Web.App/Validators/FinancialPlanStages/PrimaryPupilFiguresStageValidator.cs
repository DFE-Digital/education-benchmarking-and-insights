using FluentValidation;
using Web.App.Domain.FinancialPlanStages;
using Web.App.Extensions;

namespace Web.App.Validators.FinancialPlanStages
{
    public class PrimaryPupilFiguresStageValidator : AbstractValidator<PrimaryPupilFiguresStage>
    {
        public PrimaryPupilFiguresStageValidator()
        {
            RuleFor(p => p)
                   .Must(HasPrimaryPupilFigures)
                   .WithMessage("Enter pupil figures for at least one year")
                   .WithName("PupilFigures");

            When(p => p.PupilsNursery is not null, () =>
            {
                RuleFor(p => p.PupilsNursery)
                    .GreaterThanOrEqualTo(0)
                    .WithMessage("Pupil figures for nursery must be 0 or more");
            });

            When(p => !string.IsNullOrEmpty(p.PupilsMixedReceptionYear1), () =>
            {
                RuleFor(p => p.PupilsMixedReceptionYear1)
                    .Must(x => x.ToInt() is not null)
                    .WithMessage("Pupil figures for reception and year 1 must be a whole number")
                    .Must(x => x.ToInt() is >= 0)
                    .WithMessage("Pupil figures for reception and year 1 must be 0 or more");
            });

            When(p => !string.IsNullOrEmpty(p.PupilsMixedYear1Year2), () =>
            {
                RuleFor(p => p.PupilsMixedYear1Year2)
                    .Must(x => x.ToInt() is not null)
                    .WithMessage("Pupil figures for year 1 and year 2 must be a whole number")
                    .Must(x => x.ToInt() is >= 0)
                    .WithMessage("Pupil figures for year 1 and year 2 must be 0 or more");
            });

            When(p => !string.IsNullOrEmpty(p.PupilsMixedYear2Year3), () =>
            {
                RuleFor(p => p.PupilsMixedYear2Year3)
                    .Must(x => x.ToInt() is not null)
                    .WithMessage("Pupil figures for year 2 and year 3 must be a whole number")
                    .Must(x => x.ToInt() is >= 0)
                    .WithMessage("Pupil figures for year 2 and year 3 must be 0 or more");
            });

            When(p => !string.IsNullOrEmpty(p.PupilsMixedYear3Year4), () =>
            {
                RuleFor(p => p.PupilsMixedYear3Year4)
                    .Must(x => x.ToInt() is not null)
                    .WithMessage("Pupil figures for year 3 and year 4 must be a whole number")
                    .Must(x => x.ToInt() is >= 0)
                    .WithMessage("Pupil figures for year 3 and year 4 must be 0 or more");
            });

            When(p => !string.IsNullOrEmpty(p.PupilsMixedYear4Year5), () =>
            {
                RuleFor(p => p.PupilsMixedYear4Year5)
                    .Must(x => x.ToInt() is not null)
                    .WithMessage("Pupil figures for year 4 and year 5 must be a whole number")
                    .Must(x => x.ToInt() is >= 0)
                    .WithMessage("Pupil figures for year 4 and year 5 must be 0 or more");
            });

            When(p => !string.IsNullOrEmpty(p.PupilsMixedYear5Year6), () =>
            {
                RuleFor(p => p.PupilsMixedYear5Year6)
                    .Must(x => x.ToInt() is not null)
                    .WithMessage("Pupil figures for year 5 and year 6 must be a whole number")
                    .Must(x => x.ToInt() is >= 0)
                    .WithMessage("Pupil figures for year 5 and year 6 must be 0 or more");
            });

            When(p => !string.IsNullOrEmpty(p.PupilsReception), () =>
            {
                RuleFor(p => p.PupilsReception)
                    .Must(x => x.ToInt() is not null)
                    .WithMessage("Pupil figures for reception must be a whole number")
                    .Must(x => x.ToInt() is >= 0)
                    .WithMessage("Pupil figures for reception must be 0 or more");
            });

            When(p => !string.IsNullOrEmpty(p.PupilsYear1), () =>
            {
                RuleFor(p => p.PupilsYear1)
                    .Must(x => x.ToInt() is not null)
                    .WithMessage("Pupil figures for year 1 must be a whole number")
                    .Must(x => x.ToInt() is >= 0)
                    .WithMessage("Pupil figures for year 1 must be 0 or more");
            });

            When(p => !string.IsNullOrEmpty(p.PupilsYear2), () =>
            {
                RuleFor(p => p.PupilsYear2)
                    .Must(x => x.ToInt() is not null)
                    .WithMessage("Pupil figures for year 2 must be a whole number")
                    .Must(x => x.ToInt() is >= 0)
                    .WithMessage("Pupil figures for year 2 must be 0 or more");
            });

            When(p => !string.IsNullOrEmpty(p.PupilsYear3), () =>
            {
                RuleFor(p => p.PupilsYear3)
                    .Must(x => x.ToInt() is not null)
                    .WithMessage("Pupil figures for year 3 must be a whole number")
                    .Must(x => x.ToInt() is >= 0)
                    .WithMessage("Pupil figures for year 3 must be 0 or more");
            });

            When(p => !string.IsNullOrEmpty(p.PupilsYear4), () =>
            {
                RuleFor(p => p.PupilsYear4)
                    .Must(x => x.ToInt() is not null)
                    .WithMessage("Pupil figures for year 4 must be a whole number")
                    .Must(x => x.ToInt() is >= 0)
                    .WithMessage("Pupil figures for year 4 must be 0 or more");
            });

            When(p => !string.IsNullOrEmpty(p.PupilsYear5), () =>
            {
                RuleFor(p => p.PupilsYear5)
                    .Must(x => x.ToInt() is not null)
                    .WithMessage("Pupil figures for year 5 must be a whole number")
                    .Must(x => x.ToInt() is >= 0)
                    .WithMessage("Pupil figures for year 5 must be 0 or more");
            });

            When(p => !string.IsNullOrEmpty(p.PupilsYear6), () =>
            {
                RuleFor(p => p.PupilsYear6)
                    .Must(x => x.ToInt() is not null)
                    .WithMessage("Pupil figures for year 6 must be a whole number")
                    .Must(x => x.ToInt() is >= 0)
                    .WithMessage("Pupil figures for year 6 must be 0 or more");
            });
        }

        private static bool HasPrimaryPupilFigures(PrimaryPupilFiguresStage model)
        {
            return model.PupilsNursery is > 0 ||
                   model.PupilsMixedReceptionYear1.ToInt() is > 0 ||
                   model.PupilsMixedYear1Year2.ToInt() is > 0 ||
                   model.PupilsMixedYear2Year3.ToInt() is > 0 ||
                   model.PupilsMixedYear3Year4.ToInt() is > 0 ||
                   model.PupilsMixedYear4Year5.ToInt() is > 0 ||
                   model.PupilsMixedYear5Year6.ToInt() is > 0 ||
                   model.PupilsReception.ToInt() is > 0 ||
                   model.PupilsYear1.ToInt() is > 0 ||
                   model.PupilsYear2.ToInt() is > 0 ||
                   model.PupilsYear3.ToInt() is > 0 ||
                   model.PupilsYear4.ToInt() is > 0 ||
                   model.PupilsYear5.ToInt() is > 0 ||
                   model.PupilsYear6.ToInt() is > 0;
        }
    }
}