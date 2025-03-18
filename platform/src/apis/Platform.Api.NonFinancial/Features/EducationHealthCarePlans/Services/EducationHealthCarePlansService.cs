using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Platform.Api.NonFinancial.Features.EducationHealthCarePlans.Models;
using Platform.Api.NonFinancial.Shared;
using Platform.Sql;
using Platform.Sql.QueryBuilders;

namespace Platform.Api.NonFinancial.Features.EducationHealthCarePlans.Services;

public interface IEducationHealthCarePlansService
{
    Task<LocalAuthorityNumberOfPlansResponse[]> Get(string[] codes, string dimension, CancellationToken cancellationToken = default);
    Task<History<LocalAuthorityNumberOfPlansYearResponse>?> GetHistory(string[] codes, string dimension, CancellationToken cancellationToken = default);
}

public class EducationHealthCarePlansService(IDatabaseFactory dbFactory) : IEducationHealthCarePlansService
{
    public async Task<LocalAuthorityNumberOfPlansResponse[]> Get(string[] codes, string dimension, CancellationToken cancellationToken = default)
    {
        using var conn = await dbFactory.GetConnection();
        var builder = new LocalAuthorityEducationHealthCarePlansDefaultCurrentQuery(dimension)
            .WhereLaCodesIn(codes);

        var results = await conn.QueryAsync<LocalAuthorityNumberOfPlans>(builder, cancellationToken);
        return results.Select(Mapper.MapToLocalAuthorityNumberOfPlansResponse).ToArray();
    }

    public async Task<History<LocalAuthorityNumberOfPlansYearResponse>?> GetHistory(string[] codes, string dimension, CancellationToken cancellationToken = default)
    {
        using var conn = await dbFactory.GetConnection();
        var years = await conn.QueryYearsLocalAuthorityAsync(codes.First(), cancellationToken);
        if (years == null)
        {
            return null;
        }

        var builder = new LocalAuthorityEducationHealthCarePlansDefaultQuery(dimension)
            .WhereLaCodesIn(codes)
            .WhereRunIdBetween(years.StartYear, years.EndYear);

        var results = await conn.QueryAsync<LocalAuthorityNumberOfPlansYear>(builder, cancellationToken);
        return new History<LocalAuthorityNumberOfPlansYearResponse>
        {
            StartYear = years.StartYear,
            EndYear = years.EndYear,
            Plans = results.Select(Mapper.MapToLocalAuthorityNumberOfPlansYearResponse).ToArray()
        };
    }
}