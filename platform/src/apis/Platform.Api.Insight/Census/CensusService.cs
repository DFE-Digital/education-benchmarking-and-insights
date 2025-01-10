using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Platform.Sql;
using Platform.Sql.QueryBuilders;

namespace Platform.Api.Insight.Census;

public interface ICensusService
{
    Task<CensusSchoolModel?> GetAsync(string urn, string dimension = CensusDimensions.Total);
    Task<(CensusYearsModel?, IEnumerable<CensusHistoryModel>)> GetSchoolHistoryAsync(string urn, string dimension = CensusDimensions.Total, CancellationToken cancellationToken = default);
    Task<CensusSchoolModel?> GetCustomAsync(string urn, string identifier, string dimension = CensusDimensions.Total);
    Task<IEnumerable<CensusSchoolModel>> QueryAsync(string[] urns, string? companyNumber, string? laCode, string? phase, string dimension = CensusDimensions.Total);
    Task<(CensusYearsModel?, IEnumerable<CensusHistoryModel>)> GetComparatorAveHistoryAsync(string urn, string dimension = CensusDimensions.Total, CancellationToken cancellationToken = default);
    Task<(CensusYearsModel?, IEnumerable<CensusHistoryModel>)> GetNationalAvgHistoryAsync(string overallPhase, string financeType, string dimension = CensusDimensions.Total, CancellationToken cancellationToken = default);
}

public class CensusService(IDatabaseFactory dbFactory) : ICensusService
{
    public async Task<CensusSchoolModel?> GetAsync(string urn, string dimension = CensusDimensions.Total)
    {
        var builder = new CensusSchoolDefaultCurrentQuery(dimension)
            .WhereUrnEqual(urn);
        
        using var conn = await dbFactory.GetConnection();
        return await conn.QueryFirstOrDefaultAsync<CensusSchoolModel>(builder);
    }

    public async Task<(CensusYearsModel?, IEnumerable<CensusHistoryModel>)> GetSchoolHistoryAsync(string urn, string dimension = CensusDimensions.Total, CancellationToken cancellationToken = default)
    {
        using var conn = await dbFactory.GetConnection();
        var yearsBuilder = new YearsSchoolQuery(urn);
        var years = await conn.QueryFirstOrDefaultAsync<CensusYearsModel>(yearsBuilder, cancellationToken);

        if (years == null)
        {
            return (null, Array.Empty<CensusHistoryModel>());
        }
        
        var historyBuilder = new CensusSchoolDefaultQuery(dimension)
            .WhereUrnEqual(urn)
            .WhereRunIdBetween(years.StartYear, years.EndYear);
        
        return (years, await conn.QueryAsync<CensusHistoryModel>(historyBuilder, cancellationToken));
    }

    public async Task<CensusSchoolModel?> GetCustomAsync(string urn, string identifier, string dimension = CensusDimensions.Total)
    {
        var builder = new CensusSchoolCustomQuery(dimension)
            .WhereUrnEqual(urn)
            .WhereRunIdEqual(identifier);
        
        using var conn = await dbFactory.GetConnection();
        return await conn.QueryFirstOrDefaultAsync<CensusSchoolModel>(builder);
    }

    public async Task<IEnumerable<CensusSchoolModel>> QueryAsync(string[] urns, string? companyNumber, string? laCode, string? phase, string dimension = CensusDimensions.Total)
    {
        var builder = new CensusSchoolDefaultCurrentQuery(dimension);
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
        return await conn.QueryAsync<CensusSchoolModel>(builder);
    }

    public async Task<(CensusYearsModel?, IEnumerable<CensusHistoryModel>)> GetComparatorAveHistoryAsync(string urn, string dimension = CensusDimensions.Total, CancellationToken cancellationToken = default)
    {
        using var conn = await dbFactory.GetConnection();
        var yearsBuilder = new YearsSchoolQuery(urn);
        
        var years = await conn.QueryFirstOrDefaultAsync<CensusYearsModel>(yearsBuilder, cancellationToken);

        if (years == null)
        {
            return (null, Array.Empty<CensusHistoryModel>());
        }
        
        var historyBuilder = new CensusSchoolDefaultComparatorAveQuery(dimension)
            .WhereUrnEqual(urn)
            .WhereRunIdBetween(years.StartYear, years.EndYear);
        
        return (years, await conn.QueryAsync<CensusHistoryModel>(historyBuilder, cancellationToken));
    }

    public async Task<(CensusYearsModel?, IEnumerable<CensusHistoryModel>)> GetNationalAvgHistoryAsync(string overallPhase, string financeType, string dimension = CensusDimensions.Total, CancellationToken cancellationToken = default)
    {
        var yearBuilder = new YearsOverallPhaseQuery(overallPhase, financeType);
        
        using var conn = await dbFactory.GetConnection();

        var years = await conn.QueryFirstOrDefaultAsync<CensusYearsModel>(yearBuilder, cancellationToken);

        if (years == null)
        {
            return (null, Array.Empty<CensusHistoryModel>());
        }
        
        var historyBuilder = new CensusSchoolDefaultNationalAveQuery(dimension)
            .WhereOverallPhaseEqual(overallPhase)
            .WhereFinanceTypeEqual(financeType)
            .WhereRunIdBetween(years.StartYear, years.EndYear);
        
        return (years, await conn.QueryAsync<CensusHistoryModel>(historyBuilder, cancellationToken));
    }
}