using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Platform.Api.Trust.Features.Details.Models;
using Platform.Domain;
using Platform.Sql;
using Platform.Sql.QueryBuilders;

namespace Platform.Api.Trust.Features.Details.Services;

public interface ITrustDetailsService
{
    Task<TrustResponse?> GetAsync(string companyNumber, CancellationToken cancellationToken = default);
    Task<IEnumerable<TrustCharacteristicResponse>> QueryAsync(string[] companyNumbers, CancellationToken cancellationToken = default);
}

[ExcludeFromCodeCoverage]
public class TrustDetailsService(IDatabaseFactory dbFactory) : ITrustDetailsService
{
    public async Task<TrustResponse?> GetAsync(string companyNumber, CancellationToken cancellationToken = default)
    {
        var trustBuilder = new TrustQuery()
            .WhereCompanyNumberEqual(companyNumber);

        using var conn = await dbFactory.GetConnection();
        var trust = await conn.QueryFirstOrDefaultAsync<TrustResponse>(trustBuilder, cancellationToken);
        if (trust is null)
        {
            return null;
        }

        var schoolsBuilder = new SchoolQuery(TrustSchoolResponse.Fields)
            .WhereTrustCompanyNumberEqual(companyNumber)
            .WhereFinanceTypeEqual(FinanceType.Academy)
            .WhereUrnInCurrentFinances();

        trust.Schools = await conn.QueryAsync<TrustSchoolResponse>(schoolsBuilder, cancellationToken);
        return trust;
    }

    public async Task<IEnumerable<TrustCharacteristicResponse>> QueryAsync(string[] companyNumbers, CancellationToken cancellationToken = default)
    {
        using var conn = await dbFactory.GetConnection();
        var builder = new TrustCharacteristicsQuery()
            .WhereCompanyNumberIn(companyNumbers);

        return await conn.QueryAsync<TrustCharacteristicResponse>(builder, cancellationToken);
    }
}