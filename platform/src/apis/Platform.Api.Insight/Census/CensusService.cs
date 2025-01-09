using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Platform.Sql;

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
        var sql = GetSchoolSql(dimension);
        var parameters = new { URN = urn };

        using var conn = await dbFactory.GetConnection();
        ;
        return await conn.QueryFirstOrDefaultAsync<CensusSchoolModel>(sql, parameters);
    }

    public async Task<(CensusYearsModel?, IEnumerable<CensusHistoryModel>)> GetSchoolHistoryAsync(string urn, string dimension = CensusDimensions.Total, CancellationToken cancellationToken = default)
    {
        const string yearSql = "SELECT * FROM VW_YearsSchool WHERE URN = @URN";
        var yearParams = new { URN = urn };

        using var conn = await dbFactory.GetConnection();

        var years = await conn.QueryFirstOrDefaultAsync<CensusYearsModel>(yearSql, yearParams, cancellationToken);

        if (years == null)
        {
            return (null, Array.Empty<CensusHistoryModel>());
        }

        var historySql = GetSchoolHistorySql(dimension);
        var historyParams = new { URN = urn, years.StartYear, years.EndYear };
        return (years, await conn.QueryAsync<CensusHistoryModel>(historySql, historyParams, cancellationToken));
    }

    public async Task<CensusSchoolModel?> GetCustomAsync(string urn, string identifier, string dimension = CensusDimensions.Total)
    {
        var sql = GetSchoolCustomSql(dimension);
        var parameters = new { URN = urn, RunId = identifier };

        using var conn = await dbFactory.GetConnection();

        return await conn.QueryFirstOrDefaultAsync<CensusSchoolModel>(sql, parameters);
    }

    public async Task<IEnumerable<CensusSchoolModel>> QueryAsync(string[] urns, string? companyNumber, string? laCode, string? phase, string dimension = CensusDimensions.Total)
    {
        var sql = GetQueryTemplateSql(dimension);
        var builder = new SqlBuilder();
        var template = builder.AddTemplate(sql);
        if (urns.Length != 0)
        {
            builder.Where("URN IN @URNS", new
            {
                URNS = urns
            });
        }
        else if (!string.IsNullOrWhiteSpace(companyNumber))
        {
            builder.Where("TrustCompanyNumber = @CompanyNumber AND OverallPhase = @Phase", new
            {
                CompanyNumber = companyNumber,
                Phase = phase
            });
        }
        else if (!string.IsNullOrWhiteSpace(laCode))
        {
            builder.Where("LaCode = @LaCode AND OverallPhase = @Phase", new
            {
                LaCode = laCode,
                Phase = phase
            });
        }
        else
        {
            throw new ArgumentNullException(nameof(urns), $"{nameof(urns)} or {nameof(companyNumber)} or {nameof(laCode)} must be supplied");
        }

        using var conn = await dbFactory.GetConnection();
        return await conn.QueryAsync<CensusSchoolModel>(template.RawSql, template.Parameters);
    }

    public async Task<(CensusYearsModel?, IEnumerable<CensusHistoryModel>)> GetComparatorAveHistoryAsync(string urn, string dimension = CensusDimensions.Total, CancellationToken cancellationToken = default)
    {

        const string yearSql = "SELECT * FROM VW_YearsSchool WHERE URN = @URN";
        var yearParams = new { URN = urn };

        using var conn = await dbFactory.GetConnection();

        var years = await conn.QueryFirstOrDefaultAsync<CensusYearsModel>(yearSql, yearParams, cancellationToken);

        if (years == null)
        {
            return (null, Array.Empty<CensusHistoryModel>());
        }

        var historySql = GetComparatorAveHistorySql(dimension);
        var historyParams = new { URN = urn, years.StartYear, years.EndYear };
        return (years, await conn.QueryAsync<CensusHistoryModel>(historySql, historyParams, cancellationToken));
    }

    public async Task<(CensusYearsModel?, IEnumerable<CensusHistoryModel>)> GetNationalAvgHistoryAsync(string overallPhase, string financeType, string dimension = CensusDimensions.Total, CancellationToken cancellationToken = default)
    {
        const string yearSql = "SELECT * FROM VW_YearsOverallPhase WHERE OverallPhase = @OverallPhase AND FinanceType = @FinanceType";
        var yearParams = new
        {
            OverallPhase = overallPhase,
            FinanceType = financeType
        };

        using var conn = await dbFactory.GetConnection();

        var years = await conn.QueryFirstOrDefaultAsync<CensusYearsModel>(yearSql, yearParams, cancellationToken);

        if (years == null)
        {
            return (null, Array.Empty<CensusHistoryModel>());
        }

        var historySql = GetNationalAvgHistorySql(dimension);
        var historyParams = new
        {
            OverallPhase = overallPhase,
            FinanceType = financeType,
            years.StartYear,
            years.EndYear
        };
        return (years, await conn.QueryAsync<CensusHistoryModel>(historySql, historyParams, cancellationToken));
    }

    private static string GetQueryTemplateSql(string dimension)
    {
        return dimension switch
        {
            CensusDimensions.Total => "SELECT * FROM VW_CensusSchoolDefaultCurrentTotal /**where**/",
            CensusDimensions.HeadcountPerFte => "SELECT * FROM VW_CensusSchoolDefaultCurrentHeadcountPerFte /**where**/",
            CensusDimensions.PercentWorkforce => "SELECT * FROM VW_CensusSchoolDefaultCurrentPercentWorkforce /**where**/",
            CensusDimensions.PupilsPerStaffRole => "SELECT * FROM VW_CensusSchoolDefaultCurrentPupilsPerStaffRole /**where**/",
            _ => throw new ArgumentOutOfRangeException(nameof(dimension), "Unknown dimension")
        };
    }

    private static string GetSchoolSql(string dimension)
    {
        return dimension switch
        {
            CensusDimensions.Total => "SELECT * FROM VW_CensusSchoolDefaultCurrentTotal WHERE URN = @URN",
            CensusDimensions.HeadcountPerFte => "SELECT * FROM VW_CensusSchoolDefaultCurrentHeadcountPerFte WHERE URN = @URN",
            CensusDimensions.PercentWorkforce => "SELECT * FROM VW_CensusSchoolDefaultCurrentPercentWorkforce WHERE URN = @URN",
            CensusDimensions.PupilsPerStaffRole => "SELECT * FROM VW_CensusSchoolDefaultCurrentPupilsPerStaffRole WHERE URN = @URN",
            _ => throw new ArgumentOutOfRangeException(nameof(dimension), "Unknown dimension")
        };
    }

    private static string GetSchoolCustomSql(string dimension)
    {
        return dimension switch
        {
            CensusDimensions.Total => "SELECT * FROM VW_CensusSchoolCustomTotal WHERE URN = @URN AND RunId = @RunId",
            CensusDimensions.HeadcountPerFte => "SELECT * FROM VW_CensusSchoolCustomHeadcountPerFte WHERE URN = @URN AND RunId = @RunId",
            CensusDimensions.PercentWorkforce => "SELECT * FROM VW_CensusSchoolCustomPercentWorkforce WHERE URN = @URN AND RunId = @RunId",
            CensusDimensions.PupilsPerStaffRole => "SELECT * FROM VW_CensusSchoolCustomPupilsPerStaffRole WHERE URN = @URN AND RunId = @RunId",
            _ => throw new ArgumentOutOfRangeException(nameof(dimension), "Unknown dimension")
        };
    }

    private static string GetSchoolHistorySql(string dimension)
    {
        return dimension switch
        {
            CensusDimensions.Total => "SELECT * FROM VW_CensusSchoolDefaultTotal WHERE URN = @URN AND RunId BETWEEN @StartYear AND @EndYear",
            CensusDimensions.HeadcountPerFte => "SELECT * FROM VW_CensusSchoolDefaultHeadcountPerFte WHERE URN = @URN AND RunId BETWEEN @StartYear AND @EndYear",
            CensusDimensions.PercentWorkforce => "SELECT * FROM VW_CensusSchoolDefaultPercentWorkforce WHERE URN = @URN AND RunId BETWEEN @StartYear AND @EndYear",
            CensusDimensions.PupilsPerStaffRole => "SELECT * FROM VW_CensusSchoolDefaultPupilsPerStaffRole WHERE URN = @URN AND RunId BETWEEN @StartYear AND @EndYear",
            _ => throw new ArgumentOutOfRangeException(nameof(dimension), "Unknown dimension")
        };
    }

    private static string GetComparatorAveHistorySql(string dimension)
    {
        return dimension switch
        {
            CensusDimensions.Total => "SELECT * FROM VW_CensusSchoolDefaultComparatorAveTotal WHERE URN = @URN AND RunId BETWEEN @StartYear AND @EndYear",
            CensusDimensions.HeadcountPerFte => "SELECT * FROM VW_CensusSchoolDefaultComparatorAveHeadcountPerFte WHERE URN = @URN AND RunId BETWEEN @StartYear AND @EndYear",
            CensusDimensions.PercentWorkforce => "SELECT * FROM VW_CensusSchoolDefaultComparatorAvePercentWorkforce WHERE URN = @URN AND RunId BETWEEN @StartYear AND @EndYear",
            CensusDimensions.PupilsPerStaffRole => "SELECT * FROM VW_CensusSchoolDefaultComparatorAvePupilsPerStaffRole WHERE URN = @URN AND RunId BETWEEN @StartYear AND @EndYear",
            _ => throw new ArgumentOutOfRangeException(nameof(dimension), "Unknown dimension")
        };
    }

    private static string GetNationalAvgHistorySql(string dimension)
    {
        return dimension switch
        {
            CensusDimensions.Total => "SELECT * FROM VW_CensusSchoolDefaultNationalAveTotal WHERE OverallPhase = @OverallPhase AND FinanceType = @FinanceType AND RunId BETWEEN @StartYear AND @EndYear",
            CensusDimensions.HeadcountPerFte => "SELECT * FROM VW_CensusSchoolDefaultNationalAveHeadcountPerFte WHERE OverallPhase = @OverallPhase AND FinanceType = @FinanceType AND RunId BETWEEN @StartYear AND @EndYear",
            CensusDimensions.PercentWorkforce => "SELECT * FROM VW_CensusSchoolDefaultNationalAvePercentWorkforce WHERE OverallPhase = @OverallPhase AND FinanceType = @FinanceType AND RunId BETWEEN @StartYear AND @EndYear",
            CensusDimensions.PupilsPerStaffRole => "SELECT * FROM VW_CensusSchoolDefaultNationalAvePupilsPerStaffRole WHERE OverallPhase = @OverallPhase AND FinanceType = @FinanceType AND RunId BETWEEN @StartYear AND @EndYear",
            _ => throw new ArgumentOutOfRangeException(nameof(dimension), "Unknown dimension")
        };
    }
}