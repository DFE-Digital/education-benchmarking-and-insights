namespace Web.App.Domain.FinancialPlanStages;

public class PrePopulateDataStage : Stage
{
    public bool? UseFigures { get; set; }
    public string? TotalIncome { get; set; }
    public string? TotalExpenditure { get; set; }
    public string? TotalTeacherCosts { get; set; }
    public decimal? TotalNumberOfTeachersFte { get; set; }
    public string? EducationSupportStaffCosts { get; set; }

    public override void SetPlanValues(FinancialPlanInput planInput)
    {
        planInput.UseFigures = UseFigures;
        if (UseFigures is true)
        {
            planInput.TotalIncome = TotalIncome;
            planInput.TotalExpenditure = TotalExpenditure;
            planInput.TotalTeacherCosts = TotalTeacherCosts;
            planInput.TotalNumberOfTeachersFte = TotalNumberOfTeachersFte;
            planInput.EducationSupportStaffCosts = EducationSupportStaffCosts;
        }
    }
}