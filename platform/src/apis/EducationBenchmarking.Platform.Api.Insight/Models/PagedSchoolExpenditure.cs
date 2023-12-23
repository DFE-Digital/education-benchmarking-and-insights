using System.Collections.Generic;
using EducationBenchmarking.Platform.Infrastructure.Cosmos;
using EducationBenchmarking.Platform.Shared;

namespace EducationBenchmarking.Platform.Api.Insight.Models;

public class PagedSchoolExpenditure : PagedResults<SchoolExpenditure>
{
        public static PagedSchoolExpenditure Create(IEnumerable<SchoolTrustFinancialDataObject> results, int page, int pageSize)
    {
        var schools = new List<SchoolExpenditure>();
        
        foreach (var result in results)
        {
            schools.Add(new SchoolExpenditure
            {
                Urn = result.URN.ToString(),
                Name = result.SchoolName,
                NumberOfPupils = result.NoPupils,
                TotalExpenditure = result.TotalExpenditure,
                TotalIncome = result.TotalIncome,
                TotalTeachingSupportStaffCosts = result.TeachingStaff + result.SupplyTeachingStaff + result.EducationalConsultancy + result.EducationSupportStaff + result.AgencyTeachingStaff,
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
                ProfessionalServicesNonCurriculumCosts = result.BroughtProfessionalServices
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