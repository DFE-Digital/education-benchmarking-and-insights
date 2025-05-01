using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Platform.Api.Benchmark.Features.FinancialPlans.Models;
using Platform.Api.Benchmark.Features.FinancialPlans.Services;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;
using Platform.Functions.OpenApi.Examples;

namespace Platform.Api.Benchmark.Features.FinancialPlans;

public class GetFinancialPlanFunction(ILogger<GetFinancialPlanFunction> logger, IFinancialPlansService service)
{
    [Function(nameof(GetFinancialPlanFunction))]
    [OpenApiOperation(nameof(GetFinancialPlanFunction), Constants.Features.FinancialPlans)]
    [OpenApiParameter("urn", Type = typeof(string), Required = true)]
    [OpenApiParameter("year", Type = typeof(int), Required = true, Example = typeof(ExampleYear))]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(FinancialPlanDetails))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = Routes.FinancialPlan)] HttpRequestData req,
        string urn,
        int year)
    {
        var correlationId = req.GetCorrelationId();

        using (logger.BeginScope(new Dictionary<string, object>
               {
                   { "Application", Constants.ApplicationName },
                   { "CorrelationID", correlationId },
                   { "URN", urn },
                   { "Year", year }
               }))
        {
            try
            {
                var plan = await service.DetailsAsync(urn, year);
                return plan != null
                    ? await req.CreateJsonResponseAsync(plan)
                    : req.CreateNotFoundResponse();
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get financial plan");
                return req.CreateErrorResponse();
            }
        }
    }
}