using Newtonsoft.Json;
using Web.App.Domain;
using Web.App.Extensions;

namespace Web.App.ViewModels;

public class SchoolDeploymentPlanViewModel(School school, FinancialPlan plan, string? referrer)
{
    public string? Name => school.Name;
    public bool IsPrimary => school.IsPrimary;
    public int Year => plan.Year;
    public string? Urn => plan.Urn;
    public decimal TotalIncome => plan.TotalIncome ?? 0;
    public decimal TotalExpenditure => plan.TotalExpenditure ?? 0;
    public decimal TotalTeacherCosts => plan.TotalTeacherCosts ?? 0;
    public decimal EducationSupportStaffCosts => plan.EducationSupportStaffCosts ?? 0;
    public decimal TotalNumberOfTeachersFte => plan.TotalNumberOfTeachersFte ?? 0;
    public int TimetablePeriods => plan.TimetablePeriods.ToInt() ?? 0;
    public decimal PupilsNursery => plan.PupilsNursery ?? 0;
    public int PupilsReception => plan.PupilsReception.ToInt() ?? 0;
    public int PupilsYear1 => plan.PupilsYear1.ToInt() ?? 0;
    public int PupilsYear2 => plan.PupilsYear2.ToInt() ?? 0;
    public int PupilsYear3 => plan.PupilsYear3.ToInt() ?? 0;
    public int PupilsYear4 => plan.PupilsYear4.ToInt() ?? 0;
    public int PupilsYear5 => plan.PupilsYear5.ToInt() ?? 0;
    public int PupilsYear6 => plan.PupilsYear6.ToInt() ?? 0;
    public int PupilsMixedReceptionYear1 => plan.PupilsMixedReceptionYear1.ToInt() ?? 0;
    public int PupilsMixedYear1Year2 => plan.PupilsMixedYear1Year2.ToInt() ?? 0;
    public int PupilsMixedYear2Year3 => plan.PupilsMixedYear2Year3.ToInt() ?? 0;
    public int PupilsMixedYear3Year4 => plan.PupilsMixedYear3Year4.ToInt() ?? 0;
    public int PupilsMixedYear4Year5 => plan.PupilsMixedYear4Year5.ToInt() ?? 0;
    public int PupilsMixedYear5Year6 => plan.PupilsMixedYear5Year6.ToInt() ?? 0;
    public int PupilsYear7 => plan.PupilsYear7.ToInt() ?? 0;
    public int PupilsYear8 => plan.PupilsYear8.ToInt() ?? 0;
    public int PupilsYear9 => plan.PupilsYear9.ToInt() ?? 0;
    public int PupilsYear10 => plan.PupilsYear10.ToInt() ?? 0;
    public int PupilsYear11 => plan.PupilsYear11.ToInt() ?? 0;
    public decimal PupilsYear12 => plan.PupilsYear12 ?? 0;
    public decimal PupilsYear13 => plan.PupilsYear13 ?? 0;
    public int TeachersNursery => plan.TeachersNursery.ToInt() ?? 0;
    public int TeachersMixedReceptionYear1 => plan.TeachersMixedReceptionYear1.ToInt() ?? 0;
    public int TeachersMixedYear1Year2 => plan.TeachersMixedYear1Year2.ToInt() ?? 0;
    public int TeachersMixedYear2Year3 => plan.TeachersMixedYear2Year3.ToInt() ?? 0;
    public int TeachersMixedYear3Year4 => plan.TeachersMixedYear3Year4.ToInt() ?? 0;
    public int TeachersMixedYear4Year5 => plan.TeachersMixedYear4Year5.ToInt() ?? 0;
    public int TeachersMixedYear5Year6 => plan.TeachersMixedYear5Year6.ToInt() ?? 0;
    public int TeachersReception => plan.TeachersReception.ToInt() ?? 0;
    public int TeachersYear1 => plan.TeachersYear1.ToInt() ?? 0;
    public int TeachersYear2 => plan.TeachersYear2.ToInt() ?? 0;
    public int TeachersYear3 => plan.TeachersYear3.ToInt() ?? 0;
    public int TeachersYear4 => plan.TeachersYear4.ToInt() ?? 0;
    public int TeachersYear5 => plan.TeachersYear5.ToInt() ?? 0;
    public int TeachersYear6 => plan.TeachersYear6.ToInt() ?? 0;
    public int TeachersYear7 => plan.TeachersYear7.ToInt() ?? 0;
    public int TeachersYear8 => plan.TeachersYear8.ToInt() ?? 0;
    public int TeachersYear9 => plan.TeachersYear9.ToInt() ?? 0;
    public int TeachersYear10 => plan.TeachersYear10.ToInt() ?? 0;
    public int TeachersYear11 => plan.TeachersYear11.ToInt() ?? 0;
    public int TeachersYear12 => plan.TeachersYear12.ToInt() ?? 0;
    public int TeachersYear13 => plan.TeachersYear13.ToInt() ?? 0;
    public decimal AssistantsNursery => plan.AssistantsNursery ?? 0;
    public decimal AssistantsMixedReceptionYear1 => plan.AssistantsMixedReceptionYear1 ?? 0;
    public decimal AssistantsMixedYear1Year2 => plan.AssistantsMixedYear1Year2 ?? 0;
    public decimal AssistantsMixedYear2Year3 => plan.AssistantsMixedYear2Year3 ?? 0;
    public decimal AssistantsMixedYear3Year4 => plan.AssistantsMixedYear3Year4 ?? 0;
    public decimal AssistantsMixedYear4Year5 => plan.AssistantsMixedYear4Year5 ?? 0;
    public decimal AssistantsMixedYear5Year6 => plan.AssistantsMixedYear5Year6 ?? 0;
    public decimal AssistantsReception => plan.AssistantsReception ?? 0;
    public decimal AssistantsYear1 => plan.AssistantsYear1 ?? 0;
    public decimal AssistantsYear2 => plan.AssistantsYear2 ?? 0;
    public decimal AssistantsYear3 => plan.AssistantsYear3 ?? 0;
    public decimal AssistantsYear4 => plan.AssistantsYear4 ?? 0;
    public decimal AssistantsYear5 => plan.AssistantsYear5 ?? 0;
    public decimal AssistantsYear6 => plan.AssistantsYear6 ?? 0;
    public decimal TargetContactRatio => plan.TargetContactRatio;

