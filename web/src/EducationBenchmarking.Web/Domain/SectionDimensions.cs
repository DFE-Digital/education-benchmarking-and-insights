using System.Diagnostics.CodeAnalysis;

namespace EducationBenchmarking.Web.Domain;

[ExcludeFromCodeCoverage]
public record SectionDimensions
{
    public string? TotalExpenditure { get; set; }
    public string? TeachingSupportStaff { get; set; }
    public string? NonEducationalSupportStaff { get; set; }
    public string? EducationalSupplies { get; set; }
    public string? EducationalIct { get; set; }
    public string? PremisesStaffServices { get; set; }
    public string? Utilities { get; set; }
    public string? AdministrativeSupplies { get; set; }
    public string? CateringStaffServices { get; set; }
    public string? OtherCosts { get; set; }
}