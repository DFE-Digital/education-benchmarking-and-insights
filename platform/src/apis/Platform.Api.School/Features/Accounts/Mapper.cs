using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Platform.Api.School.Features.Accounts.Models;
using Platform.Domain;

namespace Platform.Api.School.Features.Accounts;

//TODO: Add unit test coverage for mapper
[ExcludeFromCodeCoverage]
public static class Mapper
{
    public static IncomeResponse MapToApiResponse(this IncomeModelDto model)
    {
        if (model == null)
        {
            throw new ArgumentNullException(nameof(model), "Model cannot be null.");
        }

        return new IncomeResponse
        {
            URN = model.URN,
            SchoolName = model.SchoolName,
            SchoolType = model.SchoolType,
            LAName = model.LAName,
            PeriodCoveredByReturn = model.PeriodCoveredByReturn,
            TotalIncome = model.TotalIncome,
            TotalGrantFunding = model.TotalGrantFunding,
            TotalSelfGeneratedFunding = model.TotalSelfGeneratedFunding,
            DirectRevenueFinancing = model.DirectRevenueFinancing,
            DirectGrants = model.DirectGrants,
            PrePost16Funding = model.PrePost16Funding,
            OtherDfeGrants = model.OtherDfeGrants,
            OtherIncomeGrants = model.OtherIncomeGrants,
            GovernmentSource = model.GovernmentSource,
            CommunityGrants = model.CommunityGrants,
            Academies = model.Academies,
            IncomeFacilitiesServices = model.IncomeFacilitiesServices,
            IncomeCatering = model.IncomeCateringServices,
            DonationsVoluntaryFunds = model.DonationsVoluntaryFunds,
            ReceiptsSupplyTeacherInsuranceClaims = model.ReceiptsSupplyTeacherInsuranceClaims,
            InvestmentIncome = model.InvestmentIncome,
            OtherSelfGeneratedIncome = model.OtherSelfGeneratedIncome
        };
    }

    public static IncomeHistoryResponse MapToApiResponse(this YearsModelDto years, IEnumerable<IncomeHistoryModelDto> models)
    {
        return new IncomeHistoryResponse
        {
            StartYear = years.StartYear,
            EndYear = years.EndYear,
            Rows = models.Select(x => x.MapToApiResponse())
        };
    }

    public static BalanceResponse MapToApiResponse(this BalanceModelDto model)
    {
        if (model == null)
        {
            throw new ArgumentNullException(nameof(model), "Model cannot be null.");
        }

        return new BalanceResponse
        {
            URN = model.URN,
            SchoolName = model.SchoolName,
            SchoolType = model.SchoolType,
            LAName = model.LAName,
            PeriodCoveredByReturn = model.PeriodCoveredByReturn,
            InYearBalance = model.InYearBalance,
            RevenueReserve = model.RevenueReserve
        };
    }

    public static BalanceHistoryResponse MapToApiResponse(this YearsModelDto years, IEnumerable<BalanceHistoryModelDto> models)
    {
        return new BalanceHistoryResponse
        {
            StartYear = years.StartYear,
            EndYear = years.EndYear,
            Rows = models.Select(x => x.MapToApiResponse())
        };
    }

