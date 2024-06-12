using System;
using System.Collections.Generic;
using System.Linq;

namespace Platform.Api.Benchmark.FinancialPlans;

public record FinancialPlanDeployment
{
    public string? URN { get; set; }
    public int Year { get; set; }
    public int TotalIncome { get; set; }
    public int TotalExpenditure { get; set; }
    public int TotalTeacherCosts { get; set; }
    public int EducationSupportStaffCosts { get; set; }
    public decimal TotalNumberOfTeachersFte { get; set; }
    public int TimetablePeriods { get; set; }
    public decimal TotalPupils { get; set; }
    public decimal TeachingPeriods { get; set; }
    public decimal TotalTeachingAssistants { get; set; }
    public decimal OtherPeriods { get; set; }
    public decimal TotalTeachingPeriods { get; set; }
    public decimal TeacherContactRatio { get; set; }
    public decimal PupilTeacherRatio { get; set; }
    public decimal AverageClassSize { get; set; }
    public decimal AverageTeachingAssistantCost { get; set; }
    public decimal AverageTeacherCost { get; set; }
    public decimal AverageTeacherLoad { get; set; }
    public decimal IncomePerPupil { get; set; }
    public decimal TeacherCostPercentageExpenditure { get; set; }
    public decimal TeacherCostPercentageIncome { get; set; }
    public decimal InYearBalance { get; set; }
    public decimal CostPerLesson { get; set; }

    public ManagementRole[] ManagementRoles { get; set; } = Array.Empty<ManagementRole>();
    public ScenarioPlan[] ScenarioPlans { get; set; } = Array.Empty<ScenarioPlan>();
    public PrimaryPupilGroup[] PrimaryStaffDeployment { get; set; } = Array.Empty<PrimaryPupilGroup>();
    public PupilGroup[] StaffDeployment { get; set; } = Array.Empty<PupilGroup>();
    public decimal TargetContactRatio { get; set; }
}

public record ManagementRole(string Description, decimal FullTimeEquivalent, decimal TeachingPeriods);

public record ScenarioPlan(string Description, decimal TeachingPeriods, decimal ActualFte, decimal FteRequired);

public record PrimaryPupilGroup : PupilGroup
{
    public PrimaryPupilGroup(string description, decimal pupilsOnRoll, decimal periodAllocation,
        decimal totalTeachingPeriods, decimal totalNumberOfTeachersFte, decimal totalTeacherCosts,
        decimal teachingAssistants, decimal totalTeachingAssistants, decimal educationSupportStaffCosts,
        decimal totalPupils) : base(
        description, pupilsOnRoll, periodAllocation, totalTeachingPeriods, totalNumberOfTeachersFte,
        totalTeacherCosts, totalPupils)
    {
        TeachingAssistants = teachingAssistants;
        TeachingAssistantCost = totalTeachingAssistants == 0 || TeachingAssistants == 0
            ? 0
            : TeachingAssistants / totalTeachingAssistants * educationSupportStaffCosts;
    }

    public decimal TeachingAssistants { get; set; }

    public decimal TeachingAssistantCost { get; set; }
}

public record PupilGroup
{
    public PupilGroup(string description, decimal pupilsOnRoll, decimal periodAllocation,
        decimal totalTeachingPeriods, decimal totalNumberOfTeachersFte, decimal totalTeacherCosts,
        decimal totalPupils)
    {
        Description = description;
        PupilsOnRoll = pupilsOnRoll;
        PeriodAllocation = periodAllocation;

        FteTeachers = totalTeachingPeriods == 0 || PeriodAllocation == 0
            ? 0
            : PeriodAllocation / totalTeachingPeriods * totalNumberOfTeachersFte;
        TeacherCost = totalTeachingPeriods == 0 || PeriodAllocation == 0
            ? 0
            : PeriodAllocation / totalTeachingPeriods * totalTeacherCosts;
        PercentPupilsOnRoll = totalPupils == 0 || PupilsOnRoll == 0 ? 0 : PupilsOnRoll / totalPupils * 100;
        PercentTeacherCost = totalTeachingPeriods == 0 || PeriodAllocation == 0
            ? 0
            : PeriodAllocation / totalTeachingPeriods * 100;
    }

