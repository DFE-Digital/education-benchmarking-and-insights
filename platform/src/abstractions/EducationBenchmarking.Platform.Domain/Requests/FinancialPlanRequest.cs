namespace EducationBenchmarking.Platform.Domain.Requests;

public record FinancialPlanRequest
{
    public string? User { get; set; }
    public bool UseFigures { get; set; }
    public string TotalIncome { get; set; }
    public string TotalExpenditure { get; set; }
    public string TotalTeacherCosts { get; set; }
    public string TotalNumberOfTeachersFte { get; set; }
    public string EducationSupportStaffCosts { get; set; }
}