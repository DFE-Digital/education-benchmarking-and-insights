using FluentValidation;
using Web.App.Domain.FinancialPlanStages;
using Web.App.Extensions;

namespace Web.App.Validators.FinancialPlanStages;

public class TeacherPeriodAllocationStageValidator : AbstractValidator<TeacherPeriodAllocationStage>
{
    public TeacherPeriodAllocationStageValidator()
    {
        When(p => p.PupilsNursery > 0, () =>
        {
            RuleFor(p => p.TeachersNursery)
                .NotEmpty()
                .WithMessage("Enter your teacher period allocation figures from nursery")
                .Must(x => x.ToInt() is not null)
                .WithMessage("Teacher period allocation figures from nursery must be a whole number")
                .Must(x => x.ToInt() is > 0)
                .WithMessage("Teacher period allocation figures from nursery must be 0 or more");
        });

        When(p => p.PupilsMixedReceptionYear1.ToInt() > 0, () =>
        {
            RuleFor(p => p.TeachersMixedReceptionYear1)
                .NotEmpty()
                .WithMessage("Enter your teacher period allocation figures from reception and year 1")
                .Must(x => x.ToInt() is not null)
                .WithMessage("Teacher period allocation figures from reception and year 1 must be a whole number")
                .Must(x => x.ToInt() is > 0)
                .WithMessage("Teacher period allocation figures from reception and year 1 must be 0 or more");
        });

        When(p => p.PupilsMixedYear1Year2.ToInt() > 0, () =>
        {
            RuleFor(p => p.TeachersMixedYear1Year2)
                .NotEmpty()
                .WithMessage("Enter your teacher period allocation figures from year 1 and year 2")
                .Must(x => x.ToInt() is not null)
                .WithMessage("Teacher period allocation figures from year 1 and year 2 must be a whole number")
                .Must(x => x.ToInt() is > 0)
                .WithMessage("Teacher period allocation figures from year 1 and year 2 must be 0 or more");
        });

        When(p => p.PupilsMixedYear2Year3.ToInt() > 0, () =>
        {
            RuleFor(p => p.TeachersMixedYear2Year3)
                .NotEmpty()
                .WithMessage("Enter your teacher period allocation figures from year 2 and year 3")
                .Must(x => x.ToInt() is not null)
                .WithMessage("Teacher period allocation figures from year 2 and year 3 must be a whole number")
                .Must(x => x.ToInt() is > 0)
                .WithMessage("Teacher period allocation figures from year 2 and year 3 must be 0 or more");
        });

        When(p => p.PupilsMixedYear3Year4.ToInt() > 0, () =>
        {
            RuleFor(p => p.TeachersMixedYear3Year4)
                .NotEmpty()
                .WithMessage("Enter your teacher period allocation figures from year 3 and year 4")
                .Must(x => x.ToInt() is not null)
                .WithMessage("Teacher period allocation figures from year 3 and year 4 must be a whole number")
                .Must(x => x.ToInt() is > 0)
                .WithMessage("Teacher period allocation figures from year 3 and year 4 must be 0 or more");
        });

        When(p => p.PupilsMixedYear4Year5.ToInt() > 0, () =>
        {
            RuleFor(p => p.TeachersMixedYear4Year5)
                .NotEmpty()
                .WithMessage("Enter your teacher period allocation figures from year 4 and year 5")
                .Must(x => x.ToInt() is not null)
                .WithMessage("Teacher period allocation figures from year 4 and year 5 must be a whole number")
                .Must(x => x.ToInt() is > 0)
                .WithMessage("Teacher period allocation figures from year 4 and year 5 must be 0 or more");
        });

        When(p => p.PupilsMixedYear5Year6.ToInt() > 0, () =>
        {
            RuleFor(p => p.TeachersMixedYear5Year6)
                .NotEmpty()
                .WithMessage("Enter your teacher period allocation figures from year 5 and year 6")
                .Must(x => x.ToInt() is not null)
                .WithMessage("Teacher period allocation figures from year 5 and year 6 must be a whole number")
                .Must(x => x.ToInt() is > 0)
                .WithMessage("Teacher period allocation figures from year 5 and year 6 must be 0 or more");
        });

        When(p => p.PupilsReception.ToInt() > 0, () =>
        {
            RuleFor(p => p.TeachersReception)
                .NotEmpty()
                .WithMessage("Enter your teacher period allocation figures from reception")
                .Must(x => x.ToInt() is not null)
                .WithMessage("Teacher period allocation figures from reception must be a whole number")
                .Must(x => x.ToInt() is > 0)
                .WithMessage("Teacher period allocation figures from reception must be 0 or more");
        });

        When(p => p.PupilsYear1.ToInt() > 0, () =>
        {
            RuleFor(p => p.TeachersYear1)
                .NotEmpty()
                .WithMessage("Enter your teacher period allocation figures from year 1")
                .Must(x => x.ToInt() is not null)
                .WithMessage("Teacher period allocation figures from year 1 must be a whole number")
                .Must(x => x.ToInt() is > 0)
                .WithMessage("Teacher period allocation figures from year 1 must be 0 or more");
        });

        When(p => p.PupilsYear2.ToInt() > 0, () =>
        {
            RuleFor(p => p.TeachersYear2)
                .NotEmpty()
                .WithMessage("Enter your teacher period allocation figures from year 2")
                .Must(x => x.ToInt() is not null)
                .WithMessage("Teacher period allocation figures from year 2 must be a whole number")
                .Must(x => x.ToInt() is > 0)
                .WithMessage("Teacher period allocation figures from year 2 must be 0 or more");
        });

        When(p => p.PupilsYear3.ToInt() > 0, () =>
        {
            RuleFor(p => p.TeachersYear3)
                .NotEmpty()
                .WithMessage("Enter your teacher period allocation figures from year 3")
                .Must(x => x.ToInt() is not null)
                .WithMessage("Teacher period allocation figures from year 3 must be a whole number")
                .Must(x => x.ToInt() is > 0)
                .WithMessage("Teacher period allocation figures from year 3 must be 0 or more");
        });

        When(p => p.PupilsYear4.ToInt() > 0, () =>
        {
            RuleFor(p => p.TeachersYear4)
                .NotEmpty()
                .WithMessage("Enter your teacher period allocation figures from year 4")
                .Must(x => x.ToInt() is not null)
                .WithMessage("Teacher period allocation figures from year 4 must be a whole number")
                .Must(x => x.ToInt() is > 0)
                .WithMessage("Teacher period allocation figures from year 4 must be 0 or more");
        });

        When(p => p.PupilsYear5.ToInt() > 0, () =>
        {
            RuleFor(p => p.TeachersYear5)
                .NotEmpty()
                .WithMessage("Enter your teacher period allocation figures from year 7")
                .Must(x => x.ToInt() is not null)
                .WithMessage("Teacher period allocation figures from year 5 must be a whole number")
                .Must(x => x.ToInt() is > 0)
                .WithMessage("Teacher period allocation figures from year 5 must be 0 or more");
        });

        When(p => p.PupilsYear6.ToInt() > 0, () =>
        {
            RuleFor(p => p.TeachersYear6)
                .NotEmpty()
                .WithMessage("Enter your teacher period allocation figures from year 6")
                .Must(x => x.ToInt() is not null)
                .WithMessage("Teacher period allocation figures from year 6 must be a whole number")
                .Must(x => x.ToInt() is > 0)
                .WithMessage("Teacher period allocation figures from year 6 must be 0 or more");
        });

        When(p => p.PupilsYear7.ToInt() > 0, () =>
        {
            RuleFor(p => p.TeachersYear7)
                .NotEmpty()
                .WithMessage("Enter your teacher period allocation figures from year 7")
                .Must(x => x.ToInt() is not null)
                .WithMessage("Teacher period allocation figures from year 7 must be a whole number")
                .Must(x => x.ToInt() is > 0)
                .WithMessage("Teacher period allocation figures from year 7 must be 0 or more");
        });

        When(p => p.PupilsYear8.ToInt() > 0, () =>
        {
            RuleFor(p => p.TeachersYear8)
                .NotEmpty()
                .WithMessage("Enter your teacher period allocation figures from year 8")
                .Must(x => x.ToInt() is not null)
                .WithMessage("Teacher period allocation figures from year 8 must be a whole number")
                .Must(x => x.ToInt() is > 0)
                .WithMessage("Teacher period allocation figures from year 8 must be 0 or more");
        });

        When(p => p.PupilsYear9.ToInt() > 0, () =>
        {
            RuleFor(p => p.TeachersYear9)
                .NotEmpty()
                .WithMessage("Enter your teacher period allocation figures from year 9")
                .Must(x => x.ToInt() is not null)
                .WithMessage("Teacher period allocation figures from year 9 must be a whole number")
                .Must(x => x.ToInt() is > 0)
                .WithMessage("Teacher period allocation figures from year 9 must be 0 or more");
        });

        When(p => p.PupilsYear10.ToInt() > 0, () =>
        {
            RuleFor(p => p.TeachersYear10)
                .NotEmpty()
                .WithMessage("Enter your teacher period allocation figures from year 10")
                .Must(x => x.ToInt() is not null)
                .WithMessage("Teacher period allocation figures from year 10 must be a whole number")
                .Must(x => x.ToInt() is > 0)
                .WithMessage("Teacher period allocation figures from year 10 must be 0 or more");
        });

        When(p => p.PupilsYear11.ToInt() > 0, () =>
        {
            RuleFor(p => p.TeachersYear11)
                .NotEmpty()
                .WithMessage("Enter your teacher period allocation figures from year 11")
                .Must(x => x.ToInt() is not null)
                .WithMessage("Teacher period allocation figures from year 11 must be a whole number")
                .Must(x => x.ToInt() is > 0)
                .WithMessage("Teacher period allocation figures from year 11 must be 0 or more");
        });

        When(p => p.PupilsYear12 > 0, () =>
        {
            RuleFor(p => p.TeachersYear12)
                .NotEmpty()
                .WithMessage("Enter your teacher period allocation figures from year 12")
                .Must(x => x.ToInt() is not null)
                .WithMessage("Teacher period allocation figures from year 12 must be a whole number")
                .Must(x => x.ToInt() is > 0)
                .WithMessage("Teacher period allocation figures from year 12 must be 0 or more");
        });

        When(p => p.PupilsYear13 > 0, () =>
        {
            RuleFor(p => p.TeachersYear13)
                .NotEmpty()
                .WithMessage("Enter your teacher period allocation figures from year 13")
                .Must(x => x.ToInt() is not null)
                .WithMessage("Teacher period allocation figures from year 13 must be a whole number")
                .Must(x => x.ToInt() is > 0)
                .WithMessage("Teacher period allocation figures from year 13 must be 0 or more");
        });
    }
}