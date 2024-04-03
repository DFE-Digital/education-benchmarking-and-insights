using System;
using System.Diagnostics.CodeAnalysis;

namespace Platform.Domain;

[ExcludeFromCodeCoverage]
public record ExpenditureResponseModel
{
    public int YearEnd { get; private set; }
    public Dimension Dimension { get; private set; }
    public decimal? TotalExpenditure { get; private set; }
    public decimal? TotalTeachingSupportStaffCosts { get; private set; }
    public decimal? TeachingStaffCosts { get; private set; }
    public decimal? SupplyTeachingStaffCosts { get; private set; }
    public decimal? EducationalConsultancyCosts { get; private set; }
    public decimal? EducationSupportStaffCosts { get; private set; }
    public decimal? AgencySupplyTeachingStaffCosts { get; private set; }
    /*public decimal NetCateringCosts { get; set; }
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
    public int? FloorArea { get; set; }*/

    public static ExpenditureResponseModel Create(SchoolTrustFinancialDataObject? dataObject, int term, Dimension dimension = Dimension.Actuals)
    {
        return dataObject is null
            ? new ExpenditureResponseModel
            {
                YearEnd = term,
                Dimension = dimension,
            }
            : new ExpenditureResponseModel
            {
                YearEnd = term,
                Dimension = dimension,
                TotalExpenditure = CalculationValue(dataObject.TotalExpenditure, dataObject, dimension),
                TotalTeachingSupportStaffCosts = CalculationValue(CalcTotalTeachingSupportStaffCosts(dataObject), dataObject, dimension),
                TeachingStaffCosts = CalculationValue(dataObject.TeachingStaff, dataObject, dimension),
                SupplyTeachingStaffCosts = CalculationValue(dataObject.SupplyTeachingStaff, dataObject, dimension),
                EducationalConsultancyCosts = CalculationValue(dataObject.EducationalConsultancy, dataObject, dimension),
                EducationSupportStaffCosts = CalculationValue(dataObject.EducationSupportStaff, dataObject, dimension),
                AgencySupplyTeachingStaffCosts = CalculationValue(dataObject.AgencyTeachingStaff, dataObject, dimension),
                /*NetCateringCosts = dataObject.CateringExp,
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
                WaterSewerageCosts = dataObject.WaterSewerage*/
            };
    }

    private static decimal CalcTotalTeachingSupportStaffCosts(SchoolTrustFinancialDataObject? dataObject)
    {
        return dataObject == null
            ? 0
            : dataObject.TeachingStaff + dataObject.SupplyTeachingStaff +
              dataObject.EducationalConsultancy + dataObject.EducationSupportStaff +
              dataObject.AgencyTeachingStaff;
    }

    private static decimal CalculationValue(decimal value, SchoolTrustFinancialDataObject dataObject, Dimension dimension)
    {
        return dimension switch
        {
            Dimension.Actuals => value,
            Dimension.PoundPerPupil => dataObject.NoPupils != 0 ? value / dataObject.NoPupils : 0,
            Dimension.PercentIncome => dataObject.TotalIncome != 0 ? value / dataObject.TotalIncome * 100 : 0,
            Dimension.PercentExpenditure => dataObject.TotalExpenditure != 0 ? value / dataObject.TotalExpenditure * 100 : 0,
            _ => throw new ArgumentOutOfRangeException(nameof(dimension), dimension, "Unknown value dimension")
        };
    }
}