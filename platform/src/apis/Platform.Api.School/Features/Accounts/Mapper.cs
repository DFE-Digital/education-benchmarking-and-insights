using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Platform.Api.School.Features.Accounts.Models;

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