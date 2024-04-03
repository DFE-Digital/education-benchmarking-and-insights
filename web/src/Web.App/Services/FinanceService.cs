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
    Task<IEnumerable<Workforce>> GetSchoolWorkforceHistory(string urn, string dimension);
    Task<IEnumerable<Balance>> GetSchoolBalanceHistory(string urn, string dimension);
    Task<IEnumerable<Income>> GetSchoolIncomeHistory(string urn, string dimension);
    Task<IEnumerable<Expenditure>> GetSchoolExpenditureHistory(string urn, string dimension);
    Task<IEnumerable<Balance>> GetTrustBalanceHistory(string companyNo, string dimension);
    Task<IEnumerable<Income>> GetTrustIncomeHistory(string companyNo, string dimension);
    Task<IEnumerable<Expenditure>> GetTrustExpenditureHistory(string companyNo, string dimension);
}

public class FinanceService(IInsightApi insightApi) : IFinanceService
{
    public async Task<FinanceYears> GetYears()
    {
        return await insightApi.GetCurrentReturnYears().GetResultOrThrow<FinanceYears>();
    }

    public async Task<IEnumerable<Workforce>> GetSchoolWorkforceHistory(string urn, string dimension)
    {
        var query = BuildAiQueryForDimension(dimension);
        return await insightApi.GetSchoolWorkforceHistory(urn, query).GetResultOrDefault<IEnumerable<Workforce>>() ?? Array.Empty<Workforce>();
    }

    public async Task<IEnumerable<Balance>> GetSchoolBalanceHistory(string urn, string dimension)
    {
        var query = BuildAiQueryForDimension(dimension);
        return await insightApi.GetSchoolBalanceHistory(urn, query).GetResultOrDefault<IEnumerable<Balance>>() ?? Array.Empty<Balance>();
    }

    public async Task<IEnumerable<Income>> GetSchoolIncomeHistory(string urn, string dimension)
    {
        var query = BuildAiQueryForDimension(dimension);
        return await insightApi.GetSchoolIncomeHistory(urn, query).GetResultOrDefault<IEnumerable<Income>>() ?? Array.Empty<Income>();
    }

    public async Task<IEnumerable<Expenditure>> GetSchoolExpenditureHistory(string urn, string dimension)
    {
        var query = BuildAiQueryForDimension(dimension);
        return await insightApi.GetSchoolExpenditureHistory(urn, query).GetResultOrDefault<IEnumerable<Expenditure>>() ?? Array.Empty<Expenditure>();
    }

    public async Task<IEnumerable<Income>> GetTrustIncomeHistory(string companyNo, string dimension)
    {
        var query = BuildAiQueryForDimension(dimension);
        return await insightApi.GetTrustIncomeHistory(companyNo, query).GetResultOrDefault<IEnumerable<Income>>() ?? Array.Empty<Income>();
    }

    public async Task<IEnumerable<Balance>> GetTrustBalanceHistory(string companyNo, string dimension)
    {
        var query = BuildAiQueryForDimension(dimension);
        return await insightApi.GetTrustBalanceHistory(companyNo, query).GetResultOrDefault<IEnumerable<Balance>>() ?? Array.Empty<Balance>();
    }

    public async Task<IEnumerable<Expenditure>> GetTrustExpenditureHistory(string companyNo, string dimension)
    {
        var query = BuildAiQueryForDimension(dimension);
        return await insightApi.GetTrustExpenditureHistory(companyNo, query).GetResultOrDefault<IEnumerable<Expenditure>>() ?? Array.Empty<Expenditure>();
    }

    public async Task<IEnumerable<SchoolExpenditure>> GetExpenditure(IEnumerable<string> urns)
    {
        var query = BuildApiQueryForComparatorSet(urns);
        return await insightApi.GetSchoolsExpenditure(query).GetResultOrDefault<IEnumerable<SchoolExpenditure>>() ?? Array.Empty<SchoolExpenditure>();
    }

    public async Task<IEnumerable<SchoolWorkforce>> GetWorkforce(IEnumerable<string> urns)
    {
        var query = BuildApiQueryForComparatorSet(urns);
        return await insightApi.GetSchoolsWorkforce(query).GetResultOrDefault<IEnumerable<SchoolWorkforce>>() ?? Array.Empty<SchoolWorkforce>();
    }

    public async Task<Finances> GetFinances(string urn)
    {
        return await insightApi.GetSchoolFinances(urn).GetResultOrThrow<Finances>();
    }

    private static ApiQuery BuildAiQueryForDimension(string dimension)
    {
        var query = new ApiQuery().AddIfNotNull("dimension", dimension);
        return query;
    }


    private static ApiQuery BuildApiQueryForComparatorSet(IEnumerable<string> urns)
    {
        var query = new ApiQuery();
        foreach (var urn in urns)
        {
            query.AddIfNotNull("urns", urn);
        }

        return query;
    }
}