    public string Description { get; set; }
    public decimal PupilsOnRoll { get; set; }
    public decimal PeriodAllocation { get; set; }
    public decimal FteTeachers { get; set; }
    public decimal TeacherCost { get; set; }
    public decimal PercentPupilsOnRoll { get; set; }
    public decimal PercentTeacherCost { get; set; }
}

public static class DeploymentPlanFactory
{
    public static FinancialPlanDeployment Create(FinancialPlanDetails plan)
    {
        var totalPupils = TotalPupils(plan);
        var teachingPeriods = TeachingPeriods(plan);
        var totalTeachingAssistants = TotalTeachingAssistants(plan);
        var otherPeriods = OtherPeriods(plan);

        var totalTeachingPeriods = teachingPeriods + otherPeriods;
        var pupilTeacherRatio = totalPupils / plan.TotalNumberOfTeachersFte ?? 0;
        var averageClassSize = totalTeachingPeriods > 0 ? totalPupils * teachingPeriods / totalTeachingPeriods : 0;
        var averageTeacherLoad = totalTeachingPeriods / plan.TotalNumberOfTeachersFte ?? 0;
        var teacherContactRatio = totalTeachingPeriods > 0 || teachingPeriods > 0
            ? totalTeachingPeriods / (teachingPeriods * plan.TotalNumberOfTeachersFte) ?? 0
            : 0;
        var incomePerPupil = totalPupils > 0 ? plan.TotalIncome / totalPupils ?? 0 : 0;
        var teacherCostPercentageExpenditure = plan.TotalTeacherCosts / (decimal?)plan.TotalExpenditure * 100 ?? 0;
        var teacherCostPercentageIncome = plan.TotalTeacherCosts / (decimal?)plan.TotalIncome * 100 ?? 0;
        var inYearBalance = plan.TotalIncome - plan.TotalExpenditure ?? 0;
        var costPerLesson = totalTeachingPeriods > 0 ? plan.TotalTeacherCosts / totalTeachingPeriods ?? 0 : 0;
        var averageTeacherCost = plan.TotalTeacherCosts / plan.TotalNumberOfTeachersFte ?? 0;
        var averageTeachingAssistantCost =
            (totalTeachingAssistants > 0 ? plan.EducationSupportStaffCosts / totalTeachingAssistants : 0) ?? 0;

        var managementRoles = BuildManagementRoles(plan);
        var scenarioPlans = BuildScenarioPlans(managementRoles, plan, totalTeachingPeriods);


        var primaryStaffDeployment =
            BuildPrimaryStaffDeployment(plan, totalTeachingPeriods, totalTeachingAssistants, totalPupils);
        var staffDeployment = BuildStaffDeployment(plan, totalTeachingPeriods, totalPupils);

        return new FinancialPlanDeployment
        {
            URN = plan.Urn,
            Year = plan.Year,
            TotalIncome = plan.TotalIncome ?? 0,
            TotalExpenditure = plan.TotalExpenditure ?? 0,
            TotalTeacherCosts = plan.TotalTeacherCosts ?? 0,
            EducationSupportStaffCosts = plan.EducationSupportStaffCosts ?? 0,
            TotalNumberOfTeachersFte = plan.TotalNumberOfTeachersFte ?? 0,
            TimetablePeriods = plan.TimetablePeriods ?? 0,
            TargetContactRatio = plan.TargetContactRatio,
            TotalPupils = totalPupils,
            TeachingPeriods = teachingPeriods,
            TotalTeachingAssistants = totalTeachingAssistants,
            OtherPeriods = otherPeriods,
            TotalTeachingPeriods = totalTeachingPeriods,
            PupilTeacherRatio = pupilTeacherRatio,
            AverageClassSize = averageClassSize,
            AverageTeacherLoad = averageTeacherLoad,
            TeacherContactRatio = teacherContactRatio,
            IncomePerPupil = incomePerPupil,
            TeacherCostPercentageExpenditure = teacherCostPercentageExpenditure,
            TeacherCostPercentageIncome = teacherCostPercentageIncome,
            InYearBalance = inYearBalance,
            CostPerLesson = costPerLesson,
            AverageTeacherCost = averageTeacherCost,
            AverageTeachingAssistantCost = averageTeachingAssistantCost,
            ManagementRoles = managementRoles,
            ScenarioPlans = scenarioPlans,
            StaffDeployment = staffDeployment,
            PrimaryStaffDeployment = primaryStaffDeployment
        };
    }


