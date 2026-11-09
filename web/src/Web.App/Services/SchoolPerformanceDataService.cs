using Microsoft.FeatureManagement;
using Web.App.Domain;

namespace Web.App.Services;

public record PerformanceDataResult(
    bool ShowKs4Progress,
    bool ShowAttainment8,
    KS4ProgressBandings? Bandings,
    IReadOnlyList<string> TopRankedBuilding,
    IReadOnlyList<string> TopRankedPupil);

public interface ISchoolPerformanceDataService
{
    Task<PerformanceDataResult> GetPerformanceDataAsync(
        string urn,
        string[]? buildingUrns,
        string[]? pupilUrns);

    SchoolExpenditureWithPerformance[] Merge(
        SchoolExpenditure[]? expenditures,
        KS4ProgressBandings? bandings,
        IReadOnlyList<string> topRanked);
}

public class SchoolPerformanceDataService(
    IFeatureManager featureManager,
    IProgressBandingsService progressBandingsService,
    IComparatorSetRankingService attainment8RankingService,
    ILogger<SchoolPerformanceDataService> logger) : ISchoolPerformanceDataService
{
    public async Task<PerformanceDataResult> GetPerformanceDataAsync(
        string urn,
        string[]? buildingUrns,
        string[]? pupilUrns)
    {
        var showKs4Progress = await featureManager.IsEnabledAsync(FeatureFlags.KS4ProgressBanding);
        var showAttainment8 = await featureManager.IsEnabledAsync(FeatureFlags.Attainment8);

        if (showKs4Progress && showAttainment8)
        {
            showKs4Progress = false;
            showAttainment8 = false;
            logger.LogWarning(
                "An error detected in feature management for school comparison: {KS4} and {A8} are both true",
                nameof(FeatureFlags.KS4ProgressBanding),
                nameof(FeatureFlags.Attainment8));
        }

        var bandings = showKs4Progress
            ? await progressBandingsService.GetKS4ProgressBandings(
                (buildingUrns ?? []).Union(pupilUrns ?? []).Distinct().ToArray())
            : null;

        var topBuilding = showAttainment8
            ? await attainment8RankingService.GetTopRankedAttainment8Async(urn, buildingUrns ?? [])
            : [];

        var topPupil = showAttainment8
            ? await attainment8RankingService.GetTopRankedAttainment8Async(urn, pupilUrns ?? [])
            : [];

        return new PerformanceDataResult(showKs4Progress, showAttainment8, bandings, topBuilding, topPupil);
    }

    public SchoolExpenditureWithPerformance[] Merge(
        SchoolExpenditure[]? expenditures,
        KS4ProgressBandings? bandings,
        IReadOnlyList<string> topRanked)
    {
        if (expenditures == null || expenditures.Length == 0) return [];

        var topSet = topRanked.Count > 0
            ? new HashSet<string>(topRanked)
            : null;

        return
        [
            .. expenditures.Select(e =>
            {
                var item = new SchoolExpenditureWithPerformance(e, bandings?[e.URN]);
                if (topSet != null && e.URN != null && topSet.Contains(e.URN))
                {
                    item.TopRankedAttainment8 = true;
                }

                return item;
            })
        ];
    }
}
