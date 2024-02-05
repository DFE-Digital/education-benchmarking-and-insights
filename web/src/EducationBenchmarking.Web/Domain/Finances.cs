using System.Diagnostics.CodeAnalysis;

namespace EducationBenchmarking.Web.Domain;

[ExcludeFromCodeCoverage]
public record Finances
{
    public string? Urn { get; set; }
    public string? SchoolName { get; set; }
    public int YearEnd { get; set; }
    public string? OverallPhase { get; set; }
    public decimal NumberOfPupils { get; set; }
    public bool HasSixthForm { get; set; }

    public decimal TotalExpenditure { get; set; }
    public decimal TeachingStaffCosts { get; set; }
    public decimal TotalIncome { get; set; }
    public decimal BreakdownEducationalSuppliesCosts { get; set; }
    public decimal SupplyTeachingStaffCosts { get; set; }
    public decimal EducationSupportStaffCosts { get; set; }
    public decimal AdministrativeClericalStaffCosts { get; set; }
    public decimal OtherStaffCosts { get; set; }
    public decimal MaintenancePremisesCosts { get; set; }
    public decimal TotalNumberOfTeachersFte { get; set; }
}