using EducationBenchmarking.Web.Domain.FinancialPlanStages;
using EducationBenchmarking.Web.Validators.FinancialPlanStages;
using FluentValidation.Results;

namespace EducationBenchmarking.Web.Validators;

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

    public async Task<ValidationResult> ValidateAsync(SelectYearStage stage)
    {
        return await SelectYear.ValidateAsync(stage);
    }

    public async Task<ValidationResult> ValidateAsync(PrePopulateDataStage stage)
    {
        return await PrePopulateData.ValidateAsync(stage);
    }

    public async Task<ValidationResult> ValidateAsync(TimetableCycleStage stage)
    {
        return await TimetableCycle.ValidateAsync(stage);
    }

    public async Task<ValidationResult> ValidateAsync(TotalIncomeStage stage)
    {
        return await TotalIncome.ValidateAsync(stage);
    }

    public async Task<ValidationResult> ValidateAsync(TotalExpenditureStage stage)
    {
        return await TotalExpenditureStage.ValidateAsync(stage);
    }

    public async Task<ValidationResult> ValidateAsync(TotalTeacherCostsStage stage)
    {
        return await TotalTeacherCostsStage.ValidateAsync(stage);
    }

    public async Task<ValidationResult> ValidateAsync(TotalNumberTeachersStage stage)
    {
        return await TotalNumberTeachers.ValidateAsync(stage);
    }

    public async Task<ValidationResult> ValidateAsync(TotalEducationSupportStage stage)
    {
        return await TotalEducationSupport.ValidateAsync(stage);
    }

    public async Task<ValidationResult> ValidateAsync(PrimaryHasMixedAgeClassesStage stage)
    {
        return await PrimaryHasMixedAgeClasses.ValidateAsync(stage);
    }

    public async Task<ValidationResult> ValidateAsync(PrimaryMixedAgeClassesStage stage)
    {
        return await PrimaryMixedAgeClasses.ValidateAsync(stage);
    }

    public async Task<ValidationResult> ValidateAsync(PrimaryPupilFiguresStage stage)
    {
        return await PrimaryPupilFigures.ValidateAsync(stage);
    }

    public async Task<ValidationResult> ValidateAsync(PupilFiguresStage stage)
    {
        return await PupilFigures.ValidateAsync(stage);
    }

    public async Task<ValidationResult> ValidateAsync(TeacherPeriodAllocationStage stage)
    {
        return await TeacherPeriodAllocation.ValidateAsync(stage);
    }

    public async Task<ValidationResult> ValidateAsync(TeachingAssistantFiguresStage stage)
    {
        return await TeachingAssistantFigures.ValidateAsync(stage);
    }

    public async Task<ValidationResult> ValidateAsync(OtherTeachingPeriodsStage stage)
    {
        return await OtherTeachingPeriods.ValidateAsync(stage);
    }

    public async Task<ValidationResult> ValidateAsync(OtherTeachingPeriodsConfirmStage stage)
    {
        return await OtherTeachingPeriodsConfirm.ValidateAsync(stage);
    }

    public async Task<ValidationResult> ValidateAsync(ManagementRolesStage stage)
    {
        return await ManagementRoles.ValidateAsync(stage);
    }

    public async Task<ValidationResult> ValidateAsync(ManagersPerRoleStage stage)
    {
        return await ManagersPerRole.ValidateAsync(stage);
    }

    public async Task<ValidationResult> ValidateAsync(TeachingPeriodsManagerStage stage)
    {
        return await TeachingPeriodsManager.ValidateAsync(stage);
    }
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