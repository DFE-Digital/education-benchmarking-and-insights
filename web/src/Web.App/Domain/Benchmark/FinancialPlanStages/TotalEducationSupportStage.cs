namespace Web.App.Domain.Benchmark.FinancialPlanStages;

public class TotalEducationSupportStage : Stage
{
    public string? EducationSupportStaffCosts { get; set; }

    public override void SetPlanValues(FinancialPlanInput planInput)
    {
        planInput.EducationSupportStaffCosts = EducationSupportStaffCosts;
    }
}