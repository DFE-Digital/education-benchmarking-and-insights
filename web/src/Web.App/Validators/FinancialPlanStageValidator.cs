using FluentValidation.Results;
using Web.App.Domain;
using Web.App.Validators.FinancialPlanStages;

namespace Web.App.Validators;

public class FinancialPlanStageValidator : IFinancialPlanStageValidator
{
    private static readonly SelectYearStageValidator SelectYear = new();
    private static readonly PrePopulateDataStageValidator PrePopulateData = new();
    private static readonly TimetableCycleStageValidator TimetableCycle = new();
    private static readonly TotalIncomeStageValidator TotalIncome = new();
    private static readonly TotalExpenditureStageValidator TotalExpenditureStage = new();
    private static readonly TotalTeacherCostsStageValidator TotalTeacherCostsStage = new();
    private static readonly TotalNumberTeachersStageValidator TotalNumberTeachers = new();
    private static readonly TotalEducationSupportStageValidator TotalEducationSupport = new();
    private static readonly PrimaryHasMixedAgeClassesStageValidator PrimaryHasMixedAgeClasses = new();
    private static readonly PrimaryMixedAgeClassesStageValidator PrimaryMixedAgeClasses = new();
    private static readonly PrimaryPupilFiguresStageValidator PrimaryPupilFigures = new();
    private static readonly PupilFiguresStageValidator PupilFigures = new();
    private static readonly TeacherPeriodAllocationStageValidator TeacherPeriodAllocation = new();
    private static readonly TeachingAssistantFiguresStageValidator TeachingAssistantFigures = new();
    private static readonly OtherTeachingPeriodsStageValidator OtherTeachingPeriods = new();
    private static readonly OtherTeachingPeriodsConfirmStageValidator OtherTeachingPeriodsConfirm = new();
    private static readonly ManagementRolesStageValidator ManagementRoles = new();
    private static readonly ManagersPerRoleStageValidator ManagersPerRole = new();
    private static readonly TeachingPeriodsManagerStageValidator TeachingPeriodsManager = new();

    public async Task<ValidationResult> ValidateAsync(SelectYearStage stage) => await SelectYear.ValidateAsync(stage);

    public async Task<ValidationResult> ValidateAsync(PrePopulateDataStage stage) => await PrePopulateData.ValidateAsync(stage);

    public async Task<ValidationResult> ValidateAsync(TimetableCycleStage stage) => await TimetableCycle.ValidateAsync(stage);

    public async Task<ValidationResult> ValidateAsync(TotalIncomeStage stage) => await TotalIncome.ValidateAsync(stage);

    public async Task<ValidationResult> ValidateAsync(TotalExpenditureStage stage) => await TotalExpenditureStage.ValidateAsync(stage);

    public async Task<ValidationResult> ValidateAsync(TotalTeacherCostsStage stage) => await TotalTeacherCostsStage.ValidateAsync(stage);

    public async Task<ValidationResult> ValidateAsync(TotalNumberTeachersStage stage) => await TotalNumberTeachers.ValidateAsync(stage);

    public async Task<ValidationResult> ValidateAsync(TotalEducationSupportStage stage) => await TotalEducationSupport.ValidateAsync(stage);

    public async Task<ValidationResult> ValidateAsync(PrimaryHasMixedAgeClassesStage stage) => await PrimaryHasMixedAgeClasses.ValidateAsync(stage);

    public async Task<ValidationResult> ValidateAsync(PrimaryMixedAgeClassesStage stage) => await PrimaryMixedAgeClasses.ValidateAsync(stage);

    public async Task<ValidationResult> ValidateAsync(PrimaryPupilFiguresStage stage) => await PrimaryPupilFigures.ValidateAsync(stage);

    public async Task<ValidationResult> ValidateAsync(PupilFiguresStage stage) => await PupilFigures.ValidateAsync(stage);

    public async Task<ValidationResult> ValidateAsync(TeacherPeriodAllocationStage stage) => await TeacherPeriodAllocation.ValidateAsync(stage);

    public async Task<ValidationResult> ValidateAsync(TeachingAssistantFiguresStage stage) => await TeachingAssistantFigures.ValidateAsync(stage);

    public async Task<ValidationResult> ValidateAsync(OtherTeachingPeriodsStage stage) => await OtherTeachingPeriods.ValidateAsync(stage);

    public async Task<ValidationResult> ValidateAsync(OtherTeachingPeriodsConfirmStage stage) => await OtherTeachingPeriodsConfirm.ValidateAsync(stage);

    public async Task<ValidationResult> ValidateAsync(ManagementRolesStage stage) => await ManagementRoles.ValidateAsync(stage);

    public async Task<ValidationResult> ValidateAsync(ManagersPerRoleStage stage) => await ManagersPerRole.ValidateAsync(stage);

    public async Task<ValidationResult> ValidateAsync(TeachingPeriodsManagerStage stage) => await TeachingPeriodsManager.ValidateAsync(stage);
}

public interface IFinancialPlanStageValidator
{
    Task<ValidationResult> ValidateAsync(SelectYearStage stage);
    Task<ValidationResult> ValidateAsync(PrePopulateDataStage stage);
    Task<ValidationResult> ValidateAsync(TimetableCycleStage stage);
    Task<ValidationResult> ValidateAsync(TotalIncomeStage stage);
    Task<ValidationResult> ValidateAsync(TotalExpenditureStage stage);
    Task<ValidationResult> ValidateAsync(TotalTeacherCostsStage stage);
    Task<ValidationResult> ValidateAsync(TotalNumberTeachersStage stage);
    Task<ValidationResult> ValidateAsync(TotalEducationSupportStage stage);
    Task<ValidationResult> ValidateAsync(PrimaryHasMixedAgeClassesStage stage);
    Task<ValidationResult> ValidateAsync(PrimaryMixedAgeClassesStage stage);
    Task<ValidationResult> ValidateAsync(PrimaryPupilFiguresStage stage);
    Task<ValidationResult> ValidateAsync(PupilFiguresStage stage);
    Task<ValidationResult> ValidateAsync(TeacherPeriodAllocationStage stage);
    Task<ValidationResult> ValidateAsync(TeachingAssistantFiguresStage stage);
    Task<ValidationResult> ValidateAsync(OtherTeachingPeriodsStage stage);
    Task<ValidationResult> ValidateAsync(OtherTeachingPeriodsConfirmStage stage);
    Task<ValidationResult> ValidateAsync(ManagementRolesStage stage);
    Task<ValidationResult> ValidateAsync(ManagersPerRoleStage stage);
    Task<ValidationResult> ValidateAsync(TeachingPeriodsManagerStage stage);
}