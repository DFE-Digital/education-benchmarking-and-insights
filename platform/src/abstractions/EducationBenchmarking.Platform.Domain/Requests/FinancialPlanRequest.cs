using System.Diagnostics.CodeAnalysis;

namespace EducationBenchmarking.Platform.Domain.Requests;

[ExcludeFromCodeCoverage]
public record FinancialPlanRequest
{
    public string? User { get; set; }
    public bool? UseFigures { get; set; }
    public decimal? TotalIncome { get; set; }
    public decimal? TotalExpenditure { get; set; }
    public decimal? TotalTeacherCosts { get; set; }
    public decimal? TotalNumberOfTeachersFte { get; set; }
    public decimal? EducationSupportStaffCosts { get; set; }
    public int? TimetablePeriods { get; set; }
}