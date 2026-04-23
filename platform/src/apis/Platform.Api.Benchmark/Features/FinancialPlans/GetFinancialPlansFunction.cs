using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using Platform.Api.Benchmark.Features.FinancialPlans.Models;
using Platform.Api.Benchmark.Features.FinancialPlans.Services;
using Platform.Functions.Extensions;
using Platform.OpenApi;
using Platform.OpenApi.Attributes;

namespace Platform.Api.Benchmark.Features.FinancialPlans;

public class GetFinancialPlansFunction(IFinancialPlansService service)
{
    [Function(nameof(GetFinancialPlansFunction))]
    [OpenApiOperation(nameof(GetFinancialPlansFunction), Constants.Features.FinancialPlans)]
    [OpenApiUrnsParameter]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(IEnumerable<FinancialPlanSummary>))]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = Routes.FinancialPlans)] HttpRequestData req,
        CancellationToken cancellationToken = default)
    {
        var urns = req.Query["urns"]?.Split(",").Where(x => !string.IsNullOrEmpty(x)).ToArray() ?? [];
        var plans = await service.QueryAsync(urns, cancellationToken);
        return await req.CreateJsonResponseAsync(plans, cancellationToken);
    }
}
