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
            var customSet = await schoolComparatorSetService.ReadComparatorSet(urn, customDataId);
            var customResult = await expenditureApi
                .SchoolCustom(urn, customDataId, BuildQuery(resultAs: SchoolSpendingDimensions.ResultAsOptions.Actuals))
                .GetResultOrDefault<SchoolExpenditure>();

            var building = await GetCustomSet(urn, customSet?.Building, customResult, resultAs);
            var pupil = await GetCustomSet(urn, customSet?.Pupil, customResult, resultAs);
            return new ComparisonExpenditureResult(building, pupil);
        }

        var userData = await userDataService.GetSchoolDataAsync(user, urn);
        if (string.IsNullOrEmpty(userData.ComparatorSet))
        {
            var defaultSet = await schoolComparatorSetService.ReadComparatorSet(urn);
            var defaultBuilding = await QueryDefaultSet(defaultSet?.Building, resultAs);
            var defaultPupil = await QueryDefaultSet(defaultSet?.Pupil, resultAs);
            return new ComparisonExpenditureResult(defaultBuilding, defaultPupil);
        }

        var userDefinedSet = await schoolComparatorSetService.ReadUserDefinedComparatorSet(urn, userData.ComparatorSet);
        var sharedExpenditures = await QueryDefaultSet(userDefinedSet?.Set, resultAs);
        return new ComparisonExpenditureResult(sharedExpenditures, sharedExpenditures);
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
