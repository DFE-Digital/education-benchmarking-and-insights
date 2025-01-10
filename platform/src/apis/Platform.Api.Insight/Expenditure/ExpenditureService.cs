using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Platform.Sql;
using Platform.Sql.QueryBuilders;

namespace Platform.Api.Insight.Expenditure;

public interface IExpenditureService
{
    Task<ExpenditureSchoolModel?> GetSchoolAsync(string urn, string dimension = ExpenditureDimensions.Actuals);
    Task<(ExpenditureYearsModel?, IEnumerable<ExpenditureHistoryModel>)> GetSchoolHistoryAsync(string urn, string dimension = ExpenditureDimensions.Actuals, CancellationToken cancellationToken = default);
    Task<IEnumerable<ExpenditureSchoolModel>> QuerySchoolsAsync(string[] urns, string? companyNumber, string? laCode, string? phase, string dimension = ExpenditureDimensions.Actuals);
    Task<ExpenditureSchoolModel?> GetCustomSchoolAsync(string urn, string identifier, string dimension = ExpenditureDimensions.Actuals);
    Task<ExpenditureTrustModel?> GetTrustAsync(string companyNumber, string dimension = ExpenditureDimensions.Actuals);
    Task<(ExpenditureYearsModel?, IEnumerable<ExpenditureHistoryModel>)> GetTrustHistoryAsync(string companyNumber, string dimension = ExpenditureDimensions.Actuals);
    Task<IEnumerable<ExpenditureTrustModel>> QueryTrustsAsync(string[] companyNumbers, string dimension = ExpenditureDimensions.Actuals);
    Task<(ExpenditureYearsModel?, IEnumerable<ExpenditureHistoryModel>)> GetComparatorAveHistoryAsync(string urn, string dimension = ExpenditureDimensions.Actuals, CancellationToken cancellationToken = default);
    Task<(ExpenditureYearsModel?, IEnumerable<ExpenditureHistoryModel>)> GetNationalAvgHistoryAsync(string overallPhase, string financeType, string dimension = ExpenditureDimensions.Actuals, CancellationToken cancellationToken = default);
}

public class ExpenditureService(IDatabaseFactory dbFactory) : IExpenditureService
{
    public async Task<ExpenditureSchoolModel?> GetSchoolAsync(string urn, string dimension = ExpenditureDimensions.Actuals)
    {
        using var conn = await dbFactory.GetConnection();
        var builder = new ExpenditureSchoolDefaultCurrentQuery(dimension)
            .WhereUrnEqual(urn);

        return await conn.QueryFirstOrDefaultAsync<ExpenditureSchoolModel>(builder);
    }

    public async Task<(ExpenditureYearsModel?, IEnumerable<ExpenditureHistoryModel>)> GetSchoolHistoryAsync(string urn, string dimension = ExpenditureDimensions.Actuals, CancellationToken cancellationToken = default)
    {
        using var conn = await dbFactory.GetConnection();
        var yearsBuilder = new YearsSchoolQuery(urn);
        var years = await conn.QueryFirstOrDefaultAsync<ExpenditureYearsModel>(yearsBuilder, cancellationToken);

        if (years == null)
        {
            return (null, Array.Empty<ExpenditureHistoryModel>());
        }

        var historyBuilder = new ExpenditureSchoolDefaultQuery(dimension)
            .WhereUrnEqual(urn)
            .WhereRunIdBetween(years.StartYear, years.EndYear);

        return (years, await conn.QueryAsync<ExpenditureHistoryModel>(historyBuilder, cancellationToken));
    }

    public async Task<IEnumerable<ExpenditureSchoolModel>> QuerySchoolsAsync(string[] urns, string? companyNumber, string? laCode, string? phase, string dimension = ExpenditureDimensions.Actuals)
    {
        var builder = new ExpenditureSchoolDefaultCurrentQuery(dimension);

        if (urns.Length != 0)
        {
            builder.WhereUrnIn(urns);

        }
        else if (!string.IsNullOrWhiteSpace(companyNumber))
        {
            builder
                .WhereTrustCompanyNumberEqual(companyNumber)
                .WhereOverallPhaseEqual(phase);
        }
        else if (!string.IsNullOrWhiteSpace(laCode))
        {
            builder
                .WhereLaCodeEqual(laCode)
                .WhereOverallPhaseEqual(phase);
        }
        else
        {
            throw new ArgumentNullException(nameof(urns), $"{nameof(urns)} or {nameof(companyNumber)} or {nameof(laCode)} must be supplied");
        }

        using var conn = await dbFactory.GetConnection();
        return await conn.QueryAsync<ExpenditureSchoolModel>(builder);
    }

