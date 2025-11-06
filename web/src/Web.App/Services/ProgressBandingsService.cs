using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.Insight;
using Web.App.Infrastructure.Extensions;

namespace Web.App.Services;

public interface IProgressBandingsService
{
    Task<KS4ProgressBandings?> GetKS4ProgressBandings(IEnumerable<string> set, CancellationToken cancellationToken = default);
}

// todo: unit tests
public class ProgressBandingsService(ISchoolInsightApi schoolInsightApi) : IProgressBandingsService
{
    public async Task<KS4ProgressBandings?> GetKS4ProgressBandings(IEnumerable<string> set, CancellationToken cancellationToken = default)
    {
        var query = new ApiQuery();
        var schools = set as string[] ?? set.ToArray();
        if (schools.Length == 0)
        {
            return null;
        }

        foreach (var urn in schools)
        {
            query.AddIfNotNull("urns", urn);
        }

        var results = await schoolInsightApi
            .GetCharacteristicsAsync(query, cancellationToken)
            .GetResultOrDefault<SchoolCharacteristic[]>() ?? [];

        return new KS4ProgressBandings(results
            .Where(r => !string.IsNullOrWhiteSpace(r.URN))
            .Select(r => new KeyValuePair<string, string?>(r.URN!, r.KS4ProgressBanding)));
    }
}