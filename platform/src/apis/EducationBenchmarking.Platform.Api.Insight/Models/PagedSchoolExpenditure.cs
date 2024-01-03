using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using EducationBenchmarking.Platform.Infrastructure.Cosmos;
using EducationBenchmarking.Platform.Shared;

namespace EducationBenchmarking.Platform.Api.Insight.Models;

[ExcludeFromCodeCoverage]
public class PagedSchoolExpenditure : PagedResults<SchoolExpenditure>
{
    public static PagedSchoolExpenditure Create(IEnumerable<SchoolTrustFinancialDataObject> results, int page,
        int pageSize)
    {
        var schools = new List<SchoolExpenditure>();

        foreach (var result in results)
        {
            schools.Add(new SchoolExpenditure
            {
                Urn = result.URN.ToString(),
                Name = result.SchoolName,
                SchoolType = result.Type,
                LocalAuthority = result.LA.ToString(),
                NumberOfPupils = result.NoPupils,
                TotalExpenditure = result.TotalExpenditure,
                TotalIncome = result.TotalIncome,
                TotalTeachingSupportStaffCosts = result.TeachingStaff + result.SupplyTeachingStaff +
                                                 result.EducationalConsultancy + result.EducationSupportStaff +
                                                 result.AgencyTeachingStaff,
                TeachingStaffCosts = result.TeachingStaff,
                SupplyTeachingStaffCosts = result.SupplyTeachingStaff,
                EducationalConsultancyCosts = result.EducationalConsultancy,
                EducationSupportStaffCosts = result.EducationSupportStaff,
                AgencySupplyTeachingStaffCosts = result.AgencyTeachingStaff,
                NetCateringCosts = result.CateringExp,
                CateringStaffCosts = result.CateringStaff,
                CateringSuppliesCosts = result.CateringSupplies,
                IncomeCatering = result.IncomeFromCatering,
                AdministrativeSuppliesCosts = result.AdministrativeSupplies,
                LearningResourcesIctCosts = result.ICTLearningResources,
                ExaminationFeesCosts = result.ExaminationFees,
                BreakdownEducationalSuppliesCosts = result.EducationalSupplies,
                LearningResourcesNonIctCosts = result.LearningResources,
                AdministrativeClericalStaffCosts = result.AdministrativeClericalStaff,
                AuditorsCosts = result.AuditorCosts,
                OtherStaffCosts = result.OtherStaffCosts,
                ProfessionalServicesNonCurriculumCosts = result.BroughtProfessionalServices,
                CleaningCaretakingCosts = result.CleaningCaretaking,
                MaintenancePremisesCosts = result.Premises,
                OtherOccupationCosts = result.OtherOccupationCosts,
                PremisesStaffCosts = result.PremisesStaff,
                TotalOtherCosts = result.OtherInsurancePremiums + result.DirectRevenue +
                                  result.BuildingGroundsMaintenance + result.IndirectEmployeeExpenses +
                                  result.InterestCharges + result.PFICharges + result.RentRates +
                                  result.Specialfacilities + result.StaffDevelopment + result.StaffInsurance +
                                  result.SupplyTeacherInsurance + result.CommunityFocusedStaff +
                                  result.CommunityFocusedSchoolCosts,
                OtherInsurancePremiumsCosts = result.OtherInsurancePremiums,
                DirectRevenueFinancingCosts = result.DirectRevenue,
                GroundsMaintenanceCosts = result.BuildingGroundsMaintenance,
                IndirectEmployeeExpenses = result.IndirectEmployeeExpenses,
                InterestChargesLoanBank = result.InterestCharges,
                PrivateFinanceInitiativeCharges = result.PFICharges,
                RentRatesCosts = result.RentRates,
                SpecialFacilitiesCosts = result.Specialfacilities,
                StaffDevelopmentTrainingCosts = result.StaffDevelopment,
                StaffRelatedInsuranceCosts = result.StaffInsurance,
                SupplyTeacherInsurableCosts = result.SupplyTeacherInsurance,
                CommunityFocusedSchoolStaff = result.CommunityFocusedStaff,
                CommunityFocusedSchoolCosts = result.CommunityFocusedSchoolCosts
            });
        }

        return new PagedSchoolExpenditure
        {
            Page = page,
            PageSize = pageSize,
            Results = schools,
            TotalResults = schools.Count
        };
    }
}