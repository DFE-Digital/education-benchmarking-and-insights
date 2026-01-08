using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Platform.Api.Trust.Features.Accounts.Models;
using Platform.Domain;

namespace Platform.Api.Trust.Features.Accounts;

public static class Mapper
{
    public static IncomeHistoryResponse MapToApiResponse(this YearsModelDto years, IEnumerable<IncomeHistoryModelDto> models)
    {
        return new IncomeHistoryResponse
        {
            StartYear = years.StartYear,
            EndYear = years.EndYear,
            Rows = models.Select(x => x.MapToApiResponse())
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

    public static BalanceResponse MapToApiResponse(this BalanceModelDto model)
    {
        if (model == null)
        {
            throw new ArgumentNullException(nameof(model), "Model cannot be null.");
        }

        return new BalanceResponse
        {
            TrustName = model.TrustName,
            CompanyNumber = model.CompanyNumber,
            InYearBalance = model.InYearBalance,
            CentralInYearBalance = model.InYearBalanceCS,
            SchoolInYearBalance = model.InYearBalanceSchool,
            RevenueReserve = model.RevenueReserve
        };
    }

    public static IEnumerable<BalanceResponse> MapToApiResponse(this IEnumerable<BalanceModelDto> models)
    {
        return models.Select(x => x.MapToApiResponse());
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

    public static IEnumerable<ExpenditureResponse> MapToApiResponse(this IEnumerable<ExpenditureModelDto> models, string? category = null, bool excludeCentralService = false)
    {
        return models.Select(m => MapToApiResponse(m, category, excludeCentralService));
    }

    public static ExpenditureResponse MapToApiResponse(this ExpenditureModelDto model, string? category = null, bool excludeCentralService = false)
    {
        if (model == null)
        {
            throw new ArgumentNullException(nameof(model), "Model cannot be null.");
        }

        var response = new ExpenditureResponse
        {
            TrustName = model.TrustName,
            CompanyNumber = model.CompanyNumber,
            TotalExpenditure = excludeCentralService ? model.TotalExpenditureSchool : model.TotalExpenditure,
            CentralTotalExpenditure = excludeCentralService ? null : model.TotalExpenditureCS,
            SchoolTotalExpenditure = excludeCentralService ? null : model.TotalExpenditureSchool
        };

        if (ShouldDisplay(category, Categories.Cost.TeachingTeachingSupportStaff))
        {
            response.TotalTeachingSupportStaffCosts = excludeCentralService ? model.TotalTeachingSupportStaffCostsSchool : model.TotalTeachingSupportStaffCosts;
            response.TeachingStaffCosts = excludeCentralService ? model.TeachingStaffCostsSchool : model.TeachingStaffCosts;
            response.SupplyTeachingStaffCosts = excludeCentralService ? model.SupplyTeachingStaffCostsSchool : model.SupplyTeachingStaffCosts;
            response.EducationalConsultancyCosts = excludeCentralService ? model.EducationalConsultancyCostsSchool : model.EducationalConsultancyCosts;
            response.EducationSupportStaffCosts = excludeCentralService ? model.EducationSupportStaffCostsSchool : model.EducationSupportStaffCosts;
            response.AgencySupplyTeachingStaffCosts = excludeCentralService ? model.AgencySupplyTeachingStaffCostsSchool : model.AgencySupplyTeachingStaffCosts;

            response.CentralTotalTeachingSupportStaffCosts = excludeCentralService ? null : model.TotalTeachingSupportStaffCostsCS;
            response.CentralTeachingStaffCosts = excludeCentralService ? null : model.TeachingStaffCostsCS;
            response.CentralSupplyTeachingStaffCosts = excludeCentralService ? null : model.SupplyTeachingStaffCostsCS;
            response.CentralEducationalConsultancyCosts = excludeCentralService ? null : model.EducationalConsultancyCostsCS;
            response.CentralEducationSupportStaffCosts = excludeCentralService ? null : model.EducationSupportStaffCostsCS;
            response.CentralAgencySupplyTeachingStaffCosts = excludeCentralService ? null : model.AgencySupplyTeachingStaffCostsCS;

            response.SchoolTotalTeachingSupportStaffCosts = excludeCentralService ? null : model.TotalTeachingSupportStaffCostsSchool;
            response.SchoolTeachingStaffCosts = excludeCentralService ? null : model.TeachingStaffCostsSchool;
            response.SchoolSupplyTeachingStaffCosts = excludeCentralService ? null : model.SupplyTeachingStaffCostsSchool;
            response.SchoolEducationalConsultancyCosts = excludeCentralService ? null : model.EducationalConsultancyCostsSchool;
            response.SchoolEducationSupportStaffCosts = excludeCentralService ? null : model.EducationSupportStaffCostsSchool;
            response.SchoolAgencySupplyTeachingStaffCosts = excludeCentralService ? null : model.AgencySupplyTeachingStaffCostsSchool;
        }

        if (ShouldDisplay(category, Categories.Cost.NonEducationalSupportStaff))
        {
            response.TotalNonEducationalSupportStaffCosts = excludeCentralService ? model.TotalNonEducationalSupportStaffCostsSchool : model.TotalNonEducationalSupportStaffCosts;
            response.AdministrativeClericalStaffCosts = excludeCentralService ? model.AdministrativeClericalStaffCostsSchool : model.AdministrativeClericalStaffCosts;
            response.AuditorsCosts = excludeCentralService ? model.AuditorsCostsSchool : model.AuditorsCosts;
            response.OtherStaffCosts = excludeCentralService ? model.OtherStaffCostsSchool : model.OtherStaffCosts;
            response.ProfessionalServicesNonCurriculumCosts = excludeCentralService ? model.ProfessionalServicesNonCurriculumCostsSchool : model.ProfessionalServicesNonCurriculumCosts;

            response.CentralTotalNonEducationalSupportStaffCosts = excludeCentralService ? null : model.TotalNonEducationalSupportStaffCostsCS;
            response.CentralAdministrativeClericalStaffCosts = excludeCentralService ? null : model.AdministrativeClericalStaffCostsCS;
            response.CentralAuditorsCosts = excludeCentralService ? null : model.AuditorsCostsCS;
            response.CentralOtherStaffCosts = excludeCentralService ? null : model.OtherStaffCostsCS;
            response.CentralProfessionalServicesNonCurriculumCosts = excludeCentralService ? null : model.ProfessionalServicesNonCurriculumCostsCS;

            response.SchoolTotalNonEducationalSupportStaffCosts = excludeCentralService ? null : model.TotalNonEducationalSupportStaffCostsSchool;
            response.SchoolAdministrativeClericalStaffCosts = excludeCentralService ? null : model.AdministrativeClericalStaffCostsSchool;
            response.SchoolAuditorsCosts = excludeCentralService ? null : model.AuditorsCostsSchool;
            response.SchoolOtherStaffCosts = excludeCentralService ? null : model.OtherStaffCostsSchool;
            response.SchoolProfessionalServicesNonCurriculumCosts = excludeCentralService ? null : model.ProfessionalServicesNonCurriculumCostsSchool;

            // attempt to parse value `###` from string of the format `EMLBANDS###` as an int
            const string emlBandsPrefix = "EMLBANDS";
            if (!string.IsNullOrWhiteSpace(model.EmlBand)
                && model.EmlBand.Length > emlBandsPrefix.Length
                && int.TryParse(model.EmlBand[emlBandsPrefix.Length..], out var bandValue))
            {
                response.HighestSalaryEmolumentBandValue = bandValue;
            }

            response.TotalPupils = model.TotalPupils;
        }

        if (ShouldDisplay(category, Categories.Cost.EducationalSupplies))
        {
            response.TotalEducationalSuppliesCosts = excludeCentralService ? model.TotalEducationalSuppliesCostsSchool : model.TotalEducationalSuppliesCosts;
            response.ExaminationFeesCosts = excludeCentralService ? model.ExaminationFeesCostsSchool : model.ExaminationFeesCosts;
            response.LearningResourcesNonIctCosts = excludeCentralService ? model.LearningResourcesNonIctCostsSchool : model.LearningResourcesNonIctCosts;

            response.CentralTotalEducationalSuppliesCosts = excludeCentralService ? null : model.TotalEducationalSuppliesCostsCS;
            response.CentralExaminationFeesCosts = excludeCentralService ? null : model.ExaminationFeesCostsCS;
            response.CentralLearningResourcesNonIctCosts = excludeCentralService ? null : model.LearningResourcesNonIctCostsCS;

            response.SchoolTotalEducationalSuppliesCosts = excludeCentralService ? null : model.TotalEducationalSuppliesCostsSchool;
            response.SchoolExaminationFeesCosts = excludeCentralService ? null : model.ExaminationFeesCostsSchool;
            response.SchoolLearningResourcesNonIctCosts = excludeCentralService ? null : model.LearningResourcesNonIctCostsSchool;
        }

        if (ShouldDisplay(category, Categories.Cost.EducationalIct))
        {
            response.LearningResourcesIctCosts = excludeCentralService ? model.LearningResourcesIctCostsSchool : model.LearningResourcesIctCosts;

            response.CentralLearningResourcesIctCosts = excludeCentralService ? null : model.LearningResourcesIctCostsCS;

            response.SchoolLearningResourcesIctCosts = excludeCentralService ? null : model.LearningResourcesIctCostsSchool;
        }

        if (ShouldDisplay(category, Categories.Cost.PremisesStaffServices))
        {
            response.TotalPremisesStaffServiceCosts = excludeCentralService ? model.TotalPremisesStaffServiceCostsSchool : model.TotalPremisesStaffServiceCosts;
            response.CleaningCaretakingCosts = excludeCentralService ? model.CleaningCaretakingCostsSchool : model.CleaningCaretakingCosts;
            response.MaintenancePremisesCosts = excludeCentralService ? model.MaintenancePremisesCostsSchool : model.MaintenancePremisesCosts;
            response.OtherOccupationCosts = excludeCentralService ? model.OtherOccupationCostsSchool : model.OtherOccupationCosts;
            response.PremisesStaffCosts = excludeCentralService ? model.PremisesStaffCostsSchool : model.PremisesStaffCosts;

            response.CentralTotalPremisesStaffServiceCosts = excludeCentralService ? null : model.TotalPremisesStaffServiceCostsCS;
            response.CentralCleaningCaretakingCosts = excludeCentralService ? null : model.CleaningCaretakingCostsCS;
            response.CentralMaintenancePremisesCosts = excludeCentralService ? null : model.MaintenancePremisesCostsCS;
            response.CentralOtherOccupationCosts = excludeCentralService ? null : model.OtherOccupationCostsCS;
            response.CentralPremisesStaffCosts = excludeCentralService ? null : model.PremisesStaffCostsCS;

            response.SchoolTotalPremisesStaffServiceCosts = excludeCentralService ? null : model.TotalPremisesStaffServiceCostsSchool;
            response.SchoolCleaningCaretakingCosts = excludeCentralService ? null : model.CleaningCaretakingCostsSchool;
            response.SchoolMaintenancePremisesCosts = excludeCentralService ? null : model.MaintenancePremisesCostsSchool;
            response.SchoolOtherOccupationCosts = excludeCentralService ? null : model.OtherOccupationCostsSchool;
            response.SchoolPremisesStaffCosts = excludeCentralService ? null : model.PremisesStaffCostsSchool;
        }

        if (ShouldDisplay(category, Categories.Cost.Utilities))
        {
            response.TotalUtilitiesCosts = excludeCentralService ? model.TotalUtilitiesCostsSchool : model.TotalUtilitiesCosts;
            response.EnergyCosts = excludeCentralService ? model.EnergyCostsSchool : model.EnergyCosts;
            response.WaterSewerageCosts = excludeCentralService ? model.WaterSewerageCostsSchool : model.WaterSewerageCosts;

            response.CentralTotalUtilitiesCosts = excludeCentralService ? null : model.TotalUtilitiesCostsCS;
            response.CentralEnergyCosts = excludeCentralService ? null : model.EnergyCostsCS;
            response.CentralWaterSewerageCosts = excludeCentralService ? null : model.WaterSewerageCostsCS;

            response.SchoolTotalUtilitiesCosts = excludeCentralService ? null : model.TotalUtilitiesCostsSchool;
            response.SchoolEnergyCosts = excludeCentralService ? null : model.EnergyCostsSchool;
            response.SchoolWaterSewerageCosts = excludeCentralService ? null : model.WaterSewerageCostsSchool;
        }

        if (ShouldDisplay(category, Categories.Cost.AdministrationSupplies))
        {
            response.AdministrativeSuppliesNonEducationalCosts = excludeCentralService ? model.AdministrativeSuppliesNonEducationalCostsSchool : model.AdministrativeSuppliesNonEducationalCosts;

            response.CentralAdministrativeSuppliesNonEducationalCosts = excludeCentralService ? null : model.AdministrativeSuppliesNonEducationalCostsCS;

            response.SchoolAdministrativeSuppliesNonEducationalCosts = excludeCentralService ? null : model.AdministrativeSuppliesNonEducationalCostsSchool;
        }

        if (ShouldDisplay(category, Categories.Cost.CateringStaffServices))
        {
            response.TotalGrossCateringCosts = excludeCentralService ? model.TotalGrossCateringCostsSchool : model.TotalGrossCateringCosts;
            response.TotalNetCateringCosts = excludeCentralService ? model.TotalNetCateringCostsSchool : model.TotalNetCateringCosts;
            response.CateringStaffCosts = excludeCentralService ? model.CateringStaffCostsSchool : model.CateringStaffCosts;
            response.CateringSuppliesCosts = excludeCentralService ? model.CateringSuppliesCostsSchool : model.CateringSuppliesCosts;

            response.CentralTotalGrossCateringCosts = excludeCentralService ? null : model.TotalGrossCateringCostsCS;
            response.CentralTotalNetCateringCosts = excludeCentralService ? null : model.TotalNetCateringCostsCS;
            response.CentralCateringStaffCosts = excludeCentralService ? null : model.CateringStaffCostsCS;
            response.CentralCateringSuppliesCosts = excludeCentralService ? null : model.CateringSuppliesCostsCS;

            response.SchoolTotalGrossCateringCosts = excludeCentralService ? null : model.TotalGrossCateringCostsSchool;
            response.SchoolTotalNetCateringCosts = excludeCentralService ? null : model.TotalNetCateringCostsSchool;
            response.SchoolCateringStaffCosts = excludeCentralService ? null : model.CateringStaffCostsSchool;
            response.SchoolCateringSuppliesCosts = excludeCentralService ? null : model.CateringSuppliesCostsSchool;
        }

        if (ShouldDisplay(category, Categories.Cost.Other))
        {
            response.TotalOtherCosts = excludeCentralService ? model.TotalOtherCostsSchool : model.TotalOtherCosts;
            response.GroundsMaintenanceCosts = excludeCentralService ? model.GroundsMaintenanceCostsSchool : model.GroundsMaintenanceCosts;
            response.IndirectEmployeeExpenses = excludeCentralService ? model.IndirectEmployeeExpensesSchool : model.IndirectEmployeeExpenses;
            response.InterestChargesLoanBank = excludeCentralService ? model.InterestChargesLoanBankSchool : model.InterestChargesLoanBank;
            response.OtherInsurancePremiumsCosts = excludeCentralService ? model.OtherInsurancePremiumsCostsSchool : model.OtherInsurancePremiumsCosts;
            response.PrivateFinanceInitiativeCharges = excludeCentralService ? model.PrivateFinanceInitiativeChargesSchool : model.PrivateFinanceInitiativeCharges;
            response.RentRatesCosts = excludeCentralService ? model.RentRatesCostsSchool : model.RentRatesCosts;
            response.SpecialFacilitiesCosts = excludeCentralService ? model.SpecialFacilitiesCostsSchool : model.SpecialFacilitiesCosts;
            response.StaffDevelopmentTrainingCosts = excludeCentralService ? model.StaffDevelopmentTrainingCostsSchool : model.StaffDevelopmentTrainingCosts;
            response.StaffRelatedInsuranceCosts = excludeCentralService ? model.StaffRelatedInsuranceCostsSchool : model.StaffRelatedInsuranceCosts;
            response.SupplyTeacherInsurableCosts = excludeCentralService ? model.SupplyTeacherInsurableCostsSchool : model.SupplyTeacherInsurableCosts;

            response.CentralTotalOtherCosts = excludeCentralService ? null : model.TotalOtherCostsCS;
            response.CentralGroundsMaintenanceCosts = excludeCentralService ? null : model.GroundsMaintenanceCostsCS;
            response.CentralIndirectEmployeeExpenses = excludeCentralService ? null : model.IndirectEmployeeExpensesCS;
            response.CentralInterestChargesLoanBank = excludeCentralService ? null : model.InterestChargesLoanBankCS;
            response.CentralOtherInsurancePremiumsCosts = excludeCentralService ? null : model.OtherInsurancePremiumsCostsCS;
            response.CentralPrivateFinanceInitiativeCharges = excludeCentralService ? null : model.PrivateFinanceInitiativeChargesCS;
            response.CentralRentRatesCosts = excludeCentralService ? null : model.RentRatesCostsCS;
            response.CentralSpecialFacilitiesCosts = excludeCentralService ? null : model.SpecialFacilitiesCostsCS;
            response.CentralStaffDevelopmentTrainingCosts = excludeCentralService ? null : model.StaffDevelopmentTrainingCostsCS;
            response.CentralStaffRelatedInsuranceCosts = excludeCentralService ? null : model.StaffRelatedInsuranceCostsCS;
            response.CentralSupplyTeacherInsurableCosts = excludeCentralService ? null : model.SupplyTeacherInsurableCostsCS;

            response.SchoolTotalOtherCosts = excludeCentralService ? null : model.TotalOtherCostsSchool;
            response.SchoolGroundsMaintenanceCosts = excludeCentralService ? null : model.GroundsMaintenanceCostsSchool;
            response.SchoolIndirectEmployeeExpenses = excludeCentralService ? null : model.IndirectEmployeeExpensesSchool;
            response.SchoolInterestChargesLoanBank = excludeCentralService ? null : model.InterestChargesLoanBankSchool;
            response.SchoolOtherInsurancePremiumsCosts = excludeCentralService ? null : model.OtherInsurancePremiumsCostsSchool;
            response.SchoolPrivateFinanceInitiativeCharges = excludeCentralService ? null : model.PrivateFinanceInitiativeChargesSchool;
            response.SchoolRentRatesCosts = excludeCentralService ? null : model.RentRatesCostsSchool;
            response.SchoolSpecialFacilitiesCosts = excludeCentralService ? null : model.SpecialFacilitiesCostsSchool;
            response.SchoolStaffDevelopmentTrainingCosts = excludeCentralService ? null : model.StaffDevelopmentTrainingCostsSchool;
            response.SchoolStaffRelatedInsuranceCosts = excludeCentralService ? null : model.StaffRelatedInsuranceCostsSchool;
            response.SchoolSupplyTeacherInsurableCosts = excludeCentralService ? null : model.SupplyTeacherInsurableCostsSchool;
        }

        return response;
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