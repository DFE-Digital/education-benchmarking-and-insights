using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;
namespace Web.App.Services;

public interface IFinanceService
{
    Task<FinanceYears> GetYears();
}

public class FinanceService(IInsightApi insightApi) : IFinanceService
{
    public async Task<FinanceYears> GetYears() => await insightApi.GetCurrentReturnYears().GetResultOrThrow<FinanceYears>();
}