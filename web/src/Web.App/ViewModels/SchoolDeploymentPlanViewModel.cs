using Newtonsoft.Json;
using Web.App.Domain;
using Web.App.Extensions;

namespace Web.App.ViewModels;

public class SchoolDeploymentPlanViewModel(School school, FinancialPlanInput planInput, string? referrer)
{
    public string? Name => school.SchoolName;
    public bool IsPrimary => school.IsPrimary;
    public int Year => planInput.Year;
    public string? Urn => planInput.Urn;
    public int TotalIncome => planInput.TotalIncome.ToInt() ?? 0;
    public int TotalExpenditure => planInput.TotalExpenditure.ToInt() ?? 0;
    public int TotalTeacherCosts => planInput.TotalTeacherCosts.ToInt() ?? 0;
    public int EducationSupportStaffCosts => planInput.EducationSupportStaffCosts.ToInt() ?? 0;
    public decimal TotalNumberOfTeachersFte => planInput.TotalNumberOfTeachersFte ?? 0;
    public int TimetablePeriods => planInput.TimetablePeriods.ToInt() ?? 0;
    public decimal PupilsNursery => planInput.PupilsNursery ?? 0;
    public int PupilsReception => planInput.PupilsReception.ToInt() ?? 0;
    public int PupilsYear1 => planInput.PupilsYear1.ToInt() ?? 0;
    public int PupilsYear2 => planInput.PupilsYear2.ToInt() ?? 0;
    public int PupilsYear3 => planInput.PupilsYear3.ToInt() ?? 0;
    public int PupilsYear4 => planInput.PupilsYear4.ToInt() ?? 0;
    public int PupilsYear5 => planInput.PupilsYear5.ToInt() ?? 0;
    public int PupilsYear6 => planInput.PupilsYear6.ToInt() ?? 0;
    public int PupilsMixedReceptionYear1 => planInput.PupilsMixedReceptionYear1.ToInt() ?? 0;
    public int PupilsMixedYear1Year2 => planInput.PupilsMixedYear1Year2.ToInt() ?? 0;
    public int PupilsMixedYear2Year3 => planInput.PupilsMixedYear2Year3.ToInt() ?? 0;
    public int PupilsMixedYear3Year4 => planInput.PupilsMixedYear3Year4.ToInt() ?? 0;
    public int PupilsMixedYear4Year5 => planInput.PupilsMixedYear4Year5.ToInt() ?? 0;
    public int PupilsMixedYear5Year6 => planInput.PupilsMixedYear5Year6.ToInt() ?? 0;
    public int PupilsYear7 => planInput.PupilsYear7.ToInt() ?? 0;
    public int PupilsYear8 => planInput.PupilsYear8.ToInt() ?? 0;
    public int PupilsYear9 => planInput.PupilsYear9.ToInt() ?? 0;
    public int PupilsYear10 => planInput.PupilsYear10.ToInt() ?? 0;
    public int PupilsYear11 => planInput.PupilsYear11.ToInt() ?? 0;
    public decimal PupilsYear12 => planInput.PupilsYear12 ?? 0;
    public decimal PupilsYear13 => planInput.PupilsYear13 ?? 0;
    public int TeachersNursery => planInput.TeachersNursery.ToInt() ?? 0;
    public int TeachersMixedReceptionYear1 => planInput.TeachersMixedReceptionYear1.ToInt() ?? 0;
    public int TeachersMixedYear1Year2 => planInput.TeachersMixedYear1Year2.ToInt() ?? 0;
    public int TeachersMixedYear2Year3 => planInput.TeachersMixedYear2Year3.ToInt() ?? 0;
    public int TeachersMixedYear3Year4 => planInput.TeachersMixedYear3Year4.ToInt() ?? 0;
    public int TeachersMixedYear4Year5 => planInput.TeachersMixedYear4Year5.ToInt() ?? 0;
    public int TeachersMixedYear5Year6 => planInput.TeachersMixedYear5Year6.ToInt() ?? 0;
    public int TeachersReception => planInput.TeachersReception.ToInt() ?? 0;
    public int TeachersYear1 => planInput.TeachersYear1.ToInt() ?? 0;
    public int TeachersYear2 => planInput.TeachersYear2.ToInt() ?? 0;
    public int TeachersYear3 => planInput.TeachersYear3.ToInt() ?? 0;
    public int TeachersYear4 => planInput.TeachersYear4.ToInt() ?? 0;
    public int TeachersYear5 => planInput.TeachersYear5.ToInt() ?? 0;
    public int TeachersYear6 => planInput.TeachersYear6.ToInt() ?? 0;
    public int TeachersYear7 => planInput.TeachersYear7.ToInt() ?? 0;
    public int TeachersYear8 => planInput.TeachersYear8.ToInt() ?? 0;
    public int TeachersYear9 => planInput.TeachersYear9.ToInt() ?? 0;
    public int TeachersYear10 => planInput.TeachersYear10.ToInt() ?? 0;
    public int TeachersYear11 => planInput.TeachersYear11.ToInt() ?? 0;
    public int TeachersYear12 => planInput.TeachersYear12.ToInt() ?? 0;
    public int TeachersYear13 => planInput.TeachersYear13.ToInt() ?? 0;
    public decimal AssistantsNursery => planInput.AssistantsNursery ?? 0;
    public decimal AssistantsMixedReceptionYear1 => planInput.AssistantsMixedReceptionYear1 ?? 0;
    public decimal AssistantsMixedYear1Year2 => planInput.AssistantsMixedYear1Year2 ?? 0;
    public decimal AssistantsMixedYear2Year3 => planInput.AssistantsMixedYear2Year3 ?? 0;
    public decimal AssistantsMixedYear3Year4 => planInput.AssistantsMixedYear3Year4 ?? 0;
    public decimal AssistantsMixedYear4Year5 => planInput.AssistantsMixedYear4Year5 ?? 0;
    public decimal AssistantsMixedYear5Year6 => planInput.AssistantsMixedYear5Year6 ?? 0;
    public decimal AssistantsReception => planInput.AssistantsReception ?? 0;
    public decimal AssistantsYear1 => planInput.AssistantsYear1 ?? 0;
    public decimal AssistantsYear2 => planInput.AssistantsYear2 ?? 0;
    public decimal AssistantsYear3 => planInput.AssistantsYear3 ?? 0;
    public decimal AssistantsYear4 => planInput.AssistantsYear4 ?? 0;
    public decimal AssistantsYear5 => planInput.AssistantsYear5 ?? 0;
    public decimal AssistantsYear6 => planInput.AssistantsYear6 ?? 0;
    public decimal TargetContactRatio => planInput.TargetContactRatio;

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

