namespace Web.App.Domain.FinancialPlanStages;

public class PrePopulateDataStage : Stage
{
    public bool? UseFigures { get; set; }
    public string? TotalIncome { get; set; }
    public string? TotalExpenditure { get; set; }
    public string? TotalTeacherCosts { get; set; }
    public decimal? TotalNumberOfTeachersFte { get; set; }
    public string? EducationSupportStaffCosts { get; set; }

    public override void SetPlanValues(FinancialPlan plan)
    {
        plan.UseFigures = UseFigures;
        if (UseFigures is true)
        {
            plan.TotalIncome = TotalIncome;
            plan.TotalExpenditure = TotalExpenditure;
            plan.TotalTeacherCosts = TotalTeacherCosts;
            plan.TotalNumberOfTeachersFte = TotalNumberOfTeachersFte;
            plan.EducationSupportStaffCosts = EducationSupportStaffCosts;
        }
    }
}