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
    
    public decimal AdministrativeSuppliesCosts { get; set; }
    
    public decimal LearningResourcesIctCosts { get; set; }
    
    public decimal ExaminationFeesCosts { get; set; }
    public decimal BreakdownEducationalSuppliesCosts { get; set; }
    public decimal LearningResourcesNonIctCosts { get; set; }
    
    public decimal AdministrativeClericalStaffCosts { get; set; }
    public decimal AuditorsCosts { get; set; }
    public decimal OtherStaffCosts { get; set; }
    public decimal ProfessionalServicesNonCurriculumCosts { get; set; }
    
    public decimal CleaningCaretakingCosts { get; set; }
    public decimal MaintenancePremisesCosts { get; set; }
    public decimal OtherOccupationCosts { get; set; }
    public decimal PremisesStaffCosts { get; set; }
}