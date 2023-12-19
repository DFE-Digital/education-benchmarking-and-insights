using EducationBenchmarking.Platform.Infrastructure.Cosmos;
using EducationBenchmarking.Platform.Shared;

namespace EducationBenchmarking.Platform.Api.Insight.Db;

public static class SchoolFactory
{
    public static School Create(Edubase edubase)
    {
        return new School
        {
            Urn = edubase.URN.ToString(),
            Kind = edubase.TypeOfEstablishment,
            FinanceType = edubase.FinanceType,
            Name = edubase.EstablishmentName
        };
    }
    
    public static SchoolExpenditure Create(SchoolTrustFinancialDataObject finances)
    {
        return new SchoolExpenditure
        {
            Urn = finances.URN.ToString(),
            Name = finances.SchoolName,
            FinanceType = finances.FinanceType,
            LocalAuthority = finances.LA.ToString(),
            TotalExpenditure = finances.TotalExpenditure,
            TeachingStaffCosts = finances.TeachingStaff,
            SupplyTeachingStaffCosts = finances.SupplyTeachingStaff,
            EducationalConsultancyCosts = finances.EducationalConsultancy,
            EducationSupportStaffCosts = finances.EducationSupportStaff,
            AgencySupplyTeachingStaffCosts = finances.AgencyTeachingStaff,
            OtherInsurancePremiumsCosts = finances.OtherInsurancePremiums,
            DirectRevenueFinancingCosts = finances.DirectRevenueFinancing,
            GroundsMaintenanceCosts = finances.GroundsMaintenanceImprovement,
            IndirectEmployeeExpenses = finances.IndirectEmployeeExpenses,
            InterestChargesLoanBank = finances.InterestLoansAndBanking,
            PrivateFinanceInitiativeCharges = finances.PFICharges,
            RentRatesCosts = finances.RentRates,
            SpecialFacilitiesCosts = finances.Specialfacilities,
            StaffDevelopmentTrainingCosts = finances.StaffDevelopment,
            StaffRelatedInsuranceCosts = finances.StaffInsurance,
            SupplyTeacherInsurableCosts = finances.SupplyTeacherInsurance,
            CommunityFocusedSchoolStaff = finances.CommunityFocusedStaff,
            CommunityFocusedSchoolCosts = finances.CommunityFocusedSchoolCosts
            
        };
    }
}