    private static decimal TotalPupils(FinancialPlanDetails plan)
    {
        var nursery = plan.PupilsNursery ?? 0;
        var reception = plan.PupilsReception ?? 0;
        var year1 = plan.PupilsYear1 ?? 0;
        var year2 = plan.PupilsYear2 ?? 0;
        var year3 = plan.PupilsYear3 ?? 0;
        var year4 = plan.PupilsYear4 ?? 0;
        var year5 = plan.PupilsYear5 ?? 0;
        var year6 = plan.PupilsYear6 ?? 0;
        var mixedReceptionYear1 = plan.PupilsMixedReceptionYear1 ?? 0;
        var mixedYear1Year2 = plan.PupilsMixedYear1Year2 ?? 0;
        var mixedYear2Year3 = plan.PupilsMixedYear2Year3 ?? 0;
        var mixedYear3Year4 = plan.PupilsMixedYear3Year4 ?? 0;
        var mixedYear4Year5 = plan.PupilsMixedYear4Year5 ?? 0;
        var mixedYear5Year6 = plan.PupilsMixedYear5Year6 ?? 0;
        var year7 = plan.PupilsYear7 ?? 0;
        var year8 = plan.PupilsYear8 ?? 0;
        var year9 = plan.PupilsYear9 ?? 0;
        var year10 = plan.PupilsYear10 ?? 0;
        var year11 = plan.PupilsYear11 ?? 0;
        var year12 = plan.PupilsYear12 ?? 0;
        var year13 = plan.PupilsYear13 ?? 0;

        return nursery + reception + year1 + year2 + year3 +
               year4 + year5 + year6 + mixedReceptionYear1 +
               mixedYear1Year2 + mixedYear2Year3 + mixedYear3Year4 +
               mixedYear4Year5 + mixedYear5Year6 + year7 + year8 +
               year9 + year10 + year11 + year12 + year13;
    }

    private static decimal TeachingPeriods(FinancialPlanDetails plan)
    {
        var nursery = plan.TeachersNursery ?? 0;
        var reception = plan.TeachersReception ?? 0;
        var year1 = plan.TeachersYear1 ?? 0;
        var year2 = plan.TeachersYear2 ?? 0;
        var year3 = plan.TeachersYear3 ?? 0;
        var year4 = plan.TeachersYear4 ?? 0;
        var year5 = plan.TeachersYear5 ?? 0;
        var year6 = plan.TeachersYear6 ?? 0;
        var mixedReceptionYear1 = plan.TeachersMixedReceptionYear1 ?? 0;
        var mixedYear1Year2 = plan.TeachersMixedYear1Year2 ?? 0;
        var mixedYear2Year3 = plan.TeachersMixedYear2Year3 ?? 0;
        var mixedYear3Year4 = plan.TeachersMixedYear3Year4 ?? 0;
        var mixedYear4Year5 = plan.TeachersMixedYear4Year5 ?? 0;
        var mixedYear5Year6 = plan.TeachersMixedYear5Year6 ?? 0;
        var year7 = plan.TeachersYear7 ?? 0;
        var year8 = plan.TeachersYear8 ?? 0;
        var year9 = plan.TeachersYear9 ?? 0;
        var year10 = plan.TeachersYear10 ?? 0;
        var year11 = plan.TeachersYear11 ?? 0;
        var year12 = plan.TeachersYear12 ?? 0;
        var year13 = plan.TeachersYear13 ?? 0;

        return nursery + reception + year1 + year2 + year3 +
               year4 + year5 + year6 + mixedReceptionYear1 +
               mixedYear1Year2 + mixedYear2Year3 + mixedYear3Year4 +
               mixedYear4Year5 + mixedYear5Year6 + year7 + year8 +
               year9 + year10 + year11 + year12 + year13;
    }

