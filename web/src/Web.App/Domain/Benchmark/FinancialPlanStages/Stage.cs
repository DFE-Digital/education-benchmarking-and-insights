namespace Web.App.Domain;

public abstract class Stage
{
    public string? Referrer { get; set; }

    public virtual void SetPlanValues(FinancialPlanInput planInput)
    {
    }
}