    public static ExpenditureResponse MapToApiResponse(this ExpenditureModelDto model, string? category = null)
    {
        if (model == null)
        {
            throw new ArgumentNullException(nameof(model), "Model cannot be null.");
        }

        var response = new ExpenditureResponse
        {
            URN = model.URN,
            SchoolName = model.SchoolName,
            SchoolType = model.SchoolType,
            LAName = model.LAName,
            TotalPupils = model.TotalPupils,
            PeriodCoveredByReturn = model.PeriodCoveredByReturn,
            TotalInternalFloorArea = model.TotalInternalFloorArea,
            TotalExpenditure = model.TotalExpenditure
        };

        if (ShouldDisplay(category, Categories.Cost.TeachingTeachingSupportStaff))
        {
            response.TotalTeachingSupportStaffCosts = model.TotalTeachingSupportStaffCosts;
            response.TeachingStaffCosts = model.TeachingStaffCosts;
            response.SupplyTeachingStaffCosts = model.SupplyTeachingStaffCosts;
            response.EducationalConsultancyCosts = model.EducationalConsultancyCosts;
            response.EducationSupportStaffCosts = model.EducationSupportStaffCosts;
            response.AgencySupplyTeachingStaffCosts = model.AgencySupplyTeachingStaffCosts;
        }

        if (ShouldDisplay(category, Categories.Cost.NonEducationalSupportStaff))
        {
            response.TotalNonEducationalSupportStaffCosts = model.TotalNonEducationalSupportStaffCosts;
            response.AdministrativeClericalStaffCosts = model.AdministrativeClericalStaffCosts;
            response.AuditorsCosts = model.AuditorsCosts;
            response.OtherStaffCosts = model.OtherStaffCosts;
            response.ProfessionalServicesNonCurriculumCosts = model.ProfessionalServicesNonCurriculumCosts;
        }

        if (ShouldDisplay(category, Categories.Cost.EducationalSupplies))
        {
            response.TotalEducationalSuppliesCosts = model.TotalEducationalSuppliesCosts;
            response.ExaminationFeesCosts = model.ExaminationFeesCosts;
            response.LearningResourcesNonIctCosts = model.LearningResourcesNonIctCosts;
        }

        if (ShouldDisplay(category, Categories.Cost.EducationalIct))
        {
            response.LearningResourcesIctCosts = model.LearningResourcesIctCosts;
        }

        if (ShouldDisplay(category, Categories.Cost.PremisesStaffServices))
        {
            response.TotalPremisesStaffServiceCosts = model.TotalPremisesStaffServiceCosts;
            response.CleaningCaretakingCosts = model.CleaningCaretakingCosts;
            response.MaintenancePremisesCosts = model.MaintenancePremisesCosts;
            response.OtherOccupationCosts = model.OtherOccupationCosts;
            response.PremisesStaffCosts = model.PremisesStaffCosts;
        }

        if (ShouldDisplay(category, Categories.Cost.Utilities))
        {
            response.TotalUtilitiesCosts = model.TotalUtilitiesCosts;
            response.EnergyCosts = model.EnergyCosts;
            response.WaterSewerageCosts = model.WaterSewerageCosts;
        }

        if (ShouldDisplay(category, Categories.Cost.AdministrationSupplies))
        {
            response.AdministrativeSuppliesNonEducationalCosts = model.AdministrativeSuppliesNonEducationalCosts;
        }

        if (ShouldDisplay(category, Categories.Cost.CateringStaffServices))
        {
            response.TotalGrossCateringCosts = model.TotalGrossCateringCosts;
            response.TotalNetCateringCosts = model.TotalNetCateringCosts;
            response.CateringStaffCosts = model.CateringStaffCosts;
            response.CateringSuppliesCosts = model.CateringSuppliesCosts;
        }

        if (ShouldDisplay(category, Categories.Cost.Other))
        {
            response.TotalOtherCosts = model.TotalOtherCosts;
            response.GroundsMaintenanceCosts = model.GroundsMaintenanceCosts;
            response.IndirectEmployeeExpenses = model.IndirectEmployeeExpenses;
            response.InterestChargesLoanBank = model.InterestChargesLoanBank;
            response.OtherInsurancePremiumsCosts = model.OtherInsurancePremiumsCosts;
            response.PrivateFinanceInitiativeCharges = model.PrivateFinanceInitiativeCharges;
            response.RentRatesCosts = model.RentRatesCosts;
            response.SpecialFacilitiesCosts = model.SpecialFacilitiesCosts;
            response.StaffDevelopmentTrainingCosts = model.StaffDevelopmentTrainingCosts;
            response.StaffRelatedInsuranceCosts = model.StaffRelatedInsuranceCosts;
            response.SupplyTeacherInsurableCosts = model.SupplyTeacherInsurableCosts;
            response.CommunityFocusedSchoolStaff = model.CommunityFocusedSchoolStaff;
            response.CommunityFocusedSchoolCosts = model.CommunityFocusedSchoolCosts;
        }

        return response;
    }

    public static IEnumerable<ExpenditureResponse> MapToApiResponse(this IEnumerable<ExpenditureModelDto> models, string? category = null)
    {
        return models.Select(m => MapToApiResponse(m, category));
    }

