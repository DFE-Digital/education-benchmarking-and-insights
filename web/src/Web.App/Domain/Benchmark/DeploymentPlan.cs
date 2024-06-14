namespace Web.App.Domain.Benchmark;

public record DeploymentPlan
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
    public decimal TargetContactRatio { get; set; }
    public string? ContactRatioRating { get; set; }
    public string? InYearBalancePercentIncomeRating { get; set; }
    public string? AverageClassSizeRating { get; set; }

    public ManagementRole[] ManagementRoles { get; set; } = Array.Empty<ManagementRole>();
    public ScenarioPlan[] ScenarioPlans { get; set; } = Array.Empty<ScenarioPlan>();
    public PrimaryPupilGroup[] PrimaryStaffDeployment { get; set; } = Array.Empty<PrimaryPupilGroup>();
    public PupilGroup[] StaffDeployment { get; set; } = Array.Empty<PupilGroup>();
}

public record ManagementRole(string Description, decimal FullTimeEquivalent, decimal TeachingPeriods);

public record ScenarioPlan(string Description, decimal TeachingPeriods, decimal ActualFte, decimal FteRequired);

public record PrimaryPupilGroup : PupilGroup
{
    public decimal TeachingAssistants { get; set; }
    public decimal TeachingAssistantCost { get; set; }
}

public record PupilGroup
{
    public string Description { get; set; }
    public decimal PupilsOnRoll { get; set; }
    public decimal PeriodAllocation { get; set; }
    public decimal FteTeachers { get; set; }
    public decimal TeacherCost { get; set; }
    public decimal PercentPupilsOnRoll { get; set; }
    public decimal PercentTeacherCost { get; set; }
}