    public decimal TotalPupils => PupilsNursery + PupilsReception + PupilsYear1 + PupilsYear2 + PupilsYear3 +
                                  PupilsYear4 + PupilsYear5 + PupilsYear6 + PupilsMixedReceptionYear1 +
                                  PupilsMixedYear1Year2 + PupilsMixedYear2Year3 + PupilsMixedYear3Year4 +
                                  PupilsMixedYear4Year5 + PupilsMixedYear5Year6 + PupilsYear7 + PupilsYear8 +
                                  PupilsYear9 + PupilsYear10 + PupilsYear11 + PupilsYear12 + PupilsYear13;

    public decimal TeachingPeriods => TeachersNursery + TeachersReception + TeachersYear1 + TeachersYear2 +
                                      TeachersYear3 + TeachersYear4 + TeachersYear5 + TeachersYear6 +
                                      TeachersMixedReceptionYear1 + TeachersMixedYear1Year2 + TeachersMixedYear2Year3 +
                                      TeachersMixedYear3Year4 + TeachersMixedYear4Year5 + TeachersMixedYear5Year6 +
                                      TeachersYear7 + TeachersYear8 + TeachersYear9 + TeachersYear10 + TeachersYear11 +
                                      TeachersYear12 + TeachersYear13;

    public decimal TotalTeachingAssistants => AssistantsNursery + AssistantsReception + AssistantsYear1 +
                                              AssistantsYear2 + AssistantsYear3 + AssistantsYear4 + AssistantsYear5 +
                                              AssistantsYear6 + AssistantsMixedReceptionYear1 +
                                              AssistantsMixedYear1Year2 + AssistantsMixedYear2Year3 +
                                              AssistantsMixedYear3Year4 + AssistantsMixedYear4Year5 +
                                              AssistantsMixedYear5Year6;

