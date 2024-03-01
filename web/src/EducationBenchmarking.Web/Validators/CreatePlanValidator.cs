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

    public static Action<ValidationStrategy<SchoolPlanCreateViewModel>> TeacherPeriodAllocation => o =>
        o.IncludeRuleSets(PlanSteps.TeacherPeriodAllocation);

    public static Action<ValidationStrategy<SchoolPlanCreateViewModel>> OtherTeachingPeriods => o =>
        o.IncludeRuleSets(PlanSteps.OtherTeachingPeriods);

    public static Action<ValidationStrategy<SchoolPlanCreateViewModel>> TeachingAssistantsFigures => o =>
        o.IncludeRuleSets(PlanSteps.TeachingAssistantFigures);

    public static Action<ValidationStrategy<SchoolPlanCreateViewModel>> ManagementRoles => o =>
        o.IncludeRuleSets(PlanSteps.ManagementRoles);

    public static Action<ValidationStrategy<SchoolPlanCreateViewModel>> ManagersPerRole => o =>
        o.IncludeRuleSets(PlanSteps.ManagersPerRole);
}

public class CreatePlanValidator : AbstractValidator<SchoolPlanCreateViewModel>
{
    public CreatePlanValidator()
    {
        RulesForSelectYear();
        RulesForPrePopulateData();
        RulesForTimetableCycle();
        RulesForTotalIncome();
        RulesForTotalExpenditure();
        RulesForTotalTeacherCosts();
        RulesForTotalNumberTeachers();
        RulesForTotalEducationSupport();
        RulesForPrimaryHasMixedAgeClasses();
        RulesForPrimaryMixedAgeClasses();
        RulesForPupilFigures();
        RulesForPrimaryPupilFigures();
        RulesForTeacherPeriodAllocation();
        RulesForTeachingAssistantFigures();
        RulesForOtherTeachingPeriods();
        RulesForOtherManagementRoles();
        RulesForManagersPerRole();
    }

