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

public class PutFinancialPlanFunction(IFinancialPlansService service)
{
    [Function(nameof(PutFinancialPlanFunction))]
    [OpenApiOperation(nameof(PutFinancialPlanFunction), Constants.Features.FinancialPlans)]
    [OpenApiUrnParameter]
    [OpenApiParameter("year", Type = typeof(int), Required = true, Example = typeof(OpenApiExamples.ExampleYear))]
    [OpenApiSecurityHeader]
    [OpenApiRequestBody("application/json", typeof(FinancialPlanDetails), Description = "The financial plan object")]
    [OpenApiResponseWithBody(HttpStatusCode.Created, "application/json", typeof(FinancialPlanDetails))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NoContent)]
    [OpenApiResponseWithoutBody(HttpStatusCode.Conflict)]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "put", Route = Routes.FinancialPlan)] HttpRequestData req,
        string urn,
        int year,
        CancellationToken cancellationToken = default)
    {
        var body = await req.ReadAsJsonAsync<FinancialPlanDetails>(cancellationToken);

        //TODO : Consider adding request validator
        var result = await service.UpsertAsync(urn, year, body);

        return await result.CreateResponse(req);
    }
}