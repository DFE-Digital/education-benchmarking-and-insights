using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Platform.Api.Benchmark.OpenApi;
using Platform.Functions.Extensions;
namespace Platform.Api.Benchmark.FinancialPlans;

public class FinancialPlansFunctions(ILogger<FinancialPlansFunctions> logger, IFinancialPlansService service)
{
    [Function(nameof(SingleFinancialPlanAsync))]
    [OpenApiOperation(nameof(SingleFinancialPlanAsync), "Financial Plans")]
    [OpenApiParameter("urn", Type = typeof(string), Required = true)]
    [OpenApiParameter("year", Type = typeof(int), Required = true)]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(FinancialPlanDetails))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> SingleFinancialPlanAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "financial-plan/{urn}/{year}")] HttpRequestData req,
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
                var plan = await service.DetailsAsync(urn, year);
                return plan != null
                    ? await req.CreateJsonResponseAsync(plan)
                    : req.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get financial plan");
                return req.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }

    [Function(nameof(DeploymentPlanAsync))]
    [OpenApiOperation(nameof(DeploymentPlanAsync), "Financial Plans")]
    [OpenApiParameter("urn", Type = typeof(string), Required = true)]
    [OpenApiParameter("year", Type = typeof(int), Required = true)]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(FinancialPlanDeployment))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> DeploymentPlanAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "financial-plan/{urn}/{year}/deployment")] HttpRequestData req,
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
                var plan = await service.DeploymentPlanAsync(urn, year);
                return plan != null
                    ? await req.CreateJsonResponseAsync(plan)
                    : req.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get deployment plan");
                return req.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }

    [Function(nameof(QueryFinancialPlanAsync))]
    [OpenApiOperation(nameof(QueryFinancialPlanAsync), "Financial Plans")]
    [OpenApiParameter("urns", In = ParameterLocation.Query, Description = "List of school URNs to include", Type = typeof(string[]), Required = true)]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(IEnumerable<FinancialPlanSummary>))]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> QueryFinancialPlanAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "financial-plans")] HttpRequestData req)
    {
        var correlationId = req.GetCorrelationId();

        using (logger.BeginScope(new Dictionary<string, object>
               {
                   {
                       "Application", Constants.ApplicationName
                   },
                   {
                       "CorrelationID", correlationId
                   }
               }))
        {
            try
            {
                var urns = req.Query["urns"]?.Split(",").Where(x => !string.IsNullOrEmpty(x)).ToArray() ?? [];
                var plans = await service.QueryAsync(urns);
                return await req.CreateJsonResponseAsync(plans);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to query financial plan");
                return req.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }

    [Function(nameof(UpsertFinancialPlanAsync))]
    [OpenApiOperation(nameof(UpsertFinancialPlanAsync), "Financial Plans")]
    [OpenApiParameter("urn", Type = typeof(string), Required = true)]
    [OpenApiParameter("year", Type = typeof(int), Required = true)]
    [OpenApiSecurityHeader]
    [OpenApiRequestBody("application/json", typeof(FinancialPlanDetails), Description = "The financial plan object")]
    [OpenApiResponseWithBody(HttpStatusCode.Created, "application/json", typeof(FinancialPlanDetails))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NoContent)]
    [OpenApiResponseWithoutBody(HttpStatusCode.Conflict)]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> UpsertFinancialPlanAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "put", Route = "financial-plan/{urn}/{year}")] HttpRequestData req,
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
                var body = await req.ReadAsJsonAsync<FinancialPlanDetails>();

                //TODO : Consider adding request validator
                var result = await service.UpsertAsync(urn, year, body);

                return await result.CreateResponse(req);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to upsert financial plan");
                return req.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }

    [Function(nameof(RemoveFinancialPlanAsync))]
    [OpenApiOperation(nameof(RemoveFinancialPlanAsync), "Financial Plans")]
    [OpenApiParameter("urn", Type = typeof(string), Required = true)]
    [OpenApiParameter("year", Type = typeof(int), Required = true)]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithoutBody(HttpStatusCode.OK)]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> RemoveFinancialPlanAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "delete", Route = "financial-plan/{urn}/{year}")] HttpRequestData req,
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
                return req.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}