using EducationBenchmarking.Web.Extensions;
using EducationBenchmarking.Web.ViewModels;
using FluentValidation;
using FluentValidation.Internal;

namespace EducationBenchmarking.Web.Validators;

public static class Strategy
{
    public static Action<ValidationStrategy<SchoolPlanCreateViewModel>> SelectYear => o =>
        o.IncludeRuleSets(PlanSteps.SelectYear);

    public static Action<ValidationStrategy<SchoolPlanCreateViewModel>> PrePopulateData => o =>
        o.IncludeRuleSets(PlanSteps.PrePopulateData);

    public static Action<ValidationStrategy<SchoolPlanCreateViewModel>> TimetableCycle => o =>
        o.IncludeRuleSets(PlanSteps.TimetableCycle);

    public static Action<ValidationStrategy<SchoolPlanCreateViewModel>> TotalIncome => o =>
        o.IncludeRuleSets(PlanSteps.TotalIncome);

    public static Action<ValidationStrategy<SchoolPlanCreateViewModel>> TotalExpenditure => o =>
        o.IncludeRuleSets(PlanSteps.TotalExpenditure);

    public static Action<ValidationStrategy<SchoolPlanCreateViewModel>> TotalTeacherCosts => o =>
        o.IncludeRuleSets(PlanSteps.TotalTeacherCosts);

    public static Action<ValidationStrategy<SchoolPlanCreateViewModel>> TotalNumberTeachers => o =>
        o.IncludeRuleSets(PlanSteps.TotalNumberTeachers);

    public static Action<ValidationStrategy<SchoolPlanCreateViewModel>> TotalEducationSupport => o =>
        o.IncludeRuleSets(PlanSteps.TotalEducationSupport);

    public static Action<ValidationStrategy<SchoolPlanCreateViewModel>> PrimaryHasMixedAgeClasses => o =>
        o.IncludeRuleSets(PlanSteps.PrimaryHasMixedAgeClasses);

    public static Action<ValidationStrategy<SchoolPlanCreateViewModel>> PrimaryMixedAgeClasses => o =>
        o.IncludeRuleSets(PlanSteps.PrimaryMixedAgeClasses);

    public static Action<ValidationStrategy<SchoolPlanCreateViewModel>> PupilFigures => o =>
        o.IncludeRuleSets(PlanSteps.PupilFigures);

    public static Action<ValidationStrategy<SchoolPlanCreateViewModel>> PrimaryPupilFigures => o =>
        o.IncludeRuleSets(PlanSteps.PrimaryPupilFigures);
}

public class CreatePlanValidator : AbstractValidator<SchoolPlanCreateViewModel>
{
    public CreatePlanValidator()
    {
        RuleSet(PlanSteps.SelectYear, () =>
        {
            RuleFor(p => p.Year)
                .Must(x => x.HasValue && Constants.AvailableYears.Contains(x.Value))
                .WithMessage("Select the academic year you want to plan");
        });

        RuleSet(PlanSteps.PrePopulateData, () =>
        {
            RuleFor(p => p.UseFigures)
                .Must(x => x.HasValue)
                .WithMessage("Select yes if you want to use these figures");
        });

        RuleSet(PlanSteps.TimetableCycle, () =>
        {
            RuleFor(p => p.TimetablePeriods)
                .NotEmpty()
                .WithMessage("Enter the number of periods in one timetable cycle")
                .Must(x => int.TryParse(x, out _))
                .WithMessage("Number of periods in one timetable cycle must be a whole number")
                .Must(x => int.TryParse(x, out var val) && val > 0)
                .WithMessage("Number of periods in one timetable cycle must be 1 or more");
        });

        RuleSet(PlanSteps.TotalIncome, () =>
        {
            RuleFor(p => p.TotalIncome)
                .NotEmpty()
                .WithMessage("Enter your total income")
                .GreaterThanOrEqualTo(0)
                .WithMessage("Total income must be 0 or more");
        });

        RuleSet(PlanSteps.TotalExpenditure, () =>
        {
            RuleFor(p => p.TotalExpenditure)
                .NotEmpty()
                .WithMessage("Enter your total expenditure")
                .GreaterThanOrEqualTo(0)
                .WithMessage("Total expenditure must be 0 or more");
        });

        RuleSet(PlanSteps.TotalTeacherCosts, () =>
        {
            RuleFor(p => p.TotalTeacherCosts)
                .NotEmpty()
                .WithMessage("Enter your total teacher costs")
                .GreaterThanOrEqualTo(0)
                .WithMessage("Total teacher costs must be 0 or more");
        });

        RuleSet(PlanSteps.TotalNumberTeachers, () =>
        {
            RuleFor(p => p.TotalNumberOfTeachersFte)
                .NotEmpty()
                .WithMessage("Enter your number of full-time equivalent teachers")
                .GreaterThanOrEqualTo(1)
                .WithMessage("Number of full-time equivalent teachers must be 1 or more");
        });

        RuleSet(PlanSteps.TotalEducationSupport, () =>
        {
            RuleFor(p => p.EducationSupportStaffCosts)
                .NotEmpty()
                .WithMessage("Enter your total education support staff costs")
                .GreaterThanOrEqualTo(0)
                .WithMessage("Total education support staff costs must be 0 or more");
        });

        RuleSet(PlanSteps.PrimaryHasMixedAgeClasses, () =>
        {
            RuleFor(p => p.HasMixedAgeClasses)
                .NotNull()
                .WithMessage("Select yes if you have mixed age classes");
        });

        RuleSet(PlanSteps.PrimaryMixedAgeClasses, () =>
        {
            RuleFor(p => p)
                .Must(x => x.MixedAgeReceptionYear1 || x.MixedAgeYear1Year2
                || x.MixedAgeYear2Year3 || x.MixedAgeYear3Year4 || x.MixedAgeYear4Year5 || x.MixedAgeYear5Year6)
                .WithMessage("Select which years have mixed age classes")
                .WithName("MixedAgeClasses");
        });

        RuleSet(PlanSteps.PupilFigures, () =>
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
        });

        RuleSet(PlanSteps.PrimaryPupilFigures, () =>
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
        });
    }

    private static bool HasPrimaryPupilFigures(SchoolPlanCreateViewModel model)
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

    private static bool HasPupilFigures(SchoolPlanCreateViewModel model)
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