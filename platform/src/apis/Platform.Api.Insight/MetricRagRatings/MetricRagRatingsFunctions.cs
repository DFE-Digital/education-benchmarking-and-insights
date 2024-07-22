using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;
namespace Platform.Api.Insight.MetricRagRatings;

public class MetricRagRatingsFunctions(IMetricRagRatingsService service, ILogger<MetricRagRatingsFunctions> logger)
{
    [Function(nameof(UserDefinedAsync))]
    [OpenApiOperation(nameof(UserDefinedAsync), "Metric RAG Ratings")]
    [OpenApiParameter("identifier", Type = typeof(string), Required = true)]
    [OpenApiParameter("useCustomData", In = ParameterLocation.Query, Description = "Sets whether or not to use custom data context", Type = typeof(bool))]
    [OpenApiParameter("setType", In = ParameterLocation.Query, Description = "Comparator set type", Type = typeof(string))]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(MetricRagRating[]))]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> UserDefinedAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "metric-rag/{identifier}")] HttpRequestData req,
        string identifier)
    {
        var correlationId = req.GetCorrelationId();
        var queryParams = req.GetParameters<MetricRagRatingParameters>();

        using (logger.BeginScope(new Dictionary<string, object>
               {
                   {
                       "Application", Constants.ApplicationName
                   },
                   {
                       "CorrelationID", correlationId
                   },
                   {
                       "Identifier", identifier
                   }
               }))
        {
            try
            {
                var result = await service.UserDefinedAsync(identifier, queryParams.DataContext, queryParams.SetType);
                return await req.CreateJsonResponseAsync(result);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed get user defined metric rag ratings");
                return req.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }

    [Function(nameof(QueryDefaultAsync))]
    [OpenApiOperation(nameof(QueryDefaultAsync), "Metric RAG Ratings")]
    [OpenApiParameter("urns", In = ParameterLocation.Query, Description = "List of school URNs", Type = typeof(string[]), Required = true)]
    [OpenApiParameter("categories", In = ParameterLocation.Query, Description = "List of cost category", Type = typeof(string[]))]
    [OpenApiParameter("statuses", In = ParameterLocation.Query, Description = "List of RAG statuses", Type = typeof(string[]))]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(MetricRagRating[]))]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> QueryDefaultAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "metric-rag/default")] HttpRequestData req)
    {
        var correlationId = req.GetCorrelationId();
        var queryParams = req.GetParameters<MetricRagRatingsParameters>();

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
                var result = await service.QueryAsync(queryParams.Schools, queryParams.Categories, queryParams.Statuses);
                return await req.CreateJsonResponseAsync(result);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed query metric rag ratings");
                return req.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}