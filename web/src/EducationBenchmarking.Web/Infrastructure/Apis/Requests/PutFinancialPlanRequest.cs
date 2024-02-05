namespace EducationBenchmarking.Web.Infrastructure.Apis;

public class PutFinancialPlanRequest
{
    public int Year { get; set; }
    public string? Urn { get; set; }
    
    public bool? UseFigures { get; set; }
}