    public decimal OtherPeriods => plan.OtherTeachingPeriods
        .Sum(x => x.PeriodsPerTimetable.ToInt() ?? 0);

    public decimal TotalTeachingPeriods => TeachingPeriods + OtherPeriods;

    public decimal PupilTeacherRatio => TotalPupils / TotalNumberOfTeachersFte;

    public decimal AverageClassSize => TotalPupils * TimetablePeriods / TotalTeachingPeriods;

    public Rating AverageClassSizeRating =>
        RatingCalculations.AverageClassSize(AverageClassSize);

    public decimal AverageTeacherLoad => TotalTeachingPeriods / TotalNumberOfTeachersFte;

    public decimal TeacherContactRatio => TotalTeachingPeriods / (TimetablePeriods * TotalNumberOfTeachersFte);

    public Rating TeacherContactRatioRating =>
        RatingCalculations.ContactRatio(TeacherContactRatio);

    public decimal IncomePerPupil => TotalIncome / TotalPupils;

    public decimal TeacherCostPercentageExpenditure => TotalTeacherCosts / TotalExpenditure * 100;

    public decimal TeacherCostPercentageIncome => TotalTeacherCosts / TotalIncome * 100;

    public decimal InYearBalance => TotalIncome - TotalExpenditure;

    public Rating InYearBalanceRating =>
        RatingCalculations.InYearBalancePercentIncome(InYearBalance / TotalIncome * 100);

    public decimal CostPerLesson => TotalTeacherCosts / TotalTeachingPeriods;

    public decimal AverageTeacherCost => TotalTeacherCosts / TotalNumberOfTeachersFte;

    public decimal AverageTeachingAssistantCost => EducationSupportStaffCosts / TotalTeachingAssistants;

    public ManagementRole[] ManagementRoles => BuildManagementRoles();

    public ScenarioPlan[] ScenarioPlans => BuildScenarioPlans();

    public PrimaryPupilGroup[] PrimaryStaffDeployment => BuildPrimaryStaffDeployment();

    public PupilGroup[] StaffDeployment => BuildStaffDeployment();

    public string ChartData => IsPrimary
        ? PrimaryStaffDeployment.Select((x, i) => new
            {
                group = x.Description,
                pupilsOnRoll = x.PercentPupilsOnRoll,
                teacherCost = x.PercentTeacherCost,
                id = i
            })
            .ToArray().ToJson(Formatting.None)
        : StaffDeployment.Select((x, i) => new
            {
                group = x.Description,
                pupilsOnRoll = x.PercentPupilsOnRoll,
                teacherCost = x.PercentTeacherCost,
                id = i
            })
            .ToArray().ToJson(Formatting.None);

    public string? Referrer => referrer;