    private void RulesForManagersPerRole()
    {
        RuleSet(PlanSteps.ManagersPerRole, () =>
        {
            When(p => p.ManagementRoleHeadteacher, () =>
            {
                RuleFor(p => p.NumberHeadteacher)
                    .NotEmpty()
                    .WithMessage("Enter your total number of headteachers")
                    .Must(x => x.ToInt() is not null)
                    .WithMessage("Total number of headteachers must be a whole number")
                    .Must(x => x.ToInt() is > 0)
                    .WithMessage("Total number of headteachers must be 0 or more");
            });

            When(p => p.ManagementRoleDeputyHeadteacher, () =>
            {
                RuleFor(p => p.NumberDeputyHeadteacher)
                    .NotEmpty()
                    .WithMessage("Enter your total number of deputy headteachers")
                    .Must(x => x.ToInt() is not null)
                    .WithMessage("Total number of deputy headteachers must be a whole number")
                    .Must(x => x.ToInt() is > 0)
                    .WithMessage("Total number of deputy headteachers must be 0 or more");
            });

            When(p => p.ManagementRoleNumeracyLead, () =>
            {
                RuleFor(p => p.NumberNumeracyLead)
                    .NotEmpty()
                    .WithMessage("Enter your total number of numeracy leads")
                    .Must(x => x.ToInt() is not null)
                    .WithMessage("Total number of numeracy leads must be a whole number")
                    .Must(x => x.ToInt() is > 0)
                    .WithMessage("Total number of numeracy leads must be 0 or more");
            });

            When(p => p.ManagementRoleLiteracyLead, () =>
            {
                RuleFor(p => p.NumberLiteracyLead)
                    .NotEmpty()
                    .WithMessage("Enter your total number of literacy leads")
                    .Must(x => x.ToInt() is not null)
                    .WithMessage("Total number of literacy leads must be a whole number")
                    .Must(x => x.ToInt() is > 0)
                    .WithMessage("Total number of literacy leads must be 0 or more");
            });

            When(p => p.ManagementRoleHeadSmallCurriculum, () =>
            {
                RuleFor(p => p.NumberHeadSmallCurriculum)
                    .NotEmpty()
                    .WithMessage("Enter your total number of head of small curriculum areas")
                    .Must(x => x.ToInt() is not null)
                    .WithMessage("Total number of head of small curriculum areas must be a whole number")
                    .Must(x => x.ToInt() is > 0)
                    .WithMessage("Total number of head of small curriculum areas must be 0 or more");
            });

            When(p => p.ManagementRoleHeadKs1, () =>
            {
                RuleFor(p => p.NumberHeadKs1)
                    .NotEmpty()
                    .WithMessage("Enter your total number of heads of KS1")
                    .Must(x => x.ToInt() is not null)
                    .WithMessage("Total number of heads of KS1 must be a whole number")
                    .Must(x => x.ToInt() is > 0)
                    .WithMessage("Total number of heads of KS1 must be 0 or more");
            });

            When(p => p.ManagementRoleHeadKs2, () =>
            {
                RuleFor(p => p.NumberHeadKs2)
                    .NotEmpty()
                    .WithMessage("Enter your total number of heads of KS2")
                    .Must(x => x.ToInt() is not null)
                    .WithMessage("Total number of heads of KS2 must be a whole number")
                    .Must(x => x.ToInt() is > 0)
                    .WithMessage("Total number of heads of KS2 must be 0 or more");
            });

            When(p => p.ManagementRoleSenco, () =>
            {
                RuleFor(p => p.NumberSenco)
                    .NotEmpty()
                    .WithMessage("Enter your total number of special education needs coordinators")
                    .Must(x => x.ToInt() is not null)
                    .WithMessage("Total number of special education needs coordinators must be a whole number")
                    .Must(x => x.ToInt() is > 0)
                    .WithMessage("Total number of special education needs coordinators must be 0 or more");
            });

            When(p => p.ManagementRoleAssistantHeadteacher, () =>
            {
                RuleFor(p => p.NumberAssistantHeadteacher)
                    .NotEmpty()
                    .WithMessage("Enter your total number of assistant headteachers")
                    .Must(x => x.ToInt() is not null)
                    .WithMessage("Total number of assistant headteachers must be a whole number")
                    .Must(x => x.ToInt() is > 0)
                    .WithMessage("Total number of assistant headteachers must be 0 or more");
            });

            When(p => p.ManagementRoleHeadLargeCurriculum, () =>
            {
                RuleFor(p => p.NumberHeadLargeCurriculum)
                    .NotEmpty()
                    .WithMessage("Enter your total number of head of large curriculum areas")
                    .Must(x => x.ToInt() is not null)
                    .WithMessage("Total number of head of large curriculum areas must be a whole number")
                    .Must(x => x.ToInt() is > 0)
                    .WithMessage("Total number of head of large curriculum areas must be 0 or more");
            });

            When(p => p.ManagementRolePastoralLeader, () =>
            {
                RuleFor(p => p.NumberPastoralLeader)
                    .NotEmpty()
                    .WithMessage("Enter your total number of pastoral leaders")
                    .Must(x => x.ToInt() is not null)
                    .WithMessage("Total number of pastoral leaders must be a whole number")
                    .Must(x => x.ToInt() is > 0)
                    .WithMessage("Total number of pastoral leaders must be 0 or more");
            });

            When(p => p.ManagementRoleOtherMembers, () =>
            {
                RuleFor(p => p.NumberOtherMembers)
                    .NotEmpty()
                    .WithMessage("Enter your total number of other members of management or leadership staff")
                    .Must(x => x.ToInt() is not null)
                    .WithMessage("Total number of other members of management or leadership staff must be a whole number")
                    .Must(x => x.ToInt() is > 0)
                    .WithMessage("Total number of other members of management or leadership staff must be 0 or more");
            });
        });
    }

    private void RulesForOtherManagementRoles()
    {
        RuleSet(PlanSteps.ManagementRoles, () =>
        {
            RuleFor(p => p)
                .Must(x => x.ManagementRoleHeadteacher || x.ManagementRoleDeputyHeadteacher ||
                           x.ManagementRoleNumeracyLead || x.ManagementRoleLiteracyLead ||
                           x.ManagementRoleHeadSmallCurriculum || x.ManagementRoleHeadKs1 ||
                           x.ManagementRoleHeadKs2 || x.ManagementRoleSenco ||
                           x.ManagementRoleAssistantHeadteacher || x.ManagementRoleHeadLargeCurriculum ||
                           x.ManagementRolePastoralLeader || x.ManagementRoleOtherMembers)
                .WithMessage("Select at least one management role")
                .WithName("ManagementRoles");
        });
    }

