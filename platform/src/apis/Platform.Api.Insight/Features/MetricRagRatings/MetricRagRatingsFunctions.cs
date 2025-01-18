using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Platform.Api.Insight.OpenApi.Examples;
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;
using Platform.Functions.OpenApi.Examples;

namespace Platform.Api.Insight.MetricRagRatings;

public class MetricRagRatingsFunctions(
    ILogger<MetricRagRatingsFunctions> logger,
    IMetricRagRatingsService service,
    IValidator<MetricRagRatingsParameters> metricRagRatingsParametersValidator)
{
    [Function(nameof(UserDefinedAsync))]
    [OpenApiOperation(nameof(UserDefinedAsync), "Metric RAG Ratings")]
    [OpenApiParameter("identifier", Type = typeof(string), Required = true)]
    [OpenApiParameter("useCustomData", In = ParameterLocation.Query, Description = "Sets whether or not to use custom data context", Type = typeof(bool))]
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
                var result = await service.UserDefinedAsync(identifier, queryParams.DataContext);
                return await req.CreateJsonResponseAsync(result);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed get user defined metric rag ratings");
                return req.CreateErrorResponse();
            }
        }
    }

    [Function(nameof(QueryDefaultAsync))]
    [OpenApiOperation(nameof(QueryDefaultAsync), "Metric RAG Ratings")]
    [OpenApiParameter("urns", In = ParameterLocation.Query, Description = "List of school URNs", Type = typeof(string[]), Required = false)]
    [OpenApiParameter("phase", In = ParameterLocation.Query, Description = "School overall phase", Type = typeof(string), Example = typeof(ExampleOverallPhase))]
    [OpenApiParameter("companyNumber", In = ParameterLocation.Query, Description = "Eight digit trust company number", Type = typeof(string))]
    [OpenApiParameter("laCode", In = ParameterLocation.Query, Description = "Local authority three digit code", Type = typeof(string))]
    [OpenApiParameter("categories", In = ParameterLocation.Query, Description = "List of cost category", Type = typeof(string[]), Example = typeof(ExampleCategoryCost))]
    [OpenApiParameter("statuses", In = ParameterLocation.Query, Description = "List of RAG statuses", Type = typeof(string[]), Example = typeof(ExampleRagStatuses))]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(MetricRagRating[]))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, "application/json", typeof(ValidationError[]))]
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
                var validationResult = await metricRagRatingsParametersValidator.ValidateAsync(queryParams);
                if (!validationResult.IsValid)
                {
                    return await req.CreateValidationErrorsResponseAsync(validationResult.Errors);
                }

                var result = await service.QueryAsync(queryParams.Urns, queryParams.Categories, queryParams.Statuses, queryParams.CompanyNumber, queryParams.LaCode, queryParams.Phase);
                return await req.CreateJsonResponseAsync(result);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed query metric rag ratings");
                return req.CreateErrorResponse();
            }
        }
    }
}