    private PupilGroup[] BuildStaffDeployment()
    {
        var groups = new List<PupilGroup>();

        if (PupilsYear7 > 0)
        {
            groups.Add(new(
                "Year 7",
                PupilsYear7,
                TeachersYear7,
                TotalTeachingPeriods,
                TotalNumberOfTeachersFte,
                TotalTeacherCosts,
                TotalPupils));
        }

        if (PupilsYear8 > 0)
        {
            groups.Add(new(
                "Year 8",
                PupilsYear8,
                TeachersYear8,
                TotalTeachingPeriods,
                TotalNumberOfTeachersFte,
                TotalTeacherCosts,
                TotalPupils));
        }

        if (PupilsYear9 > 0)
        {
            groups.Add(new(
                "Year 9",
                PupilsYear9,
                TeachersYear9,
                TotalTeachingPeriods,
                TotalNumberOfTeachersFte,
                TotalTeacherCosts,
                TotalPupils));
        }

        if (PupilsYear10 > 0)
        {
            groups.Add(new(
                "Year 10",
                PupilsYear10,
                TeachersYear10,
                TotalTeachingPeriods,
                TotalNumberOfTeachersFte,
                TotalTeacherCosts,
                TotalPupils));
        }

        if (PupilsYear11 > 0)
        {
            groups.Add(new(
                "Year 11",
                PupilsYear11,
                TeachersYear11,
                TotalTeachingPeriods,
                TotalNumberOfTeachersFte,
                TotalTeacherCosts,
                TotalPupils));
        }

        if (PupilsYear12 > 0)
        {
            groups.Add(new(
                "Year 12",
                PupilsYear12,
                TeachersYear12,
                TotalTeachingPeriods,
                TotalNumberOfTeachersFte,
                TotalTeacherCosts,
                TotalPupils));
        }

        if (PupilsYear13 > 0)
        {
            groups.Add(new(
                "Year 13",
                PupilsYear13,
                TeachersYear13,
                TotalTeachingPeriods,
                TotalNumberOfTeachersFte,
                TotalTeacherCosts,
                TotalPupils));
        }

        foreach (var period in plan.OtherTeachingPeriods)
        {
            var timetable = period.PeriodsPerTimetable.ToInt() ?? 0;
            if (period.PeriodName is not null && timetable > 0)
            {
                groups.Add(new(
                    period.PeriodName,
                    0M,
                    timetable,
                    TotalTeachingPeriods,
                    TotalNumberOfTeachersFte,
                    TotalTeacherCosts,
                    TotalPupils));
            }
        }

        return groups.ToArray();
    }

