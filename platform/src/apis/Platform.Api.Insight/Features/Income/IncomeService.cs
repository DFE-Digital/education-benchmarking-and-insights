using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Platform.Sql;
using Platform.Sql.QueryBuilders;

namespace Platform.Api.Insight.Income;

public interface IIncomeService
{
    Task<IncomeSchoolModel?> GetSchoolAsync(string urn);
    Task<(IncomeYearsModel? years, IEnumerable<IncomeHistoryModel> rows)> GetSchoolHistoryAsync(string urn, string dimension = IncomeDimensions.Actuals);
    Task<(IncomeYearsModel?, IEnumerable<IncomeHistoryModel>)> GetTrustHistoryAsync(string companyNumber, string dimension = IncomeDimensions.Actuals);
}

public class IncomeService(IDatabaseFactory dbFactory) : IIncomeService
{
    public async Task<IncomeSchoolModel?> GetSchoolAsync(string urn)
    {
        var builder = new IncomeSchoolDefaultCurrentQuery(IncomeDimensions.Actuals)
            .WhereUrnEqual(urn);

        using var conn = await dbFactory.GetConnection();
        return await conn.QueryFirstOrDefaultAsync<IncomeSchoolModel>(builder);
    }

    public async Task<(IncomeYearsModel?, IEnumerable<IncomeHistoryModel>)> GetSchoolHistoryAsync(string urn, string dimension = IncomeDimensions.Actuals)
    {
        using var conn = await dbFactory.GetConnection();
        var yearsBuilder = new YearsSchoolQuery(urn);
        var years = await conn.QueryFirstOrDefaultAsync<IncomeYearsModel>(yearsBuilder);

        if (years == null)
        {
            return (null, Array.Empty<IncomeHistoryModel>());
        }

        var historyBuilder = new IncomeSchoolDefaultQuery(dimension)
            .WhereUrnEqual(urn)
            .WhereRunIdBetween(years.StartYear, years.EndYear);

        return (years, await conn.QueryAsync<IncomeHistoryModel>(historyBuilder));
    }

    public async Task<(IncomeYearsModel?, IEnumerable<IncomeHistoryModel>)> GetTrustHistoryAsync(string companyNumber, string dimension = IncomeDimensions.Actuals)
    {
        using var conn = await dbFactory.GetConnection();
        var yearsBuilder = new YearsTrustQuery(companyNumber);
        var years = await conn.QueryFirstOrDefaultAsync<IncomeYearsModel>(yearsBuilder);

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