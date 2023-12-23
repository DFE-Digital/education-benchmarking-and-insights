namespace EducationBenchmarking.Web.Domain;

public record SchoolExpenditure
{
    public string Urn { get; set; }
    public string Name { get; set; }
    public string FinanceType { get; set; }
    public string LocalAuthority { get; set; }
    
    public decimal TotalExpenditure { get; set; }
    public decimal NumberOfPupils { get; set; }
    public decimal TotalIncome { get; set; }
    
    public decimal TotalTeachingSupportStaffCosts { get; set; }
    public decimal TeachingStaffCosts { get; set; } 
    public decimal SupplyTeachingStaffCosts { get; set; } 
    public decimal EducationalConsultancyCosts { get; set; } 
    public decimal EducationSupportStaffCosts { get; set; } 
    public decimal AgencySupplyTeachingStaffCosts { get; set; }
    
    public decimal NetCateringCosts { get; set; }
    public decimal CateringStaffCosts { get; set; }
    public decimal CateringSuppliesCosts { get; set; }
    public decimal IncomeCatering { get; set; }
}