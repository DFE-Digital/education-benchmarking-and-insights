using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Platform.Api.Benchmark.Features.FinancialPlans.Services;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;
using Platform.Functions.OpenApi.Examples;

namespace Platform.Api.Benchmark.Features.FinancialPlans;

public class DeleteFinancialPlanFunction(ILogger<DeleteFinancialPlanFunction> logger, IFinancialPlansService service)
{
    [Function(nameof(DeleteFinancialPlanFunction))]
    [OpenApiOperation(nameof(DeleteFinancialPlanFunction), Constants.Features.FinancialPlans)]
    [OpenApiParameter("urn", Type = typeof(string), Required = true)]
    [OpenApiParameter("year", Type = typeof(int), Required = true, Example = typeof(ExampleYear))]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithoutBody(HttpStatusCode.OK)]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "delete", Route = Routes.FinancialPlan)] HttpRequestData req,
        string urn,
        int year)
    {
        var correlationId = req.GetCorrelationId();

        using (logger.BeginScope(new Dictionary<string, object>
               {
                   {
                       "Application", Constants.ApplicationName
                   },
                   {
                       "CorrelationID", correlationId
                   },
                   {
                       "URN", urn
                   },
                   {
                       "Year", year
                   }
               }))
        {
            try
            {
                await service.DeleteAsync(urn, year);
                return req.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to delete financial plan");
                return req.CreateErrorResponse();
            }
        }
    }
}