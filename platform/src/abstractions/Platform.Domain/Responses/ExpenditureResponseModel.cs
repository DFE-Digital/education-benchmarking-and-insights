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
    public decimal? TotalNonEducationalSupportStaffCosts { get; private set; }
    public decimal? AdministrativeClericalStaffCosts { get; private set; }
    public decimal? AuditorsCosts { get; private set; }
    public decimal? OtherStaffCosts { get; private set; }
    public decimal? ProfessionalServicesNonCurriculumCosts { get; private set; }
    public decimal? TotalEducationalSuppliesCosts { get; private set; }
    public decimal? ExaminationFeesCosts { get; private set; }
    public decimal? LearningResourcesNonIctCosts { get; private set; }
    public decimal? LearningResourcesIctCosts { get; private set; }
    public decimal? TotalPremisesStaffServiceCosts { get; private set; }
    public decimal? CleaningCaretakingCosts { get; private set; }
    public decimal? MaintenancePremisesCosts { get; private set; }
    public decimal? OtherOccupationCosts { get; private set; }
    public decimal? PremisesStaffCosts { get; private set; }
    public decimal? TotalUtilitiesCosts { get; private set; }
    public decimal? EnergyCosts { get; private set; }
    public decimal? WaterSewerageCosts { get; private set; }
    public decimal? AdministrativeSuppliesCosts { get; private set; }
    public decimal? TotalGrossCateringCosts { get; private set; }
    public decimal? CateringStaffCosts { get; private set; }
    public decimal? CateringSuppliesCosts { get; private set; }
    public decimal? TotalOtherCosts { get; private set; }
    public decimal? DirectRevenueFinancingCosts { get; private set; }
    public decimal? GroundsMaintenanceCosts { get; private set; }
    public decimal? IndirectEmployeeExpenses { get; private set; }
    public decimal? InterestChargesLoanBank { get; private set; }
    public decimal? OtherInsurancePremiumsCosts { get; private set; }
    public decimal? PrivateFinanceInitiativeCharges { get; private set; }
    public decimal? RentRatesCosts { get; private set; }
    public decimal? SpecialFacilitiesCosts { get; private set; }
    public decimal? StaffDevelopmentTrainingCosts { get; private set; }
    public decimal? StaffRelatedInsuranceCosts { get; private set; }
    public decimal? SupplyTeacherInsurableCosts { get; private set; }
    public decimal? CommunityFocusedSchoolStaff { get; private set; }
    public decimal? CommunityFocusedSchoolCosts { get; private set; }

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
                TotalNonEducationalSupportStaffCosts = CalculationValue(CalcTotalNonEducationalSupportStaffCosts(dataObject), dataObject, dimension),
                AdministrativeClericalStaffCosts = CalculationValue(dataObject.AdministrativeClericalStaff, dataObject, dimension),
                AuditorsCosts = CalculationValue(dataObject.AuditorCosts, dataObject, dimension),
                OtherStaffCosts = CalculationValue(dataObject.OtherStaffCosts, dataObject, dimension),
                ProfessionalServicesNonCurriculumCosts = CalculationValue(dataObject.BroughtProfessionalServices, dataObject, dimension),
                TotalEducationalSuppliesCosts = CalculationValue(CalcTotalEducationalSuppliesCosts(dataObject), dataObject, dimension),
                ExaminationFeesCosts = CalculationValue(dataObject.ExaminationFees, dataObject, dimension),
                LearningResourcesNonIctCosts = CalculationValue(dataObject.LearningResources, dataObject, dimension),
                LearningResourcesIctCosts = CalculationValue(dataObject.IctLearningResources, dataObject, dimension),
                TotalPremisesStaffServiceCosts = CalculationValue(CalcTotalPremisesStaffServiceCosts(dataObject), dataObject, dimension),
                CleaningCaretakingCosts = CalculationValue(dataObject.CleaningCaretaking, dataObject, dimension),
                MaintenancePremisesCosts = CalculationValue(dataObject.Premises, dataObject, dimension),
                OtherOccupationCosts = CalculationValue(dataObject.OtherOccupationCosts, dataObject, dimension),
                PremisesStaffCosts = CalculationValue(dataObject.PremisesStaff, dataObject, dimension),
                TotalUtilitiesCosts = CalculationValue(CalcTotalUtilitiesCosts(dataObject), dataObject, dimension),
                EnergyCosts = CalculationValue(dataObject.Energy, dataObject, dimension),
                WaterSewerageCosts = CalculationValue(dataObject.WaterSewerage, dataObject, dimension),
                AdministrativeSuppliesCosts = CalculationValue(dataObject.AdministrativeSupplies, dataObject, dimension),
                TotalGrossCateringCosts = CalculationValue(CalcTotalGrossCateringCosts(dataObject), dataObject, dimension),
                CateringStaffCosts = CalculationValue(dataObject.CateringStaff, dataObject, dimension),
                CateringSuppliesCosts = CalculationValue(dataObject.CateringSupplies, dataObject, dimension),
                TotalOtherCosts = CalculationValue(CalcTotalOtherCosts(dataObject), dataObject, dimension),
                OtherInsurancePremiumsCosts = CalculationValue(dataObject.OtherInsurancePremiums, dataObject, dimension),
                DirectRevenueFinancingCosts = CalculationValue(dataObject.DirectRevenue, dataObject, dimension),
                GroundsMaintenanceCosts = CalculationValue(dataObject.BuildingGroundsMaintenance, dataObject, dimension),
                IndirectEmployeeExpenses = CalculationValue(dataObject.IndirectEmployeeExpenses, dataObject, dimension),
                InterestChargesLoanBank = CalculationValue(dataObject.InterestCharges, dataObject, dimension),
                PrivateFinanceInitiativeCharges = CalculationValue(dataObject.PfiCharges, dataObject, dimension),
                RentRatesCosts = CalculationValue(dataObject.RentRates, dataObject, dimension),
                SpecialFacilitiesCosts = CalculationValue(dataObject.SpecialFacilities, dataObject, dimension),
                StaffDevelopmentTrainingCosts = CalculationValue(dataObject.StaffDevelopment, dataObject, dimension),
                StaffRelatedInsuranceCosts = CalculationValue(dataObject.StaffInsurance, dataObject, dimension),
                SupplyTeacherInsurableCosts = CalculationValue(dataObject.SupplyTeacherInsurance, dataObject, dimension),
                CommunityFocusedSchoolStaff = CalculationValue(dataObject.CommunityFocusedStaff, dataObject, dimension),
                CommunityFocusedSchoolCosts = CalculationValue(dataObject.CommunityFocusedSchoolCosts, dataObject, dimension),
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

    private static decimal CalcTotalNonEducationalSupportStaffCosts(SchoolTrustFinancialDataObject? dataObject)
    {
        return dataObject == null
            ? 0
            : dataObject.AdministrativeClericalStaff + dataObject.AuditorCosts +
              dataObject.OtherStaffCosts + dataObject.BroughtProfessionalServices;
    }

    private static decimal CalcTotalEducationalSuppliesCosts(SchoolTrustFinancialDataObject? dataObject)
    {
        return dataObject == null
            ? 0
            : dataObject.ExaminationFees + dataObject.LearningResources;
    }

    private static decimal CalcTotalPremisesStaffServiceCosts(SchoolTrustFinancialDataObject? dataObject)
    {
        return dataObject == null
            ? 0
            : dataObject.CleaningCaretaking + dataObject.PremisesStaff +
              dataObject.OtherOccupationCosts + dataObject.PremisesStaff;
    }

    private static decimal CalcTotalUtilitiesCosts(SchoolTrustFinancialDataObject? dataObject)
    {
        return dataObject == null
            ? 0
            : dataObject.Energy + dataObject.WaterSewerage;
    }

    private static decimal CalcTotalGrossCateringCosts(SchoolTrustFinancialDataObject? dataObject)
    {
        return dataObject == null
            ? 0
            : dataObject.CateringStaff + dataObject.CateringSupplies;
    }

    private static decimal CalcTotalOtherCosts(SchoolTrustFinancialDataObject? dataObject)
    {
        return dataObject == null
            ? 0
            : dataObject.OtherInsurancePremiums + dataObject.DirectRevenue +
              dataObject.BuildingGroundsMaintenance + dataObject.IndirectEmployeeExpenses +
              dataObject.InterestCharges + dataObject.PfiCharges + dataObject.RentRates +
              dataObject.SpecialFacilities + dataObject.StaffDevelopment + dataObject.StaffInsurance +
              dataObject.SupplyTeacherInsurance + dataObject.CommunityFocusedStaff +
              dataObject.CommunityFocusedSchoolCosts;
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