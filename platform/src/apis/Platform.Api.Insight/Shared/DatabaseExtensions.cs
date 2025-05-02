using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Platform.Sql;
using Platform.Sql.QueryBuilders;

namespace Platform.Api.Insight.Shared;

[ExcludeFromCodeCoverage]
public static class DatabaseExtensions
{
    public static async Task<YearsModel?> QueryYearsSchoolAsync(
        this IDatabaseConnection conn,
        string urn,
        CancellationToken cancellationToken = default)
    {
        var builder = new YearsSchoolQuery(urn);
        return await conn.QueryFirstOrDefaultAsync<YearsModel>(builder, cancellationToken);
    }

    public static async Task<YearsModel?> QueryYearsTrustAsync(
        this IDatabaseConnection conn,
        string companyNumber,
        CancellationToken cancellationToken = default)
    {
        var builder = new YearsTrustQuery(companyNumber);
        return await conn.QueryFirstOrDefaultAsync<YearsModel>(builder, cancellationToken);
    }

    public static async Task<YearsModel?> QueryYearsOverallPhaseAsync(
        this IDatabaseConnection conn,
        string overallPhase,
        string financeType,
        CancellationToken cancellationToken = default)
    {
        var builder = new YearsOverallPhaseQuery(overallPhase, financeType);
        return await conn.QueryFirstOrDefaultAsync<YearsModel>(builder, cancellationToken);
    }
}