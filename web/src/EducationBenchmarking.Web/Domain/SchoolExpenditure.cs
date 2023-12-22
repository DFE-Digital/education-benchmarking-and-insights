namespace EducationBenchmarking.Web.Domain;

public record SchoolExpenditure
{
    public string[]? AllDimensions { get; set; }
    
    public SectionDimensions? Dimensions { get; set; }
    public Dictionary<string, School>? Schools { get; set; }
    public Dictionary<string, decimal>? TotalExpenditure { get; set; }
    public Dictionary<string, decimal>? TotalTeachingSupportStaffCosts { get; set; }
    public Dictionary<string, decimal>? TeachingStaffCosts { get; set; }
    public Dictionary<string, decimal>? SupplyTeachingStaffCosts { get; set; }
    public Dictionary<string, decimal>? EducationalConsultancyCosts { get; set; }
    public Dictionary<string, decimal>? EducationSupportStaffCosts { get; set; }
    public Dictionary<string, decimal>? AgencySupplyTeachingStaffCosts { get; set; }
}