    public static ExpenditureHistoryResponse MapToApiResponse(this YearsModelDto years, IEnumerable<ExpenditureHistoryModelDto> models)
    {
        return new ExpenditureHistoryResponse
        {
            StartYear = years.StartYear,
            EndYear = years.EndYear,
            Rows = models.Select(x => x.MapToApiResponse())
        };
    }
    private static ExpenditureHistoryRowResponse MapToApiResponse(this ExpenditureHistoryModelDto model)
    {
        if (model == null)
        {
            throw new ArgumentNullException(nameof(model), "Model cannot be null.");
        }

        return new ExpenditureHistoryRowResponse
        {
            Year = model.RunId,
            TotalExpenditure = model.TotalExpenditure,
            TotalTeachingSupportStaffCosts = model.TotalTeachingSupportStaffCosts,
            TeachingStaffCosts = model.TeachingStaffCosts,
            SupplyTeachingStaffCosts = model.SupplyTeachingStaffCosts,
            EducationalConsultancyCosts = model.EducationalConsultancyCosts,
            EducationSupportStaffCosts = model.EducationSupportStaffCosts,
            AgencySupplyTeachingStaffCosts = model.AgencySupplyTeachingStaffCosts,
            TotalNonEducationalSupportStaffCosts = model.TotalNonEducationalSupportStaffCosts,
            AdministrativeClericalStaffCosts = model.AdministrativeClericalStaffCosts,
            AuditorsCosts = model.AuditorsCosts,
            OtherStaffCosts = model.OtherStaffCosts,
            ProfessionalServicesNonCurriculumCosts = model.ProfessionalServicesNonCurriculumCosts,
            TotalEducationalSuppliesCosts = model.TotalEducationalSuppliesCosts,
            ExaminationFeesCosts = model.ExaminationFeesCosts,
            LearningResourcesNonIctCosts = model.LearningResourcesNonIctCosts,
            LearningResourcesIctCosts = model.LearningResourcesIctCosts,
            TotalPremisesStaffServiceCosts = model.TotalPremisesStaffServiceCosts,
            CleaningCaretakingCosts = model.CleaningCaretakingCosts,
            MaintenancePremisesCosts = model.MaintenancePremisesCosts,
            OtherOccupationCosts = model.OtherOccupationCosts,
            PremisesStaffCosts = model.PremisesStaffCosts,
            TotalUtilitiesCosts = model.TotalUtilitiesCosts,
            EnergyCosts = model.EnergyCosts,
            WaterSewerageCosts = model.WaterSewerageCosts,
            AdministrativeSuppliesNonEducationalCosts = model.AdministrativeSuppliesNonEducationalCosts,
            TotalGrossCateringCosts = model.TotalGrossCateringCosts,
            TotalNetCateringCosts = model.TotalNetCateringCosts,
            CateringStaffCosts = model.CateringStaffCosts,
            CateringSuppliesCosts = model.CateringSuppliesCosts,
            TotalOtherCosts = model.TotalOtherCosts,
            GroundsMaintenanceCosts = model.GroundsMaintenanceCosts,
            IndirectEmployeeExpenses = model.IndirectEmployeeExpenses,
            InterestChargesLoanBank = model.InterestChargesLoanBank,
            OtherInsurancePremiumsCosts = model.OtherInsurancePremiumsCosts,
            PrivateFinanceInitiativeCharges = model.PrivateFinanceInitiativeCharges,
            RentRatesCosts = model.RentRatesCosts,
            SpecialFacilitiesCosts = model.SpecialFacilitiesCosts,
            StaffDevelopmentTrainingCosts = model.StaffDevelopmentTrainingCosts,
            StaffRelatedInsuranceCosts = model.StaffRelatedInsuranceCosts,
            SupplyTeacherInsurableCosts = model.SupplyTeacherInsurableCosts,
            CommunityFocusedSchoolStaff = model.CommunityFocusedSchoolStaff,
            CommunityFocusedSchoolCosts = model.CommunityFocusedSchoolCosts
        };
    }

    private static bool ShouldDisplay(string? category, string match) => string.IsNullOrEmpty(category) || category == match;

    private static BalanceHistoryRowResponse MapToApiResponse(this BalanceHistoryModelDto model)
    {
        if (model == null)
        {
            throw new ArgumentNullException(nameof(model), "Model cannot be null.");
        }

        return new BalanceHistoryRowResponse
        {
            Year = model.RunId,
            InYearBalance = model.InYearBalance,
            RevenueReserve = model.RevenueReserve
        };
    }

    private static IncomeHistoryRowResponse MapToApiResponse(this IncomeHistoryModelDto model)
    {
        if (model == null)
        {
            throw new ArgumentNullException(nameof(model), "Model cannot be null.");
        }

        return new IncomeHistoryRowResponse
        {
            Year = model.RunId,
            TotalIncome = model.TotalIncome,
            TotalGrantFunding = model.TotalGrantFunding,
            TotalSelfGeneratedFunding = model.TotalSelfGeneratedFunding,
            DirectRevenueFinancing = model.DirectRevenueFinancing,
            DirectGrants = model.DirectGrants,
            PrePost16Funding = model.PrePost16Funding,
            OtherDfeGrants = model.OtherDfeGrants,
            OtherIncomeGrants = model.OtherIncomeGrants,
            GovernmentSource = model.GovernmentSource,
            CommunityGrants = model.CommunityGrants,
            Academies = model.Academies,
            IncomeFacilitiesServices = model.IncomeFacilitiesServices,
            IncomeCatering = model.IncomeCateringServices,
            DonationsVoluntaryFunds = model.DonationsVoluntaryFunds,
            ReceiptsSupplyTeacherInsuranceClaims = model.ReceiptsSupplyTeacherInsuranceClaims,
            InvestmentIncome = model.InvestmentIncome,
            OtherSelfGeneratedIncome = model.OtherSelfGeneratedIncome
        };
    }
}