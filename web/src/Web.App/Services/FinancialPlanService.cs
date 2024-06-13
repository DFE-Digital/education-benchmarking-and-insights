using Microsoft.Azure.Cosmos;
using Web.App.Domain;
using Web.App.Domain.FinancialPlanStages;
using Web.App.Factories;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;

namespace Web.App.Services;

public interface IFinancialPlanService
{
    Task TryCreateEmpty(string? urn, int? year, string user);
    Task<FinancialPlanInput> Get(string? urn, int? year);
    Task<DeploymentPlan> DeploymentPlan(string? urn, int? year);
    Task<IEnumerable<FinancialPlan>> List(string[] urns);
    Task Update(string? urn, int? year, string user, Stage stage);
}

public class FinancialPlanService(IFinancialPlanApi api) : IFinancialPlanService
{
    public async Task TryCreateEmpty(string? urn, int? year, string user)
    {
        var plan = await api.GetAsync(urn, year).GetResultOrDefault<FinancialPlanInput>();
        if (plan == null)
        {
            var request = new PutFinancialPlanRequest { Urn = urn, Year = year, UpdatedBy = user };
            await api.UpsertAsync(request).EnsureSuccess();
        }
    }

    public async Task<FinancialPlanInput> Get(string? urn, int? year)
    {
        return await api.GetAsync(urn, year).GetResultOrThrow<FinancialPlanInput>();
    }

    public async Task<DeploymentPlan> DeploymentPlan(string? urn, int? year)
    {
        return await api.GetDeploymentPlanAsync(urn, year).GetResultOrThrow<DeploymentPlan>();
    }

    public async Task<IEnumerable<FinancialPlan>> List(string[] urns)
    {
        var query = new ApiQuery();
        foreach (var urn in urns)
        {
            query.AddIfNotNull("urns", urn);
        }
        return await api.QueryAsync(query).GetResultOrDefault<FinancialPlan[]>() ?? [];
    }

    public async Task Update(string? urn, int? year, string user, Stage stage)
    {
        var plan = await Get(urn, year);
        stage.SetPlanValues(plan);

        var request = PutFinancialPlanRequestFactory.Create(plan);
        request.UpdatedBy = user;
        await api.UpsertAsync(request).EnsureSuccess();
    }
}