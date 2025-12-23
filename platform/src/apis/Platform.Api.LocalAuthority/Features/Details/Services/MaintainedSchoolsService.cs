using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Platform.Api.LocalAuthority.Features.Details.Models;
using Platform.Domain;
using Platform.Sql;
using Platform.Sql.QueryBuilders;

namespace Platform.Api.LocalAuthority.Features.Details.Services;

public interface IMaintainedSchoolsService
{
    Task<IEnumerable<LocalAuthoritySchoolFinanceSummaryResponse>> GetFinanceSummaryAsync(
        string code,
        string dimension,
        string[] nurseryProvision,
        string[] sixthFormProvision,
        string[] specialClassProvision,
        int? limit,
        string sortField,
        string sortOrder,
        string[] overallPhase,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<LocalAuthoritySchoolWorkforceSummaryResponse>> GetWorkforceSummaryAsync(
        string code,
        string dimension,
        string[] nurseryProvision,
        string[] sixthFormProvision,
        string[] specialClassProvision,
        int? limit,
        string sortField,
        string sortOrder,
        string[] overallPhase,
        CancellationToken cancellationToken = default);
}


public class MaintainedSchoolsService(IDatabaseFactory dbFactory) : IMaintainedSchoolsService
{
    public async Task<IEnumerable<LocalAuthoritySchoolFinanceSummaryResponse>> GetFinanceSummaryAsync(
    string code,
    string dimension,
    string[] nurseryProvision,
    string[] sixthFormProvision,
    string[] specialClassProvision,
    int? limit,
    string sortField,
    string sortOrder,
    string[] overallPhase,
    CancellationToken cancellationToken = default)
    {
        var builder = new SchoolsFinanceSummaryDefaultCurrentQuery(dimension)
            .WhereLaCodeEqual(code)
            .WhereFinanceTypeEqual(FinanceType.Maintained);

        builder.OrderBy($"{sortField.Trim()} {sortOrder.Trim()}");

        if (limit is <= 1 or > 100)
        {
            throw new ArgumentOutOfRangeException(nameof(limit));
        }

        builder.Select(limit.HasValue ? $"TOP({limit.Value}) *" : "*");

        if (overallPhase.Length > 0)
        {
            builder.WhereOverallPhaseIn(overallPhase);
        }

        if (nurseryProvision.Length > 0)
        {
            builder.WhereNurseryProvisionIn(nurseryProvision);
        }

        if (sixthFormProvision.Length > 0)
        {
            builder.WhereSixthFormProvisionIn(sixthFormProvision);
        }

        if (specialClassProvision.Length > 0)
        {
            builder.WhereSpecialClassesProvisionIn(specialClassProvision);
        }

        using var conn = await dbFactory.GetConnection();

        var result = await conn.QueryAsync<LocalAuthoritySchoolFinanceSummaryResponse>(builder, cancellationToken);
        return result;
    }

    public async Task<IEnumerable<LocalAuthoritySchoolWorkforceSummaryResponse>> GetWorkforceSummaryAsync(
        string code,
        string dimension,
        string[] nurseryProvision,
        string[] sixthFormProvision,
        string[] specialClassProvision,
        int? limit,
        string sortField,
        string sortOrder,
        string[] overallPhase, CancellationToken cancellationToken = default)
    {
        var builder = new SchoolsWorkforceSummaryDefaultCurrentQuery(dimension)
            .WhereLaCodeEqual(code)
            .WhereFinanceTypeEqual(FinanceType.Maintained);

        builder.OrderBy($"{sortField.Trim()} {sortOrder.Trim()}");

        if (limit is <= 1 or > 100)
        {
            throw new ArgumentOutOfRangeException(nameof(limit));
        }

        builder.Select(limit.HasValue ? $"TOP({limit.Value}) *" : "*");

        if (overallPhase.Length > 0)
        {
            builder.WhereOverallPhaseIn(overallPhase);
        }

        if (nurseryProvision.Length > 0)
        {
            builder.WhereNurseryProvisionIn(nurseryProvision);
        }

        if (sixthFormProvision.Length > 0)
        {
            builder.WhereSixthFormProvisionIn(sixthFormProvision);
        }

        if (specialClassProvision.Length > 0)
        {
            builder.WhereSpecialClassesProvisionIn(specialClassProvision);
        }

        using var conn = await dbFactory.GetConnection();

        var result = await conn.QueryAsync<LocalAuthoritySchoolWorkforceSummaryResponse>(builder, cancellationToken);
        return result;
    }
}