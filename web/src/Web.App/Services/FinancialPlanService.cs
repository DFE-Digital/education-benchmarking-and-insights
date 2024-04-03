using Web.App.Domain;
using Web.App.Domain.FinancialPlanStages;
using Web.App.Factories;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;

namespace Web.App.Services;

public interface IFinancialPlanService
{
    Task TryCreateEmpty(string? urn, int? year);
    Task<FinancialPlan> Get(string? urn, int? year);
    Task<IEnumerable<FinancialPlan>> List(string? urn);
    Task Update(string? urn, int? year, Stage stage);
}

public class FinancialPlanService(IBenchmarkApi benchmarkApi) : IFinancialPlanService
{
    public async Task TryCreateEmpty(string? urn, int? year)
    {
        var plan = await benchmarkApi.GetFinancialPlan(urn, year).GetResultOrDefault<FinancialPlan>();
        if (plan == null)
        {
            var request = new PutFinancialPlanRequest { Urn = urn, Year = year };
            await benchmarkApi.UpsertFinancialPlan(request).EnsureSuccess();
        }
    }

    public async Task<FinancialPlan> Get(string? urn, int? year)
    {
        return await benchmarkApi.GetFinancialPlan(urn, year).GetResultOrThrow<FinancialPlan>();
    }

    public async Task<IEnumerable<FinancialPlan>> List(string? urn)
    {
        return await benchmarkApi.QueryFinancialPlan(urn).GetResultOrDefault<FinancialPlan[]>() ?? Array.Empty<FinancialPlan>();
    }

    public async Task Update(string? urn, int? year, Stage stage)
    {
        var plan = await Get(urn, year);
        stage.SetPlanValues(plan);

        var request = PutFinancialPlanRequestFactory.Create(plan);
        await benchmarkApi.UpsertFinancialPlan(request).EnsureSuccess();
    }
}