namespace Web.App.Domain.FinancialPlanStages;

public class TotalIncomeStage : Stage
{
    public string? TotalIncome { get; set; }

    public override void SetPlanValues(FinancialPlanInput planInput)
    {
        planInput.TotalIncome = TotalIncome;
    }
}