    private PrimaryPupilGroup[] BuildPrimaryStaffDeployment()
    {
        var groups = new List<PrimaryPupilGroup>();

        if (PupilsReception > 0)
        {
            groups.Add(new(
                "Nursery",
                PupilsReception,
                TeachersReception,
                TotalTeachingPeriods,
                TotalNumberOfTeachersFte,
                TotalTeacherCosts,
                AssistantsNursery,
                TotalTeachingAssistants,
                EducationSupportStaffCosts,
                TotalPupils));
        }

        if (PupilsYear1 > 0)
        {
            groups.Add(new(
                "Year 1",
                PupilsYear1,
                TeachersYear1,
                TotalTeachingPeriods,
                TotalNumberOfTeachersFte,
                TotalTeacherCosts,
                AssistantsYear1,
                TotalTeachingAssistants,
                EducationSupportStaffCosts,
                TotalPupils));
        }

        if (PupilsYear2 > 0)
        {
            groups.Add(new(
                "Year 2",
                PupilsYear2,
                TeachersYear2,
                TotalTeachingPeriods,
                TotalNumberOfTeachersFte,
                TotalTeacherCosts,
                AssistantsYear2,
                TotalTeachingAssistants,
                EducationSupportStaffCosts,
                TotalPupils));
        }

        if (PupilsYear3 > 0)
        {
            groups.Add(new(
                "Year 3",
                PupilsYear3,
                TeachersYear3,
                TotalTeachingPeriods,
                TotalNumberOfTeachersFte,
                TotalTeacherCosts,
                AssistantsYear3,
                TotalTeachingAssistants,
                EducationSupportStaffCosts,
                TotalPupils));
        }

        if (PupilsYear4 > 0)
        {
            groups.Add(new(
                "Year 4",
                PupilsYear4,
                TeachersYear4,
                TotalTeachingPeriods,
                TotalNumberOfTeachersFte,
                TotalTeacherCosts,
                AssistantsYear4,
                TotalTeachingAssistants,
                EducationSupportStaffCosts,
                TotalPupils));
        }

        if (PupilsYear5 > 0)
        {
            groups.Add(new(
                "Year 5",
                PupilsYear5,
                TeachersYear5,
                TotalTeachingPeriods,
                TotalNumberOfTeachersFte,
                TotalTeacherCosts,
                AssistantsYear5,
                TotalTeachingAssistants,
                EducationSupportStaffCosts,
                TotalPupils));
        }

        if (PupilsYear6 > 0)
        {
            groups.Add(new(
                "Year 6",
                PupilsYear6,
                TeachersYear6,
                TotalTeachingPeriods,
                TotalNumberOfTeachersFte,
                TotalTeacherCosts,
                AssistantsYear6,
                TotalTeachingAssistants,
                EducationSupportStaffCosts,
                TotalPupils));
        }

        if (PupilsMixedReceptionYear1 > 0)
        {
            groups.Add(new(
                "Reception and year 1",
                PupilsMixedReceptionYear1,
                TeachersMixedReceptionYear1,
                TotalTeachingPeriods,
                TotalNumberOfTeachersFte,
                TotalTeacherCosts,
                AssistantsMixedReceptionYear1,
                TotalTeachingAssistants,
                EducationSupportStaffCosts,
                TotalPupils));
        }

        if (PupilsMixedYear1Year2 > 0)
        {
            groups.Add(new(
                "Year 1 and year 2",
                PupilsMixedYear1Year2,
                TeachersMixedYear1Year2,
                TotalTeachingPeriods,
                TotalNumberOfTeachersFte,
                TotalTeacherCosts,
                AssistantsMixedYear1Year2,
                TotalTeachingAssistants,
                EducationSupportStaffCosts,
                TotalPupils));
        }

        if (PupilsMixedYear2Year3 > 0)
        {
            groups.Add(new(
                "Year 2 and year 3",
                PupilsMixedYear2Year3,
                TeachersMixedYear2Year3,
                TotalTeachingPeriods,
                TotalNumberOfTeachersFte,
                TotalTeacherCosts,
                AssistantsMixedYear2Year3,
                TotalTeachingAssistants,
                EducationSupportStaffCosts,
                TotalPupils));
        }

        if (PupilsMixedYear3Year4 > 0)
        {
            groups.Add(new(
                "Year 3 and year 4",
                PupilsMixedYear3Year4,
                TeachersMixedYear3Year4,
                TotalTeachingPeriods,
                TotalNumberOfTeachersFte,
                TotalTeacherCosts,
                AssistantsMixedYear3Year4,
                TotalTeachingAssistants,
                EducationSupportStaffCosts,
                TotalPupils));
        }

        if (PupilsMixedYear4Year5 > 0)
        {
            groups.Add(new(
                "Year 4 and year 5",
                PupilsMixedYear4Year5,
                TeachersMixedYear4Year5,
                TotalTeachingPeriods,
                TotalNumberOfTeachersFte,
                TotalTeacherCosts,
                AssistantsMixedYear4Year5,
                TotalTeachingAssistants,
                EducationSupportStaffCosts,
                TotalPupils));
        }

        if (PupilsMixedYear5Year6 > 0)
        {
            groups.Add(new(
                "Year 5 and year 6",
                PupilsMixedYear5Year6,
                TeachersMixedYear5Year6,
                TotalTeachingPeriods,
                TotalNumberOfTeachersFte,
                TotalTeacherCosts,
                AssistantsMixedYear5Year6,
                TotalTeachingAssistants,
                EducationSupportStaffCosts,
                TotalPupils));
        }

        foreach (var period in plan.OtherTeachingPeriods)
        {
            var timetable = period.PeriodsPerTimetable.ToInt() ?? 0;
            if (period.PeriodName is not null && timetable > 0)
            {
                groups.Add(new(
                    period.PeriodName,
                    0M,
                    timetable,
                    TotalTeachingPeriods,
                    TotalNumberOfTeachersFte,
                    TotalTeacherCosts,
                    0,
                    0,
                    0,
                    TotalPupils));
            }
        }

        return groups.ToArray();
    }

