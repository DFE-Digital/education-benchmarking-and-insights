namespace Web.App.Domain.Benchmark.FinancialPlanStages;

public class TotalTeacherCostsStage : Stage
{
    public string? TotalTeacherCosts { get; set; }

    public override void SetPlanValues(FinancialPlanInput planInput)
    {
        planInput.TotalTeacherCosts = TotalTeacherCosts;
    }
}