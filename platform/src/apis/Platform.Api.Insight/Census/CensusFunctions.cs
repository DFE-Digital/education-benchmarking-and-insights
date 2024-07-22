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
using Platform.Api.Insight.OpenApi.Examples;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;
namespace Platform.Api.Insight.Census;

public class CensusFunctions(ILogger<CensusFunctions> logger, ICensusService service)
{
    [Function(nameof(CensusAllCategories))]
    [OpenApiOperation(nameof(CensusAllCategories), "Census")]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(string[]))]
    public async Task<HttpResponseData> CensusAllCategories(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "census/categories")] HttpRequestData req)
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
            return await req.CreateJsonResponseAsync(CensusCategories.All);
        }
    }

    [Function(nameof(CensusAllDimensions))]
    [OpenApiOperation(nameof(CensusAllDimensions), "Census")]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(string[]))]
    public async Task<HttpResponseData> CensusAllDimensions(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "census/dimensions")] HttpRequestData req)
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

            return await req.CreateJsonResponseAsync(CensusDimensions.All);
        }
    }

    [Function(nameof(CensusAsync))]
    [OpenApiOperation(nameof(CensusAsync), "Census")]
    [OpenApiParameter("urn", Type = typeof(string), Required = true)]
    [OpenApiParameter("category", In = ParameterLocation.Query, Description = "Census category", Type = typeof(string), Required = false, Example = typeof(ExampleCensusCategory))]
    [OpenApiParameter("dimension", In = ParameterLocation.Query, Description = "Dimension for response values", Type = typeof(string), Example = typeof(ExampleCensusDimension))]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(CensusResponse))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> CensusAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "census/{urn}")] HttpRequestData req,
        string urn)
    {
        var correlationId = req.GetCorrelationId();
        var queryParams = req.GetParameters<CensusParameters>();

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
                var result = await service.GetAsync(urn);
                return result == null
                    ? req.CreateResponse(HttpStatusCode.NotFound)
                    : await req.CreateJsonResponseAsync(CensusResponseFactory.Create(result, queryParams.Category, queryParams.Dimension));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get census");
                return req.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }

    [Function(nameof(CustomCensusAsync))]
    [OpenApiOperation(nameof(CustomCensusAsync), "Census")]
    [OpenApiParameter("urn", Type = typeof(string), Required = true)]
    [OpenApiParameter("identifier", Type = typeof(string), Required = true)]
    [OpenApiParameter("category", In = ParameterLocation.Query, Description = "Census category", Type = typeof(string), Required = false, Example = typeof(ExampleCensusCategory))]
    [OpenApiParameter("dimension", In = ParameterLocation.Query, Description = "Dimension for response values", Type = typeof(string), Example = typeof(ExampleCensusDimension))]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(CensusResponse))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> CustomCensusAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "census/{urn}/custom/{identifier}")] HttpRequestData req,
        string urn,
        string identifier)
    {
        var correlationId = req.GetCorrelationId();
        var queryParams = req.GetParameters<CensusParameters>();

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
                var result = await service.GetCustomAsync(urn, identifier);
                return result == null
                    ? req.CreateResponse(HttpStatusCode.NotFound)
                    : await req.CreateJsonResponseAsync(CensusResponseFactory.Create(result, queryParams.Category, queryParams.Dimension));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get custom census");
                return req.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }

    [Function(nameof(CensusHistoryAsync))]
    [OpenApiOperation(nameof(CensusHistoryAsync), "Census")]
    [OpenApiParameter("urn", Type = typeof(string), Required = true)]
    [OpenApiParameter("dimension", In = ParameterLocation.Query, Description = "Dimension for response values", Type = typeof(string), Required = true, Example = typeof(ExampleCensusDimension))]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(CensusHistoryResponse[]))]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> CensusHistoryAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "census/{urn}/history")] HttpRequestData req,
        string urn)
    {
        var correlationId = req.GetCorrelationId();
        var queryParams = req.GetParameters<CensusParameters>();

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
                //TODO: Add validation for dimension
                var result = await service.GetHistoryAsync(urn);
                return await req.CreateJsonResponseAsync(result.Select(x => CensusResponseFactory.Create(x, queryParams.Dimension)));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get census history");
                return req.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }

    [Function(nameof(QueryCensusAsync))]
    [OpenApiOperation(nameof(CensusHistoryAsync), "Census")]
    [OpenApiParameter("urns", In = ParameterLocation.Query, Description = "List of school URNs", Type = typeof(string[]), Required = true)]
    [OpenApiParameter("category", In = ParameterLocation.Query, Description = "Census category", Type = typeof(string), Required = true, Example = typeof(ExampleCensusCategory))]
    [OpenApiParameter("dimension", In = ParameterLocation.Query, Description = "Value dimension", Type = typeof(string), Required = true, Example = typeof(ExampleCensusDimension))]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(CensusResponse[]))]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> QueryCensusAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "census")] HttpRequestData req)
    {
        var correlationId = req.GetCorrelationId();
        var queryParams = req.GetParameters<CensusParameters>();

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
                //TODO: Add validation for urns, category and dimension
                var result = await service.QueryAsync(queryParams.Schools);
                return await req.CreateJsonResponseAsync(result.Select(x => CensusResponseFactory.Create(x, queryParams.Category, queryParams.Dimension)));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get census");
                return req.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}