    private ScenarioPlan[] BuildScenarioPlans()
    {
        var managementPeriods = ManagementRoles.Sum(x => x.TeachingPeriods);
        var managementFte = ManagementRoles.Sum(x => x.FullTimeEquivalent);

        var withoutManagementFteRequired =
            TotalTeachingPeriods / (TargetContactRatio * TimetablePeriods) - managementFte;
        var plans = new List<ScenarioPlan>
        {
            new(
                "Teacher with management time",
                managementPeriods,
                managementFte,
                managementFte),
            new(
                "Teacher without management time",
                TotalTeachingPeriods - managementPeriods,
                TotalNumberOfTeachersFte - managementFte,
                withoutManagementFteRequired),
            new(
                "Total",
                TotalTeachingPeriods,
                TotalNumberOfTeachersFte,
                managementFte + withoutManagementFteRequired)
        };

        return plans.ToArray();
    }

    private ManagementRole[] BuildManagementRoles()
    {
        var roles = new List<ManagementRole>();
        if (plan.ManagementRoleHeadteacher)
        {
            roles.Add(new(
                "Headteacher",
                plan.NumberHeadteacher.ToInt() ?? 0,
                plan.TeachingPeriodsHeadteacher.Sum(x => x.ToInt() ?? 0)));
        }

        if (plan.ManagementRoleDeputyHeadteacher)
        {
            roles.Add(new(
                "Deputy headteacher",
                plan.NumberDeputyHeadteacher.ToInt() ?? 0,
                plan.TeachingPeriodsDeputyHeadteacher.Sum(x => x.ToInt() ?? 0)));
        }

        if (plan.ManagementRoleNumeracyLead)
        {
            roles.Add(new(
                "Numeracy lead",
                plan.NumberNumeracyLead.ToInt() ?? 0,
                plan.TeachingPeriodsNumeracyLead.Sum(x => x.ToInt() ?? 0)));
        }

        if (plan.ManagementRoleLiteracyLead)
        {
            roles.Add(new(
                "Literacy lead",
                plan.NumberLiteracyLead.ToInt() ?? 0,
                plan.TeachingPeriodsLiteracyLead.Sum(x => x.ToInt() ?? 0)));
        }

        if (plan.ManagementRoleHeadSmallCurriculum)
        {
            roles.Add(new(
                "Head of small curriculum area",
                plan.NumberHeadSmallCurriculum.ToInt() ?? 0,
                plan.TeachingPeriodsHeadSmallCurriculum.Sum(x => x.ToInt() ?? 0)));
        }

        if (plan.ManagementRoleHeadKs1)
        {
            roles.Add(new(
                "Head of KS1",
                plan.NumberHeadKs1.ToInt() ?? 0,
                plan.TeachingPeriodsHeadKs1.Sum(x => x.ToInt() ?? 0)));
        }

        if (plan.ManagementRoleHeadKs2)
        {
            roles.Add(new(
                "Head of KS2",
                plan.NumberHeadKs2.ToInt() ?? 0,
                plan.TeachingPeriodsHeadKs2.Sum(x => x.ToInt() ?? 0)));
        }

        if (plan.ManagementRoleSenco)
        {
            roles.Add(new(
                "Special educational needs coordinator (SENCO)",
                plan.NumberSenco.ToInt() ?? 0,
                plan.TeachingPeriodsSenco.Sum(x => x.ToInt() ?? 0)));
        }

        if (plan.ManagementRoleAssistantHeadteacher)
        {
            roles.Add(new(
                "Assistant headteacher",
                plan.NumberAssistantHeadteacher.ToInt() ?? 0,
                plan.TeachingPeriodsAssistantHeadteacher.Sum(x => x.ToInt() ?? 0)));
        }

        if (plan.ManagementRoleHeadLargeCurriculum)
        {
            roles.Add(new(
                "Head of large curriculum area",
                plan.NumberHeadLargeCurriculum.ToInt() ?? 0,
                plan.TeachingPeriodsHeadLargeCurriculum.Sum(x => x.ToInt() ?? 0)));
        }

        if (plan.ManagementRolePastoralLeader)
        {
            roles.Add(new(
                "Pastoral leader",
                plan.NumberPastoralLeader.ToInt() ?? 0,
                plan.TeachingPeriodsPastoralLeader.Sum(x => x.ToInt() ?? 0)));
        }

        if (plan.ManagementRoleOtherMembers)
        {
            roles.Add(new(
                "Other members of management or leadership staff",
                plan.NumberOtherMembers.ToInt() ?? 0,
                plan.TeachingPeriodsOtherMembers.Sum(x => x.ToInt() ?? 0)));
        }

        return roles.ToArray();
    }

