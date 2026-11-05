using Web.App.Domain;
using Web.App.Domain.Schools;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;

namespace Web.App.Services;

public interface IComparatorSetRankingService
{
    Task<IReadOnlyList<string>> GetTopRankedAsync(string currentUrn, string?[] comparatorUrns, int count = 5, CancellationToken cancellationToken = default);
}

public class Attainment8ComparatorSetRankingService(ISchoolApi schoolApi) : IComparatorSetRankingService
{
    public async Task<IReadOnlyList<string>> GetTopRankedAsync(string currentUrn, string?[] comparatorUrns, int count = 5, CancellationToken cancellationToken = default)
    {
        if (count <= 0)
        {
            return [];
        }

        var candidateUrns = comparatorUrns
            .Where(urn => !string.Equals(urn, currentUrn))
            .Distinct()
            .ToList();

        var query = new ApiQuery();
        if (candidateUrns.Count == 0)
        {
            return [];
        }

        foreach (var urn in candidateUrns)
        {
            query.AddIfNotNull("urns", urn);
        }

        var results = await schoolApi
            .QueryAsync(query, cancellationToken)
            .GetResultOrDefault<SchoolCharacteristic[]>() ?? [];

        return
        [
            .. results
                .Where(x => x.URN != currentUrn)
                .Where(x => x.Attainment8.HasValue)
                .OrderByDescending(x => x.Attainment8)
                .Take(count)
                .Select(x => x.URN!)
        ];
    }
}
