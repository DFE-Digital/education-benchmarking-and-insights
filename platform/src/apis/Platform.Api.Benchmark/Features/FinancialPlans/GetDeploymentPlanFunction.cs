using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Platform.Api.Benchmark.Features.FinancialPlans.Models;
using Platform.Api.Benchmark.Features.FinancialPlans.Services;
using Platform.Functions.Extensions;
using Platform.OpenApi;
using Platform.OpenApi.Attributes;
using Platform.Api.Benchmark.OpenApi;

namespace Platform.Api.Benchmark.Features.FinancialPlans;

public class GetDeploymentPlanFunction(IFinancialPlansService service)
{
    [Function(nameof(GetDeploymentPlanFunction))]
    [OpenApiOperation(nameof(GetDeploymentPlanFunction), Constants.Features.FinancialPlans)]
    [OpenApiUrnParameter]
    [OpenApiParameter("year", Type = typeof(int), Required = true, Example = typeof(OpenApiExamples.ExampleYear))]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(FinancialPlanDeployment))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = Routes.DeploymentPlan)] HttpRequestData req,
        string urn,
        int year,
        CancellationToken cancellationToken = default)
    {
        var plan = await service.DeploymentPlanAsync(urn, year, cancellationToken);
        return plan != null
            ? await req.CreateJsonResponseAsync(plan, cancellationToken)
            : req.CreateNotFoundResponse();
    }
}
