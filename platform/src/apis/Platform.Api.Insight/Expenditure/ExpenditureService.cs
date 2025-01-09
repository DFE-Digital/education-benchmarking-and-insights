using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Platform.Sql;

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
        var sql = GetSchoolSql(dimension);
        var parameters = new { URN = urn };

        using var conn = await dbFactory.GetConnection();
        ;
        return await conn.QueryFirstOrDefaultAsync<ExpenditureSchoolModel>(sql, parameters);
    }

    public async Task<(ExpenditureYearsModel?, IEnumerable<ExpenditureHistoryModel>)> GetSchoolHistoryAsync(string urn, string dimension = ExpenditureDimensions.Actuals, CancellationToken cancellationToken = default)
    {
        const string yearSql = "SELECT * FROM VW_YearsSchool WHERE URN = @URN";
        var yearParams = new { URN = urn };

        using var conn = await dbFactory.GetConnection();

        var years = await conn.QueryFirstOrDefaultAsync<ExpenditureYearsModel>(yearSql, yearParams, cancellationToken);

        if (years == null)
        {
            return (null, Array.Empty<ExpenditureHistoryModel>());
        }

        var historySql = GetSchoolHistorySql(dimension);
        var historyParams = new { URN = urn, years.StartYear, years.EndYear };
        return (years, await conn.QueryAsync<ExpenditureHistoryModel>(historySql, historyParams, cancellationToken));
    }

    public async Task<IEnumerable<ExpenditureSchoolModel>> QuerySchoolsAsync(string[] urns, string? companyNumber, string? laCode, string? phase, string dimension = ExpenditureDimensions.Actuals)
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
        return await conn.QueryAsync<ExpenditureSchoolModel>(template.RawSql, template.Parameters);
    }

    public async Task<ExpenditureSchoolModel?> GetCustomSchoolAsync(string urn, string identifier, string dimension = ExpenditureDimensions.Actuals)
    {
        var sql = GetSchoolCustomSql(dimension);
        var parameters = new { URN = urn, RunId = identifier };

        using var conn = await dbFactory.GetConnection();

        return await conn.QueryFirstOrDefaultAsync<ExpenditureSchoolModel>(sql, parameters);
    }

    public async Task<ExpenditureTrustModel?> GetTrustAsync(string companyNumber, string dimension = ExpenditureDimensions.Actuals)
    {
        var sql = GetTrustSql(dimension);
        var parameters = new { CompanyNumber = companyNumber };

        using var conn = await dbFactory.GetConnection();
        return await conn.QueryFirstOrDefaultAsync<ExpenditureTrustModel>(sql, parameters);
    }

    public async Task<(ExpenditureYearsModel?, IEnumerable<ExpenditureHistoryModel>)> GetTrustHistoryAsync(string companyNumber, string dimension = ExpenditureDimensions.Actuals)
    {
        const string yearSql = "SELECT * FROM VW_YearsTrust WHERE CompanyNumber = @CompanyNumber";
        var yearParams = new { CompanyNumber = companyNumber };

        using var conn = await dbFactory.GetConnection();

        var years = await conn.QueryFirstOrDefaultAsync<ExpenditureYearsModel>(yearSql, yearParams);

        if (years == null)
        {
            return (null, Array.Empty<ExpenditureHistoryModel>());
        }

        var historySql = GetTrustHistorySql(dimension);
        var historyParams = new { CompanyNumber = companyNumber, years.StartYear, years.EndYear };
        return (years, await conn.QueryAsync<ExpenditureHistoryModel>(historySql, historyParams));
    }

    public async Task<IEnumerable<ExpenditureTrustModel>> QueryTrustsAsync(string[] companyNumbers, string dimension = ExpenditureDimensions.Actuals)
    {
        var sql = QueryTrustsSql(dimension);
        var parameters = new { CompanyNumbers = companyNumbers };

        using var conn = await dbFactory.GetConnection();
        return await conn.QueryAsync<ExpenditureTrustModel>(sql, parameters);
    }

    public async Task<(ExpenditureYearsModel?, IEnumerable<ExpenditureHistoryModel>)> GetComparatorAveHistoryAsync(string urn, string dimension = ExpenditureDimensions.Actuals, CancellationToken cancellationToken = default)
    {
        const string yearSql = "SELECT * FROM VW_YearsSchool WHERE URN = @URN";
        var yearParams = new { URN = urn };

        using var conn = await dbFactory.GetConnection();

        var years = await conn.QueryFirstOrDefaultAsync<ExpenditureYearsModel>(yearSql, yearParams, cancellationToken);

        if (years == null)
        {
            return (null, Array.Empty<ExpenditureHistoryModel>());
        }

        var historySql = GetComparatorAveHistorySql(dimension);
        var historyParams = new { URN = urn, years.StartYear, years.EndYear };
        return (years, await conn.QueryAsync<ExpenditureHistoryModel>(historySql, historyParams, cancellationToken));
    }

    public async Task<(ExpenditureYearsModel?, IEnumerable<ExpenditureHistoryModel>)> GetNationalAvgHistoryAsync(string overallPhase, string financeType, string dimension = ExpenditureDimensions.Actuals, CancellationToken cancellationToken = default)
    {
        const string yearSql = "SELECT * FROM VW_YearsOverallPhase WHERE OverallPhase = @OverallPhase AND FinanceType = @FinanceType";
        var yearParams = new
        {
            OverallPhase = overallPhase,
            FinanceType = financeType
        };

        using var conn = await dbFactory.GetConnection();

        var years = await conn.QueryFirstOrDefaultAsync<ExpenditureYearsModel>(yearSql, yearParams, cancellationToken);

        if (years == null)
        {
            return (null, Array.Empty<ExpenditureHistoryModel>());
        }

        var historySql = GetNationalAvgHistorySql(dimension);
        var historyParams = new
        {
            OverallPhase = overallPhase,
            FinanceType = financeType,
            years.StartYear,
            years.EndYear
        };
        return (years, await conn.QueryAsync<ExpenditureHistoryModel>(historySql, historyParams, cancellationToken));
    }


    private static string GetComparatorAveHistorySql(string dimension)
    {
        return dimension switch
        {
            ExpenditureDimensions.Actuals => "SELECT * FROM VW_ExpenditureSchoolDefaultComparatorAvgActual WHERE URN = @URN AND RunId BETWEEN @StartYear AND @EndYear",
            ExpenditureDimensions.PerUnit => "SELECT * FROM VW_ExpenditureSchoolDefaultComparatorAvgPerUnit WHERE URN = @URN AND RunId BETWEEN @StartYear AND @EndYear",
            ExpenditureDimensions.PercentExpenditure => "SELECT * FROM VW_ExpenditureSchoolDefaultComparatorAvgPercentExpenditure WHERE URN = @URN AND RunId BETWEEN @StartYear AND @EndYear",
            ExpenditureDimensions.PercentIncome => "SELECT * FROM VW_ExpenditureSchoolDefaultComparatorAvgPercentIncome WHERE URN = @URN AND RunId BETWEEN @StartYear AND @EndYear",
            _ => throw new ArgumentOutOfRangeException(nameof(dimension), "Unknown dimension")
        };
    }

    private static string GetNationalAvgHistorySql(string dimension)
    {
        return dimension switch
        {
            ExpenditureDimensions.Actuals => "SELECT * FROM VW_ExpenditureSchoolDefaultNationalAveActual WHERE OverallPhase = @OverallPhase AND FinanceType = @FinanceType AND RunId BETWEEN @StartYear AND @EndYear",
            ExpenditureDimensions.PerUnit => "SELECT * FROM VW_ExpenditureSchoolDefaultNationalAvePerUnit WHERE OverallPhase = @OverallPhase AND FinanceType = @FinanceType AND RunId BETWEEN @StartYear AND @EndYear",
            ExpenditureDimensions.PercentExpenditure => "SELECT * FROM VW_ExpenditureSchoolDefaultNationalAvePercentExpenditure WHERE OverallPhase = @OverallPhase AND FinanceType = @FinanceType AND RunId BETWEEN @StartYear AND @EndYear",
            ExpenditureDimensions.PercentIncome => "SELECT * FROM VW_ExpenditureSchoolDefaultNationalAvePercentIncome WHERE OverallPhase = @OverallPhase AND FinanceType = @FinanceType AND RunId BETWEEN @StartYear AND @EndYear",
            _ => throw new ArgumentOutOfRangeException(nameof(dimension), "Unknown dimension")
        };
    }

    private static string GetTrustSql(string dimension)
    {
        return dimension switch
        {
            ExpenditureDimensions.Actuals => "SELECT * FROM VW_ExpenditureTrustDefaultCurrentActual WHERE CompanyNumber = @CompanyNumber",
            ExpenditureDimensions.PerUnit => "SELECT * FROM VW_ExpenditureTrustDefaultCurrentPerUnit WHERE CompanyNumber = @CompanyNumber",
            ExpenditureDimensions.PercentExpenditure => "SELECT * FROM VW_ExpenditureTrustDefaultCurrentPercentExpenditure WHERE CompanyNumber = @CompanyNumber",
            ExpenditureDimensions.PercentIncome => "SELECT * FROM VW_ExpenditureTrustDefaultCurrentPercentIncome WHERE CompanyNumber = @CompanyNumber",
            _ => throw new ArgumentOutOfRangeException(nameof(dimension), "Unknown dimension")
        };
    }

    private static string QueryTrustsSql(string dimension)
    {
        return dimension switch
        {
            ExpenditureDimensions.Actuals => "SELECT * FROM VW_ExpenditureTrustDefaultCurrentActual WHERE CompanyNumber IN @CompanyNumbers",
            ExpenditureDimensions.PerUnit => "SELECT * FROM VW_ExpenditureTrustDefaultCurrentPerUnit WHERE CompanyNumber IN @CompanyNumbers",
            ExpenditureDimensions.PercentExpenditure => "SELECT * FROM VW_ExpenditureTrustDefaultCurrentPercentExpenditure WHERE CompanyNumber IN @CompanyNumbers",
            ExpenditureDimensions.PercentIncome => "SELECT * FROM VW_ExpenditureTrustDefaultCurrentPercentIncome WHERE CompanyNumber IN @CompanyNumbers",
            _ => throw new ArgumentOutOfRangeException(nameof(dimension), "Unknown dimension")
        };
    }

    private static string GetTrustHistorySql(string dimension)
    {
        return dimension switch
        {
            ExpenditureDimensions.Actuals => "SELECT * FROM VW_ExpenditureTrustDefaultActual WHERE CompanyNumber = @CompanyNumber AND RunId BETWEEN @StartYear AND @EndYear",
            ExpenditureDimensions.PerUnit => "SELECT * FROM VW_ExpenditureTrustDefaultPerUnit WHERE CompanyNumber = @CompanyNumber AND RunId BETWEEN @StartYear AND @EndYear",
            ExpenditureDimensions.PercentExpenditure => "SELECT * FROM VW_ExpenditureTrustDefaultPercentExpenditure WHERE CompanyNumber = @CompanyNumber AND RunId BETWEEN @StartYear AND @EndYear",
            ExpenditureDimensions.PercentIncome => "SELECT * FROM VW_ExpenditureTrustDefaultPercentIncome WHERE CompanyNumber = @CompanyNumber AND RunId BETWEEN @StartYear AND @EndYear",
            _ => throw new ArgumentOutOfRangeException(nameof(dimension), "Unknown dimension")
        };
    }

    private static string GetSchoolCustomSql(string dimension)
    {
        return dimension switch
        {
            ExpenditureDimensions.Actuals => "SELECT * FROM VW_ExpenditureSchoolCustomActual WHERE URN = @URN AND RunId = @RunId",
            ExpenditureDimensions.PercentExpenditure => "SELECT * FROM VW_ExpenditureSchoolCustomPercentExpenditure WHERE URN = @URN AND RunId = @RunId",
            ExpenditureDimensions.PercentIncome => "SELECT * FROM VW_ExpenditureSchoolCustomPercentIncome WHERE URN = @URN AND RunId = @RunId",
            ExpenditureDimensions.PerUnit => "SELECT * FROM VW_ExpenditureSchoolCustomPerUnit WHERE URN = @URN AND RunId = @RunId",
            _ => throw new ArgumentOutOfRangeException(nameof(dimension), "Unknown dimension")
        };
    }

    private static string GetQueryTemplateSql(string dimension)
    {
        return dimension switch
        {
            ExpenditureDimensions.Actuals => "SELECT * FROM VW_ExpenditureSchoolDefaultCurrentActual /**where**/",
            ExpenditureDimensions.PercentExpenditure => "SELECT * FROM VW_ExpenditureSchoolDefaultCurrentPercentExpenditure /**where**/",
            ExpenditureDimensions.PercentIncome => "SELECT * FROM VW_ExpenditureSchoolDefaultCurrentPercentIncome /**where**/",
            ExpenditureDimensions.PerUnit => "SELECT * FROM VW_ExpenditureSchoolDefaultCurrentPerUnit /**where**/",
            _ => throw new ArgumentOutOfRangeException(nameof(dimension), "Unknown dimension")
        };
    }

    private static string GetSchoolHistorySql(string dimension)
    {
        return dimension switch
        {
            ExpenditureDimensions.Actuals => "SELECT * FROM VW_ExpenditureSchoolDefaultActual WHERE URN = @URN AND RunId BETWEEN @StartYear AND @EndYear",
            ExpenditureDimensions.PercentExpenditure => "SELECT * FROM VW_ExpenditureSchoolDefaultPercentExpenditure WHERE URN = @URN AND RunId BETWEEN @StartYear AND @EndYear",
            ExpenditureDimensions.PercentIncome => "SELECT * FROM VW_ExpenditureSchoolDefaultPercentIncome WHERE URN = @URN AND RunId BETWEEN @StartYear AND @EndYear",
            ExpenditureDimensions.PerUnit => "SELECT * FROM VW_ExpenditureSchoolDefaultPerUnit WHERE URN = @URN AND RunId BETWEEN @StartYear AND @EndYear",
            _ => throw new ArgumentOutOfRangeException(nameof(dimension), "Unknown dimension")
        };
    }

    private static string GetSchoolSql(string dimension)
    {
        return dimension switch
        {
            ExpenditureDimensions.Actuals => "SELECT * FROM VW_ExpenditureSchoolDefaultCurrentActual WHERE URN = @URN",
            ExpenditureDimensions.PercentExpenditure => "SELECT * FROM VW_ExpenditureSchoolDefaultCurrentPercentExpenditure WHERE URN = @URN",
            ExpenditureDimensions.PercentIncome => "SELECT * FROM VW_ExpenditureSchoolDefaultCurrentPercentIncome WHERE URN = @URN",
            ExpenditureDimensions.PerUnit => "SELECT * FROM VW_ExpenditureSchoolDefaultCurrentPerUnit WHERE URN = @URN",
            _ => throw new ArgumentOutOfRangeException(nameof(dimension), "Unknown dimension")
        };
    }
}