    public async Task<ExpenditureSchoolModel?> GetCustomSchoolAsync(string urn, string identifier, string dimension = ExpenditureDimensions.Actuals)
    {
        using var conn = await dbFactory.GetConnection();
        var builder = new ExpenditureSchoolCustomQuery(dimension)
            .WhereUrnEqual(urn);

        return await conn.QueryFirstOrDefaultAsync<ExpenditureSchoolModel>(builder);
    }

    public async Task<ExpenditureTrustModel?> GetTrustAsync(string companyNumber, string dimension = ExpenditureDimensions.Actuals)
    {
        using var conn = await dbFactory.GetConnection();
        var builder = new ExpenditureTrustDefaultCurrentQuery(dimension)
            .WhereCompanyNumberEqual(companyNumber);

        return await conn.QueryFirstOrDefaultAsync<ExpenditureTrustModel>(builder);
    }

    public async Task<(ExpenditureYearsModel?, IEnumerable<ExpenditureHistoryModel>)> GetTrustHistoryAsync(string companyNumber, string dimension = ExpenditureDimensions.Actuals)
    {
        using var conn = await dbFactory.GetConnection();
        var yearsBuilder = new YearsTrustQuery(companyNumber);
        var years = await conn.QueryFirstOrDefaultAsync<ExpenditureYearsModel>(yearsBuilder);

        if (years == null)
        {
            return (null, Array.Empty<ExpenditureHistoryModel>());
        }

        var historyBuilder = new ExpenditureTrustDefaultQuery(dimension)
            .WhereCompanyNumberEqual(companyNumber)
            .WhereRunIdBetween(years.StartYear, years.EndYear);

        return (years, await conn.QueryAsync<ExpenditureHistoryModel>(historyBuilder));
    }

    public async Task<IEnumerable<ExpenditureTrustModel>> QueryTrustsAsync(string[] companyNumbers, string dimension = ExpenditureDimensions.Actuals)
    {
        using var conn = await dbFactory.GetConnection();
        var builder = new ExpenditureTrustDefaultCurrentQuery(dimension)
            .WhereCompanyNumberIn(companyNumbers);

        return await conn.QueryAsync<ExpenditureTrustModel>(builder);
    }

    public async Task<(ExpenditureYearsModel?, IEnumerable<ExpenditureHistoryModel>)> GetComparatorAveHistoryAsync(string urn, string dimension = ExpenditureDimensions.Actuals, CancellationToken cancellationToken = default)
    {
        using var conn = await dbFactory.GetConnection();
        var yearsBuilder = new YearsSchoolQuery(urn);
        var years = await conn.QueryFirstOrDefaultAsync<ExpenditureYearsModel>(yearsBuilder, cancellationToken);

        if (years == null)
        {
            return (null, Array.Empty<ExpenditureHistoryModel>());
        }

        var historyBuilder = new ExpenditureSchoolDefaultComparatorAvgQuery(dimension)
            .WhereUrnEqual(urn)
            .WhereRunIdBetween(years.StartYear, years.EndYear);

        return (years, await conn.QueryAsync<ExpenditureHistoryModel>(historyBuilder, cancellationToken));
    }

    public async Task<(ExpenditureYearsModel?, IEnumerable<ExpenditureHistoryModel>)> GetNationalAvgHistoryAsync(string overallPhase, string financeType, string dimension = ExpenditureDimensions.Actuals, CancellationToken cancellationToken = default)
    {
        using var conn = await dbFactory.GetConnection();
        var yearsBuilder = new YearsOverallPhaseQuery(overallPhase, financeType);
        var years = await conn.QueryFirstOrDefaultAsync<ExpenditureYearsModel>(yearsBuilder, cancellationToken);

        if (years == null)
        {
            return (null, Array.Empty<ExpenditureHistoryModel>());
        }

        var historyBuilder = new ExpenditureSchoolDefaultNationalAveQuery(dimension)
            .WhereOverallPhaseEqual(overallPhase)
            .WhereFinanceTypeEqual(financeType)
            .WhereRunIdBetween(years.StartYear, years.EndYear);

        return (years, await conn.QueryAsync<ExpenditureHistoryModel>(historyBuilder, cancellationToken));
    }
}