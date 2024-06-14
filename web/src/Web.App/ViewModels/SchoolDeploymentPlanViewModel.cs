using Newtonsoft.Json;
using Web.App.Domain;
using Web.App.Domain.Benchmark;
using Web.App.Extensions;

namespace Web.App.ViewModels;

public class SchoolDeploymentPlanViewModel(School school, DeploymentPlan plan)
{
    public string? Name => school.SchoolName;
    public bool IsPrimary => school.IsPrimary;
    public int Year => plan.Year;
    public string? Urn => plan.URN;
    public int TotalIncome => plan.TotalIncome;
    public int TotalExpenditure => plan.TotalExpenditure;
    public int TotalTeacherCosts => plan.TotalTeacherCosts;
    public int EducationSupportStaffCosts => plan.EducationSupportStaffCosts;
    public decimal TotalNumberOfTeachersFte => plan.TotalNumberOfTeachersFte;
    public int TimetablePeriods => plan.TimetablePeriods;
    public decimal PupilTeacherRatio => plan.PupilTeacherRatio;
    public decimal AverageClassSize => plan.AverageClassSize;
    public RatingViewModel AverageClassSizeRating => RatingViewModel.Map[plan.AverageClassSizeRating ?? ""];
    public decimal AverageTeacherLoad => plan.AverageTeacherLoad;
    public decimal TeacherContactRatio => plan.TeacherContactRatio;
    public RatingViewModel TeacherContactRatioRating => RatingViewModel.Map[plan.ContactRatioRating ?? ""];
    public decimal IncomePerPupil => plan.IncomePerPupil;
    public decimal TeacherCostPercentageExpenditure => plan.TeacherCostPercentageExpenditure;
    public decimal TeacherCostPercentageIncome => plan.TeacherCostPercentageIncome;
    public decimal InYearBalance => plan.InYearBalance;
    public RatingViewModel InYearBalanceRating => RatingViewModel.Map[plan.InYearBalancePercentIncomeRating ?? ""];
    public decimal CostPerLesson => plan.CostPerLesson;
    public decimal AverageTeacherCost => plan.AverageTeacherCost;
    public decimal AverageTeachingAssistantCost => plan.AverageTeachingAssistantCost;
    public PrimaryPupilGroup[] PrimaryStaffDeployment => plan.PrimaryStaffDeployment;
    public PupilGroup[] StaffDeployment => plan.StaffDeployment;
    public decimal TotalPupils => plan.TotalPupils;
    public decimal TotalTeachingAssistants => plan.TotalTeachingAssistants;
    public decimal TotalTeachingPeriods => plan.TotalTeachingPeriods;
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

    public ManagementRole[] ManagementRoles => plan.ManagementRoles;
    public ScenarioPlan[] ScenarioPlans => plan.ScenarioPlans;
    public decimal TargetContactRatio => plan.TargetContactRatio;
}