    private static decimal TotalTeachingAssistants(FinancialPlanDetails plan)
    {
        var nursery = plan.AssistantsNursery ?? 0;
        var reception = plan.AssistantsReception ?? 0;
        var year1 = plan.AssistantsYear1 ?? 0;
        var year2 = plan.AssistantsYear2 ?? 0;
        var year3 = plan.AssistantsYear3 ?? 0;
        var year4 = plan.AssistantsYear4 ?? 0;
        var year5 = plan.AssistantsYear5 ?? 0;
        var year6 = plan.AssistantsYear6 ?? 0;
        var mixedReceptionYear1 = plan.AssistantsMixedReceptionYear1 ?? 0;
        var mixedYear1Year2 = plan.AssistantsMixedYear1Year2 ?? 0;
        var mixedYear2Year3 = plan.TeachersMixedYear2Year3 ?? 0;
        var mixedYear3Year4 = plan.AssistantsMixedYear3Year4 ?? 0;
        var mixedYear4Year5 = plan.AssistantsMixedYear4Year5 ?? 0;
        var mixedYear5Year6 = plan.AssistantsMixedYear5Year6 ?? 0;

        return nursery + reception + year1 + year2 + year3 +
               year4 + year5 + year6 + mixedReceptionYear1 +
               mixedYear1Year2 + mixedYear2Year3 + mixedYear3Year4 +
               mixedYear4Year5 + mixedYear5Year6;
    }

    private static decimal OtherPeriods(FinancialPlanDetails plan) =>
        plan.OtherTeachingPeriods?.Sum(x => int.Parse(x.PeriodsPerTimetable ?? "")) ?? 0;

    private static ScenarioPlan[] BuildScenarioPlans(ManagementRole[] managementRoles, FinancialPlanDetails plan,
        decimal totalTeachingPeriods)
    {
        var managementPeriods = managementRoles.Sum(x => x.TeachingPeriods);
        var managementFte = managementRoles.Sum(x => x.FullTimeEquivalent);

        var withoutManagementFteRequired =
            totalTeachingPeriods / (plan.TargetContactRatio * plan.TimetablePeriods) - managementFte ?? 0;
        var plans = new List<ScenarioPlan>
        {
            new(
                "Teacher with management time",
                managementPeriods,
                managementFte,
                managementFte),
            new(
                "Teacher without management time",
                totalTeachingPeriods - managementPeriods,
                plan.TotalNumberOfTeachersFte ?? 0 - managementFte,
                withoutManagementFteRequired),
            new(
                "Total",
                totalTeachingPeriods,
                plan.TotalNumberOfTeachersFte ?? 0,
                managementFte + withoutManagementFteRequired)
        };

        return plans.ToArray();
    }

    private static ManagementRole[] BuildManagementRoles(FinancialPlanDetails plan)
    {
        var roles = new List<ManagementRole>();
        if (plan.ManagementRoleHeadteacher)
        {
            roles.Add(new(
                "Headteacher",
                plan.NumberHeadteacher ?? 0,
                plan.TeachingPeriodsHeadteacher.Sum(x => x ?? 0)));
        }

        if (plan.ManagementRoleDeputyHeadteacher)
        {
            roles.Add(new(
                "Deputy headteacher",
                plan.NumberDeputyHeadteacher ?? 0,
                plan.TeachingPeriodsDeputyHeadteacher.Sum(x => x ?? 0)));
        }

        if (plan.ManagementRoleNumeracyLead)
        {
            roles.Add(new(
                "Numeracy lead",
                plan.NumberNumeracyLead ?? 0,
                plan.TeachingPeriodsNumeracyLead.Sum(x => x ?? 0)));
        }

        if (plan.ManagementRoleLiteracyLead)
        {
            roles.Add(new(
                "Literacy lead",
                plan.NumberLiteracyLead ?? 0,
                plan.TeachingPeriodsLiteracyLead.Sum(x => x ?? 0)));
        }

        if (plan.ManagementRoleHeadSmallCurriculum)
        {
            roles.Add(new(
                "Head of small curriculum area",
                plan.NumberHeadSmallCurriculum ?? 0,
                plan.TeachingPeriodsHeadSmallCurriculum.Sum(x => x ?? 0)));
        }

        if (plan.ManagementRoleHeadKs1)
        {
            roles.Add(new(
                "Head of KS1",
                plan.NumberHeadKs1 ?? 0,
                plan.TeachingPeriodsHeadKs1.Sum(x => x ?? 0)));
        }

        if (plan.ManagementRoleHeadKs2)
        {
            roles.Add(new(
                "Head of KS2",
                plan.NumberHeadKs2 ?? 0,
                plan.TeachingPeriodsHeadKs2.Sum(x => x ?? 0)));
        }

        if (plan.ManagementRoleSenco)
        {
            roles.Add(new(
                "Special educational needs coordinator (SENCO)",
                plan.NumberSenco ?? 0,
                plan.TeachingPeriodsSenco.Sum(x => x ?? 0)));
        }

        if (plan.ManagementRoleAssistantHeadteacher)
        {
            roles.Add(new(
                "Assistant headteacher",
                plan.NumberAssistantHeadteacher ?? 0,
                plan.TeachingPeriodsAssistantHeadteacher.Sum(x => x ?? 0)));
        }

        if (plan.ManagementRoleHeadLargeCurriculum)
        {
            roles.Add(new(
                "Head of large curriculum area",
                plan.NumberHeadLargeCurriculum ?? 0,
                plan.TeachingPeriodsHeadLargeCurriculum.Sum(x => x ?? 0)));
        }

        if (plan.ManagementRolePastoralLeader)
        {
            roles.Add(new(
                "Pastoral leader",
                plan.NumberPastoralLeader ?? 0,
                plan.TeachingPeriodsPastoralLeader.Sum(x => x ?? 0)));
        }

        if (plan.ManagementRoleOtherMembers)
        {
            roles.Add(new("Other members of management or leadership staff",
                plan.NumberOtherMembers ?? 0,
                plan.TeachingPeriodsOtherMembers.Sum(x => x ?? 0)));
        }

        return roles.ToArray();
    }

