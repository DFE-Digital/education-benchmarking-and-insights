using Microsoft.FeatureManagement;
using Web.App.Domain;

namespace Web.App.Services;

public record PerformanceDataResult(
    bool ShowKs4Progress,
    bool ShowAttainment8,
    KS4ProgressBandings? Bandings,
    IReadOnlyList<string> TopRankedBuilding,
    IReadOnlyList<string> TopRankedPupil)
{
    private bool HasTopRankedAttainment8 =>
        ShowAttainment8 && (TopRankedBuilding.Count > 0 || TopRankedPupil.Count > 0);

    public bool ResolveDisplayAttainment8(bool requestedByUser) =>
        requestedByUser && HasTopRankedAttainment8;

    public bool ShouldExcludeAttainment8Column =>
        !HasTopRankedAttainment8;

    public bool ShouldExcludeProgressBandingColumn =>
        !ShowKs4Progress || Bandings == null || Bandings.Items.Length == 0;
}

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

        var buildingSet = (buildingUrns ?? []).Distinct().ToArray();
        var pupilSet = (pupilUrns ?? []).Distinct().ToArray();

        var topBuilding = showAttainment8
            ? await attainment8RankingService.GetTopRankedAttainment8Async(urn, buildingSet)
            : [];

        var topPupil = showAttainment8
            ? (buildingSet.Length == pupilSet.Length && buildingSet.All(pupilSet.Contains))
                ? topBuilding
                : await attainment8RankingService.GetTopRankedAttainment8Async(urn, pupilSet)
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
