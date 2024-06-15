namespace Web.App.Domain;

public class TotalTeacherCostsStage : Stage
{
    public string? TotalTeacherCosts { get; set; }

    public override void SetPlanValues(FinancialPlanInput planInput)
    {
        planInput.TotalTeacherCosts = TotalTeacherCosts;
    }
}