    private static PupilGroup[] BuildStaffDeployment(FinancialPlanDetails plan, decimal totalTeachingPeriods,
        decimal totalPupils)
    {
        var groups = new List<PupilGroup>();

        if (plan.PupilsYear7 > 0)
        {
            groups.Add(new(
                "Year 7",
                plan.PupilsYear7 ?? 0,
                plan.TeachersYear7 ?? 0,
                totalTeachingPeriods,
                plan.TotalNumberOfTeachersFte ?? 0,
                plan.TotalTeacherCosts ?? 0,
                totalPupils));
        }

        if (plan.PupilsYear8 > 0)
        {
            groups.Add(new(
                "Year 8",
                plan.PupilsYear8 ?? 0,
                plan.TeachersYear8 ?? 0,
                totalTeachingPeriods,
                plan.TotalNumberOfTeachersFte ?? 0,
                plan.TotalTeacherCosts ?? 0,
                totalPupils));
        }

        if (plan.PupilsYear9 > 0)
        {
            groups.Add(new(
                "Year 9",
                plan.PupilsYear9 ?? 0,
                plan.TeachersYear9 ?? 0,
                totalTeachingPeriods,
                plan.TotalNumberOfTeachersFte ?? 0,
                plan.TotalTeacherCosts ?? 0,
                totalPupils));
        }

        if (plan.PupilsYear10 > 0)
        {
            groups.Add(new(
                "Year 10",
                plan.PupilsYear10 ?? 0,
                plan.TeachersYear10 ?? 0,
                totalTeachingPeriods,
                plan.TotalNumberOfTeachersFte ?? 0,
                plan.TotalTeacherCosts ?? 0,
                totalPupils));
        }

        if (plan.PupilsYear11 > 0)
        {
            groups.Add(new(
                "Year 11",
                plan.PupilsYear11 ?? 0,
                plan.TeachersYear11 ?? 0,
                totalTeachingPeriods,
                plan.TotalNumberOfTeachersFte ?? 0,
                plan.TotalTeacherCosts ?? 0,
                totalPupils));
        }

        if (plan.PupilsYear12 > 0)
        {
            groups.Add(new(
                "Year 12",
                plan.PupilsYear12 ?? 0,
                plan.TeachersYear12 ?? 0,
                totalTeachingPeriods,
                plan.TotalNumberOfTeachersFte ?? 0,
                plan.TotalTeacherCosts ?? 0,
                totalPupils));
        }

        if (plan.PupilsYear13 > 0)
        {
            groups.Add(new(
                "Year 13",
                plan.PupilsYear13 ?? 0,
                plan.TeachersYear13 ?? 0,
                totalTeachingPeriods,
                plan.TotalNumberOfTeachersFte ?? 0,
                plan.TotalTeacherCosts ?? 0,
                totalPupils));
        }

        if (plan.OtherTeachingPeriods != null)
        {
            foreach (var period in plan.OtherTeachingPeriods)
            {
                var timetable = int.Parse(period.PeriodsPerTimetable ?? "");
                if (period.PeriodName is not null && timetable > 0)
                {
                    groups.Add(new(
                        period.PeriodName,
                        0M,
                        timetable,
                        totalTeachingPeriods,
                        plan.TotalNumberOfTeachersFte ?? 0,
                        plan.TotalTeacherCosts ?? 0,
                        totalPupils));
                }
            }
        }


        return groups.ToArray();
    }

