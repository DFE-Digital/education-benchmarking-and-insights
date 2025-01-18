using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Platform.Api.Insight.Features.Income.Models;
using Platform.Api.Insight.Features.Income.Responses;
using Platform.Api.Insight.Shared;

namespace Platform.Api.Insight.Features.Income;

//TODO: Add unit test coverage for mapper
[ExcludeFromCodeCoverage]
public static class Mapper
{
    public static IncomeSchoolResponse MapToApiResponse(this IncomeSchoolModel model)
    {
        if (model == null)
            throw new ArgumentNullException(nameof(model), "Model cannot be null.");

        return new IncomeSchoolResponse
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

    public static IncomeHistoryResponse MapToApiResponse(this YearsModel years, IEnumerable<IncomeHistoryModel> models)
    {
        return new IncomeHistoryResponse
        {
            StartYear = years.StartYear,
            EndYear = years.EndYear,
            Rows = models.Select(x => x.MapToApiResponse())
        };
    }

    private static IncomeHistoryRowResponse MapToApiResponse(this IncomeHistoryModel model)
    {
        if (model == null)
            throw new ArgumentNullException(nameof(model), "Model cannot be null.");

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