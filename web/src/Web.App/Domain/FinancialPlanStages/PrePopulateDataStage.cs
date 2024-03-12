namespace Web.App.Domain.FinancialPlanStages
{
    public class PrePopulateDataStage : Stage
    {
        public bool? UseFigures { get; set; }
        public decimal? TotalIncome { get; set; }
        public decimal? TotalExpenditure { get; set; }
        public decimal? TotalTeacherCosts { get; set; }
        public decimal? TotalNumberOfTeachersFte { get; set; }
        public decimal? EducationSupportStaffCosts { get; set; }

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
}