    public record ManagementRole(string Description, decimal FullTimeEquivalent, decimal TeachingPeriods);

    public record ScenarioPlan(string Description, decimal TeachingPeriods, decimal ActualFte, decimal FteRequired);

    public record PrimaryPupilGroup : PupilGroup
    {
        private readonly decimal _totalTeachingAssistants;
        private readonly decimal _educationSupportStaffCosts;

        public PrimaryPupilGroup(string description, decimal pupilsOnRoll, decimal periodAllocation,
            decimal totalTeachingPeriods, decimal totalNumberOfTeachersFte, decimal totalTeacherCosts,
            decimal teachingAssistants, decimal totalTeachingAssistants, decimal educationSupportStaffCosts,
            decimal totalPupils) : base(
            description, pupilsOnRoll, periodAllocation, totalTeachingPeriods, totalNumberOfTeachersFte,
            totalTeacherCosts, totalPupils)
        {
            TeachingAssistants = teachingAssistants;
            _totalTeachingAssistants = totalTeachingAssistants;
            _educationSupportStaffCosts = educationSupportStaffCosts;
        }

        public decimal TeachingAssistants { get; }

        public decimal TeachingAssistantCost => _totalTeachingAssistants == 0 || TeachingAssistants == 0
            ? 0
            : TeachingAssistants / _totalTeachingAssistants * _educationSupportStaffCosts;
    }


    public record PupilGroup
    {
        private readonly decimal _totalTeachingPeriods;
        private readonly decimal _totalNumberOfTeachersFte;
        private readonly decimal _totalTeacherCosts;
        private readonly decimal _totalPupils;

        public PupilGroup(string description, decimal pupilsOnRoll, decimal periodAllocation,
            decimal totalTeachingPeriods, decimal totalNumberOfTeachersFte, decimal totalTeacherCosts,
            decimal totalPupils)
        {
            Description = description;
            PupilsOnRoll = pupilsOnRoll;
            PeriodAllocation = periodAllocation;
            _totalTeachingPeriods = totalTeachingPeriods;
            _totalNumberOfTeachersFte = totalNumberOfTeachersFte;
            _totalTeacherCosts = totalTeacherCosts;
            _totalPupils = totalPupils;
        }

        public string Description { get; }
        public decimal PupilsOnRoll { get; }
        public decimal PeriodAllocation { get; }

        public decimal FteTeachers => _totalTeachingPeriods == 0 || PeriodAllocation == 0
            ? 0
            : PeriodAllocation / _totalTeachingPeriods * _totalNumberOfTeachersFte;

        public decimal TeacherCost => _totalTeachingPeriods == 0 || PeriodAllocation == 0
            ? 0
            : PeriodAllocation / _totalTeachingPeriods * _totalTeacherCosts;

        public decimal PercentPupilsOnRoll => _totalPupils == 0 || PupilsOnRoll == 0
            ? 0
            : PupilsOnRoll / _totalPupils * 100;

        public decimal PercentTeacherCost => _totalTeachingPeriods == 0 || PeriodAllocation == 0
            ? 0
            : PeriodAllocation / _totalTeachingPeriods * 100;
    }
}