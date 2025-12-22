using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Platform.Api.LocalAuthority.Features.Details.Models;
using Platform.Domain;
using Platform.Sql;
using Platform.Sql.QueryBuilders;

namespace Platform.Api.LocalAuthority.Features.Details.Services;

public interface ILocalAuthorityDetailsService
{
    Task<LocalAuthorityResponse?> GetAsync(string code, CancellationToken cancellationToken = default);
    Task<IEnumerable<LocalAuthorityResponse>> QueryAsync(CancellationToken cancellationToken = default);

}

[ExcludeFromCodeCoverage]
public class LocalAuthorityDetailsService(IDatabaseFactory dbFactory) : ILocalAuthorityDetailsService
{
    public async Task<LocalAuthorityResponse?> GetAsync(string code, CancellationToken cancellationToken = default)
    {
        var laBuilder = new LocalAuthorityQuery()
            .WhereCodeEqual(code);

        using var conn = await dbFactory.GetConnection();
        var localAuthority = await conn.QueryFirstOrDefaultAsync<LocalAuthorityResponse>(laBuilder, cancellationToken);
        if (localAuthority is null)
        {
            return null;
        }

        var schoolsBuilder = new SchoolQuery(LocalAuthoritySchoolResponse.Fields)
            .WhereLaCodeEqual(code)
            .WhereFinanceTypeEqual(FinanceType.Maintained)
            .WhereUrnInCurrentFinances();

        localAuthority.Schools = await conn.QueryAsync<LocalAuthoritySchoolResponse>(schoolsBuilder, cancellationToken);
        return localAuthority;
    }

    public async Task<IEnumerable<LocalAuthorityResponse>> QueryAsync(CancellationToken cancellationToken = default)
    {
        var laBuilder = new LocalAuthorityQuery()
            .OrderBy(nameof(LocalAuthorityResponse.Name));

        using var conn = await dbFactory.GetConnection();
        var localAuthorities = await conn.QueryAsync<LocalAuthorityResponse>(laBuilder, cancellationToken);
        return localAuthorities;
    }
}