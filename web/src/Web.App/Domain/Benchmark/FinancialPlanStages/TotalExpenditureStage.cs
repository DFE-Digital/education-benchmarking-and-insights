namespace Web.App.Domain;

public class TotalExpenditureStage : Stage
{
    public string? TotalExpenditure { get; set; }

    public override void SetPlanValues(FinancialPlanInput planInput)
    {
        planInput.TotalExpenditure = TotalExpenditure;
    }
}