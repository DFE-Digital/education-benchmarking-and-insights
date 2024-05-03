namespace Web.App.Domain;

public class FinancialPlan
{
    public int Year { get; set; }
    public string? Urn { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public bool IsComplete { get; set; }
}