using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Platform.Api.Trust.Features.Accounts.Models;
using Platform.Domain;
using Platform.Sql;
using Platform.Sql.QueryBuilders;

namespace Platform.Api.Trust.Features.Accounts.Services;

public interface IAccountsService
{
    Task<(YearsModelDto?, IEnumerable<IncomeHistoryModelDto>)> GetIncomeHistoryAsync(string companyNumber, string dimension, CancellationToken cancellationToken = default);
    Task<BalanceModelDto?> GetBalanceAsync(string companyNumber, CancellationToken cancellationToken = default);
    Task<(YearsModelDto?, IEnumerable<BalanceHistoryModelDto>)> GetBalanceHistoryAsync(string companyNumber, string dimension, CancellationToken cancellationToken = default);
    Task<IEnumerable<BalanceModelDto>> QueryBalanceAsync(string[] companyNumbers, string dimension, CancellationToken cancellationToken = default);
    Task<ExpenditureModelDto?> GetExpenditureAsync(string companyNumber, string dimension, CancellationToken cancellationToken = default);
    Task<(YearsModelDto?, IEnumerable<ExpenditureHistoryModelDto>)> GetExpenditureHistoryAsync(string companyNumber, string dimension, CancellationToken cancellationToken = default);
    Task<IEnumerable<ExpenditureModelDto>> QueryExpenditureAsync(string[] companyNumbers, string dimension, CancellationToken cancellationToken = default);
}

[ExcludeFromCodeCoverage]
public class AccountsService(IDatabaseFactory dbFactory) : IAccountsService
{
    public async Task<(YearsModelDto?, IEnumerable<IncomeHistoryModelDto>)> GetIncomeHistoryAsync(string companyNumber, string dimension, CancellationToken cancellationToken = default)
    {
        using var conn = await dbFactory.GetConnection();
        var years = await QueryYearsTrustAsync(conn, companyNumber, cancellationToken);

        if (years == null)
        {
            return (null, []);
        }

        var historyBuilder = new IncomeTrustDefaultQuery(dimension)
            .WhereCompanyNumberEqual(companyNumber)
            .WhereRunIdBetween(years.StartYear, years.EndYear);

        return (years, await conn.QueryAsync<IncomeHistoryModelDto>(historyBuilder, cancellationToken));
    }

    public async Task<BalanceModelDto?> GetBalanceAsync(string companyNumber, CancellationToken cancellationToken = default)
    {
        var builder = new BalanceTrustDefaultCurrentQuery(Dimensions.Finance.Actuals)
            .WhereCompanyNumberEqual(companyNumber);

        using var conn = await dbFactory.GetConnection();
        return await conn.QueryFirstOrDefaultAsync<BalanceModelDto>(builder, cancellationToken);
    }

    public async Task<(YearsModelDto?, IEnumerable<BalanceHistoryModelDto>)> GetBalanceHistoryAsync(string companyNumber, string dimension, CancellationToken cancellationToken = default)
    {
        using var conn = await dbFactory.GetConnection();
        var years = await QueryYearsTrustAsync(conn, companyNumber, cancellationToken);

        if (years == null)
        {
            return (null, []);
        }

        var historyBuilder = new BalanceTrustDefaultQuery(dimension)
            .WhereCompanyNumberEqual(companyNumber)
            .WhereRunIdBetween(years.StartYear, years.EndYear);

        return (years, await conn.QueryAsync<BalanceHistoryModelDto>(historyBuilder, cancellationToken));
    }

    public async Task<IEnumerable<BalanceModelDto>> QueryBalanceAsync(string[] companyNumbers, string dimension, CancellationToken cancellationToken = default)
    {
        var builder = new BalanceTrustDefaultCurrentQuery(dimension)
            .WhereCompanyNumberIn(companyNumbers);

        using var conn = await dbFactory.GetConnection();
        return await conn.QueryAsync<BalanceModelDto>(builder, cancellationToken);
    }

    public async Task<ExpenditureModelDto?> GetExpenditureAsync(string companyNumber, string dimension, CancellationToken cancellationToken = default)
    {
        using var conn = await dbFactory.GetConnection();
        var builder = new ExpenditureTrustDefaultCurrentQuery(dimension)
            .WhereCompanyNumberEqual(companyNumber);

        return await conn.QueryFirstOrDefaultAsync<ExpenditureModelDto>(builder, cancellationToken);
    }

    public async Task<(YearsModelDto?, IEnumerable<ExpenditureHistoryModelDto>)> GetExpenditureHistoryAsync(string companyNumber, string dimension, CancellationToken cancellationToken = default)
    {
        using var conn = await dbFactory.GetConnection();
        var years = await QueryYearsTrustAsync(conn, companyNumber, cancellationToken);

        if (years == null)
        {
            return (null, []);
        }

        var historyBuilder = new ExpenditureTrustDefaultQuery(dimension)
            .WhereCompanyNumberEqual(companyNumber)
            .WhereRunIdBetween(years.StartYear, years.EndYear);

        return (years, await conn.QueryAsync<ExpenditureHistoryModelDto>(historyBuilder, cancellationToken));
    }

    public async Task<IEnumerable<ExpenditureModelDto>> QueryExpenditureAsync(string[] companyNumbers, string dimension, CancellationToken cancellationToken = default)
    {
        using var conn = await dbFactory.GetConnection();
        var builder = new ExpenditureTrustDefaultCurrentQuery(dimension)
            .WhereCompanyNumberIn(companyNumbers);

        return await conn.QueryAsync<ExpenditureModelDto>(builder, cancellationToken);
    }

    private static async Task<YearsModelDto?> QueryYearsTrustAsync(
        IDatabaseConnection conn,
        string companyNumber,
        CancellationToken cancellationToken = default)
    {
        var builder = new YearsTrustQuery(companyNumber);
        return await conn.QueryFirstOrDefaultAsync<YearsModelDto>(builder, cancellationToken);
    }
}