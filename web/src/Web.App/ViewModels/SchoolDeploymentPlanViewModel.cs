using Newtonsoft.Json;
using Web.App.Domain;
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
    public Rating AverageClassSizeRating => RatingCalculations.AverageClassSize(AverageClassSize);
    public decimal AverageTeacherLoad => plan.AverageTeacherLoad;
    public decimal TeacherContactRatio => plan.TeacherContactRatio;
    public Rating TeacherContactRatioRating => RatingCalculations.ContactRatio(TeacherContactRatio);
    public decimal IncomePerPupil => plan.IncomePerPupil;
    public decimal TeacherCostPercentageExpenditure => plan.TeacherCostPercentageExpenditure;
    public decimal TeacherCostPercentageIncome => plan.TeacherCostPercentageIncome;
    public decimal InYearBalance => plan.InYearBalance;
    public Rating InYearBalanceRating =>
        RatingCalculations.InYearBalancePercentIncome(InYearBalance / TotalIncome * 100);
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