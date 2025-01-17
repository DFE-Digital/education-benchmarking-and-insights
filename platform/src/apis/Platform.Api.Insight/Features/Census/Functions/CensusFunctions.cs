using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;

namespace Platform.Api.Insight.Census;

public class CensusFunctions(
    ILogger<CensusFunctions> logger)
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
}