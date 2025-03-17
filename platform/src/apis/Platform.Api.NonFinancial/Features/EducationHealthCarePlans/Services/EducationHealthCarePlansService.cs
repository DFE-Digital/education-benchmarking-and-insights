using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Platform.Api.NonFinancial.Features.EducationHealthCarePlans.Models;
using Platform.Sql;
using Platform.Sql.QueryBuilders;

namespace Platform.Api.NonFinancial.Features.EducationHealthCarePlans.Services;

public interface IEducationHealthCarePlansService
{
    Task<LocalAuthorityNumberOfPlans[]> Get(string[] codes, string dimension, CancellationToken cancellationToken = default);
    Task<History<LocalAuthorityNumberOfPlansYear>> GetHistory(string[] codes, CancellationToken cancellationToken = default);
}

public class EducationHealthCarePlansService(IDatabaseFactory dbFactory) : EducationHealthCarePlansStubService
{
    public override async Task<LocalAuthorityNumberOfPlans[]> Get(string[] codes, string dimension, CancellationToken cancellationToken = default)
    {
        using var conn = await dbFactory.GetConnection();
        var builder = new LocalAuthorityEducationHealthCarePlansDefaultCurrentQuery(dimension)
            .WhereLaCodesIn(codes);

        var results = await conn.QueryAsync<LocalAuthorityNumberOfPlans>(builder, cancellationToken);
        return results.ToArray();
    }
}