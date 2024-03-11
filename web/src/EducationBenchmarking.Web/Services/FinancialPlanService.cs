using EducationBenchmarking.Web.Domain;
using EducationBenchmarking.Web.Domain.FinancialPlanStages;
using EducationBenchmarking.Web.Factories;
using EducationBenchmarking.Web.Infrastructure.Apis;
using EducationBenchmarking.Web.Infrastructure.Extensions;

namespace EducationBenchmarking.Web.Services;

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