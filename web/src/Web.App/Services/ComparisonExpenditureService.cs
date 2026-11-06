using System.Security.Claims;
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
        ClaimsPrincipal user,
        string urn,
        SchoolSpendingDimensions.ResultAsOptions resultAs = SchoolSpendingDimensions.ResultAsOptions.Actuals,
        string? customDataId = null);
}

public class ComparisonExpenditureService(
    IExpenditureApi expenditureApi,
    IUserDataService userDataService,
    ISchoolComparatorSetService schoolComparatorSetService) : IComparisonExpenditureService
{
    public async Task<ComparisonExpenditureResult> GetExpendituresAsync(
        ClaimsPrincipal user,
        string urn,
        SchoolSpendingDimensions.ResultAsOptions resultAs = SchoolSpendingDimensions.ResultAsOptions.Actuals,
        string? customDataId = null)
    {
        if (customDataId != null)
        {
            var building = await GetCustom(urn, true, customDataId);
            var pupil = await GetCustom(urn, false, customDataId);
            return new ComparisonExpenditureResult(building, pupil);
        }

        var defaultBuilding = await GetDefault(user, urn, true, resultAs);
        var defaultPupil = await GetDefault(user, urn, false, resultAs);
        return new ComparisonExpenditureResult(defaultBuilding, defaultPupil);
    }

    private async Task<SchoolExpenditure[]?> GetCustom(string urn, bool useBuildingSet, string customDataId)
    {
        var customSet = await schoolComparatorSetService.ReadComparatorSet(urn, customDataId);
        var set = useBuildingSet ? customSet?.Building : customSet?.Pupil;
        if (set == null || set.Length == 0) return [];

        var schools = set.Where(x => x != urn).ToArray();
        var customResult = await expenditureApi
            .SchoolCustom(urn, customDataId, BuildQuery(resultAs: SchoolSpendingDimensions.ResultAsOptions.Actuals))
            .GetResultOrDefault<SchoolExpenditure>();

        var defaultResult = await expenditureApi
            .QuerySchools(BuildQuery(schools))
            .GetResultOrDefault<SchoolExpenditure[]>();

        return customResult != null ? defaultResult?.Append(customResult).ToArray() : defaultResult;
    }

    private async Task<SchoolExpenditure[]> GetDefault(
        ClaimsPrincipal user,
        string urn,
        bool useBuildingSet,
        SchoolSpendingDimensions.ResultAsOptions resultAs)
    {
        var userData = await userDataService.GetSchoolDataAsync(user, urn);
        if (string.IsNullOrEmpty(userData.ComparatorSet))
        {
            var defaultSet = await schoolComparatorSetService.ReadComparatorSet(urn);
            var set = useBuildingSet ? defaultSet?.Building : defaultSet?.Pupil;
            if (set == null || set.Length == 0) return [];

            return await expenditureApi
                .QuerySchools(BuildQuery(set, resultAs))
                .GetResultOrThrow<SchoolExpenditure[]>();
        }

        var userDefinedSet = await schoolComparatorSetService.ReadUserDefinedComparatorSet(urn, userData.ComparatorSet);
        if (userDefinedSet == null || userDefinedSet.Set.Length == 0) return [];

        return await expenditureApi
            .QuerySchools(BuildQuery(userDefinedSet.Set, resultAs))
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