    public decimal OtherPeriods => planInput.OtherTeachingPeriods
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

    public decimal AverageTeachingAssistantCost => TotalTeachingAssistants > 0 ? EducationSupportStaffCosts / TotalTeachingAssistants : 0;

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

        foreach (var period in planInput.OtherTeachingPeriods)
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

        foreach (var period in planInput.OtherTeachingPeriods)
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
        if (planInput.ManagementRoleHeadteacher)
        {
            roles.Add(new(
                "Headteacher",
                planInput.NumberHeadteacher.ToInt() ?? 0,
                planInput.TeachingPeriodsHeadteacher.Sum(x => x.ToInt() ?? 0)));
        }

        if (planInput.ManagementRoleDeputyHeadteacher)
        {
            roles.Add(new(
                "Deputy headteacher",
                planInput.NumberDeputyHeadteacher.ToInt() ?? 0,
                planInput.TeachingPeriodsDeputyHeadteacher.Sum(x => x.ToInt() ?? 0)));
        }

        if (planInput.ManagementRoleNumeracyLead)
        {
            roles.Add(new(
                "Numeracy lead",
                planInput.NumberNumeracyLead.ToInt() ?? 0,
                planInput.TeachingPeriodsNumeracyLead.Sum(x => x.ToInt() ?? 0)));
        }

        if (planInput.ManagementRoleLiteracyLead)
        {
            roles.Add(new(
                "Literacy lead",
                planInput.NumberLiteracyLead.ToInt() ?? 0,
                planInput.TeachingPeriodsLiteracyLead.Sum(x => x.ToInt() ?? 0)));
        }

        if (planInput.ManagementRoleHeadSmallCurriculum)
        {
            roles.Add(new(
                "Head of small curriculum area",
                planInput.NumberHeadSmallCurriculum.ToInt() ?? 0,
                planInput.TeachingPeriodsHeadSmallCurriculum.Sum(x => x.ToInt() ?? 0)));
        }

        if (planInput.ManagementRoleHeadKs1)
        {
            roles.Add(new(
                "Head of KS1",
                planInput.NumberHeadKs1.ToInt() ?? 0,
                planInput.TeachingPeriodsHeadKs1.Sum(x => x.ToInt() ?? 0)));
        }

        if (planInput.ManagementRoleHeadKs2)
        {
            roles.Add(new(
                "Head of KS2",
                planInput.NumberHeadKs2.ToInt() ?? 0,
                planInput.TeachingPeriodsHeadKs2.Sum(x => x.ToInt() ?? 0)));
        }

        if (planInput.ManagementRoleSenco)
        {
            roles.Add(new(
                "Special educational needs coordinator (SENCO)",
                planInput.NumberSenco.ToInt() ?? 0,
                planInput.TeachingPeriodsSenco.Sum(x => x.ToInt() ?? 0)));
        }

        if (planInput.ManagementRoleAssistantHeadteacher)
        {
            roles.Add(new(
                "Assistant headteacher",
                planInput.NumberAssistantHeadteacher.ToInt() ?? 0,
                planInput.TeachingPeriodsAssistantHeadteacher.Sum(x => x.ToInt() ?? 0)));
        }

        if (planInput.ManagementRoleHeadLargeCurriculum)
        {
            roles.Add(new(
                "Head of large curriculum area",
                planInput.NumberHeadLargeCurriculum.ToInt() ?? 0,
                planInput.TeachingPeriodsHeadLargeCurriculum.Sum(x => x.ToInt() ?? 0)));
        }

        if (planInput.ManagementRolePastoralLeader)
        {
            roles.Add(new(
                "Pastoral leader",
                planInput.NumberPastoralLeader.ToInt() ?? 0,
                planInput.TeachingPeriodsPastoralLeader.Sum(x => x.ToInt() ?? 0)));
        }

        if (planInput.ManagementRoleOtherMembers)
        {
            roles.Add(new(
                "Other members of management or leadership staff",
                planInput.NumberOtherMembers.ToInt() ?? 0,
                planInput.TeachingPeriodsOtherMembers.Sum(x => x.ToInt() ?? 0)));
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