using Web.App.Domain;
using Web.App.Domain.Charts;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.Insight;
using Web.App.Infrastructure.Extensions;

namespace Web.App.Services;

public record ComparisonExpenditureResult(
    SchoolExpenditure[]? BuildingExpenditure,
    SchoolExpenditure[]? PupilExpenditure);

public interface IComparisonExpenditureService
{
    Task<ComparisonExpenditureResult> GetExpendituresAsync(
        string urn,
        string[]? buildingUrns,
        string[]? pupilUrns,
        SchoolSpendingDimensions.ResultAsOptions resultAs = SchoolSpendingDimensions.ResultAsOptions.Actuals,
        string? customDataId = null);
}

public class ComparisonExpenditureService(IExpenditureApi expenditureApi) : IComparisonExpenditureService
{
    public async Task<ComparisonExpenditureResult> GetExpendituresAsync(
        string urn,
        string[]? buildingUrns,
        string[]? pupilUrns,
        SchoolSpendingDimensions.ResultAsOptions resultAs = SchoolSpendingDimensions.ResultAsOptions.Actuals,
        string? customDataId = null)
    {
        if (customDataId != null)
        {
            var customResult = await expenditureApi
                .SchoolCustom(urn, customDataId, BuildQuery(resultAs: resultAs))
                .GetResultOrDefault<SchoolExpenditure>();

            var building = await GetCustomSet(urn, buildingUrns, customResult, resultAs);
            var pupil = await GetCustomSet(urn, pupilUrns, customResult, resultAs);
            return new ComparisonExpenditureResult(building, pupil);
        }

        if (buildingUrns != null && pupilUrns != null && buildingUrns.SequenceEqual(pupilUrns))
        {
            var sharedExpenditures = await QueryDefaultSet(buildingUrns, resultAs);
            return new ComparisonExpenditureResult(sharedExpenditures, sharedExpenditures);
        }

        var defaultBuilding = await QueryDefaultSet(buildingUrns, resultAs);
        var defaultPupil = await QueryDefaultSet(pupilUrns, resultAs);
        return new ComparisonExpenditureResult(defaultBuilding, defaultPupil);
    }

    private async Task<SchoolExpenditure[]?> GetCustomSet(
        string urn,
        string[]? set,
        SchoolExpenditure? customResult,
        SchoolSpendingDimensions.ResultAsOptions resultAs)
    {
        if (set == null || set.Length == 0) return [];

        var schools = set.Where(x => x != urn).ToArray();
        var defaultResult = await expenditureApi
            .QuerySchools(BuildQuery(schools, resultAs))
            .GetResultOrDefault<SchoolExpenditure[]>();

        return customResult != null ? defaultResult?.Append(customResult).ToArray() : defaultResult;
    }

    private async Task<SchoolExpenditure[]> QueryDefaultSet(
        string[]? set,
        SchoolSpendingDimensions.ResultAsOptions resultAs)
    {
        if (set == null || set.Length == 0) return [];

        return await expenditureApi
            .QuerySchools(BuildQuery(set, resultAs))
            .GetResultOrThrow<SchoolExpenditure[]>();
    }

    private static ApiQuery BuildQuery(
        IEnumerable<string>? urns = null,
        SchoolSpendingDimensions.ResultAsOptions resultAs = SchoolSpendingDimensions.ResultAsOptions.Actuals)
    {
        var query = new ApiQuery().AddIfNotNull("dimension", resultAs.GetQueryParam());
        foreach (var urn in urns ?? []) query.AddIfNotNull("urns", urn);
        return query;
    }
}
