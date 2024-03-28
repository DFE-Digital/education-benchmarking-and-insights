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
    Task<IEnumerable<Workforce>> GetWorkforceHistory(School school, string dimension);
    Task<IEnumerable<Balance>> GetBalanceHistory(School school, string dimension);
}

public class FinanceService(IInsightApi insightApi) : IFinanceService
{
    public async Task<FinanceYears> GetYears()
    {
        return await insightApi.GetCurrentReturnYears().GetResultOrThrow<FinanceYears>();
    }

    public async Task<IEnumerable<Workforce>> GetWorkforceHistory(School school, string dimension)
    {
        var query = new ApiQuery().AddIfNotNull("dimension", dimension);
        switch (school.FinanceType)
        {
            case EstablishmentTypes.Academies:
                return await insightApi.GetAcademyWorkforceHistory(school.Urn, query).GetResultOrDefault<IEnumerable<Workforce>>() ?? Array.Empty<Workforce>();
            case EstablishmentTypes.Federation:
            case EstablishmentTypes.Maintained:
                return await insightApi.GetMaintainedSchoolWorkforceHistory(school.Urn, query).GetResultOrDefault<IEnumerable<Workforce>>() ?? Array.Empty<Workforce>();
            default:
                throw new ArgumentOutOfRangeException(nameof(school.Kind));
        }
    }

    public async Task<IEnumerable<Balance>> GetBalanceHistory(School school, string dimension)
    {
        var query = new ApiQuery().AddIfNotNull("dimension", dimension);
        switch (school.FinanceType)
        {
            case EstablishmentTypes.Academies:
                return await insightApi.GetAcademyBalanceHistory(school.Urn, query).GetResultOrDefault<IEnumerable<Balance>>() ?? Array.Empty<Balance>();
            case EstablishmentTypes.Federation:
            case EstablishmentTypes.Maintained:
                return await insightApi.GetMaintainedSchoolBalanceHistory(school.Urn, query).GetResultOrDefault<IEnumerable<Balance>>() ?? Array.Empty<Balance>();
            default:
                throw new ArgumentOutOfRangeException(nameof(school.Kind));
        }
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
        var query = new ApiQuery();
        foreach (var school in array)
        {
            query.AddIfNotNull("urns", school);
        }

        return query;
    }
}