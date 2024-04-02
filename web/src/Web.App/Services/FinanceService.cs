using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;

namespace Web.App.Services;

public interface IFinanceService
{
    Task<IEnumerable<SchoolExpenditure>> GetExpenditure(IEnumerable<string> urns);
    Task<IEnumerable<SchoolWorkforce>> GetWorkforce(IEnumerable<string> urns);
    Task<Finances> GetFinances(string urns);
    Task<FinanceYears> GetYears();
    Task<IEnumerable<Workforce>> GetWorkforceHistory(string urn, string dimension);
    Task<IEnumerable<Balance>> GetBalanceHistory(string urn, string dimension);
    Task<IEnumerable<Income>> GetIncomeHistory(string urn, string dimension);
}

public class FinanceService(IInsightApi insightApi) : IFinanceService
{
    public async Task<FinanceYears> GetYears()
    {
        return await insightApi.GetCurrentReturnYears().GetResultOrThrow<FinanceYears>();
    }

    public async Task<IEnumerable<Workforce>> GetWorkforceHistory(string urn, string dimension)
    {
        var query = new ApiQuery().AddIfNotNull("dimension", dimension);
        return await insightApi.GetSchoolWorkforceHistory(urn, query).GetResultOrDefault<IEnumerable<Workforce>>() ?? Array.Empty<Workforce>();
    }

    public async Task<IEnumerable<Balance>> GetBalanceHistory(string urn, string dimension)
    {
        var query = new ApiQuery().AddIfNotNull("dimension", dimension);
        return await insightApi.GetSchoolBalanceHistory(urn, query).GetResultOrDefault<IEnumerable<Balance>>() ?? Array.Empty<Balance>();
    }

    public async Task<IEnumerable<Income>> GetIncomeHistory(string urn, string dimension)
    {
        var query = new ApiQuery().AddIfNotNull("dimension", dimension);
        return await insightApi.GetSchoolIncomeHistory(urn, query).GetResultOrDefault<IEnumerable<Income>>() ?? Array.Empty<Income>();
    }

    public async Task<IEnumerable<SchoolExpenditure>> GetExpenditure(IEnumerable<string> urns)
    {
        var query = BuildApiQueryFromComparatorSet(urns);
        return await insightApi.GetSchoolsExpenditure(query).GetResultOrDefault<IEnumerable<SchoolExpenditure>>() ?? Array.Empty<SchoolExpenditure>();
    }

    public async Task<IEnumerable<SchoolWorkforce>> GetWorkforce(IEnumerable<string> urns)
    {
        var query = BuildApiQueryFromComparatorSet(urns);
        return await insightApi.GetSchoolsWorkforce(query).GetResultOrDefault<IEnumerable<SchoolWorkforce>>() ?? Array.Empty<SchoolWorkforce>();
    }

    public async Task<Finances> GetFinances(string urn)
    {
        return await insightApi.GetSchoolFinances(urn).GetResultOrThrow<Finances>();
    }

    private static ApiQuery BuildApiQueryFromComparatorSet(IEnumerable<string> urns)
    {
        var query = new ApiQuery();
        foreach (var urn in urns)
        {
            query.AddIfNotNull("urns", urn);
        }

        return query;
    }
}