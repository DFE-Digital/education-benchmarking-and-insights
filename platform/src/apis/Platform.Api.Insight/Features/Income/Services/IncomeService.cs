using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Platform.Api.Insight.Features.Income.Models;
using Platform.Api.Insight.Shared;
using Platform.Domain;
using Platform.Sql;
using Platform.Sql.QueryBuilders;

namespace Platform.Api.Insight.Features.Income.Services;

public interface IIncomeService
{
    Task<IncomeSchoolModel?> GetSchoolAsync(string urn);
    Task<(YearsModel? years, IEnumerable<IncomeHistoryModel> rows)> GetSchoolHistoryAsync(string urn, string dimension);
    Task<(YearsModel?, IEnumerable<IncomeHistoryModel>)> GetTrustHistoryAsync(string companyNumber, string dimension);
}

[ExcludeFromCodeCoverage]
public class IncomeService(IDatabaseFactory dbFactory) : IIncomeService
{
    public async Task<IncomeSchoolModel?> GetSchoolAsync(string urn)
    {
        var builder = new IncomeSchoolDefaultCurrentQuery(Dimensions.Finance.Actuals)
            .WhereUrnEqual(urn);

        using var conn = await dbFactory.GetConnection();
        return await conn.QueryFirstOrDefaultAsync<IncomeSchoolModel>(builder);
    }

    public async Task<(YearsModel?, IEnumerable<IncomeHistoryModel>)> GetSchoolHistoryAsync(string urn, string dimension)
    {
        using var conn = await dbFactory.GetConnection();
        var years = await conn.QueryYearsSchoolAsync(urn);

        if (years == null)
        {
            return (null, Array.Empty<IncomeHistoryModel>());
        }

        var historyBuilder = new IncomeSchoolDefaultQuery(dimension)
            .WhereUrnEqual(urn)
            .WhereRunIdBetween(years.StartYear, years.EndYear);

        return (years, await conn.QueryAsync<IncomeHistoryModel>(historyBuilder));
    }

    public async Task<(YearsModel?, IEnumerable<IncomeHistoryModel>)> GetTrustHistoryAsync(string companyNumber, string dimension)
    {
        using var conn = await dbFactory.GetConnection();
        var years = await conn.QueryYearsTrustAsync(companyNumber);

        if (years == null)
        {
            return (null, Array.Empty<IncomeHistoryModel>());
        }

        var historyBuilder = new IncomeTrustDefaultQuery(dimension)
            .WhereCompanyNumberEqual(companyNumber)
            .WhereRunIdBetween(years.StartYear, years.EndYear);

        return (years, await conn.QueryAsync<IncomeHistoryModel>(historyBuilder));
    }
}