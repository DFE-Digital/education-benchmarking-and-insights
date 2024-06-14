namespace Web.App.Domain.Benchmark.FinancialPlanStages;

public abstract class Stage
{
    public string? Referrer { get; set; }

    public virtual void SetPlanValues(FinancialPlanInput planInput)
    {
    }
}