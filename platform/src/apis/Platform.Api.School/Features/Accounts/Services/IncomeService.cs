using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Platform.Api.School.Features.Accounts.Models;
using Platform.Domain;
using Platform.Sql;
using Platform.Sql.QueryBuilders;

namespace Platform.Api.School.Features.Accounts.Services;

public interface IIncomeService
{
    Task<IncomeModelDto?> GetAsync(string urn, CancellationToken cancellationToken = default);
    Task<(YearsModelDto? years, IEnumerable<IncomeHistoryModelDto> rows)> GetHistoryAsync(string urn, string dimension, CancellationToken cancellationToken = default);
    Task<(YearsModelDto? years, IEnumerable<IncomeHistoryModelDto> rows)> GetComparatorAveHistoryAsync(string urn, string dimension, CancellationToken cancellationToken = default);
}

[ExcludeFromCodeCoverage]
public class IncomeService(IDatabaseFactory dbFactory) : IIncomeService
{
    public async Task<IncomeModelDto?> GetAsync(string urn, CancellationToken cancellationToken = default)
    {
        var builder = new IncomeSchoolDefaultCurrentQuery(Dimensions.Finance.Actuals)
            .WhereUrnEqual(urn);

        using var conn = await dbFactory.GetConnection();
        return await conn.QueryFirstOrDefaultAsync<IncomeModelDto>(builder, cancellationToken);
    }

    public async Task<(YearsModelDto?, IEnumerable<IncomeHistoryModelDto>)> GetHistoryAsync(string urn, string dimension, CancellationToken cancellationToken = default)
    {
        using var conn = await dbFactory.GetConnection();
        var years = await QueryYearsSchoolAsync(conn, urn, cancellationToken);

        if (years == null)
        {
            return (null, []);
        }

        var historyBuilder = new IncomeSchoolDefaultQuery(dimension)
            .WhereUrnEqual(urn)
            .WhereRunIdBetween(years.StartYear, years.EndYear);

        return (years, await conn.QueryAsync<IncomeHistoryModelDto>(historyBuilder, cancellationToken));
    }

    public async Task<(YearsModelDto?, IEnumerable<IncomeHistoryModelDto>)> GetComparatorAveHistoryAsync(string urn, string dimension, CancellationToken cancellationToken = default)
    {
        using var conn = await dbFactory.GetConnection();
        var years = await QueryYearsSchoolAsync(conn, urn, cancellationToken);

        if (years == null)
        {
            return (null, []);
        }

        var historyBuilder = new IncomeSchoolDefaultComparatorAvgQuery(dimension)
            .WhereUrnEqual(urn)
            .WhereRunIdBetween(years.StartYear, years.EndYear);

        return (years, await conn.QueryAsync<IncomeHistoryModelDto>(historyBuilder, cancellationToken));
    }

    private static async Task<YearsModelDto?> QueryYearsSchoolAsync(IDatabaseConnection conn, string urn, CancellationToken cancellationToken = default)
    {
        var builder = new YearsSchoolQuery(urn);
        return await conn.QueryFirstOrDefaultAsync<YearsModelDto>(builder, cancellationToken);
    }
}