using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Platform.Domain;
using Platform.Sql;
using Platform.Sql.QueryBuilders;

namespace Platform.Api.Benchmark.Features.UserData.Services;

public interface IUserDataService
{
    Task<IEnumerable<Models.UserData>> QueryAsync(
        string userId,
        string? type = null,
        string? status = null,
        string? id = null,
        string? organisationId = null,
        string? organisationType = null,
        CancellationToken cancellationToken = default);
}

[ExcludeFromCodeCoverage]
public class UserDataService(IDatabaseFactory dbFactory) : IUserDataService
{
    public async Task<IEnumerable<Models.UserData>> QueryAsync(
        string userId,
        string? type = null,
        string? status = null,
        string? id = null,
        string? organisationId = null,
        string? organisationType = null,
        CancellationToken cancellationToken = default)
    {
        var userDataBuilder = new UserDataQuery()
            .WhereUserIdEqual(userId)
            .WhereStatusIn(Pipeline.JobStatus.Pending, Pipeline.JobStatus.Complete)
            .WhereActive();

        if (!string.IsNullOrEmpty(organisationId))
        {
            userDataBuilder.WhereOrganisationIdEqual(organisationId);
        }

        if (!string.IsNullOrEmpty(organisationType))
        {
            userDataBuilder.WhereOrganisationTypeEqual(organisationType);
        }

        if (!string.IsNullOrEmpty(type))
        {
            userDataBuilder.WhereTypeEqual(type);
        }

        if (!string.IsNullOrEmpty(status))
        {
            userDataBuilder.WhereStatusEqual(status);
        }

        if (!string.IsNullOrEmpty(id))
        {
            userDataBuilder.WhereIdEqual(id);
        }

        using var conn = await dbFactory.GetConnection();
        return await conn.QueryAsync<Models.UserData>(userDataBuilder, cancellationToken);
    }
}