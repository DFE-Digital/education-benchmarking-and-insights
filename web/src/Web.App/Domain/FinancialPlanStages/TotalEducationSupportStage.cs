namespace Web.App.Domain.FinancialPlanStages;

public class TotalEducationSupportStage : Stage
{
    public decimal? EducationSupportStaffCosts { get; set; }

    public override void SetPlanValues(FinancialPlan plan)
    {
        plan.EducationSupportStaffCosts = EducationSupportStaffCosts;
    }
}