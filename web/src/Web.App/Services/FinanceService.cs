using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;

namespace Web.App.Services;

public interface IFinanceService
{
    Task<IEnumerable<SchoolExpenditure>> GetExpenditure(IEnumerable<string> schools);
    Task<IEnumerable<SchoolWorkforce>> GetWorkforce(IEnumerable<string> schools);
    Task<Finances> GetFinances(School school);
    Task<FinanceYears> GetYears();
}

public class FinanceService(IInsightApi insightApi) : IFinanceService
{
    public async Task<FinanceYears> GetYears()
    {
        return await insightApi.GetFinanceYears().GetResultOrThrow<FinanceYears>();
    }

    public async Task<IEnumerable<SchoolExpenditure>> GetExpenditure(IEnumerable<string> schools)
    {
        var query = BuildApiQueryFromComparatorSet(schools);
        return await insightApi.GetSchoolsExpenditure(query).GetResultOrDefault<IEnumerable<SchoolExpenditure>>() ?? Array.Empty<SchoolExpenditure>();
    }

    public async Task<IEnumerable<SchoolWorkforce>> GetWorkforce(IEnumerable<string> schools)
    {
        var query = BuildApiQueryFromComparatorSet(schools);
        return await insightApi.GetSchoolsWorkforce(query).GetResultOrDefault<IEnumerable<SchoolWorkforce>>() ?? Array.Empty<SchoolWorkforce>();
    }

    public async Task<Finances> GetFinances(School school)
    {
        switch (school.FinanceType)
        {
            case EstablishmentTypes.Academies:
                return await insightApi.GetAcademyFinances(school.Urn).GetResultOrThrow<Finances>();
            case EstablishmentTypes.Federation:
            case EstablishmentTypes.Maintained:
                return await insightApi.GetMaintainedSchoolFinances(school.Urn).GetResultOrThrow<Finances>();
            default:
                throw new ArgumentOutOfRangeException(nameof(school.Kind));
        }
    }

    private static ApiQuery BuildApiQueryFromComparatorSet(IEnumerable<string> schools)
    {
        var array = schools.ToArray();
        var query = new ApiQuery().Page(1, array.Length);
        foreach (var school in array)
        {
            query.AddIfNotNull("urns", school);
        }

        return query;
    }
}