    private void RulesForOtherTeachingPeriods()
    {
        RuleSet(PlanSteps.OtherTeachingPeriods, () =>
        {
            RuleForEach(p => p.OtherTeachingPeriods).ChildRules(period =>
            {
                period.When(p => !string.IsNullOrEmpty(p.PeriodsPerTimetable), () =>
                {
                    period.RuleFor(p => p.PeriodName)
                        .NotEmpty()
                        .WithMessage("Enter name of teaching period");
                });

                period.When(p => !string.IsNullOrEmpty(p.PeriodName), () =>
                {
                    period.RuleFor(p => p.PeriodsPerTimetable)
                        .NotEmpty()
                        .WithMessage("Enter number of periods per timetable cycle")
                        .Must(x => x.ToInt() is not null)
                        .WithMessage("Number of periods per timetable cycle must be a whole number")
                        .Must(x => x.ToInt() is > 0)
                        .WithMessage("Number of periods per timetable cycle must be 0 or more");
                });
            }).WithName("OtherTeachingPeriods");
        });
    }

    private void RulesForTeacherPeriodAllocation()
    {
        RuleSet(PlanSteps.TeacherPeriodAllocation, () =>
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
        });
    }

    private void RulesForPrimaryPupilFigures()
    {
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

    private void RulesForPupilFigures()
    {
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
    }

    private void RulesForPrimaryMixedAgeClasses()
    {
        RuleSet(PlanSteps.PrimaryMixedAgeClasses, () =>
        {
            RuleFor(p => p)
                .Must(x => x.MixedAgeReceptionYear1 || x.MixedAgeYear1Year2
                                                    || x.MixedAgeYear2Year3 || x.MixedAgeYear3Year4 ||
                                                    x.MixedAgeYear4Year5 || x.MixedAgeYear5Year6)
                .WithMessage("Select which years have mixed age classes")
                .WithName("MixedAgeClasses");
        });
    }

    private void RulesForPrimaryHasMixedAgeClasses()
    {
        RuleSet(PlanSteps.PrimaryHasMixedAgeClasses, () =>
        {
            RuleFor(p => p.HasMixedAgeClasses)
                .NotNull()
                .WithMessage("Select yes if you have mixed age classes");
        });
    }

    private void RulesForTotalEducationSupport()
    {
        RuleSet(PlanSteps.TotalEducationSupport, () =>
        {
            RuleFor(p => p.EducationSupportStaffCosts)
                .NotEmpty()
                .WithMessage("Enter your total education support staff costs")
                .GreaterThanOrEqualTo(0)
                .WithMessage("Total education support staff costs must be 0 or more");
        });
    }

    private void RulesForTotalNumberTeachers()
    {
        RuleSet(PlanSteps.TotalNumberTeachers, () =>
        {
            RuleFor(p => p.TotalNumberOfTeachersFte)
                .NotEmpty()
                .WithMessage("Enter your number of full-time equivalent teachers")
                .GreaterThanOrEqualTo(1)
                .WithMessage("Number of full-time equivalent teachers must be 1 or more");
        });
    }

    private void RulesForTotalTeacherCosts()
    {
        RuleSet(PlanSteps.TotalTeacherCosts, () =>
        {
            RuleFor(p => p.TotalTeacherCosts)
                .NotEmpty()
                .WithMessage("Enter your total teacher costs")
                .GreaterThanOrEqualTo(0)
                .WithMessage("Total teacher costs must be 0 or more");
        });
    }

    private void RulesForTotalExpenditure()
    {
        RuleSet(PlanSteps.TotalExpenditure, () =>
        {
            RuleFor(p => p.TotalExpenditure)
                .NotEmpty()
                .WithMessage("Enter your total expenditure")
                .GreaterThanOrEqualTo(0)
                .WithMessage("Total expenditure must be 0 or more");
        });
    }

    private void RulesForTotalIncome()
    {
        RuleSet(PlanSteps.TotalIncome, () =>
        {
            RuleFor(p => p.TotalIncome)
                .NotEmpty()
                .WithMessage("Enter your total income")
                .GreaterThanOrEqualTo(0)
                .WithMessage("Total income must be 0 or more");
        });
    }

    private void RulesForTimetableCycle()
    {
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
    }

    private void RulesForPrePopulateData()
    {
        RuleSet(PlanSteps.PrePopulateData, () =>
        {
            RuleFor(p => p.UseFigures)
                .Must(x => x.HasValue)
                .WithMessage("Select yes if you want to use these figures");
        });
    }

    private void RulesForSelectYear()
    {
        RuleSet(PlanSteps.SelectYear, () =>
        {
            RuleFor(p => p.Year)
                .Must(x => x.HasValue && Constants.AvailableYears.Contains(x.Value))
                .WithMessage("Select the academic year you want to plan");
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

    private void RulesForTeachingAssistantFigures()
    {
        RuleSet(PlanSteps.TeachingAssistantFigures, () =>
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
        });
    }
}