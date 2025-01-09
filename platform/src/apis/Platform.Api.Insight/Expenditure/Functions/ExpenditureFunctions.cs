using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;

namespace Platform.Api.Insight.Expenditure;

public class ExpenditureFunctions(
    ILogger<ExpenditureFunctions> logger)
{
    [Function(nameof(ExpenditureAllCategories))]
    [OpenApiOperation(nameof(ExpenditureAllCategories), "Expenditure")]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(string[]))]
    public async Task<HttpResponseData> ExpenditureAllCategories(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "expenditure/categories")] HttpRequestData req)
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
            return await req.CreateJsonResponseAsync(ExpenditureCategories.All);
        }
    }

    [Function(nameof(ExpenditureAllDimensions))]
    [OpenApiOperation(nameof(ExpenditureAllDimensions), "Expenditure")]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(string[]))]
    public async Task<HttpResponseData> ExpenditureAllDimensions(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "expenditure/dimensions")] HttpRequestData req)
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
            return await req.CreateJsonResponseAsync(ExpenditureDimensions.All);
        }
    }
}