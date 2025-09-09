using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Platform.Api.Insight.Features.Balance.Models;
using Platform.Api.Insight.Features.Balance.Responses;
using Platform.Api.Insight.Shared;

namespace Platform.Api.Insight.Features.Balance;

//TODO: Consider adding unit test coverage for mapper
[ExcludeFromCodeCoverage]
public static class Mapper
{
    public static BalanceSchoolResponse MapToApiResponse(this BalanceSchoolModel model)
    {
        if (model == null)
        {
            throw new ArgumentNullException(nameof(model), "Model cannot be null.");
        }

        return new BalanceSchoolResponse
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

    public static BalanceHistoryResponse MapToApiResponse(this YearsModel years, IEnumerable<BalanceHistoryModel> models)
    {
        return new BalanceHistoryResponse
        {
            StartYear = years.StartYear,
            EndYear = years.EndYear,
            Rows = models.Select(x => x.MapToApiResponse())
        };
    }

    public static BalanceTrustResponse MapToApiResponse(this BalanceTrustModel model)
    {
        if (model == null)
        {
            throw new ArgumentNullException(nameof(model), "Model cannot be null.");
        }

        return new BalanceTrustResponse
        {
            TrustName = model.TrustName,
            CompanyNumber = model.CompanyNumber,
            InYearBalance = model.InYearBalance,
            CentralInYearBalance = model.InYearBalanceCS,
            SchoolInYearBalance = model.InYearBalanceSchool,
            RevenueReserve = model.RevenueReserve
        };
    }

    public static IEnumerable<BalanceTrustResponse> MapToApiResponse(this IEnumerable<BalanceTrustModel> models)
    {
        return models.Select(x => x.MapToApiResponse());
    }

    private static BalanceHistoryRowResponse MapToApiResponse(this BalanceHistoryModel model)
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
}