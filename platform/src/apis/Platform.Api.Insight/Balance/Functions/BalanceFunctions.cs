using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;

namespace Platform.Api.Insight.Balance;

public class BalanceFunctions(ILogger<BalanceFunctions> logger)
{
    [Function(nameof(BalanceAllDimensions))]
    [OpenApiOperation(nameof(BalanceAllDimensions), "Balance")]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(string[]))]
    public async Task<HttpResponseData> BalanceAllDimensions(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "balance/dimensions")] HttpRequestData req)
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

            return await req.CreateJsonResponseAsync(BalanceDimensions.All);
        }
    }
}