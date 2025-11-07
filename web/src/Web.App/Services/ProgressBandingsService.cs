using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.Insight;
using Web.App.Infrastructure.Extensions;

namespace Web.App.Services;

public interface IProgressBandingsService
{
    Task<KS4ProgressBandings?> GetKS4ProgressBandings(string[] urns, CancellationToken cancellationToken = default);
}

public class ProgressBandingsService(ISchoolInsightApi schoolInsightApi) : IProgressBandingsService
{
    public async Task<KS4ProgressBandings?> GetKS4ProgressBandings(string[] urns, CancellationToken cancellationToken = default)
    {
        var query = new ApiQuery();
        if (urns.Length == 0)
        {
            return null;
        }

        foreach (var urn in urns)
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