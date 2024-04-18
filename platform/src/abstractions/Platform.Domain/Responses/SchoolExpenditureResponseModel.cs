using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Platform.Domain;

[ExcludeFromCodeCoverage]
public record SchoolExpenditureResponseModel
{
    public string? Urn { get; set; }
    public string? Name { get; set; }
    public string? SchoolType { get; set; }
    public string? LocalAuthority { get; set; }
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
    public decimal TotalEducationalSuppliesCosts { get; set; }
    public decimal ExaminationFeesCosts { get; set; }
    public decimal BreakdownEducationalSuppliesCosts { get; set; }
    public decimal LearningResourcesNonIctCosts { get; set; }
    public decimal TotalNonEducationalSupportStaffCosts { get; set; }
    public decimal AdministrativeClericalStaffCosts { get; set; }
    public decimal AuditorsCosts { get; set; }
    public decimal OtherStaffCosts { get; set; }
    public decimal ProfessionalServicesNonCurriculumCosts { get; set; }
    public decimal TotalPremisesStaffServiceCosts { get; set; }
    public decimal CleaningCaretakingCosts { get; set; }
    public decimal MaintenancePremisesCosts { get; set; }
    public decimal OtherOccupationCosts { get; set; }
    public decimal PremisesStaffCosts { get; set; }
    public decimal TotalOtherCosts { get; set; }
    public decimal OtherInsurancePremiumsCosts { get; set; }
    public decimal DirectRevenueFinancingCosts { get; set; }
    public decimal GroundsMaintenanceCosts { get; set; }
    public decimal IndirectEmployeeExpenses { get; set; }
    public decimal InterestChargesLoanBank { get; set; }
    public decimal PrivateFinanceInitiativeCharges { get; set; }
    public decimal RentRatesCosts { get; set; }
    public decimal SpecialFacilitiesCosts { get; set; }
    public decimal StaffDevelopmentTrainingCosts { get; set; }
    public decimal StaffRelatedInsuranceCosts { get; set; }
    public decimal SupplyTeacherInsurableCosts { get; set; }
    public decimal CommunityFocusedSchoolStaff { get; set; }
    public decimal CommunityFocusedSchoolCosts { get; set; }
    public decimal TotalUtilitiesCosts { get; set; }
    public decimal EnergyCosts { get; set; }
    public decimal WaterSewerageCosts { get; set; }
    public int? FloorArea { get; set; }
    public bool HasIncompleteData { get; set; }

    public static SchoolExpenditureResponseModel Create(SchoolTrustFinancialDataObject dataObject, FloorAreaDataObject[] floorArea)
    {
        return new SchoolExpenditureResponseModel
        {
            Urn = dataObject.Urn.ToString(),
            Name = dataObject.SchoolName,
            SchoolType = dataObject.Type,
            LocalAuthority = dataObject.La.ToString(),
            NumberOfPupils = dataObject.NoPupils,
            TotalExpenditure = dataObject.TotalExpenditure,
            TotalIncome = dataObject.TotalIncome,
            TotalTeachingSupportStaffCosts = dataObject.TeachingStaff + dataObject.SupplyTeachingStaff +
                                             dataObject.EducationalConsultancy + dataObject.EducationSupportStaff +
                                             dataObject.AgencyTeachingStaff,
            TeachingStaffCosts = dataObject.TeachingStaff,
            SupplyTeachingStaffCosts = dataObject.SupplyTeachingStaff,
            EducationalConsultancyCosts = dataObject.EducationalConsultancy,
            EducationSupportStaffCosts = dataObject.EducationSupportStaff,
            AgencySupplyTeachingStaffCosts = dataObject.AgencyTeachingStaff,
            NetCateringCosts = dataObject.CateringExp,
            CateringStaffCosts = dataObject.CateringStaff,
            CateringSuppliesCosts = dataObject.CateringSupplies,
            IncomeCatering = dataObject.IncomeFromCatering,
            AdministrativeSuppliesCosts = dataObject.AdministrativeSupplies,
            LearningResourcesIctCosts = dataObject.IctLearningResources,
            TotalEducationalSuppliesCosts = dataObject.ExaminationFees + dataObject.EducationalSupplies +
                                            dataObject.LearningResources,
            ExaminationFeesCosts = dataObject.ExaminationFees,
            BreakdownEducationalSuppliesCosts = dataObject.EducationalSupplies,
            LearningResourcesNonIctCosts = dataObject.LearningResources,
            TotalNonEducationalSupportStaffCosts = dataObject.AdministrativeClericalStaff + dataObject.AuditorCosts +
                                                   dataObject.OtherStaffCosts + dataObject.BroughtProfessionalServices,
            AdministrativeClericalStaffCosts = dataObject.AdministrativeClericalStaff,
            AuditorsCosts = dataObject.AuditorCosts,
            OtherStaffCosts = dataObject.OtherStaffCosts,
            ProfessionalServicesNonCurriculumCosts = dataObject.BroughtProfessionalServices,
            TotalPremisesStaffServiceCosts = dataObject.CleaningCaretaking + dataObject.PremisesStaff +
                                             dataObject.OtherOccupationCosts + dataObject.PremisesStaff,
            CleaningCaretakingCosts = dataObject.CleaningCaretaking,
            MaintenancePremisesCosts = dataObject.Premises,
            OtherOccupationCosts = dataObject.OtherOccupationCosts,
            PremisesStaffCosts = dataObject.PremisesStaff,
            TotalOtherCosts = dataObject.OtherInsurancePremiums + dataObject.DirectRevenue +
                              dataObject.BuildingGroundsMaintenance + dataObject.IndirectEmployeeExpenses +
                              dataObject.InterestCharges + dataObject.PfiCharges + dataObject.RentRates +
                              dataObject.SpecialFacilities + dataObject.StaffDevelopment + dataObject.StaffInsurance +
                              dataObject.SupplyTeacherInsurance + dataObject.CommunityFocusedStaff +
                              dataObject.CommunityFocusedSchoolCosts,
            OtherInsurancePremiumsCosts = dataObject.OtherInsurancePremiums,
            DirectRevenueFinancingCosts = dataObject.DirectRevenue,
            GroundsMaintenanceCosts = dataObject.BuildingGroundsMaintenance,
            IndirectEmployeeExpenses = dataObject.IndirectEmployeeExpenses,
            InterestChargesLoanBank = dataObject.InterestCharges,
            PrivateFinanceInitiativeCharges = dataObject.PfiCharges,
            RentRatesCosts = dataObject.RentRates,
            SpecialFacilitiesCosts = dataObject.SpecialFacilities,
            StaffDevelopmentTrainingCosts = dataObject.StaffDevelopment,
            StaffRelatedInsuranceCosts = dataObject.StaffInsurance,
            SupplyTeacherInsurableCosts = dataObject.SupplyTeacherInsurance,
            CommunityFocusedSchoolStaff = dataObject.CommunityFocusedStaff,
            CommunityFocusedSchoolCosts = dataObject.CommunityFocusedSchoolCosts,
            TotalUtilitiesCosts = dataObject.Energy + dataObject.WaterSewerage,
            EnergyCosts = dataObject.Energy,
            WaterSewerageCosts = dataObject.WaterSewerage,
            FloorArea = floorArea.FirstOrDefault(x => x.Urn == dataObject.Urn)?.FloorArea,
            HasIncompleteData = dataObject.PeriodCoveredByReturn != 12,
        };
    }
}