    private static PrimaryPupilGroup[] BuildPrimaryStaffDeployment(FinancialPlanDetails plan,
        decimal totalTeachingPeriods, decimal totalTeachingAssistants, decimal totalPupils)
    {
        var groups = new List<PrimaryPupilGroup>();

        if (plan.PupilsReception > 0)
        {
            groups.Add(new(
                "Nursery",
                plan.PupilsReception ?? 0,
                plan.TeachersReception ?? 0,
                totalTeachingPeriods,
                plan.TotalNumberOfTeachersFte ?? 0,
                plan.TotalTeacherCosts ?? 0,
                plan.AssistantsNursery ?? 0,
                totalTeachingAssistants,
                plan.EducationSupportStaffCosts ?? 0,
                totalPupils));
        }

        if (plan.PupilsYear1 > 0)
        {
            groups.Add(new(
                "Year 1",
                plan.PupilsYear1 ?? 0,
                plan.TeachersYear1 ?? 0,
                totalTeachingPeriods,
                plan.TotalNumberOfTeachersFte ?? 0,
                plan.TotalTeacherCosts ?? 0,
                plan.AssistantsYear1 ?? 0,
                totalTeachingAssistants,
                plan.EducationSupportStaffCosts ?? 0,
                totalPupils));
        }

        if (plan.PupilsYear2 > 0)
        {
            groups.Add(new(
                "Year 2",
                plan.PupilsYear2 ?? 0,
                plan.TeachersYear2 ?? 0,
                totalTeachingPeriods,
                plan.TotalNumberOfTeachersFte ?? 0,
                plan.TotalTeacherCosts ?? 0,
                plan.AssistantsYear2 ?? 0,
                totalTeachingAssistants,
                plan.EducationSupportStaffCosts ?? 0,
                totalPupils));
        }

        if (plan.PupilsYear3 > 0)
        {
            groups.Add(new(
                "Year 3",
                plan.PupilsYear3 ?? 0,
                plan.TeachersYear3 ?? 0,
                totalTeachingPeriods,
                plan.TotalNumberOfTeachersFte ?? 0,
                plan.TotalTeacherCosts ?? 0,
                plan.AssistantsYear3 ?? 0,
                totalTeachingAssistants,
                plan.EducationSupportStaffCosts ?? 0,
                totalPupils));
        }

        if (plan.PupilsYear4 > 0)
        {
            groups.Add(new(
                "Year 4",
                plan.PupilsYear4 ?? 0,
                plan.TeachersYear4 ?? 0,
                totalTeachingPeriods,
                plan.TotalNumberOfTeachersFte ?? 0,
                plan.TotalTeacherCosts ?? 0,
                plan.AssistantsYear4 ?? 0,
                totalTeachingAssistants,
                plan.EducationSupportStaffCosts ?? 0,
                totalPupils));
        }

        if (plan.PupilsYear5 > 0)
        {
            groups.Add(new(
                "Year 5",
                plan.PupilsYear5 ?? 0,
                plan.TeachersYear5 ?? 0,
                totalTeachingPeriods,
                plan.TotalNumberOfTeachersFte ?? 0,
                plan.TotalTeacherCosts ?? 0,
                plan.AssistantsYear5 ?? 0,
                totalTeachingAssistants,
                plan.EducationSupportStaffCosts ?? 0,
                totalPupils));
        }

        if (plan.PupilsYear6 > 0)
        {
            groups.Add(new(
                "Year 6",
                plan.PupilsYear6 ?? 0,
                plan.TeachersYear6 ?? 0,
                totalTeachingPeriods,
                plan.TotalNumberOfTeachersFte ?? 0,
                plan.TotalTeacherCosts ?? 0,
                plan.AssistantsYear6 ?? 0,
                totalTeachingAssistants,
                plan.EducationSupportStaffCosts ?? 0,
                totalPupils));
        }

        if (plan.PupilsMixedReceptionYear1 > 0)
        {
            groups.Add(new(
                "Reception and year 1",
                plan.PupilsMixedReceptionYear1 ?? 0,
                plan.TeachersMixedReceptionYear1 ?? 0,
                totalTeachingPeriods,
                plan.TotalNumberOfTeachersFte ?? 0,
                plan.TotalTeacherCosts ?? 0,
                plan.AssistantsMixedReceptionYear1 ?? 0,
                totalTeachingAssistants,
                plan.EducationSupportStaffCosts ?? 0,
                totalPupils));
        }

        if (plan.PupilsMixedYear1Year2 > 0)
        {
            groups.Add(new(
                "Year 1 and year 2",
                plan.PupilsMixedYear1Year2 ?? 0,
                plan.TeachersMixedYear1Year2 ?? 0,
                totalTeachingPeriods,
                plan.TotalNumberOfTeachersFte ?? 0,
                plan.TotalTeacherCosts ?? 0,
                plan.AssistantsMixedYear1Year2 ?? 0,
                totalTeachingAssistants,
                plan.EducationSupportStaffCosts ?? 0,
                totalPupils));
        }

        if (plan.PupilsMixedYear2Year3 > 0)
        {
            groups.Add(new(
                "Year 2 and year 3",
                plan.PupilsMixedYear2Year3 ?? 0,
                plan.TeachersMixedYear2Year3 ?? 0,
                totalTeachingPeriods,
                plan.TotalNumberOfTeachersFte ?? 0,
                plan.TotalTeacherCosts ?? 0,
                plan.AssistantsMixedYear2Year3 ?? 0,
                totalTeachingAssistants,
                plan.EducationSupportStaffCosts ?? 0,
                totalPupils));
        }

        if (plan.PupilsMixedYear3Year4 > 0)
        {
            groups.Add(new(
                "Year 3 and year 4",
                plan.PupilsMixedYear3Year4 ?? 0,
                plan.TeachersMixedYear3Year4 ?? 0,
                totalTeachingPeriods,
                plan.TotalNumberOfTeachersFte ?? 0,
                plan.TotalTeacherCosts ?? 0,
                plan.AssistantsMixedYear3Year4 ?? 0,
                totalTeachingAssistants,
                plan.EducationSupportStaffCosts ?? 0,
                totalPupils));
        }

        if (plan.PupilsMixedYear4Year5 > 0)
        {
            groups.Add(new(
                "Year 4 and year 5",
                plan.PupilsMixedYear4Year5 ?? 0,
                plan.TeachersMixedYear4Year5 ?? 0,
                totalTeachingPeriods,
                plan.TotalNumberOfTeachersFte ?? 0,
                plan.TotalTeacherCosts ?? 0,
                plan.AssistantsMixedYear4Year5 ?? 0,
                totalTeachingAssistants,
                plan.EducationSupportStaffCosts ?? 0,
                totalPupils));
        }

        if (plan.PupilsMixedYear5Year6 > 0)
        {
            groups.Add(new(
                "Year 5 and year 6",
                plan.PupilsMixedYear5Year6 ?? 0,
                plan.TeachersMixedYear5Year6 ?? 0,
                totalTeachingPeriods,
                plan.TotalNumberOfTeachersFte ?? 0,
                plan.TotalTeacherCosts ?? 0,
                plan.AssistantsMixedYear5Year6 ?? 0,
                totalTeachingAssistants,
                plan.EducationSupportStaffCosts ?? 0,
                totalPupils));
        }

        if (plan.OtherTeachingPeriods != null)
        {
            foreach (var period in plan.OtherTeachingPeriods)
            {
                var timetable = int.Parse(period.PeriodsPerTimetable ?? "");
                if (period.PeriodName is not null && timetable > 0)
                {
                    groups.Add(new(
                        period.PeriodName,
                        0M,
                        timetable,
                        totalTeachingPeriods,
                        plan.TotalNumberOfTeachersFte ?? 0,
                        plan.TotalTeacherCosts ?? 0,
                        0,
                        0,
                        0,
                        totalPupils));
                }
            }
        }

        return groups.ToArray();
    }
}